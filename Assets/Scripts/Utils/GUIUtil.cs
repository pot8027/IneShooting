using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIUtil
{
    private static Rect _guiRect = new Rect();
    private static GUIStyle _guiStyle = null;

    public static Rect GetGUIRect()
    {
        return _guiRect;
    }

    public static GUIStyle GetGUIStyle()
    {
        return _guiStyle ?? (_guiStyle = new GUIStyle());
    }

    /// <summary>
    /// フォントサイズを設定.
    /// </summary>
    /// <param name="size">Size.</param>
    public static void SetFontSize(int size)
    {
        GetGUIStyle().fontSize = size;
    }

    /// <summary>
    /// フォントカラーを設定.
    /// </summary>
    /// <param name="color">Color.</param>
    public static void SetFontColor(Color color)
    {
        GetGUIStyle().normal.textColor = color;
    }

    /// <summary>
    /// フォント位置設定
    /// </summary>
    /// <param name="align">Align.</param>
    public static void SetFontAlignment(TextAnchor align)
    {
        GetGUIStyle().alignment = align;
    }

    /// <summary>
    /// ラベルの描画.
    /// </summary>
    /// <param name="x">The x coordinate.</param>
    /// <param name="y">The y coordinate.</param>
    /// <param name="w">The width.</param>
    /// <param name="h">The height.</param>
    /// <param name="text">Text.</param>
    public static void GUILabel(float x, float y, float w, float h, string text)
    {
        Rect rect = GetGUIRect();
        rect.x = x;
        rect.y = y;
        rect.width = w;
        rect.height = h;

        GUI.Label(rect, text, GetGUIStyle());
    }

    /// <summary>
    /// ボタンの配置.
    /// </summary>
    /// <returns><c>true</c>, if utton was GUIBed, <c>false</c> otherwise.</returns>
    /// <param name="x">The x coordinate.</param>
    /// <param name="y">The y coordinate.</param>
    /// <param name="w">The width.</param>
    /// <param name="h">The height.</param>
    /// <param name="text">Text.</param>
    public static bool GUIButton(float x, float y, float w, float h, string text)
    {
        Rect rect = GetGUIRect();
        rect.x = x;
        rect.y = y;
        rect.width = w;
        rect.height = h;

        return GUI.Button(rect, text, GetGUIStyle());
    }
}
