using UnityEngine;

// Ejercicio 41: cuenta cuántas frutas existen en realidad gracias al pool
public class DemoPool : MonoBehaviour
{
    void OnGUI()
    {
        var todas = FindObjectsByType<FrutaPool>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        int activas = 0;
        foreach (var f in todas) if (f.gameObject.activeSelf) activas++;
        GuiDemo.Area(() =>
        {
            GuiDemo.Texto($"Frutas creadas en total: {todas.Length}\nEn pantalla: {activas} · Guardadas en el pool: {todas.Length - activas}");
        }, 0.75f);
    }
}
