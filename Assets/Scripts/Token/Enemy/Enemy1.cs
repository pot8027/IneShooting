using UnityEngine;
using System.Collections;

public class Enemy1 : Enemy
{
    /// <summary>
    /// 敵キャラの最大HPを取得
    /// 敵キャラごとに変更する場合はこのメソッドをオーバーライドして指定する
    /// </summary>
    /// <returns>The hp.</returns>
    protected override int GetMaxHP()
    {
        return 2;
    }

    /// <summary>
    /// 個別処理用更新処理
    /// </summary>
    protected override void UpdateEach()
    {
        Vector2 min = GetWorldMin();
        Vector2 max = GetWorldMax();

        // 上下ではみ出したら跳ね返る
        if (Y < min.y || max.y < Y)
        {
            ClampScreen();
            VY *= -1;
        }
        // 左ではみ出したら消滅して終了
        if (X < min.x)
        {
            Destroy(gameObject);
            return;
        }

        // HP0時の制御
        base.UpdateEach();
    }

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
