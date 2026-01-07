using System.Collections;
using UnityEngine;

public class Bomba : MonoBehaviour
{
    public Transform center;
    [SerializeField] private float pushForce, stunTime, minPushY = 500f;
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
                    //playerMove.Stunear(stunTime,1);
                    rbPlayer.AddForce(dir * pushForce + new Vector2(0, minPushY));
                    //rbPlayer.AddForce(dir * pushForce);
                }
            }
        }
    }

public void PlayExplosion()
    {
        SoundMannager.Instance.PlaySFX(detonation);
    }

    public void Desactivar()
    {
        Destroy(gameObject);
    }
}
