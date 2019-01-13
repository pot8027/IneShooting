using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShot2 : TokenController
{
    /// <summary>
    /// プレハブ名
    /// </summary>
    private static readonly string PREFUB_NAME = "PlayerShot2";

    /// <summary>
    /// インスタンスリスト
    /// </summary>
    private static TokenManager<PlayerShot2> _parent = null;

    /// <summary>
    /// インスタンスリストを作成します
    /// </summary>
    /// <param name="size">リストサイズ</param>
    public static void InitTokenManager(int size)
    {
        _parent = new TokenManager<PlayerShot2>(PREFUB_NAME, size);
    }

    /// <summary>
    /// インスタンスを追加します
    /// </summary>
    /// <returns>The add.</returns>
    /// <param name="x">The x coordinate.</param>
    /// <param name="y">The y coordinate.</param>
    /// <param name="direction">Direction.</param>
    /// <param name="speed">Speed.</param>
    public static PlayerShot2 Add(float x, float y, float direction, float speed)
    {
        if (_parent == null)
        {
            return null;
        }
        return _parent.Add(x, y, direction, speed);
    }

    /// <summary>
    /// 個別処理用更新処理
    /// </summary>
    protected override void UpdateEach()
    {
        if (IsOutside())
        {
            Vanish();
        }
    }

    public override void Vanish()
    {
        Particle p = Particle.Add(X, Y, 0, 0);
        base.Vanish();
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

        // 敵
        if (LayerConstant.ENEMY.Equals(layerName))
        {
            Enemy e = collision.gameObject.GetComponent<Enemy>();
            e.AddDamage(3);

            Vanish();
        }
        else if (LayerConstant.ENEMY_SHOT_BREAKABLE.Equals(layerName))
        {
            EnemyShot2 e = collision.gameObject.GetComponent<EnemyShot2>();
            e.Vanish();

            Vanish();
        }
    }
}
