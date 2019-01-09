using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameClear : MonoBehaviour
{
    public GameObject gameclear1 = null;
    public GameObject gameclear2 = null;

    void Start()
    {
        gameclear1.SetActive(false);
        gameclear2.SetActive(false);

        int num = GameManager.StageNo;

        if (num == 5)
        {
            gameclear2.SetActive(true);
        }
        else
        {
            gameclear1.SetActive(true);
        }
    }

    // Update is called once per frame
    /// <summary>
    /// Update this instance.
    /// </summary>
    void Update()
    {
        // スタートキー押下
        if (InputManager.IsKeyDownPause())
        {
            // ゲームに戻る
            SceneManager.LoadScene("StageSelect");
        }
    }
}
