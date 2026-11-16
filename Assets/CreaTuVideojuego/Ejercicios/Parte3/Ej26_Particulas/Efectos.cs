using UnityEngine;

public class Efectos : MonoBehaviour
{
    public static Efectos Instancia { get; private set; }
    [SerializeField] ParticleSystem prefabChispas;

    void Awake() { Instancia = this; }

    public void Chispas(Vector3 posicion)
    {
        // Stop Action = Destroy: se borra sola al terminar
        Instantiate(prefabChispas, posicion, Quaternion.identity);
    }
}
