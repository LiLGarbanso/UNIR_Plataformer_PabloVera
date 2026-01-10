using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LevelTarget : MonoBehaviour
{
    private bool hasBeenReached;
    public Level level;
    //public Light2D light2d;

    private void Awake()
    {
        hasBeenReached = false;
        //light2d.enabled = false;
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
                //light2d.enabled = true;
            }
        }
    }
}
