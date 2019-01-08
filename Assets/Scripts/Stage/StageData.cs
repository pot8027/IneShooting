using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageData
{
    public static readonly string TYPE_PREFUB = "P";
    public static readonly string TYPE_FRAME = "F";

    /****************************
     * 0 フレーム
     * 1 データタイプ    
     * 2 プレハブ名
     * 3 X位置
     * 4 Y位置
     * 5 HP
     * 6 HPバー表示
     * 7 スコア
     * 8 撃破時ゲームクリアフラグ
     * 9 撃破時遷移先フレーム
     * 10 撃破時生成プレハブ    
    *****************************/   

    /// <summary>
    /// タイプ
    /// </summary>
    /// <value>The typee.</value>
    public string DataType { get; set; }

    /// <summary>
    /// プレハブ名
    /// </summary>
    /// <value>The name of the prefub.</value>
    public string PrefubName { get; set; }

    /// <summary>
    /// X座標
    /// </summary>
    /// <value>The point x.</value>
    public float PointX { get; set; }

    /// <summary>
    /// Y座標
    /// </summary>
    /// <value>The point y.</value>
    public float PointY { get; set; }

    /// <summary>
    /// HP
    /// </summary>
    /// <value>The hp.</value>
    public int Hp { get; set; }

    /// <summary>
    /// HPバーを表示するかどうか
    /// </summary>
    /// <value>The hp.</value>
    public bool IsDispHP { get; set; }

    /// <summary>
    /// スコア
    /// </summary>
    /// <value>The score.</value>
    public int Score { get; set; }

    /// <summary>
    /// 撃破時ゲームクリアフラグ
    /// </summary>
    /// <value><c>true</c> if flg game clear; otherwise, <c>false</c>.</value>
    public bool FlgGameClear { get; set; }

    /// <summary>
    /// 撃破時遷移先フレーム
    /// </summary>
    /// <value>The jump frame.</value>
    public long JumpFrame { get; set; }

    /// <summary>
    /// 撃破時生成プレハブ名
    /// </summary>
    /// <value>The name of the generate prefub.</value>
    public string GeneratePrefubName { get; set; }

    /// <summary>
    /// 遷移先フレーム（フレームデータ）
    /// </summary>
    /// <value>The next frame.</value>
    public long NextFrame { get; set; }

    private StageData()
    {

    }

    /// <summary>
    /// インスタンス生成
    /// </summary>
    /// <returns>The instance.</returns>
    /// <param name="line">Line.</param>
    public static StageData CreateInstance(string line)
    {
        string[] datas = line.Split(',');

        StageData result = new StageData();

        // デフォルト設定
        result.DataType = string.Empty;
        result.PrefubName = string.Empty;
        result.PointX = 0;
        result.PointY = 0;
        result.Hp = 1;
        result.IsDispHP = false;
        result.Score = 1;
        result.FlgGameClear = false;
        result.JumpFrame = -1;
        result.GeneratePrefubName = string.Empty;

        // データタイプ
        result.DataType = datas[1];

        // プレハブ用データ作成
        if (TYPE_PREFUB.Equals(result.DataType))
        {
            result.PrefubName = datas[2];
            result.PointX = float.Parse(datas[3]);
            result.PointY = float.Parse(datas[4]);

            // HP
            if (!string.IsNullOrEmpty(datas[5]))
            {
                result.Hp = int.Parse(datas[5]);
            }

            // HPバーを表示するかどうか
            if ("1".Equals(datas[6]))
            {
                result.IsDispHP = true;
            }

            // スコア
            if (!string.IsNullOrEmpty(datas[7]))
            {
                result.Score = int.Parse(datas[7]);
            }

            // 撃破時ゲームクリアフラグ
            if ("1".Equals(datas[8]))
            {
                result.FlgGameClear = true;
            }

            // 撃破時ジャンプ先フレーム
            if (!string.IsNullOrEmpty(datas[9]))
            {
                result.JumpFrame = long.Parse(datas[9]);
            }

            // 撃破時生成プレハブ名
            if (!"NULL".Equals(datas[10]))
            {
                result.GeneratePrefubName = datas[10];
            }
        }

        // フレーム用データ作成
        else if (TYPE_FRAME.Equals(result.DataType))
        {
            result.NextFrame = long.Parse(datas[2]);
        }

        return result;
    }
}
