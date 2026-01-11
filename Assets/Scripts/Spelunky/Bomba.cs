using System.Collections;
using UnityEngine;

public class Bomba : MonoBehaviour
{
    public Transform center;
    [SerializeField] private float pushForce, stunTime;
    public int dmgExplosion = 0;
    public AudioClip detonation;
    public ParticleSystem ps;

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
                    playerMove.Explotar(stunTime, dmgExplosion);
                    //rbPlayer.AddForce(dir * pushForce + new Vector2(0, minPushY));
                    //rbPlayer.AddForce(dir * pushForce);
                    rbPlayer.linearVelocity += dir*pushForce;
                }
            }

            if (collision.gameObject.CompareTag("Caja"))
            {
                if(collision.gameObject.TryGetComponent<Rigidbody2D>(out Rigidbody2D rbCaja))
                {
                    Vector2 dir = collision.gameObject.transform.position - center.position;
                    dir.Normalize();
                    //rbPlayer.AddForce(dir * pushForce + new Vector2(0, minPushY));
                    //rbCaja.AddForce(dir * pushForce);
                    rbCaja.linearVelocity += dir * pushForce;
                }
            }
        }
    }

public void PlayExplosion()
    {
        SoundMannager.Instance.PlaySFX(detonation);
        ps.Play();
    }

    public void Desactivar()
    {
        Destroy(gameObject);
    }
}
