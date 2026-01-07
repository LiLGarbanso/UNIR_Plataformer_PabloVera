using UnityEngine;

public class LianaArpon : MonoBehaviour
{
    public PlantaArpon plantaArpon;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            plantaArpon.PresaDetectada(collision.bounds.ClosestPoint(transform.position));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision != null)
        {
            plantaArpon.PresaDetectada(collision.bounds.ClosestPoint(transform.position));
        }
    }
}
