using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DropdownController : MonoBehaviour
{
    [Header("DropDown")]
    public TMP_Dropdown dropdownRegion;
    public TMP_Dropdown dropdownHeader;
    public TMP_Dropdown dropdownSector;
    public TMP_Dropdown dropdownClass;

    string regionName = "선택안함";
    string headerName = "선택안함";
    string sectorName = "선택안함";

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        SetDropdownOptionsFromArray(dropdownRegion, Managers.Data.ArrayRegion);
        SetDropdownOptionsFromArray(dropdownHeader, Managers.Data.ArrayHeader);
        SetDropdownOptionsFromArray(dropdownSector, Managers.Data.ArraySector);
        SetDropdownOptionsFromArray(dropdownClass, Managers.Data.ArrayClass);

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
        Debug.Log("선택된 파일이름 : " + regionName);
    }

    public void SyncHeaderName()
    {
        headerName = dropdownHeader.options[dropdownHeader.value].text;
        Debug.Log("선택된 헤더이름 : " + headerName);
    }

    public void SyncSectorName()
    {
        sectorName = dropdownSector.options[dropdownSector.value].text;
        Debug.Log("선택된 상권이름 : " + sectorName);
    }

    public void OnSearchButtonClicked()
    {
        List<Dictionary<string, object>> csvData = Managers.Instance.ReadCSV(regionName, headerName);

        foreach (var row in csvData)
        {
            foreach (var col in row)
            {
                Debug.Log($"{col.Key}: {col.Value}");
            }
        }
    }

}
