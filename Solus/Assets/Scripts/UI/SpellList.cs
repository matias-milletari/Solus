using UnityEngine;

public class SpellList : MonoBehaviour
{
    public GameObject spellIconPrefab;

    void Start()
    {
        var spells = PlayerController.instance.GetComponent<PlayerCasting>().spells;

        foreach (var spell in spells)
        {
            AddSpell(spell);
        }
    }

    void AddSpell(Spell spell)
    {
        var spellIconClone = Instantiate(spellIconPrefab, transform);

        spellIconClone.GetComponentInChildren<SpellCooldown>().Initialize(spell);
    }
}