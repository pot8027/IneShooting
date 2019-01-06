using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : TokenController
{
    public enum ItemType
    {
        RYO,
        INE,
        NONE
    }

    public virtual ItemType GetItemType()
    {
        return ItemType.NONE;
    }

    public virtual float GetSpeed()
    {
        return 3.0f;
    }

    private void Start()
    {
        StartCoroutine("IEUpdate1");
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
    }

    /// <summary>
    /// コルーチン１
    /// </summary>
    /// <returns>The pdate1.</returns>
    IEnumerator IEUpdate1()
    {
        float dir = Random.Range(100, 260);
        float speed = GetSpeed();
        SetVelocity(dir, speed);

        Color defColor = Renderer.color;
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            Renderer.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            Renderer.color = defColor;
        }
    }
}
