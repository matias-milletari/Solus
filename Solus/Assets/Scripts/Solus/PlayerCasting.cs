using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerCasting : MonoBehaviour
{
    public Transform castingPoint;
    public Transform spellOrbPosition;
    public List<Spell> spells;
    public LayerMask floorLayerMask;

    public delegate void SpellCastedHandler(Spell spell);
    public static event SpellCastedHandler OnSpellCasted;

    private InputManager playerInput;
    private Spell activeSpell;
    private Animator playerAnimator;
    private int selectedSpell;
    private Camera mainCamera;

    private void Awake()
    {
        playerInput = GetComponent<InputManager>();
        playerAnimator = GetComponent<Animator>();
        mainCamera = Camera.main;

        activeSpell = spells.First();
    }

    private void Start()
    {
        SelectSpell();
    }

    private void Update()
    {
        if (playerInput.NumberOneKey)
        {
            selectedSpell = 0;

            SelectSpell();
        }

        if (playerInput.NumberTwoKey)
        {
            selectedSpell = 1;

            SelectSpell();
        }

        if (playerInput.NumberThreeKey)
        {
            selectedSpell = 2;

            SelectSpell();
        }

        if (playerInput.NumberFourKey)
        {
            selectedSpell = 3;

            SelectSpell();
        }

        if (activeSpell.SpellIsReady)
        {
            activeSpell.PreviewSpell(gameObject.transform.rotation, mainCamera.transform.position, mainCamera.transform.forward);
        }

        if (playerInput.MouseLeftClick && activeSpell.SpellIsReady)
        {
            if (activeSpell.PreviewIsActive())
            {
                CastSpell();
            }
        }
    }

    void SelectSpell()
    {
        if (activeSpell == spells[selectedSpell]) return;

        if (activeSpell != null)
        {
            activeSpell.HideSpellPreview();
        }

        activeSpell = spells[selectedSpell];

        activeSpell.Initialize();
    }

    private void CastSpell()
    {
        switch (activeSpell.GetCastType())
        {
            case Spell.SpellCastType.OneHandCast:
                playerAnimator.SetTrigger("OneHandCast");
                break;

            case Spell.SpellCastType.TwoHandCast1:
                playerAnimator.SetTrigger("TwoHandCast1");
                break;

            case Spell.SpellCastType.TwoHandCast2:
                playerAnimator.SetTrigger("TwoHandCast2");
                break;
        }

        if (OnSpellCasted != null) OnSpellCasted(activeSpell);
    }

    /// <summary>
    /// Esta función es llamada al finalizar las animaciones de casteo de hechizos.
    /// </summary>
    void InstantiateSpell()
    {
        if (activeSpell.GetIsProjectile())
        {
            activeSpell.Create(castingPoint.position, mainCamera.transform.position, mainCamera.transform.forward);
        }
        else if (activeSpell.GetHasPreview())
        {
            activeSpell.Create(mainCamera.transform.position, mainCamera.transform.forward);

            activeSpell.HideSpellPreview();
        }
    }

    public Spell GetActiveSpell()
    {
        return activeSpell;
    }
}