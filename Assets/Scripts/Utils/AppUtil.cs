using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppUtil
{
    /// <summary>
    /// Mathf.Cosの角度指定版.
    /// </summary>
    /// <returns>The ex.</returns>
    /// <param name="Deg">角度</param>
    public static float CosEx(float Deg)
    {
        return Mathf.Cos(Mathf.Deg2Rad * Deg);
    }

    /// <summary>
    /// Mathf.Sinの角度指定版.
    /// </summary>
    /// <returns>The ex.</returns>
    /// <param name="Deg">角度</param>
    public static float SinEx(float Deg)
    {
        return Mathf.Sin(Mathf.Deg2Rad * Deg);
    }

    /// <summary>
    /// 入力方向を取得する.
    /// </summary>
    /// <returns>入力方向</returns>
    public static Vector2 GetInputVector()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        return new Vector2(x, y).normalized;
    }

    /// <summary>
    /// スプライトをリソースから取得する.
    /// ※スプライトは「Resources/Sprites」以下に配置していなければなりません.
    /// ※fileNameに空文字（""）を指定するとシングルスプライトから取得します.
    /// </summary>
    /// <returns>The sprite.</returns>
    /// <param name="fileName">File name.</param>
    /// <param name="spriteName">Sprite name.</param>
    public static Sprite GetSprite(string fileName, string spriteName)
    {
        if (spriteName == "")
        {
            // シングルスプライト
            return Resources.Load<Sprite>(fileName);
        }
        else
        {
            // マルチスプライト
            Sprite[] sprites = Resources.LoadAll<Sprite>(fileName);
            return System.Array.Find<Sprite>(sprites, (sprite) => sprite.name.Equals(spriteName));
        }
    }
}
