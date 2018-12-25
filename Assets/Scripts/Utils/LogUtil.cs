using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class LogUtil
{ 
    public static void WriteLog(string text)
    {
        string dirPath = Directory.GetCurrentDirectory();
        string date = DateTime.Now.ToString("yyyyMMdd");
        string path = dirPath + "/" + date + ".log";
        StreamWriter sr = null;

        try
        {
            sr = new StreamWriter(path, false, System.Text.Encoding.UTF8);

            string dt = DateTime.Now.ToString("[yyyy/MM/dd HH:mm:ss");
            sr.WriteLine(dt + "¥t" + text);
        }
        finally
        {
            if (sr != null)
            {
                sr.Close();
            }
        }
    }
}
