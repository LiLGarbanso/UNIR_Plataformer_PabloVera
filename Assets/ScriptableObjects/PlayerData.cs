using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/PlayerData")]
public class PlayerData : ScriptableObject
{
    [Header("PÁRAMETROS SALTO")]
    public float jumpSpeed;
    public float climbJumpSpeed, coyoteWindow, groundRadius, fallAnimSpeed;

    [Header("PÁRAMETROS MOVIMIENTO")]
    public float movSpeed;

    [Header("PÁRAMETROS ESCALADA")]
    public float wallRadius;
    public float climbSpeed, fallDeathSpeed, grabStamina, climbStamina, jumpStamina, restStaminaSpeed, umbralRecuperacion, climbCoyoteWindow;

    [Header("PÁRAMETROS HAMBRE")]
    public float hambreSpeed;
    public float maxInitStamina, maxEnergy = 100f;

    [Header("SFX")]
    public AudioClip SFX_Muerte;
}
