using NewHorizons.Utility;
using UnityEngine;

namespace TheStrangerTheyAre
{
    public class LeaderHandler : MonoBehaviour
    {
        GameObject leaderDialogueIntro;
        GameObject leaderDialogueVision;
        GameObject leaderDialogueAfter;

        GameObject leader;
        GameObject vesselLeader;
        GameObject vessel;
        GameObject music;

        void Start()
        {
            leaderDialogueIntro = SearchUtilities.Find("StrangersHomeworld_Body/Sector/EntryAndPartyHouse/Sector_PartyHouse/Ghostbird_NPCs/Prefab_IP_GhostBird_ScientistDescendant/CYPRESS_INTRO");
            leaderDialogueAfter = SearchUtilities.Find("StrangersHomeworld_Body/Sector/EntryAndPartyHouse/Sector_PartyHouse/Ghostbird_NPCs/Prefab_IP_GhostBird_ScientistDescendant/CYPRESS_AFTERVISION");
            leaderDialogueVision = SearchUtilities.Find("StrangersHomeworld_Body/Sector/EntryAndPartyHouse/Sector_PartyHouse/Ghostbird_NPCs/Prefab_IP_GhostBird_ScientistDescendant/CYPRESS_VISION");
            leader = SearchUtilities.Find("StrangersHomeworld_Body/Sector/EntryAndPartyHouse/Sector_PartyHouse/Ghostbird_NPCs/Prefab_IP_GhostBird_ScientistDescendant");
            vesselLeader = SearchUtilities.Find("RingedGiant_Body/Sector/Prefab_IP_GhostBird_ScientistDescendant_Vessel1");
            vessel = SearchUtilities.Find("RingedGiant_Body/Sector/Vessel_Body");
            music = SearchUtilities.Find("FinalEndTimes_STR");
        }

        void Update()
        {
            if (vessel != null)
            {
                music.SetActive(true);
                if (!Check() && Check2())
                {
                    leaderDialogueVision.SetActive(false);
                    leaderDialogueIntro.SetActive(true);
                    Destroy(leaderDialogueAfter);
                }
                if (Check3())
                {
                    leader.SetActive(false);
                    vesselLeader.SetActive(true);
                }
                else
                {
                    leader.SetActive(true);
                    vesselLeader.SetActive(false);
                }
            } else
            {
                if (!Check() && Check2())
                {
                    leaderDialogueVision.SetActive(true);
                    leaderDialogueIntro.SetActive(false);
                    if (Check4())
                    {
                        Destroy(leaderDialogueAfter);
                    }
                }
                else
                {
                    leaderDialogueVision.SetActive(false);
                    leaderDialogueIntro.SetActive(true);
                }
                music.SetActive(false);
            }
        }

        private bool Check()
        {
            return PlayerState.IsViewingProjector();
        }
        private bool Check2()
        {
            return Locator.GetShipLogManager().IsFactRevealed("HOME_VISION");
        }

        private bool Check4()
        {
            return DialogueConditionManager.SharedInstance.GetConditionState("CYPRESS_MET");
        }

        private bool Check3()
        {
            return DialogueConditionManager.SharedInstance.GetConditionState("CYPRESS_BOARDVESSEL");
        }
    }
}