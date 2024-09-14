using UnityEngine;

namespace TheStrangerTheyAre
{
    public class EyeHandlerTSTA : MonoBehaviour
    {
        GameObject lab; // for the ringed lab at the eye
        GameObject labAndPlanet; // for the ringed lab at the eye with the planet, if prisoner is not met.
        GameObject prisSector; // for the prisoner's music sector
        GameObject campfireSector; // for the prisoner's music sector

        GameObject scientistDialogueIntro;
        GameObject scientistDialogueAfter;
        GameObject leaderDialogueIntro;
        GameObject leaderDialogueAfter;
        void Awake()
        {
            lab = GameObject.Find("EyeOfTheUniverse_Body/Sector_EyeOfTheUniverse/Sector_Campfire/InstrumentZones/PrisonerZone/PlanetLightController/OriginalPlacement/Prefab_IP_VisiblePlanet/VisiblePlanet_Pivot/visibleplanet_surface/RingedLaboratory_EYE"); // gets the laboratory
            labAndPlanet = GameObject.Find("EyeOfTheUniverse_Body/Sector_EyeOfTheUniverse/Sector_Campfire/InstrumentZones/RingedLaboratory_EYE"); // gets the laboratory and planet
            prisSector = GameObject.Find("EyeOfTheUniverse_Body/Sector_EyeOfTheUniverse/Sector_Campfire/InstrumentZones/PrisonerZone");
            campfireSector = GameObject.Find("EyeOfTheUniverse_Body/Sector_EyeOfTheUniverse/Sector_Campfire");

            lab.SetActive(false);
            labAndPlanet.SetActive(false);
        }

        void Start()
        {
            scientistDialogueIntro = GameObject.Find("Sector_EyeOfTheUniverse/Sector_Campfire/Campsite/Prefab_IP_GhostBird_Scientist_EyeIdle/EYE_ScientistIntro");
            scientistDialogueAfter = GameObject.Find("Sector_EyeOfTheUniverse/Sector_Campfire/Campsite/Prefab_IP_GhostBird_Scientist_EyeIdle/EYE_ScientistAfter");

            leaderDialogueIntro = GameObject.Find("Sector_EyeOfTheUniverse/Sector_Campfire/Campsite/Prefab_IP_GhostBird_ScientistDescendant_Vessel1/EYE_CypressFire");
            leaderDialogueAfter = GameObject.Find("Sector_EyeOfTheUniverse/Sector_Campfire/Campsite/Prefab_IP_GhostBird_ScientistDescendant_Vessel1/EYE_CypressSong");
        }

        void Update()
        {
            // scientist puzzle enabling
            if (Check() && prisSector.activeSelf)
            {
                if (Check2())
                {
                    lab.SetActive(true);
                }
                else
                {
                    labAndPlanet.SetActive(true);
                }
            }
        }

        private bool Check()
        {
            return Locator.GetShipLogManager().IsFactRevealed("NEWSIM_SCIENTIST_CLONE");
        }

        private bool Check2()
        {
            return Locator.GetShipLogManager().IsFactRevealed("IP_SARCOPHAGUS_X4");
        }
    }
}