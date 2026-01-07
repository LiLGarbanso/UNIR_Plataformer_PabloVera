using UnityEngine;

public class Caldero : IInteractable
{
    public Antorcha antorcha;
    public float tCocinado, energiaObtenida, checkRadius;
    public PlayerMovement playerMovement;
    public Transform cookingPoint, escenario, player;
    public LayerMask suelo;
    private bool isCooked, isActive;
    private float currentCookTime;
    public GameObject visual;
    public Collider2D col;

    private void Start()
    {
        currentCookTime = tCocinado + Random.Range(0, tCocinado / 4);
        visual.SetActive(false);
        col.enabled = false;
    }

    private void Update()
    {
        if (isActive)
        {
            currentCookTime -= Time.deltaTime;
            if (currentCookTime < 0)
            {
                isActive = false;
                isCooked = true;
            }
        }
    }

    public void StartCooking()
    {
        if(Physics2D.Raycast(cookingPoint.position, Vector2.down, checkRadius, suelo))
        {
            if (antorcha.UsarCargaCocinar())
            {
                currentCookTime = tCocinado + Random.Range(0, tCocinado / 4);
                isActive = true;
                isCooked = false;
                gameObject.transform.SetParent(escenario);
                visual.SetActive(true);
                col.enabled = true;
            }
        }
    }

    public void ComerComida()
    {
        if (isCooked)
        {
            visual.SetActive(false);
            playerMovement.Comer(energiaObtenida);
            isActive = false;
            isCooked= false;
            col.enabled = false;
            gameObject.transform.SetParent(player);
        }
    }

    public override void Interact(GameObject interactor)
    {
        ComerComida();
    }

    public override string GetPrompt()
    {
        return "";
    }
}
