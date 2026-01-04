using UnityEngine;

[CreateAssetMenu(fileName = "EntityData", menuName = "Scriptable Objects/EntityData")]
public class EntityData : ScriptableObject
{
    [Header("SFX ENTITY")]
    public AudioClip SFX_Muerte, SFX_Dmg;

    [Header("ENTITY PARAMETERS")]
    public int lives;
}
