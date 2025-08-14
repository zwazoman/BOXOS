using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProsthesisSlot : MonoBehaviour
{
    [SerializeField] Image _image;
    [SerializeField] ArmSide _side;

    ProfileCustomMenuView _view;
    ProsthesisData _data;

    private void Start()
    {
        _view = GetComponentInParent<ProfileCustomMenuView>();
    }

    public void SetData(ProsthesisData prosthesisData)
    {
        if(_side == ArmSide.Left && prosthesisData != _view.playerProfile.leftArmData)
            _view.playerProfile.leftArmData = prosthesisData;
        else if(_side == ArmSide.Right && prosthesisData != _view.playerProfile.rightArmData)
            _view.playerProfile.rightArmData = prosthesisData;

        _data = prosthesisData;

        _image.sprite = _data.sprite;
    }

    public void ClearData()
    {
        SetData(_view.emptyProsthesis);
    }
}
