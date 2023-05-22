using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    private RectTransform joystickHandle;
    private Vector2 joystickPosition;
    private bool isJoystickActive = false;

    private void Awake()
    {
        joystickHandle = transform.Find("Handle").GetComponent<RectTransform>();        
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 localPosition;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(joystickHandle.parent as RectTransform, eventData.position, eventData.pressEventCamera, out localPosition))
        {
            joystickPosition = localPosition.normalized;
            float radius = (joystickHandle.parent as RectTransform).sizeDelta.x / 2f;
            joystickHandle.anchoredPosition = joystickPosition * radius;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isJoystickActive = true;
        OnDrag(eventData); 
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isJoystickActive = false;
        joystickPosition = Vector2.zero;
        joystickHandle.anchoredPosition = Vector2.zero; 
    }

    public Vector2 GetJoystickDirection()
    {
        return joystickPosition;
    }

    public bool IsJoystickActive()
    {
        return isJoystickActive;
    }
}
