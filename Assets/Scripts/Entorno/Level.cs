using UnityEngine;

public class Level : MonoBehaviour
{
    private bool levelFinished;
    public int bombas, cuerdas;
    public Transform checkPoint;

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

    public void SetRecursosIniciales()
    {
        EventBus.SetBombas(bombas);
        EventBus.SetCuerdas(cuerdas);
    }

    public void FiishLevel() { levelFinished = true; }
}
