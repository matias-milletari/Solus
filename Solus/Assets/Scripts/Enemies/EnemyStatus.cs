using Panda;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    private bool isStunned;
    private float stunDuration;
    private float stunTimer;
    private GameObject stunParticle;

    [Task]
    public bool IsStunned()
    {
        return isStunned;
    }

    [Task]
    public void ApplyStun()
    {
        if (isStunned)
        {
            isStunned = true;
        }

        Task.current.Succeed();
    }

    [Task]
    public void StunCooldown()
    {
        stunTimer += Time.deltaTime;

        Task.current.Succeed();
    }

    [Task]
    public void RemoveStun()
    {
        if (stunTimer >= stunDuration)
        {
            isStunned = false;
            stunTimer = 0f;
            Destroy(stunParticle);
        }

        Task.current.Succeed();
    }

    public void SetStun()
    {
        isStunned = true;
    }
}