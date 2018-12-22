﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedShot : TokenController
{
    /// <summary>
    /// プレハブ名
    /// </summary>
    private static readonly string PREFUB_NAME = "RedShot";

    /// <summary>
    /// インスタンスリスト
    /// </summary>
    private static TokenManager<RedShot> _parent = null;

    /// <summary>
    /// インスタンスリストを作成します
    /// </summary>
    /// <param name="size">リストサイズ</param>
    public static void InitTokenManager(int size)
    {
        _parent = new TokenManager<RedShot>(PREFUB_NAME, size);
    }

    /// <summary>
    /// インスタンスを追加します
    /// </summary>
    /// <returns>The add.</returns>
    /// <param name="x">The x coordinate.</param>
    /// <param name="y">The y coordinate.</param>
    /// <param name="direction">Direction.</param>
    /// <param name="speed">Speed.</param>
    public static RedShot Add(float x, float y, float direction, float speed)
    {
        if (_parent == null)
        {
            return null;
        }
        return _parent.Add(x, y, direction, speed);
    }

    /// <summary>
    /// Update this instance.
    /// </summary>
    private void Update()
    {
        if (IsOutside())
        {
            Vanish();
        }
    }

    /// <summary>
    /// 衝突時イベント
    /// </summary>
    /// <param name="collision">Collision.</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        string layerName = LayerMask.LayerToName(collision.gameObject.layer);

        // 敵
        if (LayerConstant.ENEMY.Equals(layerName))
        {
            Enemy e = collision.gameObject.GetComponent<Enemy>();
            e.Vanish();

            Vanish();
        }
    }
}

