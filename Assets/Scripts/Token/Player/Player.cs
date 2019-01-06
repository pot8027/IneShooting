using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : TokenController
{
    /// <summary>
    /// 残りボム数
    /// </summary>
    private int _bombCount = 4;

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
    /// プレイヤーを破棄
    /// </summary>
    public void DestroyPlayer()
    {
        // 涼さんが入れば１基消滅
        RemoveRyo();

        // ボムが残っていたらボムを使って終了
        if (_bombCount >= 1)
        {
            Bomb();
            return;
        }

        DestroyObj();
        GameManager.CurrentMode = GameManager.Mode.gameover;
    }

    // Start is called before the first frame update
    private void Start()
    {
        InitSize();

        StartCoroutine("IEPlayerShot");
        StartCoroutine("IEAngleUpdate");

        // デバッグ用涼さん追加
        //for (int i = 0; i < MAX_RYO; i++)
        //{
        //    AddRyo();
        //}
    }

    /// <summary>
    /// 個別処理用更新処理
    /// </summary>
    protected override void UpdateEach()
    {
        // ボム残り
        SetLeftBomb(_bombCount);

        // キー入力で移動
        Vector2 v = AppUtil.GetInputVector();
        float speed = MoveSpeed * Time.deltaTime;
        ClampScreenAndMove(v * speed);

        // ボム
        if (InputManager.IsKeyDownTriangle())
        {
            Bomb();
        }
    }


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
    }

    IEnumerator IEPlayerShot()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.07f);

            // キー入力で弾
            if (InputManager.IsInputCircle())
            {
                PlayerShot.Add(X + 0.4f, Y + 0.2f, 0.0f, 15.0f);
                PlayerShot.Add(X + 0.4f, Y - 0.2f, 0.0f, 15.0f);
            }
        }
    }

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
}
