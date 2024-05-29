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

    public override void OnDrag(PointerEventData eventData)
    {
        if (forParent)
        {
            parentScrollRect.OnDrag(eventData);
        }
        else 
            base.OnDrag(eventData);
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        if (forParent)
        {
            parentScrollRect.OnEndDrag(eventData);
        }
        else 
            base.OnEndDrag(eventData);
    }
}
