using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerActions : MonoBehaviour
{
    public Bomba bomba;
    public Cuerda cuerda;
    public Transform dropPoint, escenario;
    public int initBombs, currentBombs, initRopes, currentRopes;

    private void Start()
    {
        currentBombs = initBombs;
        currentRopes = initRopes;
    }

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
