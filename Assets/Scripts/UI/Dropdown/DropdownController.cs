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

        // �� ��Ӵٿ��� �ʱ�ȭ
        foreach (var entry in dropdownOptionsMapping)
        {
            SetDropdownOptionsFromArray(entry.Key, entry.Value);
        }
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

    #region ����ȭ
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

    public void GetValueData()//����� ����� �����´�.
    {
        SyncRegionName();
        SyncHeaderName();
        SyncSectorName();
        CSVToBinaryConverter.ConvertCSVToBinary(fileName);//�����̸��� �ش��ϴ� csv������ ���̳ʸ��� ��ȯ
        List<string> values = BinaryDataReader.GetValuesByHeader(fileName, headerName, sectorName); //�����̸�, ����̸�, �����̸��� �޾Ƽ� �׿� �ش��ϴ� ���� ����
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
            Debug.Log($"����: {latitude}, �浵: {longitude}");
        }
    }

    public void OnButtonClicked()
    {
        //GetValueData();
        GetLatLonData();
    }

    //���̳ʸ� ������ ����� ��Ӵٿ �߰��ϰ�, �ش� ����� �´� ���ڵ� �� �ߺ����� �ʰ� ��Ӵٿ �߰��ϴ°��� sector
    //�˻� ��ư�� ������ ������ ��Ӵٿ �ش��ϴ� ����, �浵 ������ return�ؿͼ� Map��ũ��Ʈ�� �����Ͽ� �� ��ġ�� ����.
    //���� ���� �� �����͸� �ҷ����� ���� �ε��̹���
}
