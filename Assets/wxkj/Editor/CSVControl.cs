using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Data;

public class CSVControl
{
    public static DataTable ReadData(string path,bool isReadFirst = false)
    {
        FileStream fs = new FileStream(@path, FileMode.Open, FileAccess.Read);
        StreamReader sr = new StreamReader(fs, System.Text.Encoding.Default);

        DataTable data = new DataTable();
        FileInfo info = new FileInfo(@path);
        data.TableName = info.Name.Remove(info.Name.LastIndexOf("."));

        string strline = "";
        string[] arrData = null;
        int count = 0;
        bool IsFirst = true;
        while ((strline = sr.ReadLine()) != null)
        {
            arrData = strline.Split(',');
            count = arrData.Length;

            if (IsFirst)
            {
                IsFirst = false;
                for (int i = 0; i < count; ++i)
                {
                    DataColumn dc = new DataColumn(arrData[i]);
                    data.Columns.Add(dc);
                }

                if (!isReadFirst) continue;
            }

            DataRow dr = data.NewRow();
            for (int i = 0; i < count; ++i)
            {
                dr[i] = arrData[i];
            }

            data.Rows.Add(dr);
        }

        sr.Close();
        fs.Close();
        
        return data;
    }

    public static void SaveData(DataTable dt,string path) 
    {
        FileInfo fi = new FileInfo(path);
        if (!fi.Directory.Exists)
        {
            fi.Directory.Create();
        }

        FileStream fs = new FileStream(path, System.IO.FileMode.Create, System.IO.FileAccess.Write);
        StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.Default);

        string strData = "";
        for (int i = 0; i < dt.Columns.Count; ++i)
        {
            strData += dt.Columns[i].ToString() + (i < dt.Columns.Count - 1 ? "," : "");
        }
        sw.WriteLine(strData);

        for (int i = 0; i < dt.Rows.Count; ++i)
        {
            strData = "";
            for (int j = 0; j < dt.Columns.Count; ++j) 
            {
                strData += dt.Rows[i][j].ToString() + (j < dt.Columns.Count - 1 ? "," : "");
            }

            sw.WriteLine(strData);
        }

        sw.Close();
        fs.Close();
    }
}
