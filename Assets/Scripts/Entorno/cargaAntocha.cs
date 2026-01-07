using UnityEngine;

public class cargaAntocha : MonoBehaviour
{
    public Antorcha antorchaScript;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                if(antorchaScript.RecogerCarga())
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }
}
