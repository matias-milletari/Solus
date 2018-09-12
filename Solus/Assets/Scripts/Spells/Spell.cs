using UnityEngine;

public class Spell : ScriptableObject
{
    [SerializeField] float speed;
    [SerializeField] float cooldown;
    [SerializeField] float durationTime;
    [SerializeField] float damage;
    [SerializeField] float maximumCastingDistance;
    [SerializeField] Sprite icon;
    [SerializeField] SpellCastType castType;
    [SerializeField] bool isProjectile;
    [SerializeField] bool hasPreview;
    [SerializeField] GameObject previewPrefab;
    [SerializeField] LayerMask floorLayerMask;

    private GameObject activeSpellPreview;

    public enum SpellCastType
    {
        OneHandCast,
        TwoHandCast1,
        TwoHandCast2
    }

    public float GetDurationTime()
    {
        return durationTime;
    }

    public float GetSpeed()
    {
        return speed;
    }

    public float GetCooldown()
    {
        return cooldown;
    }

    public float GetDamage()
    {
        return damage;
    }

    public float GetMaximumCastingDistance()
    {
        return maximumCastingDistance;
    }

    public Sprite GetIcon()
    {
        return icon;
    }

    public SpellCastType GetCastType()
    {
        return castType;
    }

    public bool GetIsProjectile()
    {
        return isProjectile;
    }

    public bool GetHasPreview()
    {
        return hasPreview;
    }

    public GameObject GetPreviewPrefab()
    {
        return previewPrefab;
    }

    public bool SpellIsReady { get; set; }

    public virtual void Initialize() { }

    public virtual void Create(Vector3 rayOrigin, Vector3 rayDirection)
    {
        if (PreviewIsActive())
        {
            Create(activeSpellPreview.transform.position, activeSpellPreview.transform.rotation, rayOrigin, rayDirection);
        }
    }

    public virtual void Create(Vector3 castingPosition, Vector3 rayOrigin, Vector3 rayDirection) { }

    public virtual void Create(Vector3 previewPosition, Quaternion previewRotation, Vector3 rayOrigin, Vector3 rayDirection) { }

    public virtual void PreviewSpell(Quaternion rotation, Vector3 rayOrigin, Vector3 rayDirection)
    {
        if (GetIsProjectile() || !GetHasPreview()) return;

        CreateSpellPreview();

        RaycastHit rayCastHit;

        if (Physics.Raycast(rayOrigin, rayDirection, out rayCastHit, maximumCastingDistance, floorLayerMask))
        {
            ShowSpellPreview(rayCastHit.point, rotation);
        }
        else
        {
            HideSpellPreview();
        }
    }

    private void CreateSpellPreview()
    {
        if (activeSpellPreview != null) return;

        activeSpellPreview = Instantiate(GetPreviewPrefab());

        activeSpellPreview.SetActive(false);
    }

    public void ShowSpellPreview(Vector3 position, Quaternion rotation)
    {
        if (!GetHasPreview()) return;

        activeSpellPreview.transform.position = position;
        activeSpellPreview.transform.rotation = rotation;

        activeSpellPreview.SetActive(true);
    }

    public void HideSpellPreview()
    {
        if (GetHasPreview()) activeSpellPreview.SetActive(false);
    }

    public bool PreviewIsActive()
    {
        if (GetHasPreview())
        {
            return activeSpellPreview != null && activeSpellPreview.activeInHierarchy;
        }

        return true;
    }
}
