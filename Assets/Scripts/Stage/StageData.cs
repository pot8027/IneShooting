using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageData
{
    public static readonly string TYPE_PREFUB = "P";
    public static readonly string TYPE_FRAME = "F";

    /****************************
     * 0 プレハブ名
     * 1 X位置
     * 2 Y位置
     * 3 HP
     * 4 HPバー表示
     * 5 スコア
     * 6 撃破時ゲームクリアフラグ
     * 7 撃破時遷移先フレーム
     * 8 撃破時生成プレハブ    
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
            string[] prefubDatas = datas[2].Split(':');
            result.PrefubName = prefubDatas[0];
            result.PointX = float.Parse(prefubDatas[1]);
            result.PointY = float.Parse(prefubDatas[2]);

            // HP
            if (!string.IsNullOrEmpty(prefubDatas[3]))
            {
                result.Hp = int.Parse(prefubDatas[3]);
            }

            // HPバーを表示するかどうか
            if ("1".Equals(prefubDatas[4]))
            {
                result.IsDispHP = true;
            }

            // スコア
            if (!string.IsNullOrEmpty(prefubDatas[5]))
            {
                result.Score = int.Parse(prefubDatas[5]);
            }

            // 撃破時ゲームクリアフラグ
            if ("1".Equals(prefubDatas[6]))
            {
                result.FlgGameClear = true;
            }

            // 撃破時ジャンプ先フレーム
            if (!string.IsNullOrEmpty(prefubDatas[7]))
            {
                result.JumpFrame = long.Parse(prefubDatas[7]);
            }

            // 撃破時生成プレハブ名
            if (!string.IsNullOrEmpty(prefubDatas[8]))
            {
                result.GeneratePrefubName = prefubDatas[8];
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
