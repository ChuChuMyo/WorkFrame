using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//범용적인 기능은 아님
public class UI_EventHandler : MonoBehaviour, IDragHandler,IPointerClickHandler//,IBeginDragHandler
{
    //public Action<PointerEventData> OnBeginDragHandler = null;
    public Action<PointerEventData> OnDragHandler = null;
    public Action<PointerEventData> OnClickHandler = null;
    /*public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
        if(OnBeginDragHandler != null)
            OnBeginDragHandler.Invoke(eventData);
            
        
    }*/

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");
        if(OnDragHandler != null)
            OnDragHandler.Invoke(eventData);
        //transform.position = eventData.position;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(OnClickHandler != null)
           OnClickHandler.Invoke(eventData);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
