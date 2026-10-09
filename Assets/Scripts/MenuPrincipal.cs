using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    void start()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void Jugar() { Debug.Log("Jugar presionado"); SceneManager.LoadScene("Nivel1"); }
    public void Opciones() { SceneManager.LoadScene("Opciones"); }
    public void Volver() { SceneManager.LoadScene("MenuPrincipal"); }
    public void Salir()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}