using UnityEngine;

public class GeneradorPonderado : MonoBehaviour
{
    [System.Serializable]
    public class Entrada { public GameObject prefab; public int peso = 1; }

    [SerializeField] Entrada[] entradas;
    [SerializeField] AnimationCurve intervaloPorTiempo =
        AnimationCurve.Linear(0f, 1.2f, 60f, 0.4f);
    [SerializeField] float rangoX = 2.2f;
    [SerializeField] int maxRepeticiones = 3;

    float tiempoPartida, reloj;
    int ultimo = -1, repeticiones;

    void Update()
    {
        tiempoPartida += Time.deltaTime;
        reloj += Time.deltaTime;

        if (reloj >= intervaloPorTiempo.Evaluate(tiempoPartida))
        {
            reloj = 0f;
            int i = Elegir();
            Vector3 pos = transform.position;
            pos.x = Random.Range(-rangoX, rangoX);
            Instantiate(entradas[i].prefab, pos, Quaternion.identity);
        }
    }

    int Elegir()
    {
        int i;
        do
        {
            int total = 0;
            foreach (var e in entradas) total += e.peso;

            int tirada = Random.Range(0, total); // 0..total-1
            i = 0;
            while (tirada >= entradas[i].peso)
            {
                tirada -= entradas[i].peso;
                i++;
            }
        } while (i == ultimo && repeticiones >= maxRepeticiones
                 && entradas.Length > 1);

        repeticiones = (i == ultimo) ? repeticiones + 1 : 1;
        ultimo = i;
        return i;
    }
}
