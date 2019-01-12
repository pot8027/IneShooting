using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class CommonText : MonoBehaviour
{
    public void SetEnable(bool enable)
    {
        this.enabled = enable;
    }

    public void SetText(string text)
    {
        gameObject.GetComponent<Text>().text = text;
    }
}
