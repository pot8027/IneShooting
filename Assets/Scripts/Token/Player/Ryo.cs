using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ryo : TokenController
{
    private static readonly float DISTANCE_FROM_PARENT = 1.0f;

    private Player _parent = null;
    public Player Parent
    {
        set { _parent = value; }
    }

    private int _adjustAngle = 0;
    public int AdjustAngle
    {
        set { _adjustAngle = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        Visible = false;
        StartCoroutine("IEUpdate");
    }

    IEnumerator IEUpdate()
    {
        while (true)
        {
            Visible = true;
            yield return new WaitForSeconds(0.01f);

            if (_parent != null)
            {
                // 表示位置角度
                float angle = (float)_parent.CurrentAngle + (float)_adjustAngle;
                float positionX = _parent.X + DISTANCE_FROM_PARENT * Mathf.Cos(Mathf.Deg2Rad * angle);
                float positionY = _parent.Y + DISTANCE_FROM_PARENT * Mathf.Sin(Mathf.Deg2Rad * angle);
                transform.localPosition = new Vector3(positionX, positionY, 0);
            }
        }
    }

    /// <summary>
    /// 衝突時イベント
    /// </summary>
    /// <param name="collision">Collision.</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Exists == false)
        {
            return;
        }

        string layerName = LayerMask.LayerToName(collision.gameObject.layer);

        if (LayerConstant.ENEMY_SHOT.Equals(layerName))
        {
            EnemyShot e = collision.gameObject.GetComponent<EnemyShot>();
            e.Vanish();
        }
        else if (LayerConstant.ENEMY_SHOT_BREAKABLE.Equals(layerName))
        {
            EnemyShot2 e = collision.gameObject.GetComponent<EnemyShot2>();
            e.Vanish();
        }
    }
}
