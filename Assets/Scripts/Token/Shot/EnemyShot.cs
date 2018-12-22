using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShot : TokenController
{
    /// <summary>
    /// プレイヤー
    /// </summary>
    private static Player _target = null;

    /// <summary>
    /// 次回コルーチンID
    /// </summary>
    protected int _currentCoroutinueID = 0;

    /// <summary>
    /// コルーチン制御用IDを変更する
    /// </summary>
    /// <param name="id">Identifier.</param>
    protected void SetCoroutinueID(int id)
    {
        StopCoroutine("IEUpdate" + _currentCoroutinueID);
        _currentCoroutinueID = id;
        StartCoroutine("IEUpdate" + _currentCoroutinueID);
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
