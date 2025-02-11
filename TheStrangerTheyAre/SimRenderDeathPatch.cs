using HarmonyLib;
using NewHorizons.Utility;

namespace TheStrangerTheyAre
{
   [HarmonyPatch]
   public class SimRendererDeathPatch
    {
        [HarmonyPrefix]
        [HarmonyPatch(typeof(DeathManager), nameof(DeathManager.KillPlayer))]
        public static bool DeathManager_KillPlayer_Prefix()
        {

            // this if statement will run if
            // - the player is currently in the solar system
            // - the player has entered the volume in which the simulation sector is manually set
            // - the player has slept at the campfire at angler's eye

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
            // - the player has slept at the campfire at angler's eye

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
