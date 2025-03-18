using System.Collections;
using UnityEngine;
using OWML.Common;
using NewHorizons.Utility.Files;

namespace TheStrangerTheyAre;
public class CustomItemSocket : OWItemSocket
{
    [SerializeField]
    OWAudioSource audio;
    [SerializeField]
    GameObject effects;
    [SerializeField]
    ElectricityEffect electricEffect;

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
        effects.SetActive(false);
    }

    public override bool PlaceIntoSocket(OWItem item)
    {
        if (base.PlaceIntoSocket(item) && item.GetComponent<CustomItem>().isCloakMineral == true)
        {
            AudioUtilities.SetAudioClip(audio, "assets/Audio/activatemachine.ogg", TheStrangerTheyAre.Instance);
            effects.SetActive(true);
            electricEffect.Play();
            audio.Play();
            activeShard.SetActive(true);
            inactiveShard.SetActive(false);
            return true;
        }
        return false;
    }

    public override OWItem RemoveFromSocket()
    {
        effects.SetActive(false);
        if (audio != null)
        {
            AudioUtilities.SetAudioClip(audio, "assets/Audio/deactivatemachine.ogg", TheStrangerTheyAre.Instance);
            audio.Play();
        }
        OWItem oWItem = base.RemoveFromSocket();
        activeShard.SetActive(false);
        inactiveShard.SetActive(true);
        return oWItem;
    }
}
