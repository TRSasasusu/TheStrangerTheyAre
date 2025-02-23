using NewHorizons.Utility;
using UnityEngine;

namespace TheStrangerTheyAre
{
    public class StrangerTextHandlerTSTA : MonoBehaviour
    {
        private GameObject[] strangerDialogue = new GameObject[11]; // create new array of gameobjects to store all custom sim reels

        private void Start()
        {
            for (int i = 0; i < 11; i++)
            {
                strangerDialogue[i] = SearchUtilities.Find("TSTA_StrangerDialogue_" + (i + 1)); // gets all custom strangerDialogue in the sim, stores in array
            }
        }

        private bool KnowsLanguage()
        {
            return Locator.GetShipLogManager().IsFactRevealed("ANGLERS_EYE_ALIENTEXT_E2");
        }

        private void Update()
        {
            foreach (GameObject dialogue in strangerDialogue)
            {
                dialogue.SetActive(KnowsLanguage());
            }
        }
    }
}
