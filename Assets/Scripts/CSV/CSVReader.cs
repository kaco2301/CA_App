using System;
using System.Collections.Generic;
using System.IO; // StreamReader ����� ���� �ʿ�
using UnityEngine;

public class CSVReader
{
    static char[] TRIM_CHARS = { '\"' };

    public static List<Dictionary<string, object>> Read(string file)
    {
        var list = new List<Dictionary<string, object>>();
        string path = Path.Combine(Application.dataPath, "StoreData", file + ".csv").Replace("\\","/"); // ���� ��� ����

        //��ο� ������ �������� �ʴ� ���
        if (!File.Exists(path))
        {
            Debug.Log(path);
            Debug.LogError("CSVReader: Failed to Load File");
            return list;
        }

        //��Ʈ����������ؼ� ���� �о����
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