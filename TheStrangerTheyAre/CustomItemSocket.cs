using System.Collections;
using UnityEngine;
using OWML.Common;

namespace TheStrangerTheyAre;
public class CustomItemSocket : OWItemSocket
{
    GameObject activeShard;
    GameObject inactiveShard;
   public override void Awake()
    {
        base.Awake();
        _acceptableType = ItemType.Scroll;
        activeShard = GameObject.Find("ENIGMA_SHARD_WEAK"); // finds the shard that should be active upon inserting the mineral
        inactiveShard = GameObject.Find("ENIGMA_SHARD"); // finds the shard that should be active upon removing the mineral
        activeShard.SetActive(false);
        inactiveShard.SetActive(true);
    }

    public override bool PlaceIntoSocket(OWItem item)
    {
        if (base.PlaceIntoSocket(item) && item.GetComponent<CustomItem>().isCloakMineral == true)
        {
            activeShard.SetActive(true);
            inactiveShard.SetActive(false);
            return true;
        }
        return false;
    }

    public override OWItem RemoveFromSocket()
    {
        OWItem oWItem = base.RemoveFromSocket();
        activeShard.SetActive(false);
        inactiveShard.SetActive(true);
        return oWItem;
    }
}
