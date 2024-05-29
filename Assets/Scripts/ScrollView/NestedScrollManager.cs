using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NestedScrollManager : MonoBehaviour
{
    public Scrollbar scrollbar;
    public Transform contentTr;
    public Slider tabSlider;

    const int SIZE = 4;
    float[] pos = new float[SIZE];//value �� �������
    float distance, targetPos;//pos���� ����

    void Start()
    {
        // �Ÿ��� ���� 0~1�� pos����
        distance = 1f / (SIZE - 1);

        for (int i = 0; i < SIZE; i++)
            pos[i] = distance * i;
    }

    void Update()
    {
        //�ǽ����̴��� ���� ��ũ�ѹٿ� ���� ����.
        if (tabSlider != null)
            tabSlider.value = scrollbar.value;

            scrollbar.value = Mathf.Lerp(scrollbar.value, targetPos, 0.1f);
    }

    //��ư Ŭ���� �г� ��ġ �Ű���
    public void TabClick(int n)
    {
        targetPos = pos[n];
    }
}
