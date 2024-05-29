using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpManager : MonoBehaviour
{
    //�г� ���
    public GameObject[] Panel;

    //�����ϸ� ��� �г� ��Ȱ��ȭ
    private void Start()
    {
        for (int i = 0; i < Panel.Length; i++)
        {
            Panel[i].SetActive(false);
        }
    }

    //��ư�� �Ҵ��� �г� Ȱ��ȭ ��ư
    public void TabClick(int n)
    {
        for (int i = 0; i < Panel.Length; i++)
        {
            Panel[i].SetActive(i == n);
        }
    }  
}
