using UnityEngine;

public class Girador : MonoBehaviour
{
    [SerializeField] float velocidad = 90f; // grados por segundo

    void Update()
    {
        // Gira un poco en cada frame, sobre el eje Z
        transform.Rotate(0f, 0f, velocidad * Time.deltaTime);
    }
}
