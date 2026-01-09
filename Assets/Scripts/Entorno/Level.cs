using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    private bool levelFinished;
    public int bombas, cuerdas;
    public Transform checkPoint;
    //public List<GameObject> levelProps;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision != null)
        {
            if (collision.gameObject.CompareTag("Player") && !levelFinished)
            {
                SetRecursosIniciales();
                EventBus.SetCheckPoint(checkPoint);
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
            }
        }
    }

    public void SetRecursosIniciales()
    {
        EventBus.SetBombas(bombas);
        EventBus.SetCuerdas(cuerdas);
    }

    public void FinishLevel() { levelFinished = true; }
}
