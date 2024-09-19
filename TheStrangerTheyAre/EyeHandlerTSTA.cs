using UnityEngine;

namespace TheStrangerTheyAre
{
    public class EyeHandlerTSTA : MonoBehaviour
    {
        GameObject leaderDialogueIntro;
        GameObject leaderDialogueAfter;
        void Awake()
        {

        }

        void Start()
        {
            leaderDialogueIntro = GameObject.Find("Sector_EyeOfTheUniverse/Sector_Campfire/Campsite/Prefab_IP_GhostBird_ScientistDescendant_Vessel1/EYE_CypressFire");
            leaderDialogueAfter = GameObject.Find("Sector_EyeOfTheUniverse/Sector_Campfire/Campsite/Prefab_IP_GhostBird_ScientistDescendant_Vessel1/EYE_CypressSong");

            leaderDialogueAfter.SetActive(false);
            leaderDialogueIntro.SetActive(true);
        }

        private bool Check()
        {
            return Locator.GetShipLogManager().IsFactRevealed("NEWSIM_SCIENTIST_CLONE");
        }
    }
}