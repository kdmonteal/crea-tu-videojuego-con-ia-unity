// Panel que aparece en cada escena de ejercicio mientras juegas.
// Muestra qué hacer, qué observar y los últimos mensajes de la Consola.
// No forma parte del código del libro: es solo una ayuda para usar el proyecto.
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PanelEjercicio : MonoBehaviour
{
    public int numero;
    public string titulo;
    [TextArea(3, 10)] public string instrucciones;
    public string scripts;

    bool visible = true;
    readonly List<string> mensajes = new List<string>();
    GUIStyle sTitulo, sTexto, sConsola, sBoton, sCaja;
    Texture2D fondo;

    void OnEnable()  { Application.logMessageReceived += AlRecibirMensaje; }
    void OnDisable() { Application.logMessageReceived -= AlRecibirMensaje; }

    void AlRecibirMensaje(string texto, string pila, LogType tipo)
    {
        string prefijo = tipo == LogType.Error || tipo == LogType.Exception ? "ERROR: " :
                         tipo == LogType.Warning ? "Aviso: " : "";
        mensajes.Add(prefijo + texto);
        if (mensajes.Count > 4) mensajes.RemoveAt(0);
    }

    void Update()
    {
        var k = Keyboard.current;
        if (k == null) return;
        if (k.hKey.wasPressedThisFrame) visible = !visible;
        if (k.rKey.wasPressedThisFrame) Reiniciar();
    }

    void Reiniciar()
    {
        Time.timeScale = 1f;
        var escena = SceneManager.GetActiveScene();
#if UNITY_EDITOR
        // En el editor funciona aunque la escena no esté en la lista de Build Profiles
        UnityEditor.SceneManagement.EditorSceneManager.LoadSceneInPlayMode(
            escena.path, new LoadSceneParameters(LoadSceneMode.Single));
#else
        SceneManager.LoadScene(escena.buildIndex);
#endif
    }

    void Estilos(float escala)
    {
        if (fondo == null)
        {
            fondo = new Texture2D(1, 1);
            fondo.SetPixel(0, 0, new Color(0.04f, 0.10f, 0.16f, 0.88f));
            fondo.Apply();
        }
        sCaja = new GUIStyle { normal = { background = fondo } };
        sTitulo = new GUIStyle(GUI.skin.label)
        {
            fontSize = Mathf.RoundToInt(24 * escala), fontStyle = FontStyle.Bold, wordWrap = true,
            normal = { textColor = new Color(0.90f, 0.90f, 0.06f) }
        };
        sTexto = new GUIStyle(GUI.skin.label)
        {
            fontSize = Mathf.RoundToInt(18 * escala), wordWrap = true, richText = true,
            normal = { textColor = Color.white }
        };
        sConsola = new GUIStyle(sTexto)
        {
            fontSize = Mathf.RoundToInt(16 * escala),
            normal = { textColor = new Color(0.62f, 0.85f, 1f) }
        };
        sBoton = new GUIStyle(GUI.skin.button) { fontSize = Mathf.RoundToInt(16 * escala) };
    }

    void OnGUI()
    {
        float escala = Mathf.Clamp(Mathf.Min(Screen.width / 520f, Screen.height / 820f), 0.8f, 3f);
        Estilos(escala);
        float m = 10 * escala;
        float ancho = Screen.width - 2 * m;
        float bw = 130 * escala, bh = 30 * escala;

        if (!visible)
        {
            if (GUI.Button(new Rect(m, m, bw * 1.4f, bh), "Ver instrucciones (H)", sBoton)) visible = true;
            return;
        }

        // Panel superior: título e instrucciones
        string cabecera = $"Ejercicio {numero} · {titulo}";
        string cuerpo = instrucciones + (string.IsNullOrEmpty(scripts) ? "" :
                        $"\n<color=#9FD6FF>Script del libro: {scripts}</color>");
        float hT = sTitulo.CalcHeight(new GUIContent(cabecera), ancho - 2 * m);
        float hC = sTexto.CalcHeight(new GUIContent(cuerpo), ancho - 2 * m);
        float alto = hT + hC + bh + 4 * m;
        GUI.Box(new Rect(m, m, ancho, alto), GUIContent.none, sCaja);
        GUI.Label(new Rect(2 * m, 2 * m, ancho - 2 * m, hT), cabecera, sTitulo);
        GUI.Label(new Rect(2 * m, 2 * m + hT + m * 0.4f, ancho - 2 * m, hC), cuerpo, sTexto);
        float yb = 2 * m + hT + hC + m;
        if (GUI.Button(new Rect(2 * m, yb, bw, bh), "Reiniciar (R)", sBoton)) Reiniciar();
        if (GUI.Button(new Rect(2 * m + bw + m, yb, bw, bh), "Ocultar (H)", sBoton)) visible = false;

        // Panel inferior: lo que el código escribe en la Consola
        if (mensajes.Count > 0)
        {
            string txt = "Consola:\n" + string.Join("\n", mensajes);
            float hm = sConsola.CalcHeight(new GUIContent(txt), ancho - 2 * m) + 2 * m;
            GUI.Box(new Rect(m, Screen.height - hm - m, ancho, hm), GUIContent.none, sCaja);
            GUI.Label(new Rect(2 * m, Screen.height - hm, ancho - 2 * m, hm - m), txt, sConsola);
        }
    }
}
