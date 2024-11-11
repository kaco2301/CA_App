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

            // ����� �ε����� ã��
            int headerIndex = System.Array.IndexOf(data[0], header);
            if (headerIndex == -1)
            {
                Debug.LogError($"Header '{header}' not found.");
                return null;
            }

            // sector ���� ���� ���� �ε��� ����Ʈ ã��
            List<int> sectorIndices = new List<int>();
            for (int i = 1; i < data.Count; i++) // ù ��° ���� ����̹Ƿ� ����
            {
                if (System.Array.IndexOf(data[i], sector) != -1)
                {
                    sectorIndices.Add(i);
                }
            }

            // ����� ���ڰ��� ��ġ�ϴ� ���� ����
            List<string> values = new List<string>();
            foreach (int index in sectorIndices)
            {
                values.Add(data[index][headerIndex]);
            }

            return values;
        }
    }

    //���� �浵 ã��
    public static List<(string, string)> GetLatLon(string fileName, string conditionHeader, string conditionValue)
    {
        string binaryFilePath = Path.Combine(Application.persistentDataPath, $"{fileName}.bin");

        using (var stream = new FileStream(binaryFilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            List<string[]> data = (List<string[]>)formatter.Deserialize(stream);

            // ����� �ε����� ã��
            int conditionHeaderIndex = System.Array.IndexOf(data[0], conditionHeader);
            int latitudeHeaderIndex = System.Array.IndexOf(data[0], "����");
            int longitudeHeaderIndex = System.Array.IndexOf(data[0], "�浵");

            if (conditionHeaderIndex == -1 || latitudeHeaderIndex == -1 || longitudeHeaderIndex == -1)
            {
                Debug.LogError($"Header '{conditionHeader}', '����', or '�浵' not found.");
                return null;
            }

            // ���ǿ� �´� ������ �浵 ���� ����
            List<(string, string)> values = new List<(string, string)>();
            for (int i = 1; i < data.Count; i++) // ù ��° ���� ����̹Ƿ� ����
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
