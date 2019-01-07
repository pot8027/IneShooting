using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBarMax : TokenController
{
    private float HPMAX = 0;
    private float HP = 0;

    private HPBarLeft _hpBarLeft = null;

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void SetMaxHP(float max)
    {
        HPMAX = max;
        HP = max;
        SetLeftHP(HP);
    }

    public void SetLeftHP(float leftHP)
    {
        if (_hpBarLeft == null)
        {
            _hpBarLeft = gameObject.transform.Find("HPBarLeft").gameObject.GetComponent<HPBarLeft>();
        }

        HP = leftHP;

        // HPバー長さ調整
        float rate = HP / HPMAX;
        _hpBarLeft.transform.localScale = new Vector3(rate, 1, 1);
        _hpBarLeft.transform.localPosition = new Vector3((1 * rate - 1.0f) / 2.0f, 0, 1);
    }
}
