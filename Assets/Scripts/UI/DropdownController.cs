using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO;

public class DropdownController : MonoBehaviour
{
    [Header("DropDown")]
    public TMP_Dropdown dropdownRegion;
    public TMP_Dropdown dropdownSector;
    public string[] arrayRegion = new string[15] { "강원", "경남", "경북", "광주", "대구", "대전", "부산", "세종", "울산", "인천", "전남", "전북", "제주", "충남", "충북" };//지역
    public string[] arrayHeader = new string[5] { "상호명", "상권업종대분류명", "상권업종중분류명", "상권업종소분류명", "표준산업분류명" };//헤더
    public string[] arraySector = new string[10] { "숙박", "소매", "수리·개인", "예술·스포츠", "음식", "부동산", "과학·기술", "교육", "시설관리·임대", "보건의료" };//업종

    string regionName;
    string sectorName;

    public void SearchStore(string fileName, string headerName)
    {
        string contents;

        List<Dictionary<string, object>> data_Dialog = CSVReader.Read(fileName);

        for (int i = 0; i < data_Dialog.Count; i++)
        {
            contents = data_Dialog[i][headerName].ToString();
            Debug.Log(contents);
        }
    }

    public void OnButtonClicked()
    {
        SearchStore(regionName, sectorName);
    }

    private void Start()
    {
        SetDropdownOptionsFromArray(dropdownRegion, arrayRegion);
        SetDropdownOptionsFromArray(dropdownSector, arraySector);
    }

    public void SetDropdownOptionsFromArray(TMP_Dropdown dropdown, string[] optionArray)
    {
        // 옵션 리스트를 생성합니다.
        List<TMP_Dropdown.OptionData> optionList = new List<TMP_Dropdown.OptionData>();

        // 배열의 각 요소를 순회하여 옵션 리스트에 추가합니다.
        foreach (string str in optionArray)
        {
            optionList.Add(new TMP_Dropdown.OptionData(str));
        }

        // 드롭다운의 현재 옵션을 모두 제거합니다.
        dropdown.ClearOptions();

        // 드롭다운에 새 옵션 리스트를 추가합니다.
        dropdown.AddOptions(optionList);
    }

    public void SyncRegionName()
    {
        regionName = dropdownRegion.options[dropdownRegion.value].text;
        Debug.Log("선택된 파일이름" + regionName);
    }

    public void SyncSectorName()
    {
        sectorName = dropdownSector.options[dropdownSector.value].text;
        Debug.Log("선택된 헤더이름" + sectorName);
    }



}
