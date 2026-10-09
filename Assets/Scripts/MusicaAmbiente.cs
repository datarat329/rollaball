using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicaAmbiente : MonoBehaviour
{
    private static MusicaAmbiente instancia;

    void Awake()
    {
        if (instancia != null) { Destroy(gameObject); return; }
        instancia = this;
        DontDestroyOnLoad(gameObject);   // la música no se corta al cambiar de escena
    }
}