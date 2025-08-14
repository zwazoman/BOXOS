using UnityEngine;

public class Spin : MonoBehaviour
{
    [SerializeField] float _speed = 10f;
    private void Update()
    {
        transform.Rotate(0, _speed * Time.deltaTime, 0);
    }
}
