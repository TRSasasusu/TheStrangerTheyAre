using UnityEngine;

namespace TheStrangerTheyAre
{
    public class EyeHandlerTSTA : MonoBehaviour
    {
        GameObject lab; // for the ringed lab at the eye
        GameObject labAndPlanet; // for the ringed lab at the eye with the planet, if prisoner is not met.
        GameObject prisSector; // for the prisoner's music sector
        GameObject campfireSector; // for the prisoner's music sector
        void Awake()
        {
            lab = GameObject.Find("EyeOfTheUniverse_Body/Sector_EyeOfTheUniverse/Sector_Campfire/InstrumentZones/PrisonerZone/PlanetLightController/OriginalPlacement/Prefab_IP_VisiblePlanet/VisiblePlanet_Pivot/visibleplanet_surface/RingedLaboratory_EYE"); // gets the laboratory
            labAndPlanet = GameObject.Find("EyeOfTheUniverse_Body/Sector_EyeOfTheUniverse/Sector_Campfire/InstrumentZones/RingedLaboratory_EYE"); // gets the laboratory and planet
            prisSector = GameObject.Find("EyeOfTheUniverse_Body/Sector_EyeOfTheUniverse/Sector_Campfire/InstrumentZones/PrisonerZone");
            campfireSector = GameObject.Find("EyeOfTheUniverse_Body/Sector_EyeOfTheUniverse/Sector_Campfire");
        }

        void Update()
        {
            // scientist puzzle enabling
            if (prisSector.activeSelf)
            {
                if (Check())
                {
                    lab.SetActive(true);
                }
                else
                {
                    labAndPlanet.SetActive(true);
                }
            } else
            {
                lab.SetActive(false);
            }
        }

        private bool Check()
        {
            return Locator.GetShipLogManager().IsFactRevealed("NEWSIM_SCIENTIST_CLONE");
        }
    }
}