using UnityEngine;
using NewHorizons.Utility;

namespace TheStrangerTheyAre
{
    public class TSTAShortcut : MonoBehaviour
    {
        // variables
        private GameObject warpDZ3; // for storing the other warp zone

        void Start()
        {
            warpDZ3 = SearchUtilities.Find("TSTA_Shortcut_DZ3"); // gets other warp zone
            warpDZ3.SetActive(false); // sets it false at the start of the loop
        }

        public virtual void OnTriggerEnter(Collider hitCollider)
        {
            //checks if player collides with the trigger volume
            if (hitCollider.CompareTag("PlayerDetector") && enabled)
            {
                warpDZ3.SetActive(true); // upon player contact, enables the other side.
            }
        }
    }
}