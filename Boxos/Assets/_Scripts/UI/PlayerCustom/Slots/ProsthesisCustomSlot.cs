using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ProsthesisCustomSlot : ProfileCustomSlot, IDropHandler
{
    [SerializeField] Image _image;
    [SerializeField] ArmSide _side;

    ProsthesisData _data;

    protected override void Start()
    {
        base.Start();
    }

    public void SetData(ProsthesisData prosthesisData)
    {
        if(_side == ArmSide.Left && prosthesisData != view.playerProfile.leftArmData)
            view.playerProfile.leftArmData = prosthesisData;
        else if(_side == ArmSide.Right && prosthesisData != view.playerProfile.rightArmData)
            view.playerProfile.rightArmData = prosthesisData;

        _data = prosthesisData;

        _image.sprite = _data.sprite;
    }

    public void ClearData()
    {
        SetData(view.emptyProsthesis);
    }

    public void OnDrop(PointerEventData eventData)
    {
        ProsthesisUI draggable;

        if(eventData.pointerDrag.TryGetComponent(out draggable))
        {
            SetData(draggable.data);
        }
    }
}
