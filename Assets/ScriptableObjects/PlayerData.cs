using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/PlayerData")]
public class PlayerData : EntityData
{
    [Header("PÁRAMETROS SALTO")]
    public float jumpSpeed;
    public float climbJumpSpeed, coyoteWindow, groundRadius;

    [Header("PÁRAMETROS MOVIMIENTO")]
    public float movSpeed;

    [Header("PÁRAMETROS ESCALADA")]
    public float wallRadius;
    public float climbSpeed, fallDeathSpeed, fallDmgSpeed, grabStamina, climbStamina, jumpStamina, restStaminaSpeed, umbralRecuperacion, climbCoyoteWindow;

    [Header("PÁRAMETROS HAMBRE")]
    public float hambreSpeed;
    public float maxInitStamina = 20f, maxEnergy = 100f;

    [Header("SFX")]
    public AudioClip SFX_Paso;
    public AudioClip SFX_Salto, SFX_Escalar, SFX_Caer, SFX_Autojump;
}
