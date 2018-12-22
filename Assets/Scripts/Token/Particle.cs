using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : TokenController
{
    /// <summary>
    /// プレハブ名
    /// </summary>
    private static readonly string PREFUB_NAME = "Particle";

    /// <summary>
    /// インスタンスリスト
    /// </summary>
    private static TokenManager<Particle> _parent = null;

    /// <summary>
    /// インスタンスリストを作成します
    /// </summary>
    /// <param name="size">リストサイズ</param>
    public static void InitTokenManager(int size)
    {
        _parent = new TokenManager<Particle>(PREFUB_NAME, size);
    }

    /// <summary>
    /// インスタンスを追加します
    /// </summary>
    /// <returns>The add.</returns>
    /// <param name="x">The x coordinate.</param>
    /// <param name="y">The y coordinate.</param>
    /// <param name="direction">Direction.</param>
    /// <param name="speed">Speed.</param>
    public static Particle Add(float x, float y, float direction, float speed)
    {
        if (_parent == null)
        {
            return null;
        }

        Particle p = _parent.Add(x, y, direction, speed);
        if (p != null)
        {
            // 方向、速度
            p.SetVelocity(Random.Range(0, 359), Random.Range(2.0f, 2.0f));
            p.SetScale(0.25f, 0.25f);
        }

        return p;
    }

    /// <summary>
    /// Update this instance.
    /// </summary>
    void Update()
    {
        // 徐々に小さく遅く
        MulVelocity(0.95f);
        MulScale(0.97f);

        // 見えなくなったら消す
        if (Scale < 0.01)
        {
            Vanish();
        }
    }
}
