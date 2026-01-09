using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    public RectTransform rtEstamina, rtHambre;
    public PlayerData playerData;
    public PlayerMovement playerMovement;

    private void Awake()
    {
        
    }

    private void Update()
    {
        rtEstamina.localScale = new Vector3(Mathf.Clamp(Mathf.Lerp(0, 1, playerMovement.currentStamina / playerData.maxInitStamina), 0, 1), 1f, 1f);
        //rtHambre.localScale = new Vector3(Mathf.Clamp(Mathf.Lerp(0, 1, playerMovement.currentEnergy / playerData.maxEnergy), 0, 1), 1f, 1f);
    }
}
