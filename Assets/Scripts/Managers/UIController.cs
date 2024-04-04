using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO;


public class UIController : MonoBehaviour
{
    [Header("DropDown")]
    public TMP_Dropdown dropdown;
    public string[] arrayRegion = new string[15] { "강원", "경남", "경북", "광주", "대구", "대전", "부산", "세종", "울산", "인천", "전남", "전북", "제주", "충남", "충북" };
    public string[] arraySector = new string[2] { "ㅁㅁ","ㄴㄴ"};

    List<TMP_Dropdown.OptionData> optionList = new List<TMP_Dropdown.OptionData>();

    public string readFileName = "";
    public string readHeaderName = "";

    public void SearchStore()
    {
        List<Dictionary<string, object>> data_Dialog = CSVReader.Read(readFileName);

        for (int i = 0; i < data_Dialog.Count; i++)
        {
            Debug.Log(data_Dialog[i][readHeaderName].ToString());
        }
    }

    

    private void SetDropdownOptions()
    {
        if (dropdown != null)
        {
            dropdown.ClearOptions();



            foreach (string str in arrayRegion)
            {
                optionList.Add(new TMP_Dropdown.OptionData(str));
            }

            dropdown.AddOptions(optionList);

            dropdown.value = 0;

            SearchStore();
        } 
    }

    public  void InitializeDropdownListener()
    {
        int index = dropdown.value;
        readFileName = dropdown.options[index].text;
        Debug.Log("선택된 지역" + readFileName);
        SetDropdownOptions();
    }

       
}
