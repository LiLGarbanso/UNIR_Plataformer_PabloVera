using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LevelTarget : MonoBehaviour
{
    private bool hasBeenReached;
    public Level level;
    public GameObject luz;

    private void Awake()
    {
        hasBeenReached = false;
        luz.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.CompareTag("Player") && !hasBeenReached)
            {
                //Notificar que es el último nivel completado
                hasBeenReached = true;
                //EventBus.SetCheckPoint(transform);
                level.FinishLevel();
                luz.SetActive(true);
            }
        }
    }
}
