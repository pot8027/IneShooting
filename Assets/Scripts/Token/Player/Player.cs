using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : TokenController
{
    /// <summary>
    /// 稲モード
    /// </summary>
    enum IneMode
    {
        Normal = 0,
        Lv1 = 1,
        Lv2 = 2
    }

    /// <summary>
    /// 稲パワー閾値
    /// </summary>
    enum InePower
    {
        Lv2 = 2000,
        Lv1 = 1000
    }

    /// <summary>
    /// 現在稲モード
    /// </summary>
    private IneMode _ineMode = IneMode.Normal;

    // 稲パワー最大
    private static readonly int INE_POWER_MAX = 3000;

    // 稲パワーチャージ速度
    private static readonly int INE_POWER_CHARGE_SPEED = 3;

    /// <summary>
    /// 稲パワー
    /// </summary>
    private int _inePower = 0;

    /// <summary>
    /// 残りボム数
    /// </summary>
    private int _bombCount = 4;

    /// <summary>
    /// 非ダメージ中フラグ
    /// </summary>
    private bool _isDamaged = false;

    /// <summary>
    /// プレハブ名
    /// </summary>
    private static readonly string PREFUB_NAME = "Player";

    /// <summary>
    /// インスタンスリスト
    /// </summary>
    private static TokenManager<Player> _parent = null;

    /// <summary>
    /// インスタンスリストを作成します
    /// </summary>
    /// <param name="size">リストサイズ</param>
    public static void InitTokenManager(int size)
    {
        _parent = new TokenManager<Player>(PREFUB_NAME, size);
    }

    /// <summary>
    /// インスタンスを追加します
    /// </summary>
    /// <returns>The add.</returns>
    /// <param name="x">The x coordinate.</param>
    /// <param name="y">The y coordinate.</param>
    /// <param name="direction">Direction.</param>
    /// <param name="speed">Speed.</param>
    public static Player Add(float x, float y, float direction, float speed)
    {
        if (_parent == null)
        {
            return null;
        }
        return _parent.Add(x, y, direction, speed);
    }

    private static readonly int MAX_RYO = 6;
    /// <summary>
    /// 涼リスト
    /// </summary>
    private List<Ryo> _ryoList = new List<Ryo>();

    /// <summary>
    /// 回転体用の現在の基準角度
    /// </summary>
    private int _currentAngle = 0;
    public int CurrentAngle
    {
        get { return _currentAngle; }
    }

    /// <summary>
    /// 被ダメージ
    /// </summary>
    public void Damage()
    {
        // システム稲起動中は無敵
        if (IsSystemIne())
        {
            return;
        }

        // 被ダメ中は処理しない
        if (_isDamaged)
        {
            return;
        }

        // 被ダメコルーチン開始
        StartCoroutine("IEDamaged");

        // 涼さんがいれば１基消滅
        RemoveRyo();

        // ボムが残っていたらボムを使って終了
        if (_bombCount >= 1)
        {
            Bomb();
            return;
        }

        // 自分を破棄してゲームオーバー
        DestroyObj();
        GameManager.CurrentMode = GameManager.Mode.gameover;
    }

    // Start is called before the first frame update
    private void Start()
    {
        InitSize();

        // ショット
        StartCoroutine("IEPlayerShot");

        // 涼さんバリア用角度更新
        StartCoroutine("IEAngleUpdate");

        // 稲パワーチャージ
        StartCoroutine("IEInePowerCharge");
    }

    /// <summary>
    /// 個別処理用更新処理
    /// </summary>
    protected override void UpdateEach()
    {
        // ボム残り
        SetLeftBomb(_bombCount);

        // 稲パワー表示更新
        UpdateInePowerDisplay();
        UpdateInePowerStatusDisplay();

        // キー入力で移動
        Vector2 v = AppUtil.GetInputVector();
        float speed = MoveSpeed * Time.deltaTime;
        ClampScreenAndMove(v * speed);

        // ボム
        if (InputManager.IsKeyDownTriangle())
        {
            Bomb();
        }

        // システム稲
        if (InputManager.IsKeyDownSquare())
        {
            LaunchSystemIne();
        }
    }

    /// <summary>
    /// 残稲ボムテキスト
    /// </summary>
    private LeftBomb _leftBombText = null;
    private void SetLeftBomb(int left)
    {
        if (_leftBombText == null)
        {
            _leftBombText = GameObject.Find("LeftBomb").GetComponent<LeftBomb>();
        }
        _leftBombText.SetLeft(left);
    }

    /// <summary>
    /// 稲パワーテキスト
    /// </summary>
    private CommonText _inePowerText = null;
    private void UpdateInePowerDisplay()
    {
        // 稲パワーテキスト
        if (_inePowerText == null)
        {
            _inePowerText = GameObject.Find("InePower").GetComponent<CommonText>();
        }
        _inePowerText.SetText(_inePower.ToString());
    }

    /// <summary>
    /// 稲パワー状態テキスト
    /// </summary>
    private CommonText _inePowerStatusText = null;
    private void UpdateInePowerStatusDisplay()
    {
        // 稲パワー状態テキスト
        if (_inePowerStatusText == null)
        {
            _inePowerStatusText = GameObject.Find("LInePowerExplain").GetComponent<CommonText>();
        }

        // 稲パワー発動中
        if (IsSystemIne())
        {
            _inePowerStatusText.SetText("スーパー稲" + _ineMode.ToString() + "発動中");
        }

        // 稲パワー未発動
        else
        {
            if (_inePower >= (int)InePower.Lv2)
            {
                _inePowerStatusText.SetText("スーパー稲" + IneMode.Lv2.ToString() + "発動可能");
            }

            else if (_inePower >= (int)InePower.Lv1)
            {
                _inePowerStatusText.SetText("スーパー稲" + IneMode.Lv1.ToString() + "発動可能");
            }

            else
            {
                _inePowerStatusText.SetText(string.Empty);
            }

        }
    }

    /// <summary>
    /// ボム発射
    /// </summary>
    private void Bomb()
    {
        if (_bombCount >= 1)
        {
            _bombCount--;
            EnemyShot1.VanishEachToken();
            EnemyShot2.VanishEachToken();
        }
    }

    /// <summary>
    /// 涼さん追加
    /// </summary>
    private void AddRyo()
    {
        if (MAX_RYO <= _ryoList.Count)
        {
            return;
        }

        GameObject prefub = Resources.Load("Prefabs/" + "Ryo") as GameObject;
        GameObject g = Object.Instantiate(prefub, new Vector3(X, Y, 0), Quaternion.identity);
        Ryo ryo = g.GetComponent<Ryo>();
        ryo.Parent = this;
        int adjustAngle = 360 / MAX_RYO * _ryoList.Count;
        ryo.AdjustAngle = adjustAngle;
        _ryoList.Add(ryo);
    }

    /// <summary>
    /// 涼さん１つ破棄
    /// </summary>
    private void RemoveRyo()
    {
        if (_ryoList.Count <= 0)
        {
            return;
        }

        Ryo ryo = _ryoList[_ryoList.Count - 1];
        Destroy(ryo.gameObject);
        _ryoList.Remove(ryo);
    }

    /// <summary>
    /// システム稲発動
    /// </summary>
    private void LaunchSystemIne()
    {
        // システム稲中は発動しない
        if (IsSystemIne())
        {
            return;
        }

        // システム稲：LV2
        if (_inePower >= (int)InePower.Lv2)
        {
            StartCoroutine("IESystemIneLv2");
        }

        // システム稲：LV1
        else if (_inePower >= (int)InePower.Lv1)
        {
            StartCoroutine("IESystemIneLv1");
        }
    }

    /// <summary>
    /// 衝突時イベント
    /// </summary>
    /// <param name="collision">Collision.</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Exists == false)
        {
            return;
        }

        string layerName = LayerMask.LayerToName(collision.gameObject.layer);

        // アイテム
        if (LayerConstant.ITEM.Equals(layerName))
        {
            Item item = collision.gameObject.GetComponent<Item>();
            if (item.Exists == false)
            {
                return;
            }

            Item.ItemType itemType = item.GetItemType();

            // 涼
            if (Item.ItemType.RYO == itemType)
            {
                AddRyo();
                item.Exists = false;
                Destroy(collision.gameObject);
            }

            // 稲
            else if (Item.ItemType.INE == itemType)
            {
                _bombCount++;
                item.Exists = false;
                Destroy(collision.gameObject);
            }
        }

        // 敵
        else if (LayerConstant.ENEMY.Equals(layerName))
        {
            Damage();
        }
    }

    /// <summary>
    /// システム稲起動中判定
    /// </summary>
    /// <returns><c>true</c>, if system ine was ised, <c>false</c> otherwise.</returns>
    private bool IsSystemIne()
    {
        return _ineMode >= IneMode.Lv1;
    }

    /// <summary>
    /// ショット
    /// </summary>
    private void Shot()
    {
        // 稲モード：LV1
        if (_ineMode == IneMode.Lv1)
        {
            PlayerShot2.Add(X + 0.4f, Y + 0.2f, 0.0f, 15.0f);
            PlayerShot2.Add(X + 0.4f, Y - 0.2f, 0.0f, 15.0f);
        }

        // 稲モード：LV2
        else if (_ineMode == IneMode.Lv2)
        {
            PlayerShot.Add(X + 0.3f, Y + 0.2f, 0.0f, 20.0f);
            PlayerShot2.Add(X + 0.4f, Y + 0.1f, 0.0f, 20.0f);
            PlayerShot2.Add(X + 0.5f, Y, 0.0f, 20.0f);
            PlayerShot2.Add(X + 0.4f, Y - 0.1f, 0.0f, 20.0f);
            PlayerShot.Add(X + 0.3f, Y - 0.2f, 0.0f, 20.0f);
        }

        // 稲モード：通常
        else
        {
            PlayerShot.Add(X + 0.4f, Y + 0.2f, 0.0f, 15.0f);
            PlayerShot.Add(X + 0.4f, Y - 0.2f, 0.0f, 15.0f);
        }
    }

    /// <summary>
    /// 被ダメ時コルーチン
    /// </summary>
    /// <returns>The amaged.</returns>
    IEnumerator IEDamaged()
    {
        _isDamaged = true;

        float defAlpha = Alpha;

        for (int i = 0; i < 60; i++)
        {
            yield return new WaitForSeconds(0.01f);
            Alpha = 0.2f;
            yield return new WaitForSeconds(0.01f);
            Alpha = defAlpha;
        }

        _isDamaged = false;
    }

    /// <summary>
    /// ショットコルーチン
    /// </summary>
    /// <returns>The layer shot.</returns>
    IEnumerator IEPlayerShot()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.07f);

            // キー入力で弾
            if (InputManager.IsInputCircle())
            {
                Shot();
            }
        }
    }

    /// <summary>
    /// 涼さんバリア用角度更コルーチン
    /// </summary>
    /// <returns>The ngle update.</returns>
    IEnumerator IEAngleUpdate()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.01f);

            // 角度更新
            _currentAngle++;
            _currentAngle++;
            _currentAngle++;
            if (_currentAngle >= 360)
            {
                _currentAngle = 0;
            }
        }
    }

    /// <summary>
    /// 稲パワーをチャージコルーチン.常時発動
    /// </summary>
    /// <returns>The ne power charge.</returns>
    IEnumerator IEInePowerCharge()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.01f);

            // システム稲起動中はチャージしない
            if (IsSystemIne())
            {
                continue;
            }

            // 稲パワーマックスならチャージしない
            if (_inePower >= INE_POWER_MAX)
            {
                continue;
            }

            // 稲パワー増加
            _inePower += INE_POWER_CHARGE_SPEED;
            if (_inePower >= INE_POWER_MAX)
            {
                _inePower = INE_POWER_MAX;
            }
        }
    }

    /// <summary>
    /// システム稲LV1コルーチン. システム稲起動中のみ
    /// </summary>
    /// <returns>The ystem ine.</returns>
    IEnumerator IESystemIneLv1()
    {
        _ineMode = IneMode.Lv1;
        Color defColor = Renderer.color;
        Renderer.color = Color.red;

        GameObject g = Resources.Load("Prefabs/" + "PlayerShadow") as GameObject;

        // 稲パワーがある間
        while (_inePower > 0)
        {
            yield return new WaitForSeconds(0.01f);

            // 稲のシャドウ
            Object.Instantiate(g, new Vector3(X, Y, 0), Quaternion.identity);

            // 稲パワー減少
            _inePower -= 4;
        }

        Renderer.color = defColor;
        _ineMode = IneMode.Normal;
    }

    /// <summary>
    /// システム稲LV2コルーチン. システム稲起動中のみ
    /// </summary>
    /// <returns>The ystem ine.</returns>
    IEnumerator IESystemIneLv2()
    {
        _ineMode = IneMode.Lv2;
        Color defColor = Renderer.color;
        Renderer.color = Color.yellow;

        GameObject g = Resources.Load("Prefabs/" + "PlayerShadow") as GameObject;

        // 稲パワーがある間
        while (_inePower > 0)
        {
            yield return new WaitForSeconds(0.01f);

            // 稲のシャドウ
            Object.Instantiate(g, new Vector3(X, Y, 0), Quaternion.identity);

            // 稲パワー減少
            _inePower -= 10;
        }

        Renderer.color = defColor;
        _ineMode = IneMode.Normal;
    }
}
