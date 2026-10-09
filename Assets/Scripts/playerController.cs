using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class playerController : MonoBehaviour
{
    public float speed = 5.0f;
    public float gravity = -20f;
    public float jumpHeight = 3.0f;

    public int contador;
    public TextMeshProUGUI textoContador;

    private nivelController nivelController;
    public CharacterController controller;

    private Vector2 moveInput;
    private float verticalVelocity;
    private bool jumpPressed;

    private Vector3 posicionInicial;
    private GameObject[] coleccionables;

    void Start()
    {
        nivelController = FindObjectOfType<nivelController>();
        controller = GetComponent<CharacterController>();

        posicionInicial = transform.position;   
        coleccionables = GameObject.FindGameObjectsWithTag("Collection");

        contador = 0;
        ActualizarInterfaz();
    }

    void Update()
    {
        if (nivelController != null && nivelController.juegoTerminado)
        {
            moveInput = Vector2.zero;
            return;
        }

        if (transform.position.y < -10f && nivelController != null)
        {
            nivelController.Perder("¡Te caíste!");
            return;
        }

        bool isGrounded = controller.isGrounded;
        if (isGrounded && verticalVelocity < 0)
            verticalVelocity = -2f;

        if (jumpPressed && isGrounded)
        {
            verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
            jumpPressed = false;
        }

        verticalVelocity += gravity * Time.deltaTime;

        Vector3 move = new Vector3(moveInput.x * speed, verticalVelocity, moveInput.y * speed);
        controller.Move(move * Time.deltaTime);
    }

    public void OnMove(InputValue value)
    {
        if (nivelController != null && nivelController.juegoTerminado) return;

        if (nivelController != null && !nivelController.timerActivo)
            nivelController.timerActivo = true;

        moveInput = value.Get<Vector2>();
    }

    public void OnMove(Vector2 value)
    {
        moveInput = value;
    }

    public void OnJump(InputValue value)
    {
        jumpPressed = value.isPressed;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collection"))
        {
            other.gameObject.SetActive(false);
            contador++;
            ActualizarInterfaz();
        }
        else if (other.CompareTag("Obstaculo") && nivelController != null)
        {
            nivelController.Perder("¡Te golpeó el obstáculo!");
        }
    }

    public void ReiniciarJugador()
    {
        controller.enabled = false;             
        transform.position = posicionInicial;
        controller.enabled = true;

        verticalVelocity = 0;
        moveInput = Vector2.zero;
        jumpPressed = false;
        contador = 0;
        ActualizarInterfaz();
    }

    public void ReaparecerColeccionables()
    {
        foreach (GameObject c in coleccionables)
            if (c != null) c.SetActive(true);
    }

    public void ActualizarInterfaz()
    {
        if (textoContador != null)
            textoContador.text = "Puntos: " + contador;
    }
}