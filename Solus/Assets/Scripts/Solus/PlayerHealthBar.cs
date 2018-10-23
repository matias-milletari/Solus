using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] Image healthBar;

    private void Awake()
    {
        PlayerHealth.OnPlayerHurt += ModifyPlayerHealth;
    }

    private void OnDestroy()
    {
        PlayerHealth.OnPlayerHurt -= ModifyPlayerHealth;
    }

    private void ModifyPlayerHealth(float healthPercentage)
    {
        healthBar.fillAmount = healthPercentage;
    }
}
