using Unity.Cinemachine;
using UnityEngine;

public class PlantaBomba : MonoBehaviour
{
    public Transform center;
    [SerializeField]private float pushForce, stunTime;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision != null)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                if(collision.gameObject.TryGetComponent<Rigidbody2D>(out Rigidbody2D rbPlayer) && collision.gameObject.TryGetComponent<PlayerMovement>(out PlayerMovement playerMove))
                {
                    Vector2 dir = collision.gameObject.transform.position - center.position;
                    dir.Normalize();
                    Debug.Log(dir * pushForce);
                    //rbPlayer.linearVelocity = dir*pushForce;
                    playerMove.Stunear(stunTime);
                    rbPlayer.AddForce(dir * pushForce);
                    gameObject.SetActive(false);
                }
            }
        }
    }
}
