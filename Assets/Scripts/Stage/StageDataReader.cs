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
        StreamReader sr = null;

        try
        {
            string path = Application.dataPath + "/Resources/Text/" + textName;
            sr = new StreamReader(path, Encoding.GetEncoding("UTF-8"));
            while (!sr.EndOfStream)
            {
                var line = sr.ReadLine();
                var values = line.Split(',');

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
        }
        finally
        {
            if (sr != null)
            {
                sr.Close();
            }
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
}
