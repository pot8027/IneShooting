using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : TokenController
{
    /// <summary>
    /// 次回コルーチンID
    /// </summary>
    protected int _currentCoroutinueID = 0;

    /// <summary>
    /// 前回コルーチンID
    /// </summary>
    protected int _prevCoroutinueID = 0;

    /// <summary>
    /// コルーチン制御用ID変更フラグ
    /// </summary>
    protected bool _changeCoroutinueID = false;

    /// <summary>
    /// コルーチン制御用IDを変更する
    /// </summary>
    /// <param name="id">Identifier.</param>
    protected void SetCoroutinueID(int id)
    {
        _prevCoroutinueID = _currentCoroutinueID;
        _currentCoroutinueID = id;
        _changeCoroutinueID = true;
    }

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
    protected virtual int GetScore()
    {
        return 0;
    }

    /// <summary>
    /// Start this instance.
    /// </summary>
    protected void Start()
    {
        HP = GetMaxHP();
        SetCoroutinueID(1);
    }

    /// <summary>
    /// Update this instance.
    /// </summary>
    protected void Update()
    {
        // ヒットポイントがなくなっていれば消えて終了。
        if (HP <= 0)
        {
            // TODO:スコア追加
            // TODO:消滅時パーティクルなど

            Destroy(gameObject);
            return;
        }

        // コルーチン制御用IDが変更されていれば新規コルーチン開始
        if (_changeCoroutinueID)
        {
            StopCoroutine("IEUpdate" + _prevCoroutinueID);
            StartCoroutine("IEUpdate" + _currentCoroutinueID);

            // 新たにコルーチンを開始したのでfalse
            _changeCoroutinueID = false;
        }
    }
}
