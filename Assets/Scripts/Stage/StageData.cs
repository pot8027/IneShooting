using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageData
{
    public static readonly string TYPE_PREFUB = "P";
    public static readonly string TYPE_FRAME = "F";

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
    /// 遷移先フレーム
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
        result.DataType = datas[1];

        // プレハブ用データ作成
        if (TYPE_PREFUB.Equals(result.DataType))
        {
            string[] prefubDatas = datas[2].Split(':');
            result.PrefubName = prefubDatas[0];
            result.PointX = float.Parse(prefubDatas[1]);
            result.PointY = float.Parse(prefubDatas[2]);
        }

        // フレーム用データ作成
        else if (TYPE_FRAME.Equals(result.DataType))
        {
            result.NextFrame = long.Parse(datas[2]);
        }

        return result;
    }
}
