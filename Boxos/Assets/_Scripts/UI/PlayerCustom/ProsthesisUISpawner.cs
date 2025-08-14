using UnityEngine;
using System.Collections.Generic;

public class ProsthesisUISpawner : MonoBehaviour
{
    [SerializeField] List<ProsthesisData> Prosthesises = new();

    [SerializeField] GameObject _prosthesisUiPrefab;
    [SerializeField] Transform _parent;

    void Start()
    {
        foreach(ProsthesisData data in Prosthesises)
        {
            ProsthesisUI prosthesis;
            Instantiate(_prosthesisUiPrefab, _parent).TryGetComponent(out prosthesis);
            prosthesis.data = data;
        }
    }
}
