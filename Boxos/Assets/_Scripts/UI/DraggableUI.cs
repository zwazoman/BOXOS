using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class DraggableUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Vector3 _intialPos;
    Transform _initialParent;

    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        _initialParent = transform;
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(_initialParent, false);
    }
}
