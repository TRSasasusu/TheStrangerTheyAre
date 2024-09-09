using System.EnterpriseServices.Internal;
using UnityEngine;

namespace TheStrangerTheyAre;

public class CustomItem : OWItem
{
    // variables
    public bool isCloakMineral;

    // dunno why this serialize field is here, but gonna keep it on the safe side
    [SerializeField]

    public override void Awake()
    {
        _type = ItemType.Scroll;
        isCloakMineral = true;
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
        return "Mineral";
    }

    public override void DropItem(Vector3 position, Vector3 normal, Transform parent, Sector sector, IItemDropTarget customDropTarget)
    {
        base.DropItem(position, normal, parent, sector, customDropTarget);
    }

    public override void PickUpItem(Transform holdTranform)
    {
        base.PickUpItem(holdTranform);
    }

    public override void UpdateCollisionLOD()
    {
        base.UpdateCollisionLOD();
    }
}
