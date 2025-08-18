using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NameCustomSlot : ProfileCustomSlot
{
    [SerializeField] TMP_InputField _nameInputField;

    protected override void Start()
    {
        base.Start();

        _nameInputField.onSubmit.AddListener(SetData);
    }
    public void SetData(string name)
    {
        view.playerProfile.profileName = name;
        _nameInputField.text = name;
    }
}
