using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeftBomb : MonoBehaviour
{
    public void SetLeft(int left)
    {
        this.GetComponent<Text>().text = left.ToString();
    }
}
