using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : TokenController
{
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
        // キー入力で移動
        Vector2 v = AppUtil.GetInputVector();
        float speed = MoveSpeed * Time.deltaTime;
        ClampScreenAndMove(v * speed);
    }

    IEnumerator IEPlayerShot()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.07f);

            // キー入力で弾
            if (Input.GetKey(KeyCode.JoystickButton1))
            {
                PlayerShot.Add(X, Y, 0.0f, 15.0f);
            }
        }
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    string name = LayerMask.LayerToName(collision.gameObject.layer);

    //    switch (name)
    //    {
    //        case "Enemy":
    //        case "Bullet":
    //            Vanish();

    //            for (int i = 0; i < 16; i++)
    //            {
    //                Particle.Add(X, Y);
    //            }

    //            Sound.PlaySe("destroy");
    //            Sound.StopBgm();

    //            break;
    //    }
    //}
}
