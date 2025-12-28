using UnityEngine;

public class AnimTorchMask : MonoBehaviour
{
    public SpriteRenderer spriteRenderer; // Sprite animado
    public SpriteMask spriteMask;

    void LateUpdate()
    {
        spriteMask.sprite = spriteRenderer.sprite;
    }
}
