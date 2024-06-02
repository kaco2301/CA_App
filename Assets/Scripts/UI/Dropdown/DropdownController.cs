using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public class DropdownController : MonoBehaviour
{

    [Header("DropDown")]
    public TMP_Dropdown dropdownRegion;
    public TMP_Dropdown dropdownHeader;
    public TMP_Dropdown dropdownSector;
    public TMP_Dropdown dropdownClass;

    string fileName = "";
    string headerName = "";
    string sectorName = "";

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        var dropdownOptionsMapping = new Dictionary<TMP_Dropdown, string[]>
        {
            { dropdownRegion, Managers.Data.ArrayRegion },
            { dropdownHeader, Managers.Data.ArrayHeader },
            { dropdownSector, Managers.Data.ArraySector },
            { dropdownClass, Managers.Data.ArrayClass }
        };

        // 각 드롭다운을 초기화
        foreach (var entry in dropdownOptionsMapping)
        {
            SetDropdownOptionsFromArray(entry.Key, entry.Value);
        }
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

    #region 동기화
    public void SyncRegionName()
    {
        fileName = dropdownRegion.options[dropdownRegion.value].text;
    }

    public void SyncHeaderName()
    {
        headerName = dropdownHeader.options[dropdownHeader.value].text;
    }

    public void SyncSectorName()
    {
        sectorName = dropdownSector.options[dropdownSector.value].text;
    }
    #endregion

    public void GetValueData()//헤더의 밸류를 가져온다.
    {
        SyncRegionName();
        SyncHeaderName();
        SyncSectorName();
        CSVToBinaryConverter.ConvertCSVToBinary(fileName);//파일이름에 해당하는 csv파일을 바이너리로 변환
        List<string> values = BinaryDataReader.GetValuesByHeader(fileName, headerName, sectorName); //파일이름, 헤더이름, 인자이름을 받아서 그에 해당하는 값을 리턴
        foreach (var value in values)
        {
            Debug.Log(value);
        }
    }

    public void GetLatLonData()
    {
        List<(string, string)> values = BinaryDataReader.GetLatLon(fileName, headerName, sectorName);

        foreach (var (latitude, longitude) in values)
        {
            Debug.Log($"위도: {latitude}, 경도: {longitude}");
        }
    }

    public void OnButtonClicked()
    {
        //GetValueData();
        GetLatLonData();
    }

    //바이너리 파일의 헤더를 드롭다운에 추가하고, 해당 헤더에 맞는 인자들 중 중복되지 않게 드롭다운에 추가하는것이 sector
    //검색 버튼을 누르면 선택한 드롭다운에 해당하는 위도, 경도 정보를 return해와서 Map스크립트에 전달하여 그 위치를 띄운다.
    //어플 시작 시 데이터를 불러오는 동안 로드이미지
}
