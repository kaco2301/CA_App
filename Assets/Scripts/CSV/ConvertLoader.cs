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
        // "데이터를 불러오는 중입니다." 텍스트 표시
        loadingText.gameObject.SetActive(true);

        // 모든 CSV 파일을 변환
        foreach (var fileName in Managers.Data.ArrayRegion)
        {
            yield return StartCoroutine(ConvertCsvToBinaryCoroutine(fileName));
        }

        // 변환 완료 후 텍스트 숨김
        loadingText.gameObject.SetActive(false);
        Debug.Log("Panel off");
    }

    //바이너리파일로 변환하는 코루틴
    private IEnumerator ConvertCsvToBinaryCoroutine(string fileName)
    {
        // CSV 파일 경로 설정
        string csvFilePath = Path.Combine(Application.dataPath, "StoreData", $"{fileName}.csv");

        // 바이너리 파일 경로 설정
        string binaryFilePath = Path.Combine(Application.persistentDataPath, $"{fileName}.bin");

        List<string[]> data = new List<string[]>();

        // CSV 파일 읽기
        using (var reader = new StreamReader(csvFilePath))
        {
            string[] headers = reader.ReadLine().Split(','); // 첫 줄에서 헤더 읽기
            data.Add(headers);

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(',');
                data.Add(values);

                // 여기서 대기하여 프레임을 나눠 작업하도록 함 (큰 파일의 경우)
                yield return null;
            }
        }

        // 바이너리 파일로 저장
        using (var stream = new FileStream(binaryFilePath, FileMode.Create, FileAccess.Write, FileShare.None))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, data);
        }

        yield return null;
    }
}
