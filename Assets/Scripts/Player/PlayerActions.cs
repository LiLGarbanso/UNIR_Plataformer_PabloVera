using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerActions : MonoBehaviour
{
    public Antorcha antorcha;
    public Caldero caldero;
    public Animator playerAnimator;
    public void Antorcha(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            antorcha.BotonAntorcha();
        }
    }

    public void Caldero(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            caldero.StartCooking();
        }
    }

    public void Baston(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            playerAnimator.SetTrigger("golpear");
        }
    }
}
