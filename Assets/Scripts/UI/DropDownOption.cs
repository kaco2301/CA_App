using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DropDownOption : MonoBehaviour
{
    [SerializeField]
    private TMP_Dropdown dropdown;
    private string[] arrayClass = new string[2] { "지역별 상권 랭킹", "시군구별 상권 랭킹" };

    private void Awake()
    {
        dropdown.ClearOptions();

        List<TMP_Dropdown.OptionData> optionList = new List<TMP_Dropdown.OptionData>();

        foreach(string str in arrayClass)
        {
            optionList.Add(new TMP_Dropdown.OptionData(str));
        }
        dropdown.AddOptions(optionList);

        dropdown.value = 0;
    }
}
