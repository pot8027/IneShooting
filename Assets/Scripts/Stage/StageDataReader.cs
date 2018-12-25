using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System;

public class StageDataReader
{
    /// <summary>
    /// ステージ情報辞書
    /// </summary>
    private Dictionary<float, List<string>> _stageDataDictionary = new Dictionary<float, List<string>>();

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

                // ID、プレハブ名、X座標、Y座標
                float frame = float.Parse(values[0].Trim());
                List<string> prefubList = new List<string>();
                for (int i = 1; i < values.Length; i++)
                {
                    prefubList.Add(values[i].Trim());
                }
                _stageDataDictionary.Add(frame, prefubList);
            }
        }
        catch(Exception e)
        {
            Debug.Log(e.ToString());
            LogUtil.WriteLog(e.ToString());
        }
    }

    /// <summary>
    /// 指定フレームのステージ情報を取得
    /// </summary>
    /// <returns>The stage data by identifier.</returns>
    /// <param name="frame">Frame.</param>
    public List<string> GetStageDataListByFrame(float frame)
    {
        List<string> result = null;
        if (_stageDataDictionary.TryGetValue(frame, out result))
        {
            return result;
        }
        return null;
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
