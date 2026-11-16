using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CanvasGroup))]
public class Navegacion : MonoBehaviour
{
    public static Navegacion Instancia { get; private set; }
    [SerializeField] float duracion = 0.35f;
    CanvasGroup grupo;

    void Awake()
    {
        if (Instancia != null) { Destroy(gameObject); return; }
        Instancia = this;
        DontDestroyOnLoad(gameObject);
        grupo = GetComponent<CanvasGroup>();
        grupo.alpha = 0f;
        grupo.blocksRaycasts = false;
    }

    public void IrAJuego() => StartCoroutine(Cambiar("Juego"));
    public void IrAMenu()  => StartCoroutine(Cambiar("Menu"));

    IEnumerator Cambiar(string escena)
    {
        grupo.blocksRaycasts = true;          // nadie toca nada
        yield return Fundir(0f, 1f);
        Time.timeScale = 1f;
        yield return SceneManager.LoadSceneAsync(escena);
        yield return Fundir(1f, 0f);
        grupo.blocksRaycasts = false;
    }

    IEnumerator Fundir(float desde, float hasta)
    {
        for (float t = 0f; t < 1f; t += Time.unscaledDeltaTime / duracion)
        {
            grupo.alpha = Mathf.Lerp(desde, hasta, t);
            yield return null;
        }
        grupo.alpha = hasta;
    }
}
