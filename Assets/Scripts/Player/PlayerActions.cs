using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerActions : MonoBehaviour
{
    public Bomba bomba;
    public Cuerda cuerda;
    public Transform dropPoint, escenario, delante;
    public int initBombs, currentBombs = 0, initRopes, currentRopes = 0;
    public HasLives playerHpSystem;
    public Text txtCuerdas, txtBombas;
    private ContactFilter2D filter = new ContactFilter2D();
    private Collider2D[] hits = new Collider2D[1];
    public LayerMask suelo;

    private void Awake()
    {
        filter.SetLayerMask(suelo);
    }
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
            int count = Physics2D.OverlapCircle(dropPoint.position, 0.5f, filter, hits);
            if (currentBombs > 0 && count <= 0)
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
        {
            EventBus.ResetearNivell();
            cuerda.LimpiarCuedas();
        }
            
    }

    public void UpdateUI()
    {
        txtBombas.text = currentBombs.ToString();
        txtCuerdas.text = currentRopes.ToString();
    }
}
