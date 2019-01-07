using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FukuLv4 : Enemy
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
        SetVelocity(90, 2);

        // 散布用角度
        float dir1 = 0;
        float dir2 = 0;

        int count = 0;
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            EnemyShot1.Add(X, Y, dir1, 7);
            EnemyShot1.Add(X, Y, dir1 + 180, 7);
            EnemyShot1.Add(X - 0.5f, Y, dir2, 7);
            EnemyShot1.Add(X - 0.5f, Y, dir2 + 180, 7);
            dir1 += 8;
            dir2 -= 8;

            count++;
            if (count >= 150)
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
        SetVelocity(90, 2);

        GameObject g = Resources.Load("Prefabs/" + "Enemy1") as GameObject;

        int count = 0;
        while (true)
        {
            yield return new WaitForSeconds(0.5f);

            // 画面上段に敵配置
            Object.Instantiate(g, new Vector3(7, 4, 0), Quaternion.identity);

            // 画面下段に敵配置
            Object.Instantiate(g, new Vector3(7, -4, 0), Quaternion.identity);

            count++;
            if (count >= 20)
            {
                SetCoroutinueID(2);
                break;
            }
        }
    }
}
