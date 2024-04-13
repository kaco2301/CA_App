using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class NestedScrollManager : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Scrollbar scrollbar;
    public Transform contentTr;

    public Transform[] circleContents;

    public Slider tabSlider;

    const int SIZE = 4;
    float[] pos = new float[SIZE];//value 값 저장공간
    float distance, curPos, targetPos;//pos사이 간격
    bool isDrag = false;
    int targetIndex;

    void Start()
    {
        // 거리에 따라 0~1인 pos대입
        distance = 1f / (SIZE - 1);

        for (int i = 0; i < SIZE; i++)
            pos[i] = distance * i;
    }

    float SetPos()
    {
            // 절반거리를 기준으로 가까운 위치를 반환
            for (int i = 0; i < SIZE; i++)
                if (scrollbar.value < pos[i] + distance * 0.5f && scrollbar.value > pos[i] - distance * 0.5f)
                {
                    targetIndex = i;
                    return pos[i];
                }
        return 0;
    }

    void Update()
    {
        if (circleContents != null)
            UpdateCircleContents();

        if (tabSlider != null)
            tabSlider.value = scrollbar.value;

        if (!isDrag)
            scrollbar.value = Mathf.Lerp(scrollbar.value, targetPos, 0.1f);
    }

    void VerticalScrollUp()
    {
        // 목표가 수직스크롤이고, 옆에서 옮겨왔다면 수직스크롤을 맨 위로 올림
        if (contentTr != null)
        {
            for (int i = 0; i < SIZE; i++)
                if (contentTr.GetChild(i).GetComponent<ScrollScript>() && curPos != pos[i] && targetPos == pos[i])
                    contentTr.GetChild(i).GetChild(1).GetComponent<Scrollbar>().value = 1;
        }
        else
            return;
    }

    public void OnBeginDrag(PointerEventData eventData) => curPos = SetPos();

    public void OnDrag(PointerEventData eventData) => isDrag = true;

    public void OnEndDrag(PointerEventData eventData)
    {
        isDrag = false;
        targetPos = SetPos();


        if (curPos == targetPos)
        {
            if (eventData.delta.x > 18 && curPos - distance >= 0)
            {
                --targetIndex;
                targetPos = curPos - distance;
            }

            else if (eventData.delta.x < -18 && curPos + distance <= 1.01f)
            {
                ++targetIndex;
                targetPos = curPos + distance;
            }
        }

        VerticalScrollUp();
    }

    private void UpdateCircleContents()
    {
        if (circleContents.Length == 0)
            return;

        for (int i = 0; i < SIZE; i++)
        {
            circleContents[i].GetComponent<Image>().color = Color.white;

            if (scrollbar.value < pos[i] + distance * 0.5f && scrollbar.value > pos[i] - distance * 0.5f)
            {
                circleContents[i].GetComponent<Image>().color = Color.black;
            }
        }
    }

    public void TabClick(int n)
    {
        targetIndex = n;
        targetPos = pos[n];
    }
}
