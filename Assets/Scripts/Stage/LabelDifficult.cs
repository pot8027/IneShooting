using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LabelDifficult : MonoBehaviour
{
    public GameObject _up = null;
    public GameObject _down = null;
    public int _leevel = 1;
    public bool _focus = false; 

    void Start()
    {
        if (_focus)
        {
            GetComponent<Text>().color = Color.blue;
        }
    }

    public void SetOnFocus()
    {
        _focus = true;
        GetComponent<Text>().color = Color.blue;
    }

    public void SetLostFocus()
    {
        _focus = false;
        GetComponent<Text>().color = Color.white;
    }
}
