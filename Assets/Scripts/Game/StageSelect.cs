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

    /// <summary>
    /// 上下キーダウン
    /// </summary>
    private bool _verticalKeyDown = false;

    // Update is called once per frame
    /// <summary>
    /// Update this instance.
    /// </summary>
    void Update()
    {
        // スタートキー押下
        if (Input.GetKeyDown(KeyCode.JoystickButton8))
        {
            // 難易度設定
            LabelDifficult l = _currenntDifficult.GetComponent<LabelDifficult>();
            GameManager.StageNo = l._leevel;

            // ゲームに戻る
            SceneManager.LoadScene("Stage");
        }

        // 上キー押下
        else if (Input.GetAxisRaw("Vertical") >= 1.0f)
        {
            if (_verticalKeyDown == false)
            {
                _verticalKeyDown = true;

                // フォーカスを外す
                LabelDifficult prev = _currenntDifficult.GetComponent<LabelDifficult>();
                prev.SetLostFocus();

                // フォーカスする
                _currenntDifficult = prev._up;
                LabelDifficult next = _currenntDifficult.GetComponent<LabelDifficult>();
                next.SetOnFocus();
            }
        }

        // 下キー押下
        else if (Input.GetAxisRaw("Vertical") <= -1.0f)
        {
            if (_verticalKeyDown == false)
            {
                _verticalKeyDown = true;

                // フォーカスを外す
                LabelDifficult prev = _currenntDifficult.GetComponent<LabelDifficult>();
                prev.SetLostFocus();

                // フォーカスする
                _currenntDifficult = prev._down;
                LabelDifficult next = _currenntDifficult.GetComponent<LabelDifficult>();
                next.SetOnFocus();
            }
        } 

        else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) < 1.0f)
        {
            _verticalKeyDown = false;
        }
    }
}
