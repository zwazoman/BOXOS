using UnityEngine;

[CreateAssetMenu(fileName = "New Prosthesis", menuName = "Prosthesis")]
public class ProsthesisData : ScriptableObject
{
    [field: SerializeField]
    public ActionData actionData;

    [field : SerializeField]
    public GameObject prefab;

    [field: SerializeField]
    public Sprite sprite;

    // visuels etc
}
