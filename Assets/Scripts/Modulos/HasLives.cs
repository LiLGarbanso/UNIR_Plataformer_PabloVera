using System.Collections.Generic;
using UnityEngine;

public class HasLives : MonoBehaviour
{
    private int currentVidas;
    public EntityData entityData;
    public List<GameObject> hp;

    private void Awake()
    {
        currentVidas = entityData.lives;
    }

    public void TakeDamage(int dmg)
    {
        if (dmg <= 0) return;
        currentVidas -= dmg;
        UpdateUI();
        SoundMannager.Instance.PlaySFX(entityData.SFX_Dmg);
        if (currentVidas <= 0)
        {
            Die();
        }
    }

    public void UpdateUI()
    {
        foreach (GameObject go in hp)
            go.SetActive(false);

        for(int i = 0; i < currentVidas; i++)
            hp[i].SetActive(true);
    }

    public void Die()
    {
        SoundMannager.Instance.PlaySFX(entityData.SFX_Muerte);
        gameObject.SetActive(false);
        EventBus.MuerteJugador();
    }

    public void ResetLives()
    {
        currentVidas = entityData.lives;
        UpdateUI();
    }
}
