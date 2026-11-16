using UnityEngine;

public class AnimadorArepa : MonoBehaviour
{
    // Convertir el nombre a número una sola vez es más rápido y seguro
    static readonly int Voltear = Animator.StringToHash("Voltear");

    Animator animator;

    void Awake() { animator = GetComponent<Animator>(); }

    public void ReproducirVolteo()
    {
        animator.SetTrigger(Voltear);
    }
}
