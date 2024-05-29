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
    float[] pos = new float[SIZE];//value �� �������
    float distance, curPos, targetPos;//pos���� ����
    bool isDrag = false;


    private void Start()
    {
        // �Ÿ��� ���� 0~1�� pos����
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
            //<-���� ������ ��ǥ�� �ϳ� ����
            if (eventData.delta.x > 18 && curPos - distance >= 0)
            {
                targetPos = curPos - distance;
            }

            //->���� ������ ��ǥ�� �ϳ� ����
            else if (eventData.delta.x < -18 && curPos + distance <= 1.01f)
            {
                targetPos = curPos + distance;
            }
        }
    }

    float SetPos()
    {
        // �г��� ���ݰŸ��� �������� ����� ��ġ�� ��ȯ�ؼ� �� �гη� ���ư�
        for (int i = 0; i < SIZE; i++)
            if (adScrollbar.value < pos[i] + distance * 0.5f && adScrollbar.value > pos[i] - distance * 0.5f)
            {
                return pos[i];
            }
        return 0;
    }

    //������ ��Ŭ �̹����� Ȱ��ȭ�гο� �°� �̵������ش�
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
            //PASSTIME���� ���� ����� �Ѿ��.
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
