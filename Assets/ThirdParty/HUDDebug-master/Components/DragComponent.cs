using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

[RequireComponent(typeof(EventTrigger))]
public class DragComponent : MonoBehaviour
{
    private RectTransform rectTransform;

    public void Awake()
    {
        rectTransform = GetComponent<RectTransform>();

        //add on drag listiner
        EventTrigger eventTrigger = GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.Drag;
        entry.callback.AddListener(OnDrag);
#if UNITY_5
        eventTrigger.triggers.Add(entry);
#else
        eventTrigger.delegates.Add( entry );
#endif

    }

    public void OnDrag(UnityEngine.EventSystems.BaseEventData eventData)
    {
        PointerEventData pointerData = eventData as PointerEventData;
        if (pointerData == null)
            return; 
        Vector3 curPosition = rectTransform.position;
        curPosition.x += pointerData.delta.x;
        curPosition.y += pointerData.delta.y;
        rectTransform.position = curPosition;
    }

}
