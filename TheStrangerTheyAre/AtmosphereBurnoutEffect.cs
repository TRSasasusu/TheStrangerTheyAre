using NewHorizons.Utility;
using UnityEngine;

namespace TheStrangerTheyAre;
public class AtmosphereBurnoutEffect : MonoBehaviour
{
    // custom variables
    [SerializeField]
    private GameObject flames;
    [SerializeField]
    private Animator flamesAnim;

    // get nh stuff
    private GameObject revealVol;
    private GameObject atmosphere;
    private GameObject clouds;
    private GameObject hazard;
    private GameObject ashes;
    private GameObject[] travelerAssets = new GameObject[5];

    private bool runOnce;
    void Start()
    {
        runOnce = false; // something for the time loop checking if statement to make sure it doesn't run again

        // gets nh generated objects
        var desert = TheStrangerTheyAre.NewHorizonsAPI.GetPlanet("Sizzling Sands"); // gets the desert planet with nh
        atmosphere = desert.transform.Find("Sector/Atmosphere").gameObject;
        clouds = desert.transform.Find("Sector/Clouds").gameObject;
        hazard = desert.transform.Find("Sector/HazardVolume").gameObject;
        revealVol = desert.transform.Find("Sector/DesertSolution").gameObject;

        // gets arcadia
        travelerAssets[0] = SearchUtilities.Find("SizzlingSands_Body/Sector/Prefab_IP_GhostBird_Desert/Ghostbird_IP_ANIM");
        travelerAssets[1] = SearchUtilities.Find("SizzlingSands_Body/Sector/Prefab_IP_GhostBird_Desert/Prisoner_FocalPoint");
        travelerAssets[2] = SearchUtilities.Find("SizzlingSands_Body/Sector/Prefab_IP_GhostBird_Desert/Collider (2)");
        travelerAssets[3] = SearchUtilities.Find("SizzlingSands_Body/Sector/Prefab_IP_GhostBird_Desert/ConversationZone");
        travelerAssets[4] = SearchUtilities.Find("SizzlingSands_Body/Sector/Prefab_IP_GhostBird_Desert/AudioSource");

        // handle arcadia's ashes
        ashes = SearchUtilities.Find("SizzlingSands_Body/Sector/Props_IP_Ash");
        ashes.SetActive(false);
        revealVol.SetActive(false);
    }

    void Update()
    {
        
        var burnDuration = TimeLoop.GetSecondsElapsed() > 1320;
        var atmosphereBurnt = TimeLoop.GetSecondsElapsed() > 1330;
        var burnEnd = TimeLoop.GetSecondsElapsed() > 1334;


        if (burnDuration && !runOnce)
        {
            flames.SetActive(true);
            flamesAnim.Play("AtmosphereBurn", 0);
            runOnce = true;
        }

        if (atmosphereBurnt)
        {
            foreach (var asset in travelerAssets)
            {
                Destroy(asset);
            }
            ashes.SetActive(true);
            atmosphere.SetActive(false);
            clouds.SetActive(false);
            hazard.SetActive(false);
            revealVol.SetActive(true);
        }

        if (burnEnd)
        {
            //Locator.GetShipLogManager().RevealFact("DESERT_MAIN_ATMO");
            //Locator.GetShipLogManager().RevealFact("DESERT_LIGHT_ATMO");
            flames.SetActive(false);
        }
    }
}
