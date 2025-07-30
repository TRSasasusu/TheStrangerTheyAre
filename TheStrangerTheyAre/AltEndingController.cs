using UnityEngine;
using NewHorizons.Utility;
using static NewHorizons.External.Modules.SpawnModule;
using System.Collections;

namespace TheStrangerTheyAre
{
    public class AltEndingController : MonoBehaviour
    {
        // declare variables
        GameObject cypressVessel;
        GameObject endTimesAudio;
        GameObject vesselDiscoverAudio;
        GameObject cypressDialogue;

        // warp stuff again
        protected PlayerSpawner _spawner; // for spawning the player
        private bool hasWarped; // sets boolean to make sure warp doesn't happen repeatedly on update.
        public const float blinkTime = 0.5f; // constant for blink time
        public const float animTime = blinkTime / 2f; // constant for blink animation time

        void Start()
        {
            // define variables
            cypressVessel = SearchUtilities.Find("Sector_VesselDimension/Prefab_IP_GhostBird_ScientistDescendant_Vessel1");
            cypressDialogue = SearchUtilities.Find("Sector_VesselDimension/Prefab_IP_GhostBird_ScientistDescendant_Vessel1");
            endTimesAudio = SearchUtilities.Find("Sector_VesselDimension/Sector_VesselBridge/FinalEndTimes_ALTEND");
            vesselDiscoverAudio = SearchUtilities.Find("Sector_VesselDimension/Volumes_VesselDimension/VesselDiscoveryMusicTrigger");
            hasWarped = false;

            if (Check()) {
                // destroy vessel discover audio
                Destroy(vesselDiscoverAudio);
            } else
            {
                // destroy all altending objects
                Destroy(endTimesAudio);
                //Destroy(cypressVessel);
            }
        }

        void Update()
        {
            if (ReadyToWarpToSim() && !hasWarped)
            {
                Destroy(cypressDialogue);
                StartCoroutine(Blink());
            }
        }

        private IEnumerator Blink()
        {
            hasWarped = true; // sets has warped to true so this doesn't run constantly (because it's being called in update)
            var cameraEffectController = FindObjectOfType<PlayerCameraEffectController>(); // gets camera controller

            // close eyes
            cameraEffectController.CloseEyes(animTime); // closes eyes
            yield return new WaitForSeconds(animTime);  // waits until animation stops to proceed to next line
            GlobalMessenger.FireEvent("PlayerBlink"); // fires an event for the player blinking

            // warp to dreamfire
            _spawner = GameObject.FindGameObjectWithTag("Player").GetRequiredComponent<PlayerSpawner>(); // gets player spawner
            SpawnPoint dreamSpawn = SearchUtilities.Find("Spawn_IP_Zone_3_DreamFire").GetComponent<SpawnPoint>();
            _spawner.DebugWarp(dreamSpawn); // warps you to the dream campfire
            OWItem item = SearchUtilities.Find("Sector_RingWorld/Sector_SecretEntrance/Interactibles_SecretEntrance/Prefab_IP_DreamLanternItem_2").GetComponent<OWItem>(); // get artifact
            Locator.GetToolModeSwapper().GetItemCarryTool().PickUpItemInstantly(item); // gives player the artifact

            yield return new WaitForSeconds(animTime);  // waits until animation stops to proceed to next line
            _spawner.DebugWarp(dreamSpawn); // warps you again because dark bramble is weird with spawnpoints
            yield return new WaitForSeconds(1.5f);  // waits until animation stops to proceed to next line

            // open eyes
            cameraEffectController.OpenEyes(animTime, false); // open eyes
            yield return new WaitForSeconds(animTime); //  waits until animation stops to proceed to next line
        }

        private bool Check()
        {
            return PlayerData.GetPersistentCondition("CYPRESS_BOARDVESSEL");
        }

        private bool ReadyToWarpToSim()
        {
            return DialogueConditionManager.SharedInstance.GetConditionState("CYPRESS_TOSIM");
        }
    }
}