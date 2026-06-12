using UnityEngine;
using UnityEngine.EventSystems;

public class Button : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool IsHold = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        IsHold = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        IsHold = false;
    }
}
