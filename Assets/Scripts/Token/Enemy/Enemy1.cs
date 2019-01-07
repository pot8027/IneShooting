using UnityEngine;
using System.Collections;

public class Enemy1 : Enemy
{
    /// <summary>
    /// コルーチン１
    /// </summary>
    /// <returns>The pdate1.</returns>
    IEnumerator IEUpdate1()
    {
        SetVelocity(180, 3);

        while (true)
        {
            float dir = GetTargetAim();
            EnemyShot2.Add(X, Y, dir, 2);

            // プレイヤーと一定距離以上の場合はコルーチン２
            if (GetTargetDistance() > 4)
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
            EnemyShot1.Add(X, Y, dir, 4);

            // プレイヤーと一定距離以内の場合はコルーチン１
            if (GetTargetDistance() <= 4)
            {
                SetCoroutinueID(1);
                break;
            }

            yield return new WaitForSeconds(1.0f);
        }
    }
}
