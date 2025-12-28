using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.DefaultInputActions;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private Vector2 movDir;
    private bool isGrounded, wasGrounded, escalar, puedeEscalar, tired;
    private float lastTimeGrounded, lastVerticalVelocity, currentJumpSpeed, lastTimeCanClimb;
    public float currentStamina, currentEnergy, maxStamina;

    [Header("REFERENCIAS")]
    public LayerMask sueloMask;
    public Transform pies, delante, autojump;
    public Animator animator;
    public PlayerData playerData;
    public SpriteRenderer spRend;

    private void Awake()
    {
        wasGrounded = false;
        rb2d = GetComponent<Rigidbody2D>();
        currentJumpSpeed = playerData.jumpSpeed;
        tired = false;
        escalar = false;
        maxStamina = playerData.maxInitStamina;
        currentStamina = maxStamina;
        currentEnergy = playerData.maxEnergy;
    }

    public void ResetPlayer()
    {
        maxStamina = playerData.maxInitStamina;
        currentStamina = maxStamina;
        currentEnergy = playerData.maxEnergy;
    }

    private void FixedUpdate()
    {
        //La estamina máxima depende proporcionalmente a la energía actual
        //La energía va decreciendo por el hambre
        //Cuando la energía llega a 0, el jugador muere
        currentEnergy -= playerData.hambreSpeed * Time.deltaTime;
        if (currentEnergy < 0)
            Die();

        maxStamina = Mathf.Lerp(0f, playerData.maxInitStamina, currentEnergy / playerData.maxEnergy);

        //Si está escalando, la velocidad vertical es diferente
        if (escalar)
            rb2d.linearVelocity = new Vector2(movDir.x * playerData.movSpeed, movDir.y * playerData.climbSpeed);
        else
            rb2d.linearVelocity = new Vector2(movDir.x * playerData.movSpeed, rb2d.linearVelocity.y);


        //// Comprobación de suelo
        isGrounded = Physics2D.Raycast(pies.position, -(Vector2)transform.up, playerData.groundRadius, sueloMask);
        Debug.DrawRay(pies.position, -(Vector2)transform.up, Color.purple);

        //Guardar el último momento en el suelo para el coyote jump. Puede servir también para el jump buffer
        if (isGrounded)
        {
            lastTimeGrounded = Time.time;
            RecuperarEstamina();    //Solo se recuera estamina al estar en el suelo
            if (!wasGrounded)
            {

                if (lastVerticalVelocity < playerData.fallDeathSpeed)   //Daño por caída
                    Die();
                else if (lastVerticalVelocity < playerData.fallAnimSpeed)
                {
                    //animator.SetTrigger("Caida");
                    //retener al jugador un segundo
                }
            }
        }
        else
            if (escalar) CalcularGasoEstamina();

        //Comprobación paredes para poder escalar
        puedeEscalar = Physics2D.Raycast(delante.position, delante.right, playerData.wallRadius, sueloMask);
        Debug.DrawRay(delante.position, delante.right, Color.red);

        //Si está agotado, no puede escalar
        if (tired) puedeEscalar = false;

        Debug.DrawRay(autojump.position, autojump.right, Color.green);
        if (!puedeEscalar)
            escalar = false;
        //Si está escalando y va a llegar al borde, se le da un último impulso al jugador
        else if (!Physics2D.Raycast(autojump.position, autojump.right, playerData.wallRadius, sueloMask) && escalar)
        {
            DarSalto(true);
        }

        if (puedeEscalar)
            lastTimeCanClimb = Time.time;

        wasGrounded = isGrounded;
        lastVerticalVelocity = rb2d.linearVelocity.y;
    }

    public void Saltar(InputAction.CallbackContext context)
    {
        if (context.started)
            DarSalto();
    }

    public void DarSalto(bool climbJump = false)
    {
        if (tired) return;  //Sonido de agotado
        //Si el salto se ejecuta en la escalada, es menos potente
        if (climbJump || escalar)   //Si es un salto en escalada o el autojump al llegar a un borde
            currentJumpSpeed = playerData.climbJumpSpeed;

        float jumpDelay = Time.time - lastTimeGrounded;
        //Se puede saltar si se está en el suelo o agarrado a una pared
        if (isGrounded || escalar)
        {
            animator.SetBool("isClimbing", false);
            animator.SetTrigger("jump");
            if (escalar) currentStamina -= playerData.jumpStamina;  //Solo se consume estamina si es salto desde escalada
            escalar = false;    //Al saltar se desactiva la escalada
            rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, rb2d.linearVelocity.y + currentJumpSpeed);
        }
        else if (jumpDelay < playerData.coyoteWindow)
        {
            animator.SetBool("isClimbing", false);
            animator.SetTrigger("jump");
            if (escalar) currentStamina -= playerData.jumpStamina;
            escalar = false;
            rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, rb2d.linearVelocity.y + currentJumpSpeed);
        }

        currentJumpSpeed = playerData.jumpSpeed;
    }

    public void Moverse(InputAction.CallbackContext context)
    {
        movDir = context.ReadValue<Vector2>();

        if (!escalar)
        {
            animator.SetBool("isClimbing", false);
            if (movDir != Vector2.zero && isGrounded)
                animator.SetBool("isWalking", true);
            else
                animator.SetBool("isWalking", false);

            if (movDir.x < 0f)
            {
                delante.transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
                autojump.transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
                //sprRendHead.flipX = true;
                //posicionesGOs.localScale = Vector3.one;
                spRend.flipX = true;
            }

            if (movDir.x > 0f)
            {
                delante.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
                autojump.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
                //sprRendHead.flipX = false;
                //posicionesGOs.localScale = new Vector3(-1, 1, 1);
                spRend.flipX = false;
            }
        }
        else
        {
            movDir.x = 0;
            animator.SetBool("isClimbing", true);
        }

    }

    public void Escalar(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            float climbDelay = Time.time - lastTimeCanClimb;
            if (puedeEscalar || climbDelay < playerData.climbCoyoteWindow)
            {
                escalar = true;
                animator.SetBool("isClimbing", true);
            }
                
        }
    }

    public void CalcularGasoEstamina()
    {
        if (currentStamina < 0)
            tired = true;
        else
        {
            //Gasto pasivo de estamina por estar escalando
            currentStamina -= playerData.grabStamina * Time.deltaTime;

            //Gasto extra por escalar hacia arriba
            if (movDir.y > 0f)
                currentStamina -= playerData.climbStamina * Time.deltaTime;
        }
    }

    private void RecuperarEstamina()
    {
        if (currentStamina < maxStamina)
        {
            currentStamina += playerData.restStaminaSpeed * Time.deltaTime;

            //Se considera que ya no está agotado cuando supera el umbral de descanso
            if (currentStamina > playerData.umbralRecuperacion)
                tired = false;
        }
    }

    public void Comer(float energia)
    {
        currentEnergy += energia;
    }

    public void Die()
    {
        Debug.Log("MUERTE");
        //SoundMannager.Instance.PlaySFX(playerData.SFX_Muerte);
        EventBus.MuerteJugador();
        //ResetPlayer();
        //gameObject.SetActive(false);
    }
}
