using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Antorcha : MonoBehaviour
{
    private int currentCargas;
    public int maxCargas;
    public float ttlAntorcha;
    private float currentTtl;
    private bool encendida, agotada;
    public Light2D light2d;
    public AudioClip SFX_Encender, SFX_Apagar;
    public AudioSource src;

    private void Start()
    {
        currentCargas = 0;
        currentTtl = ttlAntorcha;
        encendida = true;
        agotada = false;
        light2d.enabled = true;
        src.Play();
    }

    private void Update()
    {
        if (encendida)
        {
            currentTtl -= Time.deltaTime;
            if(currentTtl <= 0)
            {
                agotada = true;
                ApagarAntorcha();
            }
        }
    }

    public bool UsarCargaCocinar()
    {
        if(currentCargas > 0)
        {
            currentCargas--;
            return true;
        }
        else
            return false;
    }

    public void BotonAntorcha()
    {
        if (encendida)
            ApagarAntorcha();
        else
            EncenderAntorcha();
    }

    public void ApagarAntorcha()
    {
        SoundMannager.Instance.PlaySFX(SFX_Apagar, 0.5f);
        src.Stop();
        encendida = false;
        light2d.enabled = false;
    }

    public void EncenderAntorcha()
    {
        if(agotada)
        {
            if(currentCargas > 0)
            {
                SoundMannager.Instance.PlaySFX(SFX_Encender,0.5f);
                src.Play();
                currentCargas--;
                agotada = false;
                encendida = true;
                light2d.enabled = true;
            }
        }
        else
        {
            SoundMannager.Instance.PlaySFX(SFX_Encender, 0.5f);
            src.Play();
            encendida = true;
            light2d.enabled = true;
        }
    }

    public bool RecogerCarga()
    {
        if (currentCargas < maxCargas - 1)
        {
            currentCargas++;
            return true;
        }
        else
            return false;

    }
}
