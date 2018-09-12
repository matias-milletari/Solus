using UnityEngine;
using UnityEngine.UI;

public class SpellCooldown : MonoBehaviour
{
    private Spell spell;
    private Image image;
    private Image darkMask;
    private float coolDownDuration;
    private float nextReadyTime;
    private float coolDownTimeLeft;

    public void Initialize(Spell selectedSpell)
    {
        spell = selectedSpell;
        image = GetComponent<Image>();
        image.sprite = spell.GetIcon();
        darkMask = transform.GetChild(0).GetComponent<Image>();
        darkMask.sprite = spell.GetIcon();
        coolDownDuration = spell.GetCooldown();

        PlayerCasting.OnSpellCasted += SetCooldown;

        spell.Initialize();

        AbilityReady();
    }

    void Update()
    {
        var coolDownComplete = (Time.time > nextReadyTime);

        if (coolDownComplete)
        {
            AbilityReady();
        }
        else
        {
            CoolDown();
        }
    }

    private void AbilityReady()
    {
        darkMask.enabled = false;

        spell.SpellIsReady = true;
    }

    public void CoolDown()
    {
        spell.SpellIsReady = false;

        coolDownTimeLeft -= Time.deltaTime;

        darkMask.fillAmount = (coolDownTimeLeft / coolDownDuration);
    }

    private void SetCooldown(Spell spellCasted)
    {
        if (spell == spellCasted)
        {
            nextReadyTime = coolDownDuration + Time.time;
            coolDownTimeLeft = coolDownDuration;
            darkMask.enabled = true;
        }
    }
}