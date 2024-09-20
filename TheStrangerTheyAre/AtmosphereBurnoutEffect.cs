using UnityEngine;
using OWML.Common;
using System.CodeDom;

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
    void Start()
    {
        runOnce = false; // something for the time loop checking if statement to make sure it doesn't run again

        // gets nh generated objects
        var desert = TheStrangerTheyAre.NewHorizonsAPI.GetPlanet("Sizzling Sands"); // gets the desert planet with nh
        atmosphere = desert.transform.Find("Sector/Atmosphere").gameObject;
        clouds = desert.transform.Find("Sector/Clouds").gameObject;
        hazard = desert.transform.Find("Sector/HazardVolume").gameObject;
        flames = desert.transform.Find("Sector/AtmosphereBurn").gameObject;
        flamesAnim = flames.transform.Find("Scale/Animation").gameObject.GetComponent<Animator>();
        flames.SetActive(false);
    }

    void Update()
    {
        
        var burnDuration = TimeLoop.GetSecondsElapsed() == 1320;
        var atmosphereBurnt = TimeLoop.GetSecondsElapsed() > 1330;
        var burnEnd = TimeLoop.GetSecondsElapsed() > 1334;


        if (burnDuration && !runOnce)
        {
            runOnce = true;
            TheStrangerTheyAre.WriteLine("Atmosphere should be burning...", MessageType.Success); // debug message
            flames.SetActive(true);
            flamesAnim.Play("AtmosphereBurn", 0);
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
