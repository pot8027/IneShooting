using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wan : Enemy
{
    /// <summary>
    /// コルーチン１
    /// </summary>
    /// <returns>The pdate1.</returns>
    IEnumerator IEUpdate1()
    {
        SetVelocity(180, 2);
        yield return new WaitForSeconds(1.5f);
        SetVelocity(0, 0);

        while (true)
        {
            for (int i = 0; i < 3; i++)
            {
                ShotCircle();
                yield return new WaitForSeconds(1.0f);
            }

            yield return new WaitForSeconds(3.0f);
        }
    }

    private void ShotCircle()
    {
        for (int i = 0; i < 24; i++)
        {
            EnemyShot1.Add(X, Y, i * 15, 3);
        }
    }
}
