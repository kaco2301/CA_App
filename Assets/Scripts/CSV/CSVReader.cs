using System;
using System.Collections.Generic;
using System.IO; // StreamReader 사용을 위해 필요
using UnityEngine;

public class CSVReader
{
    static char[] TRIM_CHARS = { '\"' };

    public static List<Dictionary<string, object>> Read(string file)
    {
        var list = new List<Dictionary<string, object>>();
        string path = Path.Combine(Application.dataPath, "StoreData", file + ".csv").Replace("\\","/"); // 파일 경로 조합

        //경로에 파일이 존재하지 않는 경우
        if (!File.Exists(path))
        {
            Debug.Log(path);
            Debug.LogError("CSVReader: Failed to Load File");
            return list;
        }

        //스트림리더사용해서 파일 읽어오기
        using (StreamReader reader = new StreamReader(path))
        {
            string line = reader.ReadLine();
            if (line == null) return list;

            string[] header = line.Split(',');

            while ((line = reader.ReadLine()) != null)
            {
                string[] values = line.Split(',');
                if (values.Length == 0 || values[0] == "") continue;

                var entry = new Dictionary<string, object>();
                for (var j = 0; j < header.Length && j < values.Length; j++)
                {
                    string value = values[j].Trim(TRIM_CHARS).Replace("\\", "");
                    object finalvalue = value;
                    int n;
                    float f;
                    if (int.TryParse(value, out n))
                    {
                        finalvalue = n;
                    }
                    else if (float.TryParse(value, out f))
                    {
                        finalvalue = f;
                    }
                    entry[header[j]] = finalvalue;
                }
                list.Add(entry);
            }
        }
        return list;
    }
}