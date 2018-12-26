using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FukuLv3 : Enemy
{
    /// <summary>
    /// 敵キャラの最大HPを取得
    /// 敵キャラごとに変更する場合はこのメソッドをオーバーライドして指定する
    /// </summary>
    /// <returns>The hp.</returns>
    protected override int GetMaxHP()
    {
        return 1500;
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
    /// コルーチ２
    /// </summary>
    /// <returns>The pdate1.</returns>
    IEnumerator IEUpdate2()
    {
        SetVelocity(90, 10);

        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            EnemyShot2.Add(X, Y, 0, 4.0f);
            EnemyShot2.Add(X, Y, 0, 3.0f);
            EnemyShot2.Add(X, Y, 0, 2.0f);
            EnemyShot2.Add(X, Y, 0, 1.0f);
            EnemyShot1.Add(X, Y, 180, 5.0f);
        }
    }
}
