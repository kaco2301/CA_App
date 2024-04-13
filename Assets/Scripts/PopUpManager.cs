using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpManager : MonoBehaviour
{
    public GameObject[] Panel;

    private void Start()
    {
        for (int i = 0; i < Panel.Length; i++)
        {
            Panel[i].SetActive(false);
        }
    }
    public void TabClick(int n)
    {
        for (int i = 0; i < Panel.Length; i++)
        {
            Panel[i].SetActive(i == n);
        }
    }  
}
