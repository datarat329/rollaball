using UnityEngine;

public class cameraController : MonoBehaviour
{
    // El objeto que la cámara va a seguir
    public Transform jugador;

    // CORREGIDO: Valores positivos en Y para que esté ARRIBA del jugador
    public Vector3 offset;

    // Qué tan rápido/suave sigue la cámara al jugador
    [Range(0.01f, 1f)]
    public float suavizado = 0.125f; // Un valor más bajo (como 0.125) da un seguimiento más fluido

    void Start()
    {
       offset = new Vector3(0f, 10f, 0f);
    }

    void LateUpdate()
    {
        if (jugador == null) return;

        // Calcula la posición ideal (posición del jugador + la separación)
        Vector3 posicionDeseada = jugador.position + offset;

        // Suaviza el movimiento
        Vector3 posicionSuavizada = Vector3.Lerp(transform.position, posicionDeseada, suavizado);

        // Aplica la posición y hace que mire al jugador
        transform.position = posicionSuavizada;
    }
}
