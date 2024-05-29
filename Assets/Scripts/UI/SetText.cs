using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SetText : MonoBehaviour
{
    public Button[] button;
    public Image[] image;
    public Sprite[] sprite;
    public TextMeshProUGUI[] btnNum;
    public TextMeshProUGUI[] SectorName;
    public TextMeshProUGUI[] Count;

    private void Start()
    {
        SettingText();
    }
    public void SettingText()
    {
        for (int i = 0; i < button.Length; i++)
        {
            btnNum[i].text = (i+1).ToString();
            SectorName[i].text = Managers.Data.ArraySector[i];
            image[i].sprite = sprite[i];
        }
    }
}
