using UnityEngine;

public class Pinchos : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.gameObject.CompareTag("Player"))
        {
            if(collision.gameObject.TryGetComponent<HasLives>(out var playerHpSystem))
            {
                playerHpSystem.Die();
            }
        }
    }
}
