using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : TokenController
{
    /// <summary>
    /// 残りボム数
    /// </summary>
    private int _bombCount = 5;

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

    /// <summary>
    /// プレイヤーを破棄
    /// </summary>
    public void DestroyPlayer()
    {
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
}
