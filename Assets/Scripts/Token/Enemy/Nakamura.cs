using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nakamura : Enemy
{
    /// <summary>
    /// 個別処理用更新処理
    /// </summary>
    protected override void UpdateEach()
    {
        // ヒットポイントがなくなっていれば消えて終了。
        if (HP <= 0)
        {
            // スコア追加
            AddScore(Score);

            // 自身を破棄
            Destroy(gameObject);

            // ゲームクリア判定
            if (HasGameClearFlg)
            {
                GameManager.CurrentMode = GameManager.Mode.gameclear;
            }

            // 撃破時のフレームを指定
            if (FrameJump >= 0)
            {
                GameManager.FrameCount = FrameJump;
            }

            // プレハブからインスタンス生成
            if (!string.IsNullOrEmpty(GeneratePrefubName))
            {
                GameObject g = Resources.Load("Prefabs/" + GeneratePrefubName) as GameObject;
                Object.Instantiate(g, new Vector3(X, Y, 0), Quaternion.identity);
            }

            return;
        }

        // HPバー更新
        if (IsDispHpBar)
        {
            HPBarMax.SetLeftHP(HP);
        }
    }

    /// <summary>
    /// コルーチン１
    /// </summary>
    /// <returns>The pdate1.</returns>
    IEnumerator IEUpdate1()
    {
        // 登場中は無敵
        _isMuteki = true;

        SetVelocity(90, 2);
        yield return new WaitForSeconds(3.0f);
 
        SetCoroutinueID(2);
    }

    /// <summary>
    /// IEUs the pdate2.
    /// </summary>
    /// <returns>The pdate2.</returns>
    IEnumerator IEUpdate2()
    {
        _isMuteki = false;

        SetVelocity(0, 0);

        float shotX = X - 1.2f;
        float shotY = Y + 1.3f;

        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(1.0f);

            float dir = GetTargetAim(shotX, shotY);
            EnemyShot1.Add(shotX, shotY, dir, 10);
        }

        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(0.5f);

            float dir = GetTargetAim(shotX, shotY);
            EnemyShot1.Add(shotX, shotY, dir, 10);
        }

        for (int i = 0; i < 20; i++)
        {
            yield return new WaitForSeconds(0.25f);

            float dir = GetTargetAim(shotX, shotY);
            EnemyShot1.Add(shotX, shotY, dir, 10);
        }

        for (int i = 0; i < 50; i++)
        {
            yield return new WaitForSeconds(0.1f);

            float dir = GetTargetAim(shotX, shotY);
            EnemyShot1.Add(shotX, shotY, dir, 10);
        }

        SetCoroutinueID(3);
    }

    /// <summary>
    /// IEUs the pdate3.
    /// </summary>
    /// <returns>The pdate3.</returns>
    IEnumerator IEUpdate3()
    {
        SetVelocity(90, 1);
        yield return new WaitForSeconds(2.2f);
        SetVelocity(0, 0);

        float shotX = X - 1.2f;
        float shotY = Y + 1.3f;

        for (int i = 0; i < 100; i++)
        {
            yield return new WaitForSeconds(0.1f);

            float dir = Random.Range(130, 230);
            float shotSpeed = Random.Range(6, 14);
            EnemyShot1.Add(shotX, shotY, dir, shotSpeed);

            if (i % 10 == 0)
            {
                EnemyShot2.Add(X + 0.4f, Y - 1.9f, 180, 4);
                EnemyShot2.Add(X - 1.5f, Y - 1.9f, 180, 4);
            }
        }

        SetCoroutinueID(4);
    }

    /// <summary>
    /// IEUs the pdate4.
    /// </summary>
    /// <returns>The pdate4.</returns>
    IEnumerator IEUpdate4()
    {
        SetVelocity(-90, 1);
        yield return new WaitForSeconds(2.2f);

        SetCoroutinueID(5);
    }

    /// <summary>
    /// IEUs the pdate2.
    /// </summary>
    /// <returns>The pdate2.</returns>
    IEnumerator IEUpdate5()
    {
        SetVelocity(0, 0);

        float shotX = X - 1.2f;
        float shotY = Y + 1.3f;

        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(1.0f);

            float dir = GetTargetAim(shotX, shotY);
            EnemyShot1.Add(shotX, shotY, dir, 10);
        }

        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(0.5f);

            float dir = GetTargetAim(shotX, shotY);
            EnemyShot1.Add(shotX, shotY, dir, 10);
        }

        for (int i = 0; i < 20; i++)
        {
            yield return new WaitForSeconds(0.25f);

            float dir = GetTargetAim(shotX, shotY);
            EnemyShot1.Add(shotX, shotY, dir, 10);
        }

        for (int i = 0; i < 50; i++)
        {
            yield return new WaitForSeconds(0.1f);

            float dir = GetTargetAim(shotX, shotY);
            EnemyShot1.Add(shotX, shotY, dir, 10);
        }

        SetCoroutinueID(3);
    }
}