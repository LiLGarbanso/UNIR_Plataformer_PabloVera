using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerActions : MonoBehaviour
{
    public Bomba bomba;
    public Cuerda cuerda;
    public Transform dropPoint, escenario;
    public int initBombs, currentBombs = 0, initRopes, currentRopes = 0;
    public HasLives playerHpSystem;
    public Text txtCuerdas, txtBombas;

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

    public void SetBombs(int b)
    {
        currentBombs = b;
        UpdateUI();
    }
    public void SetRopes(int r)
    {
        currentRopes = r;
        UpdateUI();
    }

    public void ColocarBomba(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if(currentBombs > 0)
            {
                currentBombs--;
                Instantiate(bomba, dropPoint.position, Quaternion.identity, escenario);
                UpdateUI();
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
                    UpdateUI();
                }
            }
        }
    }

    public void ResetearNivel(InputAction.CallbackContext context)
    {
        if (context.started)
            playerHpSystem.TakeDamage(100);
    }

    public void UpdateUI()
    {
        txtBombas.text = currentBombs.ToString();
        txtCuerdas.text = currentRopes.ToString();
    }
}
