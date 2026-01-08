using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerActions : MonoBehaviour
{
    public Bomba bomba;
    public Cuerda cuerda;
    public Transform dropPoint, escenario;
    public int initBombs, currentBombs = 0, initRopes, currentRopes = 0;

    private void OnEnable()
    {
        EventBus.OnSetBombas += SetBombs;
        EventBus.OnSetCuerdas += SetRopes;
    }

    private void OnDisable()
    {
        EventBus.OnSetBombas -= SetBombs;
        EventBus.OnSetCuerdas -= SetRopes;
    }

    public void SetBombs(int b) { currentBombs = b; }
    public void SetRopes(int r) { currentRopes = r; }

    public void ColocarBomba(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if(currentBombs > 0)
            {
                currentBombs--;
                Instantiate(bomba, dropPoint.position, Quaternion.identity, escenario);
            }
        }
    }

    public void Cuerda(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (currentRopes > 0)
            {
                if (cuerda.LanzarCuerda())
                {
                    currentRopes--;
                }
            }
        }
    }
}
