using UnityEngine;
using OWML.Common;
using NewHorizons.Components.Stars;

namespace TheStrangerTheyAre;
public class AtmosphereBurnoutEffect : MonoBehaviour
{
    // custom variables
    private GameObject atmosphere;
    private GameObject clouds;
    private GameObject hazard;
    private GameObject flames;
    private Animator flamesAnim;
    private bool runOnce;
    private void Awake()
    {
        runOnce = false;

        // gets nh generated objects
        atmosphere = GameObject.Find("SizzlingSands_Body/Sector/Atmosphere");
        clouds = GameObject.Find("SizzlingSands_Body/Sector/Clouds");
        hazard = GameObject.Find("SizzlingSands_Body/Sector/HazardVolume");
        flames = GameObject.Find("SizzlingSands_Body/Sector/AtmosphereBurn");
        flamesAnim = flames.transform.Find("Scale/Animation").gameObject.GetComponent<Animator>();

        flames.SetActive(false);
    }

    private void Update()
    {
        
        var burnDuration = TimeLoop.GetSecondsElapsed() == 1320;
        var atmosphereBurnt = TimeLoop.GetSecondsElapsed() > 1330;
        var burnEnd = TimeLoop.GetSecondsElapsed() > 1334;


        if (burnDuration && !runOnce)
        {
            runOnce = true;
            TheStrangerTheyAre.WriteLine("Atmosphere should be burning...", MessageType.Success); // debug message
            flamesAnim.Play("AtmosphereBurn", 0);
            flames.SetActive(true);
        }

        if (atmosphereBurnt)
        {
            TheStrangerTheyAre.WriteLine("Atmosphere should be finished burning...", MessageType.Success); // debug message
            atmosphere.SetActive(false);
            clouds.SetActive(false);
            hazard.SetActive(false);
            flames.SetActive(false);
        }

        if (burnEnd)
        {
            TheStrangerTheyAre.WriteLine("Effect should be stopping...", MessageType.Success); // debug message
            flames.SetActive(false);
        }
    }
}
