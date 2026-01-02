using UnityEngine;

public class PlayerAnimEvents : MonoBehaviour
{
    public PlayerData playerData;

    public void PlaySfxPaso()
    {
        SoundMannager.Instance.PlaySFX(playerData.SFX_Paso);
    }

    public void PlaySfxSalto()
    {
        SoundMannager.Instance.PlaySFX(playerData.SFX_Salto);
    }

    public void PlaySfxEscalar()
    {
        SoundMannager.Instance.PlaySFX(playerData.SFX_Escalar);
    }
}
