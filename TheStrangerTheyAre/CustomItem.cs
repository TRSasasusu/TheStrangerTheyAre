using System.EnterpriseServices.Internal;
using UnityEngine;

namespace TheStrangerTheyAre;

public class CustomItem : OWItem
{
    // variables
    public bool isCloakMineral;
    protected bool isRotating;

    // dunno why this serialize field is here, but gonna keep it on the safe side
    [SerializeField]

    public override void Awake()
    {
        _type = ItemType.Scroll;
        isCloakMineral = true;
        isRotating = true;
        base.Awake();
        SetOffset();
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
        isRotating = true;
        base.DropItem(position, normal, parent, sector, customDropTarget);
        SetOffset();
    }

    public void SetOffset(){
        Vector3 euler = this.transform.eulerAngles;
        euler.z =- 90f;
        this.transform.eulerAngles = euler;
    }

    public override void PickUpItem(Transform holdTranform)
    {
        isRotating = false;
        base.PickUpItem(holdTranform);
    }

    public override void UpdateCollisionLOD()
    {
        base.UpdateCollisionLOD();
    }
}
