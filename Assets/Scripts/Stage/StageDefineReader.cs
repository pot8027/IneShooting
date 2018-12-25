using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System;

public class StageDefineReader
{
    /// <summary>
    /// ステージ定義辞書
    /// </summary>
    private Dictionary<string, StageDefine> _stageDefineDictionary = new Dictionary<string, StageDefine>();

    /// <summary>
    /// ステージ定義を読み込む
    /// </summary>
    /// <param name="textName">Text name.</param>
    public void Load(string textName)
    {
        try
        {
            List<string> readDataList = FileUtil.LoadCSVToList("Stage/" + textName);
            foreach (string line in readDataList)
            {
                if (string.IsNullOrEmpty(line))
                {
                    continue;
                }

                if (IsComment(line))
                {
                    continue;
                }

                var values = line.Split(',');
                if (values.Length < 2)
                {
                    continue;
                }

                // ID,、プレハブ名、X座標、Y座標
                string id = values[0].Trim();
                StageDefine stageDefine = new StageDefine();
                stageDefine.PrefubName = values[1].Trim();
                stageDefine.PointX = float.Parse(values[2].Trim());
                stageDefine.PointY = float.Parse(values[3].Trim());
                _stageDefineDictionary.Add(id, stageDefine);
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
            LogUtil.WriteLog(e.ToString());
        }
    }

    /// <summary>
    /// 指定IDのステージ定義を取得
    /// </summary>
    /// <returns>The stage data by identifier.</returns>
    /// <param name="id">Identifier.</param>
    public StageDefine GetStageDataByID(string id)
    {
        return _stageDefineDictionary[id];
    }

    private bool IsComment(string line)
    {
        string head = line.Substring(0, 1);
        if ("#".Equals(head))
        {
            return true;
        }

        return false;
    }
}
