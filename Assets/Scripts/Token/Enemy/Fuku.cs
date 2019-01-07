using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuku : Enemy
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
    /// コルーチ２
    /// </summary>
    /// <returns>The pdate1.</returns>
    IEnumerator IEUpdate2()
    {
        SetVelocity(90, 3);

        while (true)
        {
            yield return new WaitForSeconds(0.3f);
            EnemyShot2.Add(X, Y, 0, -3.0f);
        }
    }
}
