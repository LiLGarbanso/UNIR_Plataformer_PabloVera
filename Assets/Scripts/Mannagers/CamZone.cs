using Unity.Cinemachine;
using UnityEngine;

public class CamZone : MonoBehaviour
{
    public CinemachineCamera cam;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                EventBus.ActivarCamZone(cam);
            }
        }
    }
}
