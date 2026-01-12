using UnityEngine;
using UnityEngine.InputSystem;

public class MenuInicial : MonoBehaviour
{
    public Animator animator;
    public GameMannager gm;
    public GameObject cam1, cam2;
    public void PulsarStart(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            cam1.SetActive(false);
            cam2.SetActive(true);
            animator.SetTrigger("start");
            gm.IniciarJuego();
        }
    }

    public void Destruir()
    {
        Destroy(gameObject);
    }
}
