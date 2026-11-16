// Ayudas para los botones de las escenas de demostración (no son parte del libro).
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public static class GuiDemo
{
    public static float Escala =>
        Mathf.Clamp(Mathf.Min(Screen.width / 520f, Screen.height / 820f), 0.8f, 3f);

    // Dibuja una columna de botones en la parte baja de la pantalla
    public static void Area(Action contenido, float desdeY = 0.56f)
    {
        float e = Escala;
        GUI.skin.button.fontSize = Mathf.RoundToInt(15 * e);
        GUI.skin.label.fontSize = Mathf.RoundToInt(15 * e);
        GUI.skin.label.wordWrap = true;
        float ancho = Mathf.Min(Screen.width - 40 * e, 360 * e);
        var r = new Rect((Screen.width - ancho) / 2f, Screen.height * desdeY, ancho, Screen.height * 0.3f);
        GUILayout.BeginArea(r);
        contenido();
        GUILayout.EndArea();
    }

    public static bool Boton(string texto) =>
        GUILayout.Button(texto, GUILayout.Height(32 * Escala));

    public static void Texto(string texto)
    {
        var s = new GUIStyle(GUI.skin.label)
        {
            fontStyle = FontStyle.Bold, alignment = TextAnchor.MiddleCenter,
            normal = { textColor = Color.white }
        };
        GUILayout.Label(texto, s);
    }

    // Toque en pantalla, clic o barra espaciadora, ignorando los clics sobre botones del panel
    public static bool Toco()
    {
        var k = Keyboard.current;
        if (k != null && k.spaceKey.wasPressedThisFrame) return true;
        var p = Pointer.current;
        if (p == null || !p.press.wasPressedThisFrame) return false;
        // Solo cuenta si el toque es en la franja central (los paneles están arriba y abajo)
        float y = p.position.ReadValue().y / Screen.height;
        return y > 0.35f && y < 0.78f; // 0 = abajo, 1 = arriba
    }
}
