using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : TokenController
{
    /// <summary>
    /// プレイヤー
    /// </summary>
    private static Player _target = null;

    /// <summary>
    /// 無敵フラグ
    /// </summary>
    protected bool _isMuteki = false;

    /// <summary>
    /// ダメージを与える
    /// </summary>
    /// <param name="damage">Damage.</param>
    public virtual void AddDamage(int damage)
    {
        // 無敵中ならダメージを受けない
        if (_isMuteki)
        {
            return;
        }

        HP -= damage;
    }

    /// <summary>
    /// Start this instance.
    /// </summary>
    protected void Start()
    {
        InitSize();
        SetCoroutinueID(1);
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
    /// プレイヤーオブジェクトが存在するか判定します。
    /// </summary>
    /// <returns><c>true</c>, if player was found, <c>false</c> otherwise.</returns>
    protected bool FindPlayer()
    {
        if (_target != null)
        {
            return true;
        }

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            _target = player.GetComponent<Player>();
            return true;
        }

        return false;
    }

    /// <summary>
    /// プレイヤーとの角度を取得
    /// </summary>
    /// <returns>The target aim.</returns>
    protected float GetTargetAim()
    {
        // プレイヤーが見つからない場合は直進
        if (FindPlayer() == false)
        {
            return 180;
        }

        float dx = _target.X - X;
        float dy = _target.Y - Y;
        return Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;
    }

    /// <summary>
    /// プレイヤーとの指定位置での角度を取得
    /// </summary>
    /// <returns>The target aim.</returns>
    protected float GetTargetAim(float pointX, float pointY)
    {
        // プレイヤーが見つからない場合は直進
        if (FindPlayer() == false)
        {
            return 180;
        }

        float dx = _target.X - pointX;
        float dy = _target.Y - pointY;
        return Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;
    }

    /// <summary>
    /// プレイヤーとの距離を取得
    /// </summary>
    /// <returns>The target distance.</returns>
    protected float GetTargetDistance()
    {
        if (_target == null)
        {
            return 0;
        }

        Vector3 Apos = _target.transform.position;
        Vector3 Bpos = transform.position;
        return Vector3.Distance(Apos, Bpos);
    }
}
