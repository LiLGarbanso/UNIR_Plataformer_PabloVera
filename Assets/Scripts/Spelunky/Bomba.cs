using System.Collections;
using UnityEngine;

public class Bomba : MonoBehaviour
{
    public Transform center, escenario, player;
    [SerializeField] private float pushForce, stunTime, minPushY = 500f, destroyTime;
    public AudioClip detonation;
    private ParticleSystem ps;
    private Collider2D col;
    private SpriteRenderer spRend;
    public Animator animatorBomba;

    private void Start()
    {
        col = GetComponent<Collider2D>();
        ps = GetComponent<ParticleSystem>();
        spRend = GetComponent<SpriteRenderer>();
        ActivarBomba();
    }

    public void ActivarBomba()
    {
        transform.SetParent(escenario);
        spRend.enabled = true;
        animatorBomba.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                if (collision.gameObject.TryGetComponent<Rigidbody2D>(out Rigidbody2D rbPlayer) && collision.gameObject.TryGetComponent<PlayerMovement>(out PlayerMovement playerMove))
                {                   
                    Vector2 dir = collision.gameObject.transform.position - center.position;
                    dir.Normalize();
                    Debug.Log(dir * pushForce);
                    //rbPlayer.linearVelocity = dir*pushForce;
                    playerMove.Stunear(stunTime);
                    rbPlayer.AddForce(dir * pushForce + new Vector2(0, minPushY));
                    SoundMannager.Instance.PlaySFX(detonation);
                }
            }
        }
    }

    public void Desactivar()
    {
        gameObject.SetActive(false);
    }

    public void Explotar()
    {
        StartCoroutine(ExplosionAnim());
    }

    IEnumerator ExplosionAnim()
    {
        col.enabled = false;
        spRend.enabled = false;
        //ps.Play();
        yield return new WaitForSeconds(destroyTime);
        gameObject.SetActive(false);
        yield return null;
    }
}
