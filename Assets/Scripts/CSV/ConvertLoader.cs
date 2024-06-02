using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class ConvertLoader : MonoBehaviour
{
    public GameObject loadingText;

    void Start()
    {
        StartCoroutine(ConvertFilesCoroutine());
    }

    private IEnumerator ConvertFilesCoroutine()
    {

        Debug.Log("Panel on");
        // "�����͸� �ҷ����� ���Դϴ�." �ؽ�Ʈ ǥ��
        loadingText.gameObject.SetActive(true);

        // ��� CSV ������ ��ȯ
        foreach (var fileName in Managers.Data.ArrayRegion)
        {
            yield return StartCoroutine(ConvertCsvToBinaryCoroutine(fileName));
        }

        // ��ȯ �Ϸ� �� �ؽ�Ʈ ����
        loadingText.gameObject.SetActive(false);
        Debug.Log("Panel off");
    }

    //���̳ʸ����Ϸ� ��ȯ�ϴ� �ڷ�ƾ
    private IEnumerator ConvertCsvToBinaryCoroutine(string fileName)
    {
        // CSV ���� ��� ����
        string csvFilePath = Path.Combine(Application.dataPath, "StoreData", $"{fileName}.csv");

        // ���̳ʸ� ���� ��� ����
        string binaryFilePath = Path.Combine(Application.persistentDataPath, $"{fileName}.bin");

        List<string[]> data = new List<string[]>();

        // CSV ���� �б�
        using (var reader = new StreamReader(csvFilePath))
        {
            string[] headers = reader.ReadLine().Split(','); // ù �ٿ��� ��� �б�
            data.Add(headers);

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(',');
                data.Add(values);

                // ���⼭ ����Ͽ� �������� ���� �۾��ϵ��� �� (ū ������ ���)
                yield return null;
            }
        }

        // ���̳ʸ� ���Ϸ� ����
        using (var stream = new FileStream(binaryFilePath, FileMode.Create, FileAccess.Write, FileShare.None))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, data);
        }

        yield return null;
    }
}
