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
    float[] pos = new float[SIZE];//value 값 저장공간
    float distance, targetPos;//pos사이 간격

    void Start()
    {
        // 거리에 따라 0~1인 pos대입
        distance = 1f / (SIZE - 1);

        for (int i = 0; i < SIZE; i++)
            pos[i] = distance * i;
    }

    void Update()
    {
        //탭슬라이더의 값을 스크롤바와 같게 만듦.
        if (tabSlider != null)
            tabSlider.value = scrollbar.value;

            scrollbar.value = Mathf.Lerp(scrollbar.value, targetPos, 0.1f);
    }

    //버튼 클릭시 패널 위치 옮겨줌
    public void TabClick(int n)
    {
        targetPos = pos[n];
    }
}
