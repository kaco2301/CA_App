using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

//CSV파일을 바이너리 파일로 변환합니다. 
public static class CSVToBinaryConverter
{
    public static void ConvertCSVToBinary(string fileName)
    {
        // CSV 파일 경로 설정
        string csvFilePath = Path.Combine(Application.dataPath, "StoreData", $"{fileName}.csv");

        // 바이너리 파일 경로 설정
        string binaryFilePath = Path.Combine(Application.persistentDataPath, $"{fileName}.bin");

        List<string[]> data = new List<string[]>();

        // CSV 파일 읽기
        using (var reader = new StreamReader(csvFilePath))
        {
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(',');
                data.Add(values);
            }
        }

        // 바이너리 파일로 저장
        using (var stream = new FileStream(binaryFilePath, FileMode.Create, FileAccess.Write, FileShare.None))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, data);
        }
    }
}


