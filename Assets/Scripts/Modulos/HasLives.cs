using UnityEngine;

public class HasLives : MonoBehaviour
{
    private int currentVidas;
    public EntityData entityData;

    private void Awake()
    {
        currentVidas = entityData.lives;
    }

    public void TakeDamage(int dmg)
    {
        currentVidas -= dmg;
        SoundMannager.Instance.PlaySFX(entityData.SFX_Dmg);
        if (currentVidas <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        SoundMannager.Instance.PlaySFX(entityData.SFX_Muerte);
    }
}
