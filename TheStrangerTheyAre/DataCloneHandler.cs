using UnityEngine;
using NewHorizons.Utility;
using System.Collections;

namespace TheStrangerTheyAre
{
    public class DataCloneHandler : MonoBehaviour
    {
        [SerializeField]
        private SpawnPoint spawnPoint;

        private GameObject scientist1; // creates variable to store the ghostbird ai scientist
        private GameObject scientist2; // creates variable to store the pre-vision scientist
        private GameObject scientist3; // creates variable to store the post-vision scientist
        private GameObject scientist4; // creates variable to store the next loop scientist
        private GameObject prisoner; // prisoner object
        private GameObject cypress; // cypress object
        private GameObject projector; // creates variable to store the projector
        private GameObject torch; // creates variable for vision torch
        private bool isChecked = false; // creates boolean to check if the pedestal got activated.

        // warp stuff again
        protected PlayerSpawner _spawner; // for spawning the player
        private bool hasWarped; // sets boolean to make sure warp doesn't happen repeatedly on update.
        public const float blinkTime = 0.5f; // constant for blink time
        public const float animTime = blinkTime / 2f; // constant for blink animation time

        void Awake()
        {
            scientist1 = GameObject.Find("Prefab_IP_GhostBird_SCIENTIST"); // gets the ghostbird ai scientist
            scientist2 = GameObject.Find("Prefab_IP_GhostBird_Scientist2"); // gets the pre-vision scientist
            scientist3 = GameObject.Find("Prefab_IP_GhostBird_Scientist3"); // gets the post-vision scientist
            scientist4 = GameObject.Find("Prefab_IP_GhostBird_Scientist4"); // gets the family reunion scientist
            prisoner = GameObject.Find("Prefab_IP_GhostBird_Prisoner_Reunion"); // gets the family reunion prisoner
            cypress = GameObject.Find("Prefab_IP_GhostBird_Cypress_Reunion"); // gets the family reunion cypress
            projector = GameObject.Find("Theatre_SIM/Prefab_IP_DreamLibraryPedestal"); // gets projector
            GlobalMessenger.AddListener("EnterDreamWorld", OnEnterDreamWorld); // checks if player enters the sim
        }

        void Start()
        {
            // finds torch
            torch = SearchUtilities.Find("SCIENTIST_VISIONTORCH");

            // disables pre and post vision scientist at start of loop
            scientist2.SetActive(false);
            scientist3.SetActive(false);
            scientist4.SetActive(false);
            prisoner.SetActive(false);
            cypress.SetActive(false);
            scientist1.SetActive(true);
        }

        void OnEnterDreamWorld()
        {
            if (scientist1.activeSelf)
            {
                scientist1.SetActive(false);
            }
        }

        private IEnumerator Blink()
        {
            var cameraEffectController = FindObjectOfType<PlayerCameraEffectController>(); // gets camera controller

            // close eyes
            cameraEffectController.CloseEyes(animTime); // closes eyes
            yield return new WaitForSeconds(animTime);  // waits until animation stops to proceed to next line
            GlobalMessenger.FireEvent("PlayerBlink"); // fires an event for the player blinking

            // move prisoner
            prisoner.SetActive(true);

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
                // make anim & sound happen
            }

            if (CheckCypress())
            {
                scientist1.SetActive(false);
                scientist2.SetActive(false);
                scientist3.SetActive(false);
                scientist4.SetActive(true);
                cypress.SetActive(true);
                if (CheckPrisoner2())
                {
                    StartCoroutine(Blink()); // blink coroutine that warps you and the prisoner to the archives after telling him about cypress & the scientist. should only run once.
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

        private bool CheckPrisoner2()
        {
            return DialogueConditionManager.SharedInstance.GetConditionState("TSTA_PrisonerLeave");
        }

        private bool IsEndingAchieved()
        {
            return DialogueConditionManager.SharedInstance.GetConditionState("tsta_familyreunion");
        }
    }
}