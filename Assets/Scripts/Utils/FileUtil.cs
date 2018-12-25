using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text;

public class FileUtil
{
    /// <summary>
    /// Resourceフォルダ配下のCSVファイルを読み込んでリストで返す
    /// </summary>
    /// <returns>The CSVT o list.</returns>
    /// <param name="path">Path.</param>
    public static List<string> LoadCSVToList(string path)
    {
        List<string> result = new List<string>();

        StringReader sr = null;

        try
        {
            TextAsset textAsset = Resources.Load(path) as TextAsset;
            string stringData = string.Empty;
            if (string.IsNullOrEmpty(textAsset.text))
            {
                stringData = Encoding.UTF8.GetString(textAsset.bytes);
            }
            else
            {
                stringData = textAsset.text;
            }

            sr = new StringReader(stringData);
            while (sr.Peek() > -1)
            {
                result.Add(sr.ReadLine());
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
            LogUtil.WriteLog(e.ToString());
        }
        finally
        {
            if (sr != null)
            {
                sr.Close();
            }
        }

        return result;
    }
}
