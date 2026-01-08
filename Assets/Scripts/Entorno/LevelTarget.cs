using UnityEngine;

public class LevelTarget : MonoBehaviour
{
    private bool hasBeenReached;
    public Level level;

    private void Awake()
    {
        hasBeenReached = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.CompareTag("Player") && !hasBeenReached)
            {
                //Notificar que es el último nivel completado
                hasBeenReached = true;
                EventBus.SetCheckPoint(transform);
                level.FiishLevel();
            }
        }
    }
}
