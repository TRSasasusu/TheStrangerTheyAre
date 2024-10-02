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
                ghostBirds[i].GetComponent<GhostBrain>().EscalateThreatAwareness(GhostData.ThreatAwareness.EverythingIsNormal); // set threat to normal when exiting sim
                ghostBirds[i].GetComponent<GhostBrain>().ChangeAction(GhostAction.Name.Wait); // change ghost action to wait
                ghostBirds[i].GetComponentInChildren<Animator>().Play("Ghostbird_Idle_Unaware", 0); // on dw exit, sets each ghostbird animation to idle
            }
        }

        void Update()
        {
            if (Locator._toolModeSwapper.GetItemCarryTool().GetHeldItem() is DreamLanternItem lantern && lantern._lanternController._lit)
            {
                if (OWInput.IsPressed(InputLibrary.toolActionPrimary, InputMode.Character) && OWInput.IsPressed(InputLibrary.toolActionSecondary, InputMode.Character))
                {
                    Sneak(); // run sneak method if player is both focusing and contracting lantern
                }
                else
                {
                    NoSneak(); // run no sneak method if player is NOT both focusing and contracting lantern
                }
            }

            if (TimeLoop.GetSecondsElapsed() > 790 && !areDeadZone1)
            {
                for (int i = 0; i < 2; i++)
                {
                    ghostBirds[i].GetComponent<GhostBrain>().Die(); // kill ghostbirds linked to zone 1
                    ghostBirds[i].SetActive(false);
                };
                areDeadZone1 = true; // set dead boolean for zone 1 true
            } else if (TimeLoop.GetSecondsElapsed() > 1230 && !areDeadZone2)
            {
                for (int i = 2; i < 8; i++)
                {
                    ghostBirds[i].GetComponent<GhostBrain>().Die(); // kill ghostbirds linked to zone 2
                };
                areDeadZone2 = true; // set dead boolean for zone 2 true
            }
        }
        void Sneak()
        {
            foreach (var bird in ghostBirds)
            {
                Vector3 smallTrigger = new Vector3(1, 1, 1);
                bird.transform.Find("ContactTrigger/ContactTrigger_Core").gameObject.transform.localScale = smallTrigger;
            }
        }

        void NoSneak()
        {
            foreach (var bird in ghostBirds)
            {
                Vector3 bigTrigger = new Vector3(2, 2, 2);
                bird.transform.Find("ContactTrigger/ContactTrigger_Core").gameObject.transform.localScale = bigTrigger;
            }
        }
    }
}