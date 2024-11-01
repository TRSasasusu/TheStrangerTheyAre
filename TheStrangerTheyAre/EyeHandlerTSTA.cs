using UnityEngine;
using NewHorizons.Utility;

namespace TheStrangerTheyAre
{
    public class EyeHandlerTSTA : MonoBehaviour
    {
        GameObject leaderDialogueIntro;
        GameObject leaderDialogueAfter;
        GameObject[] leader;

        void Start()
        {
            leaderDialogueIntro = SearchUtilities.Find("Sector_EyeOfTheUniverse/Sector_Campfire/Campsite/Prefab_IP_GhostBird_ScientistDescendant_Vessel1/EYE_CypressFire");
            leaderDialogueAfter = SearchUtilities.Find("Sector_EyeOfTheUniverse/Sector_Campfire/Campsite/Prefab_IP_GhostBird_ScientistDescendant_Vessel1/EYE_CypressSong");
            leader[0] = SearchUtilities.Find("Sector_VesselBridge/Prefab_IP_GhostBird_ScientistDescendant_Vessel2");
            leader[1] = SearchUtilities.Find("Sector_EyeOfTheUniverse/Prefab_IP_GhostBird_ScientistDescendant_EyeSurface");
            leader[2] = SearchUtilities.Find("Sector_EyeOfTheUniverse/Sector_Campfire/Campsite/Prefab_IP_GhostBird_ScientistDescendant_Vessel1");

            if (Check())
            {
                leaderDialogueAfter.SetActive(false);
                leaderDialogueIntro.SetActive(true);
            } else
            {
                foreach (GameObject lead in leader)
                {
                    Destroy(lead);
                }
            }
        }

        private bool Check()
        {
            return PlayerData.GetPersistentCondition("CYPRESS_BOARDVESSEL");
        }
    }
}