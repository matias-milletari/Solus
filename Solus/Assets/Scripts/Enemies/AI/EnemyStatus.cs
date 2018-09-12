using JetBrains.Annotations;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    private bool isStunned;
    private float stunDuration;
    private float stunTimer;
    private GameObject stunParticle;

    void Update()
    {
        if (IsStunned())
        {
            stunTimer += Time.deltaTime;

            if (stunTimer >= stunDuration)
            {
                SetStun(false, null, null);
            }
        }
    }

    public bool IsStunned()
    {
        return isStunned;
    }

    public void SetStun(bool stunned, float? duration, GameObject particle)
    {
        if (stunned)
        {
            isStunned = true;
            stunDuration = (float)duration;
            stunParticle = particle;
        }
        else
        {
            isStunned = false;
            stunTimer = 0f;
            Destroy(stunParticle);
        }
    }
}