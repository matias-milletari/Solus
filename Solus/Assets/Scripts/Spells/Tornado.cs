using UnityEngine;

[CreateAssetMenu(menuName = "Game/Spell/Tornado")]
public class Tornado : Spell
{
    public TornadoController tornado;
    public float damageTimeInterval;
    public LayerMask enemyLayer;

    public override void Initialize()
    {
        tornado.speed = GetSpeed();
        tornado.damage = GetDamage();
        tornado.maximumCastingDistance = GetMaximumCastingDistance();
        tornado.durationTime = GetDurationTime();
        tornado.damageTimeInterval = damageTimeInterval;
        tornado.enemyLayer = enemyLayer;
    }

    public override void Create(Vector3 position, Quaternion rotation, Vector3 rayOrigin, Vector3 rayDirection)
    {
        tornado.CastTornadoSpell(position, rotation, rayOrigin, rayDirection);
    }
}