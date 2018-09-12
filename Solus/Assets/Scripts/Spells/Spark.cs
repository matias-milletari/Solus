using UnityEngine;

[CreateAssetMenu(menuName = "Game/Spell/Spark")]
public class Spark : Spell
{
    public SparkController spark;
    public GameObject explosionPrefab;
    public LayerMask enemyLayer;
    public GameObject stunParticle;
    public float stunDuration;
    public AudioClip sparkCastAudioClip;

    public override void Initialize()
    {
        spark.speed = GetSpeed();
        spark.damage = GetDamage();
        spark.durationTime = GetDurationTime();
        spark.explosionPrefab = explosionPrefab;
        spark.enemyLayer = enemyLayer;
        spark.sparkCastAudioClip = sparkCastAudioClip;
        spark.stunParticle = stunParticle;
        spark.stunDuration = stunDuration;
    }

    public override void Create(Vector3 castingPosition, Vector3 rayOrigin, Vector3 rayDirection)
    {
        spark.CastSparkSpell(castingPosition, rayOrigin, rayDirection);
    }
}