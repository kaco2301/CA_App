using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AdScroll : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public float passTime = 3.0f;
    public Scrollbar adScrollbar;

    public Transform[] circleContents;

    const int SIZE = 4;
    float[] pos = new float[SIZE];//value 값 저장공간
    float distance, curPos, targetPos;//pos사이 간격
    bool isDrag = false;


    private void Start()
    {
        // 거리에 따라 0~1인 pos대입
        distance = 1f / (SIZE - 1);

        for (int i = 0; i < SIZE; i++)
            pos[i] = distance * i;

        StartCoroutine(AutoScrollCoroutine());
    }

    private void Update()
    {
        if (circleContents != null)
            UpdateCircleContents();

        if (!isDrag)
            adScrollbar.value = Mathf.Lerp(adScrollbar.value, targetPos, 0.1f);

    }

    public void OnBeginDrag(PointerEventData eventData) => curPos = SetPos();

    public void OnDrag(PointerEventData eventData) => isDrag = true;

    public void OnEndDrag(PointerEventData eventData)
    {
        isDrag = false;
        targetPos = SetPos();


        if (curPos == targetPos)
        {
            //<-으로 가려면 목표가 하나 감소
            if (eventData.delta.x > 18 && curPos - distance >= 0)
            {
                targetPos = curPos - distance;
            }

            //->으로 가려면 목표가 하나 증가
            else if (eventData.delta.x < -18 && curPos + distance <= 1.01f)
            {
                targetPos = curPos + distance;
            }
        }
    }

    float SetPos()
    {
        // 패널의 절반거리를 기준으로 가까운 위치를 반환해서 그 패널로 돌아감
        for (int i = 0; i < SIZE; i++)
            if (adScrollbar.value < pos[i] + distance * 0.5f && adScrollbar.value > pos[i] - distance * 0.5f)
            {
                return pos[i];
            }
        return 0;
    }

    //광고의 서클 이미지를 활성화패널에 맞게 이동시켜준다
    private void UpdateCircleContents()
    {
        if (circleContents.Length == 0)
            return;

        for (int i = 0; i < SIZE; i++)
        {
            circleContents[i].GetComponent<Image>().color = Color.white;

            if (adScrollbar.value < pos[i] + distance * 0.5f && adScrollbar.value > pos[i] - distance * 0.5f)
            {
                circleContents[i].GetComponent<Image>().color = Color.black;
            }
        }
    }

    private IEnumerator AutoScrollCoroutine()
    {
        while (true)
        {
            //PASSTIME마다 다음 광고로 넘어간다.
            yield return new WaitForSeconds(passTime);

            if (!isDrag)
            {
                int currentIndex = Mathf.RoundToInt(adScrollbar.value / distance);
                int nextIndex = (currentIndex + 1) % SIZE;
                targetPos = pos[nextIndex];
            }
        }
    }
}
