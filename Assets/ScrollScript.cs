using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ScrollScript : ScrollRect
{
    bool forParent;
    NestedScrollManager NM;
    ScrollRect parentScrollRect;

    protected override void Start()
    {
        NM = GameObject.FindWithTag("NestedScrollManager").GetComponent<NestedScrollManager>();
        parentScrollRect = GameObject.FindWithTag("NestedScrollManager").GetComponent<ScrollRect>();
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        //드래그 순간 수평이동이 크면 부모드래그, 수직이동 크면 자식드래그
        forParent = Mathf.Abs(eventData.delta.x) > Mathf.Abs(eventData.delta.y);
        
        if (forParent)
        {
            NM.OnBeginDrag(eventData);
            parentScrollRect.OnBeginDrag(eventData);
        }
        else base.OnBeginDrag(eventData);
    }

    public override void OnDrag(PointerEventData eventData)
    {
        if (forParent)
        {
            NM.OnDrag(eventData);
            parentScrollRect.OnDrag(eventData);
        }
        else base.OnDrag(eventData);
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        if (forParent)
        {
            NM.OnEndDrag(eventData);
            parentScrollRect.OnEndDrag(eventData);
        }
        else base.OnEndDrag(eventData);
    }
}
