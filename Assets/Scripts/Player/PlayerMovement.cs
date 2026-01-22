using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.DefaultInputActions;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private Vector2 movDir;
    private bool isGrounded, wasGrounded, escalar, puedeEscalar, tired, canMove, hasJump, isStunned;
    private float lastTimeGrounded, lastVerticalVelocity, currentJumpSpeed, lastTimeCanClimb;
    private float currentEnergy, maxStamina, drag = 0.98f;
    private HasLives liveSystem;
    private RaycastHit2D[] hits = new RaycastHit2D[1];
    private ContactFilter2D filter = new ContactFilter2D();

    [Header("REFERENCIAS")]
    public LayerMask sueloMask;
    public string sueloMaskStr, cuerdasMask, cajasMask;
    public Transform pies, delante, autojump, bombDrop;
    public Animator animator;
    public PlayerData playerData;
    public SpriteRenderer spRend;
    public float currentStamina;

    [Header("DEBUG")]
    public bool debug;

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
        canMove = true;
        hasJump = false;
        liveSystem = GetComponent<HasLives>();
        filter.SetLayerMask(LayerMask.GetMask(sueloMaskStr, cuerdasMask, cajasMask));
        filter.useTriggers = true;
    }

    public void ResetPlayer()
    {
        maxStamina = playerData.maxInitStamina;
        currentStamina = maxStamina;
        currentEnergy = playerData.maxEnergy;
        canMove = true;
        isStunned = false;
        tired = false;
        escalar = false;
        hasJump = false;
        escalar = false;
        rb2d.linearVelocity = Vector2.zero;
    }

    private void FixedUpdate()
    {
        if (!canMove) return;
        //La estamina máxima depende proporcionalmente a la energía actual
        //La energía va decreciendo por el hambre
        //Cuando la energía llega a 0, el jugador muere
        //currentEnergy -= playerData.hambreSpeed * Time.fixedDeltaTime;
        if (currentEnergy < 0)
            Die();

        //maxStamina = Mathf.Lerp(0f, playerData.maxInitStamina, currentEnergy / playerData.maxEnergy);

        //Si está escalando, la velocidad vertical es diferente
        if (escalar)
            rb2d.linearVelocity = new Vector2(movDir.x * playerData.movSpeed, movDir.y * playerData.climbSpeed);
        else if (isStunned)
            rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, rb2d.linearVelocity.y);
        else
            rb2d.linearVelocity = new Vector2(movDir.x * playerData.movSpeed, rb2d.linearVelocity.y);

        //// Comprobación de suelo
        isGrounded = Physics2D.Raycast(pies.position, -(Vector2)transform.up, playerData.groundRadius, sueloMask);
        Debug.DrawRay(pies.position, -(Vector2)transform.up, Color.purple);

        //Guardar el último momento en el suelo para el coyote jump. Puede servir también para el jump buffer
        if (isGrounded)
        {
            hasJump = false;
            lastTimeGrounded = Time.time;
            RecuperarEstamina();    //Solo se recuera estamina al estar en el suelo
            if (!wasGrounded)
            {

                if(debug)
                    Debug.Log("Land Velocity: " + lastVerticalVelocity);
                if (lastVerticalVelocity < playerData.fallDeathSpeed)   //Daño por caída
                {
                    liveSystem.Die();
                    //Animación muerte
                }
                else if (lastVerticalVelocity < playerData.fallDmgSpeed)
                {
                    liveSystem.TakeDamage(1);
                    Stunear(2f);
                }
                else
                {
                    SoundMannager.Instance.PlaySFX(playerData.SFX_Caer);
                }
            }
        }
        else
            if (escalar) CalcularGasoEstamina();

        //Comprobación paredes para poder escalar
        int count1 = Physics2D.Raycast(delante.position, delante.right, filter, hits, playerData.wallRadius);
        int count2 = Physics2D.Raycast(autojump.position, autojump.right, filter, hits, playerData.wallRadius);

        if (count1 > 0)
            puedeEscalar = true;
        else
            puedeEscalar = false;
        Debug.DrawRay(delante.position, delante.right, Color.red);

        //Si está agotado, no puede escalar
        if (tired) puedeEscalar = false;

        Debug.DrawRay(autojump.position, autojump.right, Color.green);
        if (!puedeEscalar)
            escalar = false;
        else if (count2 == 0 && escalar)    //Si está escalando y va a llegar al borde, se le da un último impulso al jugador
        {
            DarSalto(true);
        }

        if (puedeEscalar)
            lastTimeCanClimb = Time.time;

        wasGrounded = isGrounded;

        if(!isGrounded)
            lastVerticalVelocity = rb2d.linearVelocity.y;   //Guardamos la última velocidad solo si aún estamos en el aire

        rb2d.linearVelocity *= drag;
    }

    public void Saltar(InputAction.CallbackContext context)
    {
        if (context.started)
            DarSalto();
    }

    public void DarSalto(bool climbJump = false)
    {
        if (tired || hasJump || !canMove) return;  //Sonido de agotado
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
            if (climbJump)
                SoundMannager.Instance.PlaySFX(playerData.SFX_Autojump);
            else
                SoundMannager.Instance.PlaySFX(playerData.SFX_Salto);
            hasJump = true;
        }
        else if (jumpDelay < playerData.coyoteWindow)
        {
            animator.SetBool("isClimbing", false);
            animator.SetTrigger("jump");
            if (escalar) currentStamina -= playerData.jumpStamina;
            escalar = false;
            rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, rb2d.linearVelocity.y + currentJumpSpeed);
            if (climbJump)
                SoundMannager.Instance.PlaySFX(playerData.SFX_Autojump);
            else
                SoundMannager.Instance.PlaySFX(playerData.SFX_Salto);
            hasJump = true;
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
                delante.localRotation = Quaternion.Euler(0f, 180f, 0f);
                autojump.localRotation = Quaternion.Euler(0f, 180f, 0f);
                bombDrop.localScale = new Vector3(-1, 1, 1);
                spRend.flipX = true;
            }

            if (movDir.x > 0f)
            {
                delante.localRotation = Quaternion.Euler(0f, 0f, 0f);
                autojump.localRotation = Quaternion.Euler(0f, 0f, 0f);
                bombDrop.localScale = Vector3.one;
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
        if (puedeEscalar && !tired)
        {
            escalar = true;
            animator.SetBool("isClimbing", true);
            hasJump = false;
        }
        //if (context.performed)
        //{
        //    float climbDelay = Time.time - lastTimeCanClimb;
        //    if (puedeEscalar || climbDelay < playerData.climbCoyoteWindow)
        //    {
        //        escalar = true;
        //        animator.SetBool("isClimbing", true);
        //        hasJump = false;
        //    }
        //}
    }

    public void CalcularGasoEstamina()
    {
        if (!debug)
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
        currentEnergy = Mathf.Clamp(currentEnergy, 0, playerData.maxEnergy);
    }

    public void Die()
    {
        Debug.Log("MUERTE");
        //SoundMannager.Instance.PlaySFX(playerData.SFX_Muerte);
        gameObject.SetActive(false);
        EventBus.MuerteJugador();
        //ResetPlayer();
        //gameObject.SetActive(false);
    }

    public void Explotar(float s, int dmg = 0)
    {
        liveSystem.TakeDamage(dmg);

        //animación
        StartCoroutine(OndaExpansiva(s));
    }

    IEnumerator OndaExpansiva(float segundos)
    {
        isStunned = true;
        yield return new WaitForSeconds(segundos);
        isStunned = false;
        yield return null;
    }

    public void Stunear(float s, int dmg = 0)
    {
        liveSystem.TakeDamage(dmg);
        isStunned = true;
        animator.SetTrigger("estamparse");
        StartCoroutine(Stun(s));
    }

    public Collider2D mainCol, colFricc;
    IEnumerator Stun(float segundos)
    {
        //colFricc.enabled = true;
        //mainCol.enabled = false;
        canMove = false;
        yield return new WaitForSeconds(segundos);
        canMove = true;
        isStunned = false;
        //mainCol.enabled = true;
        //colFricc.enabled = false;
        yield return null;
    }
}
