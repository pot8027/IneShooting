﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameClear : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    /// <summary>
    /// Update this instance.
    /// </summary>
    void Update()
    {
        // スタートキー押下
        if (Input.GetKeyUp(KeyCode.JoystickButton8))
        {
            // ゲームに戻る
            Application.LoadLevel("Stage");
        }
    }
}