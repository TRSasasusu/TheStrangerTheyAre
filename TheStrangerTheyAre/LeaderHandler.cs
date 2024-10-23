using NewHorizons.Utility;
using UnityEngine;
using System.Collections;

namespace TheStrangerTheyAre
{
    public class LeaderHandler : MonoBehaviour
    {
        GameObject leaderDialogueIntro; // leader intro dialogue
        GameObject leaderDialogueVision; // leader vision respond remote
        GameObject leaderDialogueAfter; // leader dialogue after vision

        GameObject leader; // leader in the partyhouse
        GameObject vesselLeader; // leader in the vessel
        GameObject vessel; // the vessel itself
        GameObject vesselReveal; // the shiplog condition to reveal vessel
        GameObject music; // final end times music
        Vector3 originalPos; // original position of remote dialogue

        [SerializeField]
        GameObject remoteParent; // temporary parent for remote dialogue
        [SerializeField]
        GameObject[] othersDialogue;

        public const float blinkTime = 0.5f; // constant for blink time
        public const float animTime = blinkTime / 2f; // constant for blink animation time

        void Start()
        {
            leaderDialogueIntro = SearchUtilities.Find("StrangersHomeworld_Body/Sector/EntryAndPartyHouse/Sector_PartyHouse/Ghostbird_NPCs/Prefab_IP_GhostBird_ScientistDescendant/CYPRESS_INTRO");
            leaderDialogueAfter = SearchUtilities.Find("StrangersHomeworld_Body/Sector/EntryAndPartyHouse/Sector_PartyHouse/Ghostbird_NPCs/Prefab_IP_GhostBird_ScientistDescendant/CYPRESS_AFTERVISION");
            leaderDialogueVision = SearchUtilities.Find("StrangersHomeworld_Body/Sector/EntryAndPartyHouse/Sector_PartyHouse/Ghostbird_NPCs/Prefab_IP_GhostBird_ScientistDescendant/CYPRESS_VISION");
            leader = SearchUtilities.Find("StrangersHomeworld_Body/Sector/EntryAndPartyHouse/Sector_PartyHouse/Ghostbird_NPCs/Prefab_IP_GhostBird_ScientistDescendant");
            vesselLeader = SearchUtilities.Find("RingedGiant_Body/Sector/Prefab_IP_GhostBird_ScientistDescendant_Vessel1");
            vessel = SearchUtilities.Find("RingedGiant_Body/Sector/Vessel_Body");
            vesselReveal = SearchUtilities.Find("VESSEL_REVEAL");
            music = SearchUtilities.Find("FinalEndTimes_STR");

            originalPos = leaderDialogueVision.transform.position; // gets original pos of remote dialogue
            leaderDialogueVision.transform.parent = remoteParent.transform; // temporarily parents remote dialogue to object out of reach of player
        }

        private IEnumerator Blink()
        {
            var cameraEffectController = FindObjectOfType<PlayerCameraEffectController>(); // gets camera controller

            // close eyes
            cameraEffectController.CloseEyes(animTime); // closes eyes
            yield return new WaitForSeconds(animTime);  // waits until animation stops to proceed to next line
            GlobalMessenger.FireEvent("PlayerBlink"); // fires an event for the player blinking

            // destroy leader
            leader.SetActive(false);
            vesselLeader.SetActive(true);

            // open eyes
            cameraEffectController.OpenEyes(animTime, false); // open eyes
            yield return new WaitForSeconds(animTime); //  waits until animation stops to proceed to next line
        }

        void Update()
        {
            if (!Check() && Check2())
            {
                leaderDialogueAfter.SetActive(true);
                leaderDialogueIntro.SetActive(false);
                leaderDialogueVision.transform.position = originalPos; // sets back to original pos
                if (Check4())
                {
                    Destroy(leaderDialogueVision);
                }
                foreach (var item in othersDialogue)
                {
                    item.SetActive(true);
                }
            }
            else
            {
                leaderDialogueAfter.SetActive(false);
                leaderDialogueIntro.SetActive(true);
                leaderDialogueVision.transform.position = Vector3.zero; // sets pos relative to parent
                foreach (var item in othersDialogue)
                {
                    item.SetActive(false);
                }
            }

            if (vessel != null)
            {
                music.SetActive(true);
                Destroy(leaderDialogueVision);
                if (Check3())
                {
                    StartCoroutine(Blink());
                }
                else
                {
                    leader.SetActive(true);
                    vesselLeader.SetActive(false);
                }
            } else
            {
                Destroy(vesselReveal);
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
            return DialogueConditionManager.SharedInstance.GetConditionState("HOME_LEADER_E");
        }

        private bool Check3()
        {
            return DialogueConditionManager.SharedInstance.GetConditionState("CYPRESS_BOARDVESSEL");
        }
    }
}