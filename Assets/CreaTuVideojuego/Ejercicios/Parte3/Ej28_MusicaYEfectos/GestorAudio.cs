using UnityEngine;
using UnityEngine.Audio;

public class GestorAudio : MonoBehaviour
{
    public static GestorAudio Instancia { get; private set; }
    [SerializeField] AudioMixer mezcla;
    [SerializeField] AudioSource fuenteMusica;
    [SerializeField] AudioSource fuenteEfectos;

    void Awake() { Instancia = this; }

    public void Sonar(AudioClip clip)
    {
        fuenteEfectos.pitch = Random.Range(0.95f, 1.05f);
        fuenteEfectos.PlayOneShot(clip);
    }

    public void VolumenMusica(float valor)  => Fijar("VolMusica", valor);
    public void VolumenEfectos(float valor) => Fijar("VolEfectos", valor);

    void Fijar(string parametro, float valor)
    {
        // El oído percibe en escala logarítmica: 0..1 → decibeles
        float db = Mathf.Log10(Mathf.Max(valor, 0.0001f)) * 20f;
        mezcla.SetFloat(parametro, db);
    }
}
