using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class BinaryDataReader
{

    public static List<string> GetValuesByHeader(string fileName, string header, string sector)
    {
        string binaryFilePath = Path.Combine(Application.persistentDataPath, $"{fileName}.bin");

        using (var stream = new FileStream(binaryFilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            List<string[]> data = (List<string[]>)formatter.Deserialize(stream);

            // 헤더의 인덱스를 찾음
            int headerIndex = System.Array.IndexOf(data[0], header);
            if (headerIndex == -1)
            {
                Debug.LogError($"Header '{header}' not found.");
                return null;
            }

            // sector 값을 가진 행의 인덱스 리스트 찾기
            List<int> sectorIndices = new List<int>();
            for (int i = 1; i < data.Count; i++) // 첫 번째 행은 헤더이므로 제외
            {
                if (System.Array.IndexOf(data[i], sector) != -1)
                {
                    sectorIndices.Add(i);
                }
            }

            // 헤더와 인자값이 일치하는 값을 수집
            List<string> values = new List<string>();
            foreach (int index in sectorIndices)
            {
                values.Add(data[index][headerIndex]);
            }

            return values;
        }
    }

    //위도 경도 찾기
    public static List<(string, string)> GetLatLon(string fileName, string conditionHeader, string conditionValue)
    {
        string binaryFilePath = Path.Combine(Application.persistentDataPath, $"{fileName}.bin");

        using (var stream = new FileStream(binaryFilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            List<string[]> data = (List<string[]>)formatter.Deserialize(stream);

            // 헤더의 인덱스를 찾음
            int conditionHeaderIndex = System.Array.IndexOf(data[0], conditionHeader);
            int latitudeHeaderIndex = System.Array.IndexOf(data[0], "위도");
            int longitudeHeaderIndex = System.Array.IndexOf(data[0], "경도");

            if (conditionHeaderIndex == -1 || latitudeHeaderIndex == -1 || longitudeHeaderIndex == -1)
            {
                Debug.LogError($"Header '{conditionHeader}', '위도', or '경도' not found.");
                return null;
            }

            // 조건에 맞는 위도와 경도 값을 수집
            List<(string, string)> values = new List<(string, string)>();
            for (int i = 1; i < data.Count; i++) // 첫 번째 행은 헤더이므로 제외
            {
                if (data[i][conditionHeaderIndex] == conditionValue)
                {
                    values.Add((data[i][latitudeHeaderIndex], data[i][longitudeHeaderIndex]));
                }
            }
            return values;
        }
    }
}
