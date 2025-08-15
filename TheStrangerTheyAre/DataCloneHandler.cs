using UnityEngine;
using NewHorizons.Utility;
using System.Collections;

namespace TheStrangerTheyAre
{
    public class DataCloneHandler : MonoBehaviour
    {
        [SerializeField]
        private SpawnPoint spawnPoint;

        // scientist variables
        private GameObject scientist1; // creates variable to store the ghostbird ai scientist
        private GameObject scientist2; // creates variable to store the pre-vision scientist
        private GameObject scientist3; // creates variable to store the post-vision scientist
        private GameObject projector; // creates variable to store the projector
        private GameObject torch; // creates variable for vision torch

        // alt ending scene variables
        private GameObject scientist4; // creates variable to store the next loop scientist
        private GameObject prisoner; // prisoner object
        private GameObject cypress; // cypress object
        private GameObject credits;

        // prisoner variables
        private GameObject prisonerOriginal; // prisoner object
        private GameObject prisOldDialogue; // creates variable for prisoner's old dialogue
        private GameObject prisNewDialogue; // creates variable for prisoner's new dialogue

        // cypress sleep variable
        private GameObject cypressSleep;

        // dreamzone water and floor
        GameObject floorDZ4; // creates variable to store the floor of dz4
        GameObject waterDZ4; // creates variable to store the water in dz4
        GameObject liquidDZ4; // creates variable to store the liquid volume of dz4
        GameObject undergroundWater; // creates variable to store the floor in the dreamworld underground

        // booleans
        private bool isChecked = false; // creates boolean to check if the pedestal got activated.
        private bool hasCried = false; // check if prisoner has cried

        // warp stuff again
        protected PlayerSpawner _spawner; // for spawning the player
        private bool hasWarped; // sets boolean to make sure warp doesn't happen repeatedly on update.
        public const float blinkTime = 0.5f; // constant for blink time
        public const float animTime = blinkTime / 2f; // constant for blink animation time


        // ALL LINES COMMENTED OUT ARE FOR A SCRAPPED ENDING.

        void Awake()
        {
            GlobalMessenger.AddListener("EnterDreamWorld", OnEnterDreamWorld); // checks if player enters the sim
        }

        void Start()
        {
            // assign all variables
            projector = GameObject.Find("Theatre_SIM/Prefab_IP_DreamLibraryPedestal"); // gets projector

            // for prisoner scene
            prisonerOriginal = SearchUtilities.Find("DreamWorld_Body/Sector_DreamWorld/Sector_Underground/Sector_PrisonCell/Ghosts_PrisonCell/GhostNodeMap_PrisonCell_Lower/Prefab_IP_GhostBird_Prisoner");
            prisOldDialogue = SearchUtilities.Find("DreamWorld_Body/Sector_DreamWorld/Sector_Underground/Sector_PrisonCell/Ghosts_PrisonCell/GhostNodeMap_PrisonCell_Lower/Prefab_IP_GhostBird_Prisoner/InteractReceiver");
            prisNewDialogue = SearchUtilities.Find("TSTA_Prisoner_Dialogue");

            // for reunion scene
            scientist1 = GameObject.Find("Prefab_IP_GhostBird_SCIENTIST"); // gets the ghostbird ai scientist
            scientist2 = GameObject.Find("Prefab_IP_GhostBird_Scientist2"); // gets the pre-vision scientist
            scientist3 = GameObject.Find("Prefab_IP_GhostBird_Scientist3"); // gets the post-vision scientist
            scientist4 = GameObject.Find("Prefab_IP_GhostBird_Scientist4"); // gets the family reunion scientist
            prisoner = GameObject.Find("Prefab_IP_GhostBird_Prisoner_Reunion"); // gets the family reunion prisoner
            cypress = GameObject.Find("Prefab_IP_GhostBird_Cypress_Reunion"); // gets the family reunion cypress
            credits = SearchUtilities.Find("DreamWorld_Body/Sector_DreamWorld/Sector_DreamZone_4/Interactibles_DreamZone_4_Upper/Sector_NewSim/New_SimSector/FourthArchive/GhostBirds/CUTSCENE_GHOSTBIRDS/NODE_SCIENTIST/Prefab_IP_GhostBird_Scientist4/FamilyReunionCreditsVol");

            // for sleeping cypress
            cypressSleep = GameObject.Find("Prefab_IP_GhostBird_Cypress_Sleep"); // gets the family reunion cypress

            // finds torch
            torch = SearchUtilities.Find("SCIENTIST_VISIONTORCH");

            // water stuff
            waterDZ4 = SearchUtilities.Find("WaterPlane_DreamZone4"); // gets the underwater floor in the fourth sector of the simulation
            floorDZ4 = SearchUtilities.Find("DreamWorld_Body/Sector_DreamWorld/Sector_DreamZone_4/Geo_DreamZone_4_Upper/Terrain_IP_Dreamworld_Floorbed"); // gets the underwater floor in the fourth sector of the simulation
            liquidDZ4 = SearchUtilities.Find("DreamWorld_Body/Sector_DreamWorld/Volumes_DreamWorld/DreamRiverFluidVolume"); // gets the underwater floor in the fourth sector of the simulation
            undergroundWater = SearchUtilities.Find("DreamWorld_Body/Sector_DreamWorld/Sector_Underground/Volumes_Underground/WaterVolume_Underground"); // gets the underwater floor in the fourth sector of the simulation

            hasWarped = false;

            // disables pre and post vision scientist at start of loop
            scientist2.SetActive(false);
            scientist3.SetActive(false);
            scientist4.SetActive(false);
            prisoner.SetActive(false);
            cypress.SetActive(false);
            scientist1.SetActive(true);
            credits.SetActive(false);

            if (CheckCypress())
            {
                // setup prisoner dialouge
                Destroy(prisOldDialogue);
                prisNewDialogue.SetActive(false);
                prisNewDialogue.transform.parent = prisOldDialogue.transform.parent;
                prisNewDialogue.transform.localPosition = new Vector3(0, 2.92f, 0.369f);
            }
            else
            {
                // destroy all altending objects
                Destroy(prisNewDialogue);
                Destroy(cypressSleep);
            }
        }

        void OnEnterDreamWorld()
        {
            if (scientist1.activeSelf)
            {
                scientist1.SetActive(false);
            }
        }

        void OnExitDreamWorld()
        {
            // dreamzone water enabling
            waterDZ4.SetActive(true); // activates object when player leaves the trigger
            floorDZ4.SetActive(true); // activates object when player leaves the trigger
            liquidDZ4.SetActive(true); // activates object when player leaves the trigger
            undergroundWater.SetActive(true);
        }

        private IEnumerator Blink()
        {
            var cameraEffectController = FindObjectOfType<PlayerCameraEffectController>(); // gets camera controller

            // disable water
            waterDZ4.SetActive(false); // deactivates object when inside the trigger
            floorDZ4.SetActive(false); // deactivates object when inside the trigger
            liquidDZ4.SetActive(false); // deactivates object when inside the trigger
            undergroundWater.SetActive(false);
            Destroy(prisNewDialogue); // disable prisoner dialogue

            // close eyes
            cameraEffectController.CloseEyes(animTime); // closes eyes
            yield return new WaitForSeconds(animTime);  // waits until animation stops to proceed to next line
            GlobalMessenger.FireEvent("PlayerBlink"); // fires an event for the player blinking

            // move prisoner
            prisoner.SetActive(true);
            prisonerOriginal.SetActive(false);

            // warp to archives for family reunion
            _spawner = GameObject.FindGameObjectWithTag("Player").GetRequiredComponent<PlayerSpawner>(); // gets player spawner
            _spawner.DebugWarp(spawnPoint); // warps you to new spawn point

            // open eyes
            cameraEffectController.OpenEyes(animTime, false); // open eyes
            yield return new WaitForSeconds(animTime); //  waits until animation stops to proceed to next line
            hasWarped = true; // sets has warped to true so this doesn't run constantly (because it's being called in update)
        }

        private void Update()
        {
            if (projector.GetComponent<DreamLibraryPedestal>().IsPowered() == true && isChecked == false)
            {
                scientist1.SetActive(true);
                scientist1.GetComponent<GhostBrain>().enabled = true;
                isChecked = true; // sets boolean to be checked
            }

            // on each frame, check if the scientist pre-vision is enabled
            if (scientist2.activeSelf)
            {
                scientist1.SetActive(false);
                // also check if the player saw the vision, but has not yet talked to him.

                if (!Check() && Check3())
                {
                    scientist2.SetActive(false); // sets the pre-vision scientist to false
                    scientist3.transform.position = scientist2.transform.position; // sets the position of post-vision scientist equal to pre-vision
                    scientist3.transform.rotation = scientist2.transform.rotation; // sets the rotation of post-vision scientist equal to pre-vision
                    scientist3.SetActive(true); // sets the post-vision scientist to true
                }
            }

            if (CheckPrisoner1())
            {
                if (!hasCried)
                {
                    prisonerOriginal.GetComponentInChildren<PrisonerEffects>().PlayReactToVisionAnimation();
                    hasCried = true;
                }
            }

            if (CheckPrisonerOld())
            {
                prisonerOriginal.GetComponent<PrisonerBrain>().BeginBehavior(PrisonerBehavior.LightLamp, 0.66f);
            }

            if (CheckCypress())
            {
                scientist1.SetActive(false);
                scientist2.SetActive(false);
                scientist3.SetActive(false);
                scientist4.SetActive(true);
                cypress.SetActive(true);
                
                if (CheckPrisoner2() && !hasWarped)
                {
                    StartCoroutine(Blink()); // blink coroutine that warps you and the prisoner to the archives after telling him about cypress & the scientist. should only run once.
                }

                if (IsEndingAchieved())
                {
                    credits.SetActive(true);
                    PlayerData.SetPersistentCondition("CYPRESS_BOARDVESSEL", false);
                } else {
                    credits.SetActive(false);
                }
            }
        }

        private bool Check()
        {
            return PlayerState.IsViewingProjector();
        }

        private bool Check3()
        {
            return DialogueConditionManager.SharedInstance.GetConditionState("SCIENTIST_VISIONTORCH");
        }

        private bool Check2()
        {
            return Locator.GetShipLogManager().IsFactRevealed("NEWSIM_SCIENTIST_CLONE");
        }

        private bool CheckCypress()
        {
            return PlayerData.GetPersistentCondition("CYPRESS_BOARDVESSEL");
        }

        private bool CheckPrisoner1()
        {
            return DialogueConditionManager.SharedInstance.GetConditionState("TSTA_PrisonerBrother");
        }
        private bool CheckPrisonerOld()
        {
            return DialogueConditionManager.SharedInstance.GetConditionState("TSTA_GoBackToMainPrisoner");
        }

        private bool CheckPrisoner2()
        {
            return DialogueConditionManager.SharedInstance.GetConditionState("TSTA_PrisonerLeave");
        }

        private bool IsEndingAchieved()
        {
            return DialogueConditionManager.SharedInstance.GetConditionState("FamilyReunionDone");
        }
    }
}