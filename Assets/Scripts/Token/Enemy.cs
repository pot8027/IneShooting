using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : TokenController
{
    /// <summary>
    /// ヒットポイント
    /// </summary>
    private int _hp = 50;
    public int HP
    {
        set { _hp = value; }
        get { return _hp; }
    }

    /// <summary>
    /// Start this instance.
    /// </summary>
    void Start()
    {
        
    }

    /// <summary>
    /// Update this instance.
    /// </summary>
    void Update()
    {
        // ヒットポイントがなくなっていれば消える。
        if (HP <= 0)
        {
            Vanish();

            // TODO:スコア追加
            // TODO:消滅時パーティクルなど

            return;
        }
    }

    public void AddDamage(int damage)
    {
        HP -= damage;
    }
}
