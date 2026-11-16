using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Reloj : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textoCuenta;
    [SerializeField] TextMeshProUGUI textoReloj;
    [SerializeField] float duracionPartida = 60f;

    public event Action AlTerminar;
    float restante;
    bool corriendo;

    void Start() { StartCoroutine(CuentaRegresiva()); }

    IEnumerator CuentaRegresiva()
    {
        for (int i = 3; i > 0; i--)
        {
            textoCuenta.text = i.ToString();
            yield return new WaitForSecondsRealtime(1f); // ignora timeScale
        }
        textoCuenta.text = "¡Ya!";
        yield return new WaitForSecondsRealtime(0.6f);
        textoCuenta.text = "";

        restante = duracionPartida;
        corriendo = true;
    }

    void Update()
    {
        if (!corriendo) return;

        restante -= Time.deltaTime;
        if (restante <= 0f)
        {
            restante = 0f;
            corriendo = false;
            AlTerminar?.Invoke();
        }
        int total = Mathf.CeilToInt(restante);
        textoReloj.text = $"{total / 60}:{total % 60:00}";
    }
}
