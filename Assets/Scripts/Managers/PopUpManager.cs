using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpManager : MonoBehaviour
{
    //패널 등록
    public GameObject[] Panel;

    //시작하면 모든 패널 비활성화
    private void Start()
    {
        for (int i = 0; i < Panel.Length; i++)
        {
            Panel[i].SetActive(false);
        }
    }

    //버튼에 할당할 패널 활성화 버튼
    public void TabClick(int n)
    {
        for (int i = 0; i < Panel.Length; i++)
        {
            Panel[i].SetActive(i == n);
        }
    }  
}
