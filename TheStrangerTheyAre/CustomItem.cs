using System;
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
        return String.Format(TheStrangerTheyAre.NewHorizonsAPI.GetTranslationForOtherText("MineralDisplayName"), TheStrangerTheyAre.NewHorizonsAPI.GetTranslationForUI("Mineral")); ;
    }

    public override void DropItem(Vector3 position, Vector3 normal, Transform parent, Sector sector, IItemDropTarget customDropTarget)
    {
        isRotating = true;
        base.DropItem(position, normal, parent, sector, customDropTarget);
    }
    public override void PickUpItem(Transform holdTranform)
    {
        Locator.GetShipLogManager().RevealFact("ANGLERS_EYE_MINE_MINERAL");
        isRotating = false;
        base.PickUpItem(holdTranform);
    }

    public override void UpdateCollisionLOD()
    {
        base.UpdateCollisionLOD();
    }
}
