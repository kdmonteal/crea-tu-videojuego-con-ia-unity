using UnityEngine;

// Ejercicio 28: genera un sonido corto por código para probar GestorAudio.Sonar
// sin necesidad de importar archivos de audio.
public class DemoAudio : MonoBehaviour
{
    AudioClip pop;

    void Start()
    {
        const int frecuencia = 44100;
        int muestras = frecuencia / 8;
        pop = AudioClip.Create("Pop", muestras, 1, frecuencia, false);
        var datos = new float[muestras];
        for (int i = 0; i < muestras; i++)
        {
            float t = (float)i / frecuencia;
            float tono = Mathf.Lerp(880f, 440f, (float)i / muestras);
            datos[i] = Mathf.Sin(2f * Mathf.PI * tono * t) * (1f - (float)i / muestras) * 0.5f;
        }
        pop.SetData(datos, 0);
    }

    void OnGUI()
    {
        GuiDemo.Area(() =>
        {
            if (GuiDemo.Boton("Sonar efecto (con variación de tono)"))
                GestorAudio.Instancia.Sonar(pop);
            GuiDemo.Texto("Los volúmenes necesitan un Audio Mixer: sigue los pasos del ejercicio 28.");
        }, 0.62f);
    }
}
