using UnityEngine;

[CreateAssetMenu(menuName = "Game/Spell/Icewall")]
public class IceWall : Spell
{
    public IcewallController icewall;

    public override void Initialize()
    {
        icewall.speed = GetSpeed();
        icewall.durationTime = GetDurationTime();
        icewall.maximumCastingDistance = GetMaximumCastingDistance();
        icewall.damage = GetDamage();
    }

    public override void Create(Vector3 position, Quaternion rotation, Vector3 rayOrigin, Vector3 rayDirection)
    {
        icewall.CastIcewallSpell(position, rotation, rayOrigin, rayDirection);
    }
}