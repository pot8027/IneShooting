using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FukuLv2 : Enemy
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
            yield return new WaitForSeconds(1.0f);
            SetCoroutinueID(2);
            break;
        }
    }

    /// <summary>
    /// HP100%以下
    /// </summary>
    /// <returns>The pdate1.</returns>
    IEnumerator IEUpdate2()
    {
        SetVelocity(90, 3);

        int count = 0;
        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            float dir = GetTargetAim();
            EnemyShot1.Add(X, Y, dir, 5.0f);
            EnemyShot1.Add(X, Y, dir, 4.0f);

            count++;
            if (count >= 50)
            {
                SetCoroutinueID(3);
                break;
            }
        }
    }

    /// <summary>
    /// HP50%以下
    /// </summary>
    /// <returns>The pdate1.</returns>
    IEnumerator IEUpdate3()
    {
        SetVelocity(90, 0);

        // 散布用角度
        float dir = 0;

        int count = 0;
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            EnemyShot1.Add(X, Y, dir, 4);
            EnemyShot1.Add(X, Y, dir + 180, 4);
            EnemyShot1.Add(X - 1.0f, Y, dir, 4);
            EnemyShot1.Add(X - 1.0f, Y, dir + 180, 4);
            dir += 8;

            count++;
            if (count >= 150)
            {
                SetCoroutinueID(4);
                break;
            }
        }
    }

    /// <summary>
    /// HP10%以下
    /// </summary>
    /// <returns>The pdate1.</returns>
    IEnumerator IEUpdate4()
    {
        SetVelocity(90, 12);

        int count = 0;
        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            float dir = GetTargetAim();
            EnemyShot1.Add(X, Y, dir, 6.0f);
            EnemyShot1.Add(X, Y, dir, 5.0f);
            EnemyShot1.Add(X, Y, dir, 4.0f);
            EnemyShot1.Add(X, Y, dir, 3.0f);
            EnemyShot1.Add(X, Y + 0.1f, 180, 5.0f);
            EnemyShot1.Add(X, Y - 0.1f, 180, 5.0f);

            // TODO:敵キャラ追加

            count++;
            if (count >= 50)
            {
                SetCoroutinueID(2);
                break;
            }
        }
    }
}