using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

//CSV������ ���̳ʸ� ���Ϸ� ��ȯ�մϴ�. 
public static class CSVToBinaryConverter
{
    public static void ConvertCSVToBinary(string fileName)
    {
        // CSV ���� ��� ����
        string csvFilePath = Path.Combine(Application.dataPath, "StoreData", $"{fileName}.csv");

        // ���̳ʸ� ���� ��� ����
        string binaryFilePath = Path.Combine(Application.persistentDataPath, $"{fileName}.bin");

        List<string[]> data = new List<string[]>();

        // CSV ���� �б�
        using (var reader = new StreamReader(csvFilePath))
        {
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(',');
                data.Add(values);
            }
        }

        // ���̳ʸ� ���Ϸ� ����
        using (var stream = new FileStream(binaryFilePath, FileMode.Create, FileAccess.Write, FileShare.None))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, data);
        }
    }
}


