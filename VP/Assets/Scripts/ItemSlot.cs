/*
    ------------------- Code Monkey -------------------

    Thank you for downloading this package
    I hope you find it useful in your projects
    If you have any questions let me know
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler, IPointerDownHandler
{
    public GameObject inblock;
    public int x;
    public int y;
    //public string inblock;

    public void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            InBlockCheck();
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");
        if (eventData.pointerDrag != null)
        {
            Vector2 offs = eventData.pointerDrag.GetComponent<DragDrop>().v2orig;
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition + offs;
            inblock = eventData.pointerDrag.GetComponent<RectTransform>().gameObject;
        }
    }

    public void InBlockCheck()
    {
        if (inblock != null)
        {
            float slotx = gameObject.transform.position.x;
            float itemx = inblock.transform.position.x;
            float sloty = gameObject.transform.position.y;
            float itemy = inblock.transform.position.y;
            float diffx = Mathf.Abs(slotx - itemx);
            float diffy = Mathf.Abs(sloty - itemy);
            if (diffx >= 5.0 || diffy >= 5.0)
            {
                inblock = null;
            }
        }
    }

    [System.Obsolete]
    public void OnPointerDown(PointerEventData eventData)
    {
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("delete");
            DestroyObject(inblock);
            inblock = null;
        }
    }
}