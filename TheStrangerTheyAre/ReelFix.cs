using NewHorizons.Utility;
using UnityEngine;

namespace TheStrangerTheyAre
{
    public class ReelFix : MonoBehaviour
    {
        private GameObject[] reels = new GameObject[19]; // create new array of gameobjects to store all custom sim reels

        public void Awake()
        {
            GlobalMessenger.AddListener("EnterDreamWorld", OnEnterDreamWorld); // checks if player enters the sim
        }

        void OnEnterDreamWorld()
        {
            for (int i = 0; i < 19; i++)
            {
                reels[i] = SearchUtilities.Find("TSTA_REEL_" + (i + 1)); // gets all custom reels in the sim, stores in array
                reels[i].GetComponentInParent<OWItemSocket>().Awake(); // runs awake to re-parent
                reels[i].GetComponentInParent<OWItemSocket>().Start(); // runs start to re-parent
            }
        }
    }
}
