using UnityEngine;

public class ObstaculoMovil : MonoBehaviour
{
    public Vector3 desplazamiento = new Vector3(6, 0, 0);
    public float velocidad = 2f;
    private Vector3 inicio;

    void Start() { inicio = transform.position; }

    void Update()
    {
        float t = Mathf.PingPong(Time.time * velocidad, 1f);
        transform.position = inicio + desplazamiento * t;
    }
}