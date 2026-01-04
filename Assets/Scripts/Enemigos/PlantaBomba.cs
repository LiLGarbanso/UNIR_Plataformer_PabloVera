using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class PlantaBomba : MonoBehaviour
{
    public Transform center;
    [SerializeField]private float pushForce, stunTime, minPushY=500f, delayAnim;
    public AudioClip detonation;
    private ParticleSystem ps;
    private Collider2D col;
    private SpriteRenderer spRend;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
        ps = GetComponent<ParticleSystem>();
        spRend = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision != null)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                if(collision.gameObject.TryGetComponent<Rigidbody2D>(out Rigidbody2D rbPlayer) && collision.gameObject.TryGetComponent<PlayerMovement>(out PlayerMovement playerMove))
                {
                    col.enabled = false;
                    spRend.enabled = false;
                    ps.Play();
                    Vector2 dir = collision.gameObject.transform.position - center.position;
                    dir.Normalize();
                    Debug.Log(dir * pushForce);
                    //rbPlayer.linearVelocity = dir*pushForce;
                    playerMove.Stunear(stunTime);
                    rbPlayer.AddForce(dir * pushForce + new Vector2(0, minPushY));
                    SoundMannager.Instance.PlaySFX(detonation);
                    StartCoroutine(ExplosionAnim());
                }
            }
        }
    }

    IEnumerator ExplosionAnim()
    {
        yield return new WaitForSeconds(delayAnim);
        gameObject.SetActive(false);
        yield return null;
    }
}
