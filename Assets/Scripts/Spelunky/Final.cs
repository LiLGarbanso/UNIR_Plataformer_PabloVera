using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Final : MonoBehaviour
{
    public Light2D l;
    public ParticleSystem ps;
    public AudioClip clip;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision != null)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                l.enabled = true;
                ps.Play();
                SoundMannager.Instance.PararSonido();
                SoundMannager.Instance.PlayMusic(clip);
            }
        }
    }
}
