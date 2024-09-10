using UnityEngine;
using OWML.Common;
using NewHorizons.Utility;

namespace TheStrangerTheyAre
{
    public class GhostBirdHandlerTSTA : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] ghostBirds; // array of all ghostbirds, set in unity
        [SerializeField]
        private GameObject[] initialPos; // array for storing each starting position
        private bool areDeadZone1 = false; // boolean that tests if the ghosts have died.
        private bool areDeadZone2 = false; // boolean that tests if the ghosts have died.


        void Awake()
        {
            for (int i = 0; i < ghostBirds.Length; i++)
            {
                initialPos[i].transform.position = ghostBirds[i].transform.position; // gets starting pos of each ghostbird
                initialPos[i].transform.rotation = ghostBirds[i].transform.rotation; // gets starting rotation of each ghostbird
            };
            GlobalMessenger.AddListener("ExitDreamWorld", OnExitDreamWorld); // checks if player leaves the sim
        }

        void OnExitDreamWorld()
        {
            for (int i = 0; i < ghostBirds.Length; i++)
            {
                ghostBirds[i].transform.position = initialPos[i].transform.position; // on dw exit, sets to starting pos of each ghostbird
                ghostBirds[i].transform.rotation = initialPos[i].transform.rotation; // on dw exit, sets to starting rotation of each ghostbird
                ghostBirds[i].GetComponentInChildren<Animator>().Play("Ghostbird_Idle_Unaware", 0); // on dw exit, sets each ghostbird animation to idle
            }
        }

        void Update()
        {
            if (Locator._toolModeSwapper.GetItemCarryTool().GetHeldItem() is DreamLanternItem lantern && lantern._lanternController._lit)
            {
                if (OWInput.IsPressed(InputLibrary.toolActionPrimary, InputMode.Character) && OWInput.IsPressed(InputLibrary.toolActionSecondary, InputMode.Character))
                {
                    TheStrangerTheyAre.WriteLine("Player is Sneaking", MessageType.Success); // debug message
                    Sneak(); // run sneak method if player is both focusing and contracting lantern
                }
                else
                {
                    TheStrangerTheyAre.WriteLine("Player is NOT Sneaking", MessageType.Success); // debug message
                    NoSneak(); // run no sneak method if player is NOT both focusing and contracting lantern
                }
            }

            if (TimeLoop.GetSecondsElapsed() > 790 && !areDeadZone1)
            {
                TheStrangerTheyAre.WriteLine("CONDITION SET: DEATH WAVE 1", MessageType.Success); // debug message
                for (int i = 0; i < 2; i++)
                {
                    ghostBirds[i].GetComponent<GhostBrain>().Die(); // kill ghostbirds linked to zone 1
                    ghostBirds[i].SetActive(false);
                    TheStrangerTheyAre.WriteLine("Killed Ghost No. " + i+1, MessageType.Success); // debug messagea
                };
                areDeadZone1 = true; // set dead boolean for zone 1 true
            } else if (TimeLoop.GetSecondsElapsed() > 1230 && !areDeadZone2)
            {
                TheStrangerTheyAre.WriteLine("CONDITION SET: DEATH WAVE 2", MessageType.Success); // debug message
                for (int i = 2; i < 8; i++)
                {
                    ghostBirds[i].GetComponent<GhostBrain>().Die(); // kill ghostbirds linked to zone 2
                    TheStrangerTheyAre.WriteLine("Killed Ghost No. " + i + 1, MessageType.Success); // debug message
                };
                areDeadZone2 = true; // set dead boolean for zone 2 true
            }
        }
        void Sneak()
        {
            //GameObject ghostTrigger; // defines temp var to store the contact trigger
            TheStrangerTheyAre.WriteLine("Sneak Module Active", MessageType.Success); // debug message
            foreach (var bird in ghostBirds)
            {
                bird.GetComponentInChildren<CapsuleShape>().radius = 1.4f;
            }
        }

        void NoSneak()
        {
            //GameObject ghostTrigger; // defines temp var to store the contact trigger
            TheStrangerTheyAre.WriteLine("NoSneak Module Active", MessageType.Success); // debug message
            foreach (var bird in ghostBirds)
            {
                bird.GetComponentInChildren<CapsuleShape>().radius = 50;
            }
        }
    }
}