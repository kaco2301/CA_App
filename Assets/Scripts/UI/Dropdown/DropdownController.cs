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

    string regionName = "���þ���";
    string headerName = "���þ���";
    string sectorName = "���þ���";

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
        // �ɼ� ����Ʈ�� �����մϴ�.
        List<TMP_Dropdown.OptionData> optionList = new List<TMP_Dropdown.OptionData>();

        // �迭�� �� ��Ҹ� ��ȸ�Ͽ� �ɼ� ����Ʈ�� �߰��մϴ�.
        foreach (string str in optionArray)
        {
            optionList.Add(new TMP_Dropdown.OptionData(str));
        }

        // ��Ӵٿ��� ���� �ɼ��� ��� �����մϴ�.
        dropdown.ClearOptions();

        // ��Ӵٿ �� �ɼ� ����Ʈ�� �߰��մϴ�.
        dropdown.AddOptions(optionList);
    }

    public void SyncRegionName()
    {
        regionName = dropdownRegion.options[dropdownRegion.value].text;
        Debug.Log("���õ� �����̸� : " + regionName);
    }

    public void SyncHeaderName()
    {
        headerName = dropdownHeader.options[dropdownHeader.value].text;
        Debug.Log("���õ� ����̸� : " + headerName);
    }

    public void SyncSectorName()
    {
        sectorName = dropdownSector.options[dropdownSector.value].text;
        Debug.Log("���õ� ����̸� : " + sectorName);
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
