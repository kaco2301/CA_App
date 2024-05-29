using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Managers : MonoBehaviour
{
    private static Managers instance;
    public static Managers Instance { get { Init(); return instance; } }

    DataManager _data = new DataManager();
    CSVReader _csv = new CSVReader();

    public static DataManager Data { get { return Instance._data; } }
    public static CSVReader CSV { get { return Instance._csv; } }

    static void Init()
    {
        if (instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if (go == null)
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();
            }

            DontDestroyOnLoad(go);
            instance = go.GetComponent<Managers>();
        }
    }

    private void Awake()
    {
        Init();
    }

    //CSV
    public List<Dictionary<string, object>> ReadCSV(string filePath, string fileName)
    {
        string fullPath = Path.Combine(filePath, fileName); // 파일 전체 경로 조합
        return CSVReader.Read(fullPath); // CSVReader를 사용하여 파일 읽기 및 파싱
    }

}
