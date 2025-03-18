using UnityEngine;
using NewHorizons.Utility;
using HarmonyLib;

namespace TheStrangerTheyAre
{
    [HarmonyPatch]
    public class LoadSimVol : MonoBehaviour
    {
        // variables
        private PlayerSectorDetector player; // for storing the player's sector detector
        private Sector simSector; // for storing the sim sector
        private Sector dz4Sector; // for storing the dz4 sector
        private DreamCampfireStreaming dz4Stream; // for storing the dream campfire streaming script
        public bool hasEnteredVolume;

        void Start()
        {
            hasEnteredVolume = false;
            player = Locator.GetPlayerSectorDetector(); // get player's sector detector
            GlobalMessenger.AddListener("ExitDreamWorld", OnExitDreamWorld); // checks if player leaves the sim
            simSector = SearchUtilities.Find("DreamWorld_Body/Sector_DreamWorld").GetComponent<Sector>(); // get sim root sector
            dz4Sector = SearchUtilities.Find("DreamWorld_Body/Sector_DreamWorld/Sector_DreamZone_4").GetComponent<Sector>(); // get dz4 root sector
            dz4Stream = SearchUtilities.Find("RingWorld_Body/Sector_RingInterior/Sector_Zone4/PrisonDocks/Sector_PrisonInterior/Interactibles_PrisonInterior/Prefab_IP_DreamCampfire/DreamCampfire_Streaming").GetComponent<DreamCampfireStreaming>(); // get dream campfire streaming script
        }

        public virtual void OnTriggerEnter(Collider hitCollider)
        {
            //checks if player collides with the trigger volume
            if (hitCollider.CompareTag("PlayerDetector") && enabled)
            {
                hasEnteredVolume = true;
                simSector.AddOccupant(player); // adds player to sim root sector
                dz4Sector.AddOccupant(player); // adds player to sim dz4 sector
                dz4Stream.UpdatePreloadingState(true); // runs the streamer for dz4 manually
            }
        }

        void OnExitDreamWorld()
        {
            hasEnteredVolume = false;
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(DeathManager), nameof(DeathManager.KillPlayer))]
        public static bool DeathManager_KillPlayer_Prefix()
        {
            // this if statement will run if
            // - the player is currently in the solar system
            // - the player has entered the volume in which the simulation sector is manually set

            if (TheStrangerTheyAre.NewHorizonsAPI.GetCurrentStarSystem() == "SolarSystem"
                && SearchUtilities.Find("PreBramble_Body/Sector/PreBramble_SIM/TravelHouse/Interactables/SimSetupTrigger").GetComponent<LoadSimVol>().hasEnteredVolume == true)
            {
                // removes the player from the dreamzone4 sectors if the if statement holds true
                SearchUtilities.Find("DreamWorld_Body/Sector_DreamWorld").GetComponent<Sector>().RemoveOccupant(Locator.GetPlayerSectorDetector());
                SearchUtilities.Find("DreamWorld_Body/Sector_DreamWorld/Sector_DreamZone_4").GetComponent<Sector>().RemoveOccupant(Locator.GetPlayerSectorDetector());
                SearchUtilities.Find("RingWorld_Body/Sector_RingInterior/Sector_Zone4/PrisonDocks/Sector_PrisonInterior/Interactibles_PrisonInterior/Prefab_IP_DreamCampfire/DreamCampfire_Streaming").GetComponent<DreamCampfireStreaming>().UpdatePreloadingState(false); // unloads dz4 streaming if player leaves sim
            }
            return true;
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(DreamWorldController), nameof(DreamWorldController.ExitDreamWorld), new[] { typeof(DreamWakeType) })]
        public static bool DreamWorldController_ExitDreamWorld_Prefix()
        {
            // this if statement will run if
            // - the player is currently in the solar system
            // - the player has entered the volume in which the simulation sector is manually set

            if (TheStrangerTheyAre.NewHorizonsAPI.GetCurrentStarSystem() == "SolarSystem"
                && SearchUtilities.Find("PreBramble_Body/Sector/PreBramble_SIM/TravelHouse/Interactables/SimSetupTrigger").GetComponent<LoadSimVol>().hasEnteredVolume == true)
            {
                // removes the player from the dreamzone4 sectors if the if statement holds true
                SearchUtilities.Find("DreamWorld_Body/Sector_DreamWorld").GetComponent<Sector>().RemoveOccupant(Locator.GetPlayerSectorDetector());
                SearchUtilities.Find("DreamWorld_Body/Sector_DreamWorld/Sector_DreamZone_4").GetComponent<Sector>().RemoveOccupant(Locator.GetPlayerSectorDetector());
                SearchUtilities.Find("RingWorld_Body/Sector_RingInterior/Sector_Zone4/PrisonDocks/Sector_PrisonInterior/Interactibles_PrisonInterior/Prefab_IP_DreamCampfire/DreamCampfire_Streaming").GetComponent<DreamCampfireStreaming>().UpdatePreloadingState(false); // unloads dz4 streaming if player leaves sim
            }
            return true;
        }
    }
}