using UnityEngine;
using OWML.Common;

namespace TheStrangerTheyAre
{
    public class DataCloneHandler : MonoBehaviour
    {
        GameObject scientist1; // creates variable to store the ghostbird ai scientist
        GameObject scientist2; // creates variable to store the pre-vision scientist
        GameObject scientist3; // creates variable to store the post-vision scientist
        GameObject scientist4; // creates variable to store the next loop scientist
        GameObject projector; // creates variable to store the projector
        GameObject startingPoint; // creates variable to store starting point
        private bool isChecked = false; // creates boolean to check if the pedestal got activated.


        void Awake()
        {
            scientist1 = GameObject.Find("Prefab_IP_GhostBird_SCIENTIST"); // gets the ghostbird ai scientist
            scientist2 = GameObject.Find("Prefab_IP_GhostBird_Scientist2"); // gets the pre-vision scientist
            scientist3 = GameObject.Find("Prefab_IP_GhostBird_Scientist3"); // gets the post-vision scientist
            scientist4 = GameObject.Find("Prefab_IP_GhostBird_Scientist4"); // gets the next loop scientist
            projector = GameObject.Find("Theatre_SIM/Prefab_IP_DreamLibraryPedestal"); // gets projector
            startingPoint = GameObject.Find("SCIENTISTSTART"); // gets starting ponint
        }
        private void Start()
        {
            // disables pre and post vision scientist at start of loop
            scientist2.SetActive(false);
            TheStrangerTheyAre.WriteLine("SCIENTIST V2 NO LONGER ACTIVE!", MessageType.Success); // debug message
            scientist3.SetActive(false);
            TheStrangerTheyAre.WriteLine("SCIENTIST V3 NO LONGER ACTIVE!", MessageType.Success); // debug message
            scientist4.SetActive(false); // enables next loop scientist if already met
            TheStrangerTheyAre.WriteLine("SCIENTIST V4 ACTIVE!", MessageType.Success); // debug message


            scientist1.GetComponent<GhostBrain>().enabled = false;
            scientist1.SetActive(true);
            TheStrangerTheyAre.WriteLine("SCIENTIST V1 ACTIVE!", MessageType.Success); // debug message
            // checks if the player has met the scientist data clone
            /*if (Check2()) {
                scientist1.SetActive(false); // enables ghostbird ai scientist if not already met
                TheStrangerTheyAre.WriteLine("SCIENTIST V1 NO LONGER ACTIVE!", MessageType.Success); // debug message
                scientist4.SetActive(true); // enables next loop scientist if already met
                TheStrangerTheyAre.WriteLine("SCIENTIST V4 ACTIVE!", MessageType.Success); // debug message
            } else {

            }*/
        }

        private void Update()
        {
            if (projector.GetComponent<DreamLibraryPedestal>().IsPowered() == true && isChecked == false && scientist1.activeSelf)
            {
                isChecked = true; // sets boolean to be checked
                TheStrangerTheyAre.WriteLine("P-P-P-POWAH!", MessageType.Success); // debug message
                scientist1.GetComponent<GhostBrain>().enabled = true;
            }

            /*while (isChecked == false && !projector.GetComponent<DreamLibraryPedestal>().IsPowered() && scientist1.activeSelf)
            {
                scientist1.transform.position = startingPoint.transform.position; // keeps setting scientist1 to the same location every frame until the player uses the pedestal at least once
            }*/

            // on each frame, check if the scientist pre-vision is enabled
            if (scientist2.activeSelf)
            {
                scientist1.SetActive(false);
                TheStrangerTheyAre.WriteLine("SCIENTIST V1 NO LONGER ACTIVE!", MessageType.Success); // debug message
                // also check if the player saw the vision, but has not yet talked to him.
                if (!Check() && Check3())
                {
                    scientist2.SetActive(false); // sets the pre-vision scientist to false
                    TheStrangerTheyAre.WriteLine("SCIENTIST V2 NO LONGER ACTIVE!", MessageType.Success); // debug message
                    scientist3.transform.position = scientist2.transform.position; // sets the position of post-vision scientist equal to pre-vision
                    TheStrangerTheyAre.WriteLine("GOT SCIENTIST V2 POS", MessageType.Success); // debug message
                    scientist3.transform.rotation = scientist2.transform.rotation; // sets the rotation of post-vision scientist equal to pre-vision
                    TheStrangerTheyAre.WriteLine("GOT SCIENTIST V2 ROT", MessageType.Success); // debug message
                    scientist3.SetActive(true); // sets the post-vision scientist to true
                    TheStrangerTheyAre.WriteLine("SCIENTIST V3 ACTIVE!", MessageType.Success); // debug message
                }
            }
        }

        private bool Check()
        {
            return PlayerState.IsViewingProjector();
        }

        private bool Check3()
        {
            return Locator.GetShipLogManager().IsFactRevealed("NEWSIM_SCIENTIST_VISION");
        }

        private bool Check2()
        {
            return Locator.GetShipLogManager().IsFactRevealed("NEWSIM_SCIENTIST_CLONE");
        }
    }
}