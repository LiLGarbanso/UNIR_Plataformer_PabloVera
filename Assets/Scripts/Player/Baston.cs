using UnityEngine;

public class Baston : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision != null && !collision.gameObject.CompareTag("Player"))
        {
            if(collision.gameObject.TryGetComponent<HasLives>(out var target))
            {
                target.TakeDamage(1);
            }
        }
    }
}
