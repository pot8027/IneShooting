using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FukuLv3 : Enemy
{
    private int MAX_HP = 500;

    /// <summary>
    /// 敵キャラの最大HPを取得
    /// 敵キャラごとに変更する場合はこのメソッドをオーバーライドして指定する
    /// </summary>
    /// <returns>The hp.</returns>
    protected override int GetMaxHP()
    {
        return MAX_HP;
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

        // ヒットポイントがなくなっていれば消えて終了。
        if (HP <= 0)
        {
            // TODO:消滅時パーティクルなど

            // スコア追加
            AddScore(GetScore());
            Destroy(gameObject);

            // ゲームクリア
            GameManager.CurrentMode = GameManager.Mode.gameclear;
            return;
        }
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

    /// <summary>
    /// 現在HPが指定割合以下かどうか判定
    /// </summary>
    /// <returns><c>true</c>, if under was hped, <c>false</c> otherwise.</returns>
    /// <param name="targetRate">Target rate.</param>
    private bool HpUnder(double targetRate)
    {
        double hpRate = (double)HP / (double)MAX_HP;
        return hpRate <= targetRate;
    }
}