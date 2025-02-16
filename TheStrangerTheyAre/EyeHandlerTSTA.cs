using UnityEngine;
using NewHorizons.Utility;

namespace TheStrangerTheyAre
{
    public class EyeHandlerTSTA : MonoBehaviour
    {
        private GameObject[] leader = new GameObject[3];
        private GameObject[] observatory = new GameObject[4];
        private GameObject[] endingSongs = new GameObject[3];
        private GameObject[] customSignals = new GameObject[2];

        // warp stuff
        private static EyeSpawnPoint campfireSpawn; // to store vessel spawn point
        private static PlayerSpawner _spawner; // for spawning the player
        private bool hasWarped = false;

        void Start()
        {
            leader[0] = SearchUtilities.Find("Vessel_Body/Sector_VesselBridge/Prefab_IP_GhostBird_ScientistDescendant_Vessel2");

            if (leader[0] != null)
            {
                PlayerData.SetPersistentCondition("CYPRESS_BOARDVESSEL", true);
            }

            leader[1] = SearchUtilities.Find("EyeOfTheUniverse_Body/Sector_EyeOfTheUniverse/Prefab_IP_GhostBird_ScientistDescendant_EyeSurface");
            leader[2] = SearchUtilities.Find("EyeOfTheUniverse_Body/Sector_EyeOfTheUniverse/Prefab_IP_GhostBird_ScientistDescendant_Vessel1");

            observatory[0] = SearchUtilities.Find("EyeOfTheUniverse_Body/Sector_EyeOfTheUniverse/Sector_Observatory/SystemModel");
            observatory[1] = SearchUtilities.Find("EyeOfTheUniverse_Body/Sector_EyeOfTheUniverse/Sector_Observatory/Tube_Mineral");
            observatory[2] = SearchUtilities.Find("EyeOfTheUniverse_Body/Sector_EyeOfTheUniverse/Sector_Observatory/Mineral_Sign");
            observatory[3] = SearchUtilities.Find("EyeOfTheUniverse_Body/Sector_EyeOfTheUniverse/Sector_Observatory/System_Sign");

            endingSongs[0] = SearchUtilities.Find("EyeOfTheUniverse_Body/Sector_EyeOfTheUniverse/EyeAudio_Intro1");
            endingSongs[1] = SearchUtilities.Find("EyeOfTheUniverse_Body/Sector_EyeOfTheUniverse/EyeAudio_Intro2");
            endingSongs[2] = SearchUtilities.Find("EYE_SciSector_Audio");

            customSignals[0] = SearchUtilities.Find("EyeOfTheUniverse_Body/Sector_EyeOfTheUniverse/ScientistSector/FirstSignal/Signal/ScientistInstZone1");
            customSignals[1] = SearchUtilities.Find("EyeOfTheUniverse_Body/Sector_EyeOfTheUniverse/ScientistSector/RingedGiant/DivineScientist/ScientistInstZone2");

            if (!Check())
            {
                foreach (GameObject lead in leader)
                {
                    Destroy(lead);
                }
                foreach (GameObject obj in observatory)
                {
                    Destroy(obj);
                }
                foreach (GameObject song in endingSongs)
                {
                    Destroy(song);
                }
                foreach (GameObject signal in customSignals)
                {
                    Destroy(signal);
                }
            }
        }

        public bool IsGathered()
        {
            return DialogueConditionManager.SharedInstance.GetConditionState("EyeScientistGather");
        }

        public void Update()
        {
            if (IsGathered() && !hasWarped)
            {
                SearchUtilities.Find("EyeOfTheUniverse_Body/Sector_EyeOfTheUniverse/Sector_Campfire/Volumes_Campfire/EndlessCylinder_Forest").SetActive(true);

                // teleport the player
                campfireSpawn = SearchUtilities.Find("EyeOfTheUniverse_Body/Sector_EyeOfTheUniverse/Sector_Campfire/QuantumCampfire/SPAWN_Campfire").GetComponent<EyeSpawnPoint>(); // gets campfire spawn point
                _spawner = GameObject.FindGameObjectWithTag("Player").GetRequiredComponent<PlayerSpawner>(); // gets player spawner
                _spawner.DebugWarp(campfireSpawn); // warps you to campfire
                hasWarped = true;
            }
        }

        private bool Check()
        {
            return PlayerData.GetPersistentCondition("CYPRESS_BOARDVESSEL");
        }
    }
}