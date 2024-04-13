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

    private string[] ArraySector = new string[10] { "숙박", "소매", "수리·개인", "예술·스포츠", "음식", "부동산", "과학·기술", "교육", "시설관리·임대", "보건의료" };

    private void Start()
    {
        SettingText();
    }
    public void SettingText()
    {
        for (int i = 0; i < button.Length; i++)
        {
            btnNum[i].text = (i+1).ToString();
            SectorName[i].text = ArraySector[i];
            image[i].sprite = sprite[i];
        }
    }
}
