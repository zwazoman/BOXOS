using UnityEngine;

[CreateAssetMenu(fileName = "New Prosthesis", menuName = "Prosthesis")]
public class ProsthesisData : ScriptableObject
{
    [field: SerializeField]
    public ActionData actionData;

    // visuels etc
}
