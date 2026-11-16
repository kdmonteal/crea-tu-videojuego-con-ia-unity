using UnityEngine;

public class GeneradorFrutas : MonoBehaviour
{
    [SerializeField] GameObject prefabFruta;
    [SerializeField] float intervalo = 1f;   // segundos entre frutas
    [SerializeField] float rangoX = 2.2f;    // mitad del ancho donde aparecen

    float reloj;

    void Update()
    {
        reloj += Time.deltaTime;
        if (reloj >= intervalo)
        {
            reloj = 0f;
            Vector3 pos = transform.position;
            pos.x = Random.Range(-rangoX, rangoX);
            Instantiate(prefabFruta, pos, Quaternion.identity);
        }
    }
}
