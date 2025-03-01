using System;
using UnityEngine;

namespace TheStrangerTheyAre;
public class Seals : OWItem
{
    [SerializeField]
    public int sealID;
    [SerializeField]
    private string sealName;

    public override void Awake()
    {
        base.Awake();
        _type = ItemType.SharedStone;
    }

    public override string GetDisplayName()
    {
        return String.Format(TheStrangerTheyAre.NewHorizonsAPI.GetTranslationForOtherText("SealDisplayName"), TheStrangerTheyAre.NewHorizonsAPI.GetTranslationForUI(sealName));
    }

    public override void DropItem(Vector3 position, Vector3 normal, Transform parent, Sector sector, IItemDropTarget customDropTarget)
    {
        base.DropItem(position, normal, parent, sector, customDropTarget);
    }

    public override void PickUpItem(Transform holdTranform)
    {
        base.PickUpItem(holdTranform);
    }

    public override void SocketItem(Transform socketTransform, Sector sector)
    {
        base.SocketItem(socketTransform, sector);
    }
}
