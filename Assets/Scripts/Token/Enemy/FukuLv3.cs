using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FukuLv3 : Enemy
{
    /// <summary>
    /// 1
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
    /// 2
    /// </summary>
    /// <returns>The pdate1.</returns>
    IEnumerator IEUpdate2()
    {
        SetVelocity(90, 3);

        int count = 0;
        while (true)
        {
            yield return new WaitForSeconds(0.22f);
            float dir = GetTargetAim();
            EnemyShot1.Add(X, Y, dir, 5.0f);
            EnemyShot1.Add(X, Y, dir, 4.0f);
            EnemyShot1.Add(X, Y, dir, 4.0f);
            EnemyShot1.Add(X, Y, dir, 3.0f);

            count++;
            if (count >= 50)
            {
                SetCoroutinueID(3);
                break;
            }
        }
    }

    /// <summary>
    /// 3
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
            EnemyShot1.Add(X - 0.5f, Y, dir, 4);
            EnemyShot1.Add(X - 0.5f, Y, dir + 180, 4);
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
    /// 4
    /// </summary>
    /// <returns>The pdate1.</returns>
    IEnumerator IEUpdate4()
    {
        SetVelocity(90, 2);

        GameObject g = Resources.Load("Prefabs/" + "Enemy1") as GameObject;

        int count = 0;
        while (true)
        {
            yield return new WaitForSeconds(1.2f);

            // 画面上段に敵配置
            Object.Instantiate(g, new Vector3(7, 4, 0), Quaternion.identity);

            // 画面下段に敵配置
            Object.Instantiate(g, new Vector3(7, -4, 0), Quaternion.identity);

            count++;
            if (count >= 10)
            {
                SetCoroutinueID(5);
                break;
            }
        }
    }

    /// <summary>
    /// 5
    /// </summary>
    /// <returns>The pdate1.</returns>
    IEnumerator IEUpdate5()
    {
        SetVelocity(90, 0);

        bool anglePlus = true;

        int count = 0;
        while (true)
        {
            for (int i = 1; i < 36; i++)
            {
                yield return new WaitForSeconds(0.05f);

                if (anglePlus)
                {
                    EnemyShot1.Add(X, Y, 90 + i * 5, 3.0f);
                    EnemyShot1.Add(X - 0.5f, Y, 270 - i * 5, 3.0f);
                }
                else
                {
                    EnemyShot1.Add(X, Y, 270 - i * 5, 3.0f);
                    EnemyShot1.Add(X - 0.5f, Y, 90 + i * 5, 3.0f);
                }
            }

            anglePlus = !anglePlus;

            count++;
            if (count >= 10)
            {
                SetCoroutinueID(2);
                break;
            }
        }
    }
}
