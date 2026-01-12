using UnityEngine;

public class Interruptor : MonoBehaviour
{
    public GameObject mecanismo;
    public AudioClip pulsar;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            mecanismo.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision != null)
        {
            mecanismo.SetActive(true);
            SoundMannager.Instance.PlaySFX(pulsar);
        }
    }
}
