using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBarMax : TokenController
{
    private float HPMAX = 0;
    private float CurrentHP = 0;

    private HPBarLeft _hpBarLeft = null;

    /// <summary>
    /// 非表示
    /// </summary>
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 表示
    /// </summary>
    public void Show()
    {
        gameObject.SetActive(true);
    }

    /// <summary>
    /// 最大HPセット
    /// </summary>
    /// <param name="max">Max.</param>
    public void SetMaxHP(float max)
    {
        HPMAX = max;
        CurrentHP = max;
        SetLeftHP(CurrentHP);
    }

    public void SetSpriteX(float parentSpriteX)
    {
        // ボツ
        //float rate = parentSpriteX / Renderer.bounds.size.x;
        //float calcX = Renderer.bounds.size.x * rate;

        //transform.localScale = new Vector3(calcX, Renderer.bounds.size.y, 1);
    }

    /// <summary>
    /// 現在HPセット
    /// </summary>
    /// <param name="leftHP">Left hp.</param>
    public void SetLeftHP(float leftHP)
    {
        if (_hpBarLeft == null)
        {
            _hpBarLeft = this.transform.Find("HPBarLeft").GetComponent<HPBarLeft>();
        }

        CurrentHP = leftHP;

        // HPバー長さ調整
        float rate = CurrentHP / HPMAX;
        _hpBarLeft.transform.localScale = new Vector3(rate, 1, 1);
        _hpBarLeft.transform.localPosition = new Vector3((1 * rate - 1.0f) / 2.0f, 0, 1);
    }
}
