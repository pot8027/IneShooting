using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelect : MonoBehaviour
{
    /// <summary>
    /// 現在フォーカス中の難易度
    /// </summary>
    public GameObject _currenntDifficult = null;

    private void Start()
    {
        Application.targetFrameRate = 60;
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
            // 難易度設定
            LabelDifficult l = _currenntDifficult.GetComponent<LabelDifficult>();
            GameManager.StageNo = l._leevel;

            // ゲームに戻る
            SceneManager.LoadScene("Stage");
        }

        // 上キー押下
        else if (InputManager.IsKeyDownUp())
        {
            // フォーカスを外す
            LabelDifficult prev = _currenntDifficult.GetComponent<LabelDifficult>();
            prev.SetLostFocus();

            // フォーカスする
            _currenntDifficult = prev._up;
            LabelDifficult next = _currenntDifficult.GetComponent<LabelDifficult>();
            next.SetOnFocus();
        }

        // 下キー押下
        else if (InputManager.IsKeyDownDown())
        {
            // フォーカスを外す
            LabelDifficult prev = _currenntDifficult.GetComponent<LabelDifficult>();
            prev.SetLostFocus();

            // フォーカスする
            _currenntDifficult = prev._down;
            LabelDifficult next = _currenntDifficult.GetComponent<LabelDifficult>();
            next.SetOnFocus();
        } 
    }
}
