/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading this package
    I hope you find it useful in your projects
    If you have any questions let me know
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler {

    public GameObject inblock;
    //public string inblock;
    public void OnDrop(PointerEventData eventData) {
        Debug.Log("OnDrop");
        if (eventData.pointerDrag != null) {
            //inblock = eventData.pointerDrag.GetComponent<InspectorNameAttribute>().displayName;
            Vector2 offs = eventData.pointerDrag.GetComponent<DragDrop>().v2orig;
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition+offs;
            inblock = eventData.pointerDrag.GetComponent<RectTransform>().gameObject;
        }
    }

}
