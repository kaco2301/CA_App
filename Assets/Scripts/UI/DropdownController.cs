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
    public string[] arrayRegion = new string[15] { "����", "�泲", "���", "����", "�뱸", "����", "�λ�", "����", "���", "��õ", "����", "����", "����", "�泲", "���" };//����
    public string[] arrayHeader = new string[5] { "��ȣ��", "��Ǿ�����з���", "��Ǿ����ߺз���", "��Ǿ����Һз���", "ǥ�ػ���з���" };//���
    public string[] arraySector = new string[10] { "����", "�Ҹ�", "����������", "������������", "����", "�ε���", "���С����", "����", "�ü��������Ӵ�", "�����Ƿ�" };//����

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
        Debug.Log("���õ� �����̸�" + regionName);
    }

    public void SyncSectorName()
    {
        sectorName = dropdownSector.options[dropdownSector.value].text;
        Debug.Log("���õ� ����̸�" + sectorName);
    }



}
