using UnityEngine;
using NewHorizons.Utility;

namespace TheStrangerTheyAre
{
    public class ByeByeInterloper : MonoBehaviour
    {
        GameObject interloper; // creates variable to store the interloper
        GameObject nomShuttle;

        void Awake()
        {
            interloper = SearchUtilities.Find("Comet_Body/Sector_CO"); // gets the interloper
            nomShuttle = SearchUtilities.Find("Comet_Body/Prefab_NOM_Shuttle");
        }

        public virtual void OnTriggerEnter(Collider hitCollider)
        {
            // checks if player collides with the trigger volume
            if (hitCollider.CompareTag("PlayerDetector") && enabled)
            {
                interloper.SetActive(false); // activates object when inside the trigger
                nomShuttle.SetActive(false);
            }
        }

        public virtual void OnTriggerExit(Collider hitCollider)
        {
            // checks if player collides with the trigger volume
            if (hitCollider.CompareTag("PlayerDetector") && enabled)
            {
                interloper.SetActive(true); // activates object when inside the trigger
                nomShuttle.SetActive(true);
            }
        }
    }
}