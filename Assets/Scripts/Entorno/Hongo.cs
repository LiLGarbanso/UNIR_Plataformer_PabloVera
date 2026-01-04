using UnityEngine;

public class Hongo : MonoBehaviour
{
    public GameObject luzGO;
    public bool startsActive;
    public ParticleSystem ps;

    private void Awake()
    {
        if(startsActive)
        {
            ps.Play();
            luzGO.SetActive(true);
        }
        else
        {
            ps.Stop();
            luzGO.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision != null)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                ps.Play();
                luzGO.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                ps.Stop();
                luzGO.SetActive(false);
            }
        }
    }
}
