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

    public static DataManager Data { get { return Instance._data; } }

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

        //모든 csv파일을 converting
        
    }

    private void Awake()
    {
        Init();
    }

}
