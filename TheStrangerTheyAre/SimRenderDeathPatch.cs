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
            if (TheStrangerTheyAre.NewHorizonsAPI.GetCurrentStarSystem() == "SolarSystem")
            {
                SearchUtilities.Find("DreamWorld_Body/Sector_DreamWorld").GetComponent<Sector>().RemoveOccupant(Locator.GetPlayerSectorDetector());
                SearchUtilities.Find("DreamWorld_Body/Sector_DreamWorld/Sector_DreamZone_4").GetComponent<Sector>().RemoveOccupant(Locator.GetPlayerSectorDetector());
                SearchUtilities.Find("RingWorld_Body/Sector_RingInterior/Sector_Zone4/PrisonDocks/Sector_PrisonInterior/Interactibles_PrisonInterior/Prefab_IP_DreamCampfire/DreamCampfire_Streaming").GetComponent<DreamCampfireStreaming>().UpdatePreloadingState(false); // unloads dz4 streaming if player leaves sim
            }
            return true;
        }
    }
}
