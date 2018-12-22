using UnityEngine;
using System.Collections;

public class Enemy001 : Enemy
{
    private int ccc = 0;

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
            RedShot.Add(0, 0, 20, 3);
            yield return new WaitForSeconds(2.0f);
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
        while (true)
        {
            RedShot.Add(0.1f, 0.1f, -20, 3);
            yield return new WaitForSeconds(2.0f);
            SetCoroutinueID(1);
            break;
        }
    }
}
