using UnityEngine;
using UnityEngine.InputSystem;

public class ArrastreRelativo : MonoBehaviour
{
    [SerializeField] float limiteX = 2.2f;
    [SerializeField] float sensibilidad = 1f;

    Camera cam;
    Vector3 dedoAnterior;  // posición del dedo en el frame anterior (mundo)

    void Start() { cam = Camera.main; }

    void Update()
    {
        var p = Pointer.current;
        if (p == null) return;

        Vector3 dedo = cam.ScreenToWorldPoint(p.position.ReadValue());

        if (p.press.wasPressedThisFrame)
        {
            dedoAnterior = dedo;  // empieza el arrastre: solo guardamos
        }
        else if (p.press.isPressed)
        {
            float delta = (dedo.x - dedoAnterior.x) * sensibilidad;
            Vector3 pos = transform.position;
            pos.x = Mathf.Clamp(pos.x + delta, -limiteX, limiteX);
            transform.position = pos;
            dedoAnterior = dedo;
        }
    }
}
