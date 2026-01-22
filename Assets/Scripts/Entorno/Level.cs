using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    private bool levelFinished;
    public int bombas, cuerdas;
    public Transform startPos, levelTarget;
    public List<Caja> cajas;
    public GameObject luzCheckPoint;

    private void Awake()
    {
        luzCheckPoint.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision != null)
        {
            if (collision.gameObject.CompareTag("Player") && !levelFinished)
            {
                //SetRecursosIniciales();
                //EventBus.SetCheckPoint(startPos);
                EventBus.IniciarNivel(this);
                SetRecursosIniciales();
                luzCheckPoint.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                EventBus.SetBombas(0);
                EventBus.SetCuerdas(0);
                foreach (Caja c in cajas)
                    c.ResetCaja();
            }
        }
    }

    public void SetRecursosIniciales()
    {
        EventBus.SetBombas(bombas);
        EventBus.SetCuerdas(cuerdas);
    }

    public void FinishLevel() { levelFinished = true; }
    public bool GetLevelStatus() { return levelFinished; }

    public void ResetLevel()
    {
        foreach(Caja c in cajas)
            c.ResetCaja();

        SetRecursosIniciales();
    }
}
