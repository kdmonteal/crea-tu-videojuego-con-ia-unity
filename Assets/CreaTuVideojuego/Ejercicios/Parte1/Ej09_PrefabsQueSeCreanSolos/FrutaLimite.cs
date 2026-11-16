using UnityEngine;

public class FrutaLimite : MonoBehaviour
{
    [SerializeField] float limiteY = -6f;

    void Update()
    {
        // Si cae por debajo de la pantalla, desaparece
        if (transform.position.y < limiteY)
            Destroy(gameObject);
    }
}
