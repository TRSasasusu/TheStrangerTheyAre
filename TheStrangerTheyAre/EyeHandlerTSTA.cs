using UnityEngine;
using NewHorizons.Utility;

namespace TheStrangerTheyAre
{
    public class EyeHandlerTSTA : MonoBehaviour
    {
        private GameObject[] leader = new GameObject[3];
        private GameObject[] observatory = new GameObject[4];
        private GameObject scientist;
        //private GameObject scientistZone;
        //private GameObject scientistSignal;
        private Animator scientistAnim;

        // warp stuff
        private static EyeSpawnPoint campfireSpawn; // to store vessel spawn point
        private static PlayerSpawner _spawner; // for spawning the player
        private bool hasWarped = false;

        void Start()
        {
            PlayerData.SetPersistentCondition("CYPRESS_BOARDVESSEL", true); // debug, remove when done!

            leader[0] = SearchUtilities.Find("Vessel_Body/Sector_VesselBridge/Prefab_IP_GhostBird_ScientistDescendant_Vessel2");
            leader[1] = SearchUtilities.Find("EyeOfTheUniverse_Body/Sector_EyeOfTheUniverse/Prefab_IP_GhostBird_ScientistDescendant_EyeSurface");
            leader[2] = SearchUtilities.Find("EyeOfTheUniverse_Body/Sector_EyeOfTheUniverse/Prefab_IP_GhostBird_ScientistDescendant_Vessel1");

            observatory[0] = SearchUtilities.Find("EyeOfTheUniverse_Body/Sector_EyeOfTheUniverse/Sector_Observatory/SystemModel");
            observatory[1] = SearchUtilities.Find("EyeOfTheUniverse_Body/Sector_EyeOfTheUniverse/Sector_Observatory/Tube_Mineral");
            observatory[2] = SearchUtilities.Find("EyeOfTheUniverse_Body/Sector_EyeOfTheUniverse/Sector_Observatory/Mineral_Sign");
            observatory[3] = SearchUtilities.Find("EyeOfTheUniverse_Body/Sector_EyeOfTheUniverse/Sector_Observatory/System_Sign");

            scientist = SearchUtilities.Find("EyeOfTheUniverse_Body/Sector_EyeOfTheUniverse/Sector_Campfire/Campsite/Prefab_IP_GhostBird_Scientist_Eye");
            //scientistZone = SearchUtilities.Find("EyeOfTheUniverse_Body/Sector_EyeOfTheUniverse/Sector_Campfire/InstrumentZones/ScientistSector");
            //scientistSignal = SearchUtilities.Find("EyeOfTheUniverse_Body/Sector_EyeOfTheUniverse/Sector_Campfire/Campsite/Prefab_IP_GhostBird_Scientist_Eye/ScientistSolo");
            scientistAnim = SearchUtilities.Find("EyeOfTheUniverse_Body/Sector_EyeOfTheUniverse/Sector_Campfire/Campsite/Prefab_IP_GhostBird_Scientist_Eye/Ghostbird_IP_ANIM").GetComponent<Animator>();

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
                //Destroy(scientist);
                //Destroy(scientistZone);
            }
        }

        public bool IsSciPlaying()
        {
            return DialogueConditionManager.SharedInstance.GetConditionState("EyeScientistPlaying");
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