using UnityEngine;

namespace TheStrangerTheyAre;

public class SkullItem : OWItem
{
    // variables
    public bool isSkull;
    protected bool isRotating;

    // dunno why this serialize field is here, but gonna keep it on the safe side
    [SerializeField]

    public override void Awake()
    {
        _type = ItemType.ConversationStone;
        isSkull = true;
        isRotating = true;
        base.Awake();
    }

    private void Start()
    {
        base.enabled = false;
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
    }

    public override string GetDisplayName()
    {
        return "Skull";
    }

    public override void DropItem(Vector3 position, Vector3 normal, Transform parent, Sector sector, IItemDropTarget customDropTarget)
    {
        isRotating = true;
        base.DropItem(position, normal, parent, sector, customDropTarget);
        Vector3 defaultScale = new Vector3(1, 1, 1);
        base.transform.localScale = defaultScale;
    }
    public override void PickUpItem(Transform holdTranform)
    {
        isRotating = false;
        base.PickUpItem(holdTranform);
        Vector3 smallerScale = new Vector3(0.3f, 0.3f, 0.3f);
        base.transform.localScale = smallerScale;
    }

    public override void UpdateCollisionLOD()
    {
        base.UpdateCollisionLOD();
    }
}
