using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JoyScript : ScrollRect
{
    public GameObject player;
    // Start is called before the first frame update
    protected float radius = 0f;

    protected override void Start()
    {
        player = GameObject.Find("Dragon").gameObject;
        base.Start();
        radius = (transform as RectTransform).sizeDelta.x * 0.45f;
    }

    // Update is called once per frame
    public override void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);
        var contentPosition = this.content.anchoredPosition;
        if (contentPosition.magnitude > radius)
        {
            contentPosition = contentPosition.normalized * radius;
            SetContentAnchoredPosition(contentPosition);
        }
        //if (contentPosition.x < -1)
        //    player.GetComponent<PlayerMovement>().inputX = -1;
        //else if (contentPosition.x > 1)
        //    player.GetComponent<PlayerMovement>().inputX = 1;
        //else
        //    player.GetComponent<PlayerMovement>().inputX = 0;
    }
    //public override void OnEndDrag(PointerEventData eventData)
    //{
    //    base.OnEndDrag(eventData);
    //    player.GetComponent<PlayerMovement>().inputX = 0;
    //}
}
