using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShadow : TokenController
{
    /// <summary>
    /// 個別処理用更新処理
    /// </summary>
    protected override void UpdateEach()
    {
        // 徐々に薄く
        MulAlpha(0.9f);

        // 見えなくなったら消す
        if (Alpha < 0.01f)
        {
            DestroyObj();
        }
    }
}
