using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : TokenController
{
    /// <summary>
    /// プレイヤー
    /// </summary>
    private static Player _target = null;

    /// <summary>
    /// ヒットポイント
    /// </summary>
    private int _hp = 1;
    protected int HP
    {
        set { _hp = value; }
        get { return _hp; }
    }

    /// <summary>
    /// ダメージを与える
    /// </summary>
    /// <param name="damage">Damage.</param>
    public void AddDamage(int damage)
    {
        HP -= damage;
    }

    /// <summary>
    /// 敵キャラの最大HPを取得
    /// 敵キャラごとに変更する場合はこのメソッドをオーバーライドして指定する
    /// </summary>
    /// <returns>The hp.</returns>
    protected virtual int GetMaxHP()
    {
        return 50;
    }

    /// <summary>
    /// 撃破時のスコアを取得
    /// 敵キャラごとに変更する場合はこのメソッドをオーバーライドして指定する
    /// </summary>
    /// <returns>The score.</returns>
    protected virtual float GetScore()
    {
        return 10;
    }

    /// <summary>
    /// Start this instance.
    /// </summary>
    protected void Start()
    {
        HP = GetMaxHP();
        InitSize();
        SetCoroutinueID(1);
    }

    /// <summary>
    /// 個別処理用更新処理
    /// </summary>
    protected override void UpdateEach()
    {
        // ヒットポイントがなくなっていれば消えて終了。
        if (HP <= 0)
        {
            // TODO:消滅時パーティクルなど

            // スコア追加
            AddScore(GetScore());

            Destroy(gameObject);
            return;
        }
    }

    /// <summary>
    /// プレイヤーオブジェクトが存在するか判定します。
    /// </summary>
    /// <returns><c>true</c>, if player was found, <c>false</c> otherwise.</returns>
    protected bool FindPlayer()
    {
        if (_target != null)
        {
            return true;
        }

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            _target = player.GetComponent<Player>();
            return true;
        }

        return false;
    }

    /// <summary>
    /// プレイヤーとの角度を取得
    /// </summary>
    /// <returns>The target aim.</returns>
    protected float GetTargetAim()
    {
        // プレイヤーが見つからない場合は直進
        if (FindPlayer() == false)
        {
            return 180;
        }

        float dx = _target.X - X;
        float dy = _target.Y - Y;
        return Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;
    }

    /// <summary>
    /// プレイヤーとの距離を取得
    /// </summary>
    /// <returns>The target distance.</returns>
    protected float GetTargetDistance()
    {
        if (_target == null)
        {
            return 0;
        }

        Vector3 Apos = _target.transform.position;
        Vector3 Bpos = transform.position;
        return Vector3.Distance(Apos, Bpos);
    }
}
