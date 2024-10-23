using UnityEngine;
using NewHorizons.Utility;

namespace TheStrangerTheyAre
{
    public class DataCloneHandler : MonoBehaviour
    {
        GameObject scientist1; // creates variable to store the ghostbird ai scientist
        GameObject scientist2; // creates variable to store the pre-vision scientist
        GameObject scientist3; // creates variable to store the post-vision scientist
        GameObject scientist4; // creates variable to store the next loop scientist
        GameObject projector; // creates variable to store the projector
        GameObject torch; // creates variable for vision torch
        private bool isChecked = false; // creates boolean to check if the pedestal got activated.


        void Awake()
        {
            scientist1 = GameObject.Find("Prefab_IP_GhostBird_SCIENTIST"); // gets the ghostbird ai scientist
            scientist2 = GameObject.Find("Prefab_IP_GhostBird_Scientist2"); // gets the pre-vision scientist
            scientist3 = GameObject.Find("Prefab_IP_GhostBird_Scientist3"); // gets the post-vision scientist
            scientist4 = GameObject.Find("Prefab_IP_GhostBird_Scientist4"); // gets the next loop scientist
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
            scientist1.SetActive(true);
        }

        void OnEnterDreamWorld()
        {
            if (scientist1.activeSelf)
            {
                scientist1.SetActive(false);
            }
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