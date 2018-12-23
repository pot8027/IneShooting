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
        StreamReader sr = null;

        try
        {
            string path = Application.dataPath + "/Resources/Text/" + textName;
            sr = new StreamReader(path, Encoding.GetEncoding("UTF-8"));
            while (!sr.EndOfStream)
            {
                var line = sr.ReadLine();
                var values = line.Split(',');

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
    /// 指定IDのステージ定義を取得
    /// </summary>
    /// <returns>The stage data by identifier.</returns>
    /// <param name="id">Identifier.</param>
    public StageDefine GetStageDataByID(string id)
    {
        return _stageDefineDictionary[id];
    }
}
