using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FukuLv4 : Enemy
{
    private int MAX_HP = 2000;

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

        // HPバー更新
        HPBarMax.SetLeftHP(HP);

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
    /// HPバーを表示するかどうか
    /// </summary>
    /// <returns><c>true</c>, if disp hp bar was ised, <c>false</c> otherwise.</returns>
    protected override bool IsDispHpBar()
    {
        return true;
    }

    /// <summary>
    /// 1
    /// </summary>
    /// <returns>The pdate1.</returns>
    IEnumerator IEUpdate1()
    {
        if (IsDispHpBar())
        {
            HPBarMax.Show();
            HPBarMax.SetMaxHP((float)MAX_HP);
        }

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
