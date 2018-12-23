using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShot2 : EnemyShot
{
    /// <summary>
    /// プレハブ名
    /// </summary>
    private static readonly string PREFUB_NAME = "EnemyShot2";

    /// <summary>
    /// インスタンスリスト
    /// </summary>
    private static TokenManager<EnemyShot2> _parent = null;

    /// <summary>
    /// インスタンスリストを作成します
    /// </summary>
    /// <param name="size">リストサイズ</param>
    public static void InitTokenManager(int size)
    {
        _parent = new TokenManager<EnemyShot2>(PREFUB_NAME, size);
    }

    /// <summary>
    /// インスタンスを追加します
    /// </summary>
    /// <returns>The add.</returns>
    /// <param name="x">The x coordinate.</param>
    /// <param name="y">The y coordinate.</param>
    /// <param name="direction">Direction.</param>
    /// <param name="speed">Speed.</param>
    public static EnemyShot2 Add(float x, float y, float direction, float speed)
    {
        if (_parent == null)
        {
            return null;
        }

        // コルーチンが設定されない場合があるので
        EnemyShot2 e = _parent.Add(x, y, direction, speed);
        e.SetCoroutinueID(1);
        return e;
    }

    /// <summary>
    /// Start this instance.
    /// </summary>
    private void Start()
    {
        SetCoroutinueID(1);
    }

    /// <summary>
    /// Update this instance.
    /// </summary>
    private void Update()
    {
        if (IsOutside())
        {
            Vanish();
        }
    }

    /// <summary>
    /// 衝突時イベント
    /// </summary>
    /// <param name="collision">Collision.</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        string layerName = LayerMask.LayerToName(collision.gameObject.layer);

        // プレイヤ
        if (LayerConstant.PLAYER.Equals(layerName))
        {
            Player p = collision.gameObject.GetComponent<Player>();
            p.DestroyObj();
            Vanish();
        }
    }

    /// <summary>
    /// コルーチン１
    /// </summary>
    /// <returns>The pdate1.</returns>
    IEnumerator IEUpdate1()
    {
        // 一度に曲がれる角度。大きいとくいっと曲がる
        const float ROT = 5.0f;

        while (true)
        {
            yield return new WaitForSeconds(0.02f);

            float dir = Direction;
            float aim = GetTargetAim();
            float delta = Mathf.DeltaAngle(dir, aim);

            if (Mathf.Abs(delta) < ROT)
            {

            }
            else if (delta > 0)
            {
                dir += ROT;
            }
            else
            {
                dir -= ROT;
            }

            SetVelocity(dir, Speed);
            Angle = dir;

            if (IsOutside())
            {
                Vanish();
            }
        }
    }
}
