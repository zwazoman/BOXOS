using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class DraggableUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Vector3 _intiialPos;
    RectTransform _rectTransform;

    protected virtual void Start()
    {
        TryGetComponent(out _rectTransform);
    }

    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        _intiialPos = _rectTransform.anchoredPosition;

    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition = _intiialPos;
    }
}
