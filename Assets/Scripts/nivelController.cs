using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class nivelController : MonoBehaviour
{
    private playerController player;

    [Header("Configuración del nivel")]
    public int numeroNivel = 1;
    public int goal = 12;
    public float tiempoInicial = 30f;
    public string siguienteEscena = "Nivel2";        
    public string escenaMenu = "MenuPrincipal";      

    [Header("Estado")]
    public float tiempoRestante;
    public bool timerActivo = false;
    public bool juegoTerminado = false;

    [Header("UI")]
    public TextMeshProUGUI textoTimer;
    public TextMeshProUGUI textoNivel;
    public TextMeshProUGUI textoGoalOrLose;

    public float duracionTextoMensaje = 3f;
    public float esperaFinal = 5f;

    void Start()
    {
        player = FindObjectOfType<playerController>();
        tiempoRestante = tiempoInicial;
        ActualizarTextoTimer(tiempoRestante);
        if (textoNivel != null) textoNivel.text = "Nivel: " + numeroNivel;
        if (textoGoalOrLose != null) textoGoalOrLose.text = "";
    }

    void Update()
    {
        if (juegoTerminado) return;

        if (player != null && player.contador >= goal)
        {
            Ganar();
            return;
        }

        if (timerActivo)
        {
            tiempoRestante -= Time.deltaTime;
            if (tiempoRestante > 0)
            {
                ActualizarTextoTimer(tiempoRestante);
            }
            else
            {
                tiempoRestante = 0;
                ActualizarTextoTimer(0);
                Perder("¡Tiempo agotado!");
            }
        }
    }

    void Ganar()
    {
        juegoTerminado = true;
        timerActivo = false;
        StartCoroutine(RutinaGanar());
    }

    IEnumerator RutinaGanar()
    {
        bool esUltimo = string.IsNullOrEmpty(siguienteEscena);

        if (esUltimo)
        {
            Mensaje("¡Felicidades! ¡Completaste el juego!");
            yield return new WaitForSeconds(esperaFinal);   // 5 segundos
            SceneManager.LoadScene(escenaMenu);
        }
        else
        {
            Mensaje("¡Nivel completado!");
            yield return new WaitForSeconds(duracionTextoMensaje);
            SceneManager.LoadScene(siguienteEscena);
        }
    }

    public void Perder(string motivo)
    {
        if (juegoTerminado) return;
        juegoTerminado = true;
        timerActivo = false;
        StartCoroutine(RutinaPerder(motivo));
    }

    IEnumerator RutinaPerder(string motivo)
    {
        Mensaje(motivo + "  Nivel: " + numeroNivel);
        yield return new WaitForSeconds(duracionTextoMensaje);
        ReiniciarNivel();
    }

    void ReiniciarNivel()
    {
        player.ReiniciarJugador();            
        player.ReaparecerColeccionables();    

        tiempoRestante = tiempoInicial;
        ActualizarTextoTimer(tiempoRestante);
        Mensaje("");

        timerActivo = false;                  
        juegoTerminado = false;
    }

    void Mensaje(string texto)
    {
        if (textoGoalOrLose != null) textoGoalOrLose.text = texto;
    }

    void ActualizarTextoTimer(float t)
    {
        if (textoTimer == null) return;
        if (t < 0) t = 0;
        int min = Mathf.FloorToInt(t / 60);
        int seg = Mathf.FloorToInt(t % 60);
        textoTimer.text = string.Format("{0:00}:{1:00}", min, seg);
    }
}