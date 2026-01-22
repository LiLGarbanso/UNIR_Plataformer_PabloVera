using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerActions : MonoBehaviour
{
    public BombHandler bombhandler;
    public Cuerda cuerda;
    public Transform dropPoint, escenario, delante;
    private int currentBombs = 0, currentRopes = 0;
    public HasLives playerHpSystem;
    public Text txtCuerdas, txtBombas;
    private ContactFilter2D filter = new ContactFilter2D();
    private Collider2D[] hits = new Collider2D[1];
    public LayerMask suelo;
    public Animator playerAnimator;
    public AudioClip noBombs, noROpes;

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
                bombhandler.ColocarBomba();
                UpdateUI();
            }
            else
            {
                SoundMannager.Instance.PlaySFX(noBombs);
            }
        }
    }

    public void Cuerda(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (currentRopes > 0)
            {
                playerAnimator.SetTrigger("cuerda");
                if (cuerda.LanzarCuerda())
                {
                    currentRopes--;
                    UpdateUI();
                }
            }
            else
            {
                SoundMannager.Instance.PlaySFX(noROpes);
            }
        }
    }

    public void ResetearNivel(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            EventBus.ResetearNivell();
            cuerda.LimpiarCuedas();
            bombhandler.LimpiarBombas();
        }
            
    }

    public void UpdateUI()
    {
        txtBombas.text = currentBombs.ToString();
        txtCuerdas.text = currentRopes.ToString();
    }
}
