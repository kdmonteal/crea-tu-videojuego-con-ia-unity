using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class Salto : MonoBehaviour
{
    [SerializeField] float fuerzaSalto = 7f;
    Rigidbody2D rb;

    void Awake() { rb = GetComponent<Rigidbody2D>(); }

    void Update()
    {
        var p = Pointer.current;
        bool toque = p != null && p.press.wasPressedThisFrame;
        bool tecla = Keyboard.current != null &&
                     Keyboard.current.spaceKey.wasPressedThisFrame;

        if (toque || tecla)
        {
            // Reinicia la velocidad vertical para que cada salto sea igual
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, fuerzaSalto);
        }
    }
}
