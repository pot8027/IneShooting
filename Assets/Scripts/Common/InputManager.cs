using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager
{
    private static bool _verticalKeyDown = false;
    //private static bool _horizonalKeyDown = false;

    /// <summary>
    /// 決定キー押下中判定
    /// </summary>
    /// <returns><c>true</c>, if key circle was gotten, <c>false</c> otherwise.</returns>
    public static bool IsInputCircle()
    {
        // JOYPAD
        if (Input.GetKey(KeyCode.JoystickButton1))
        {
            return true;
        }

        // キーボード
        if (Input.GetKey(KeyCode.Z))
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// 決定キー押下判定
    /// </summary>
    /// <returns><c>true</c>, if key circle down was gotten, <c>false</c> otherwise.</returns>
    public static bool IsKeyDownCircle()
    {
        // JOYPAD
        if (Input.GetKeyDown(KeyCode.JoystickButton1))
        {
            return true;
        }

        // キーボード
        if (Input.GetKeyDown(KeyCode.Z))
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// ポーズキー押下中判定
    /// </summary>
    /// <returns><c>true</c>, if key pause was gotten, <c>false</c> otherwise.</returns>
    public static bool IsInputPause()
    {
        // JOYPAD
        if (Input.GetKey(KeyCode.JoystickButton8))
        {
            return true;
        }

        // キーボード
        if (Input.GetKey(KeyCode.Space))
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// ポーズキー押下判定
    /// </summary>
    /// <returns><c>true</c>, if key pause was gotten, <c>false</c> otherwise.</returns>
    public static bool IsKeyDownPause()
    {
        // JOYPAD
        if (Input.GetKeyDown(KeyCode.JoystickButton8))
        {
            return true;
        }

        // キーボード
        if (Input.GetKeyDown(KeyCode.Space))
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// 上キー押下中判定
    /// </summary>
    /// <returns><c>true</c>, if input up was ised, <c>false</c> otherwise.</returns>
    public static bool IsInputUp()
    {
        if (Input.GetAxisRaw("Vertical") >= 1.0f)
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// 上キー押下判定
    /// </summary>
    /// <returns><c>true</c>, if input up was ised, <c>false</c> otherwise.</returns>
    public static bool IsKeyDownUp()
    {
        if (Input.GetAxisRaw("Vertical") >= 1.0f)
        {
            if (_verticalKeyDown)
            {
                return false;
            }
            else
            {
                _verticalKeyDown = true;
                return true;
            }
        }
        else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) < 1.0f)
        {
            _verticalKeyDown = false;
        }

        return false;
    }

    /// <summary>
    /// 下キー押下中判定
    /// </summary>
    /// <returns><c>true</c>, if input up was ised, <c>false</c> otherwise.</returns>
    public static bool IsInputDown()
    {
        if (Input.GetAxisRaw("Vertical") <= -1.0f)
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// 下キー押下判定
    /// </summary>
    /// <returns><c>true</c>, if input up was ised, <c>false</c> otherwise.</returns>
    public static bool IsKeyDownDown()
    {
        if (Input.GetAxisRaw("Vertical") <= -1.0f)
        {
            if (_verticalKeyDown)
            {
                return false;
            }
            else
            {
                _verticalKeyDown = true;
                return true;
            }
        }
        else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) < 1.0f)
        {
            _verticalKeyDown = false;
        }

        return false;
    }

    /// <summary>
    /// 左キー押下中判定
    /// </summary>
    /// <returns><c>true</c>, if input up was ised, <c>false</c> otherwise.</returns>
    public static bool IsInputLeft()
    {
        if (Input.GetAxisRaw("Horizonal") >= 1.0f)
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// 右キー押下中判定
    /// </summary>
    /// <returns><c>true</c>, if input up was ised, <c>false</c> otherwise.</returns>
    public static bool IsInputRight()
    {
        if (Input.GetAxisRaw("Horizonal") <= -1.0f)
        {
            return true;
        }

        return false;
    }
}
