using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProsthesisUI : DraggableUI
{
    [HideInInspector] public ProsthesisData data;

    [Header("references")]
    [SerializeField] Image _image;
    [SerializeField] TMP_Text _name;
    [SerializeField] TMP_Text _smallDesc;

    private void Start()
    {
        _image.sprite = data.sprite;
        _name.text = data.name;
        _smallDesc.text = data.smallDescription;
    }

    //drag and drop de con (reggarder sur tacticook éventuellement)
}
