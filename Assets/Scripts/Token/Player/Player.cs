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

    // Start is called before the first frame update
    private void Start()
    {
        var w = SpriteWidth / 2;
        var h = SpriteHeight / 2;
        SetSize(w, h);
    }

    // Update is called once per frame
    private void Update()
    {
        // キー入力で移動
        Vector2 v = AppUtil.GetInputVector();
        float speed = MoveSpeed * Time.deltaTime;
        ClampScreenAndMove(v * speed);

        if (Input.GetKey(KeyCode.JoystickButton1))
        {
            PlayerShot.Add(X, Y, 0.0f, 10.0f);
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
