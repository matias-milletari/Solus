using UnityEngine;

[CreateAssetMenu(menuName = "Game/Spell/Fireball")]
public class Fireball : Spell
{
    public FireballController fireball;
    public GameObject explosionPrefab;
    public float radius;
    public LayerMask enemyLayer;
    public AudioClip fireballCastAudioClip;

    public override void Initialize()
    {
        fireball.speed = GetSpeed();
        fireball.damage = GetDamage();
        fireball.durationTime = GetDurationTime();
        fireball.explosionPrefab = explosionPrefab;
        fireball.radius = radius;
        fireball.enemyLayer = enemyLayer;
        fireball.fireballCastAudioClip = fireballCastAudioClip;
    }

    public override void Create(Vector3 castingPosition, Vector3 rayOrigin, Vector3 rayDirection)
    {
        fireball.CastFireballSpell(castingPosition, rayOrigin, rayDirection);
    }
}
