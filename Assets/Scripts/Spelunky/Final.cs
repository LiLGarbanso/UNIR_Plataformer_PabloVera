using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class Final : MonoBehaviour
{
    public Light2D l;
    public ParticleSystem ps;
    public AudioClip clip;
    private bool finished;
    public float delayRestart;

    private void Start()
    {
        finished = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision != null && !finished)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                finished=true;
                l.enabled = true;
                ps.Play();
                SoundMannager.Instance.PararSonido();
                SoundMannager.Instance.PlayMusic(clip);
                StartCoroutine(FinalCoroutine());
            }
        }
    }

    IEnumerator FinalCoroutine()
    {
        yield return new WaitForSeconds(delayRestart);
        SceneManager.LoadScene(0);
        yield return null;
    }
}
