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
        switch (sealName)
        {
            case "Sizzling Sands":
                Locator.GetShipLogManager().RevealFact("DESERT_SEAL_E");
                break;
            case "Ringed Giant":
                Locator.GetShipLogManager().RevealFact("RING_SEAL_E");
                break;
            case "Velvet Vortex":
                Locator.GetShipLogManager().RevealFact("CRIMSON_SEAL_E");
                break;
            case "Burning Bombardier":
                Locator.GetShipLogManager().RevealFact("VOLCANO_SEAL_E");
                break;
            case "Distant Enigma":
                Locator.GetShipLogManager().RevealFact("QUANTUM_SEAL_E");
                break;
            default:
                break;
        }
    }

    public override void SocketItem(Transform socketTransform, Sector sector)
    {
        base.SocketItem(socketTransform, sector);
    }
}
