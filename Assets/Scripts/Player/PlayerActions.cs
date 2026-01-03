using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerActions : MonoBehaviour
{
    public GameObject antorchaGO;
    public void Antorcha(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if(antorchaGO.activeSelf)
            {
                //Sonido apagar antorcha
                antorchaGO.SetActive(false);
            }
            else
            {
                antorchaGO.SetActive(true);
            }
        }
    }
}
