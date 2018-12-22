using UnityEngine;
using System.Collections;

public class Enemy1 : Enemy
{
    /// <summary>
    /// プレイヤー
    /// </summary>
    private static Player _target = null;

    /// <summary>
    /// 敵キャラの最大HPを取得
    /// 敵キャラごとに変更する場合はこのメソッドをオーバーライドして指定する
    /// </summary>
    /// <returns>The hp.</returns>
    protected override int GetMaxHP()
    {
        return 200;
    }

    /// <summary>
    /// コルーチン１
    /// </summary>
    /// <returns>The pdate1.</returns>
    IEnumerator IEUpdate1()
    {
        while(true)
        {
            float dir = GetTargetAim();
            EnemyShot1.Add(X, Y, dir, 5);

            // プレイヤーと一定距離以上の場合はコルーチン２
            if (GetTargetDistance() > 5)
            {
                SetCoroutinueID(2);
                break;
            }

            yield return new WaitForSeconds(2.0f);
        }
    }

    /// <summary>
    /// コルーチン２
    /// </summary>
    /// <returns>The pdate2.</returns>
    IEnumerator IEUpdate2()
    {
        while (true)
        {
            float dir = GetTargetAim();
            EnemyShot1.Add(X, Y, dir, 5);

            // プレイヤーと一定距離以内の場合はコルーチン１
            if (GetTargetDistance() <= 5)
            {
                SetCoroutinueID(1);
                break;
            }

            yield return new WaitForSeconds(0.2f);
        }
    }

    /// <summary>
    /// プレイヤーオブジェクトが存在するか判定します。
    /// </summary>
    /// <returns><c>true</c>, if player was found, <c>false</c> otherwise.</returns>
    private bool FindPlayer()
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

    private float GetTargetAim()
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
    private float GetTargetDistance()
    {
        if (_target == null)
        {
            return 0;
        }

        Vector3 Apos = _target.transform.position;
        Vector3 Bpos = transform.position;
        return Vector3.Distance(Apos, Bpos);
    }

    ///// <summary>
    ///// コルーチン２
    ///// </summary>
    ///// <returns>The pdate1.</returns>
    //IEnumerator IEUpdate2()
    //{
    //    while (true)
    //    {
    //        EnemyShot1.Add(0.1f, 0.1f, -20, 3);
    //        yield return new WaitForSeconds(2.0f);
    //        SetCoroutinueID(1);
    //        break;
    //    }
    //}
}
