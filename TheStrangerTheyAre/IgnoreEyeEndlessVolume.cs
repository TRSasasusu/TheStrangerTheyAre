using UnityEngine;

namespace TheStrangerTheyAre
{
    public class IgnoreEyeEndlessVolume : MonoBehaviour
    {
        GameObject endlessVolume; // creates variable to store the endless eye volume
        [SerializeField]
        GameObject ring; // creates variable to store the ringed planet
        void Awake()
        {
            var fireVol = GameObject.Find("EyeOfTheUniverse_Body/Sector_EyeOfTheUniverse/Sector_Campfire/Volumes_Campfire"); // gets the quantum planet with nh
            endlessVolume = fireVol.transform.Find("EndlessCylinder_Forest").gameObject; // gets the endless eye volume
            ring.SetActive(false); // activates object when inside the trigger
        }

        public virtual void OnTriggerEnter(Collider hitCollider)
        {
            //checks if player collides with the trigger volume
            if (hitCollider.CompareTag("PlayerDetector") && enabled)
            {
                endlessVolume.SetActive(false); // activates object when inside the trigger
                ring.SetActive(true); // activates object when inside the trigger
            }
        }

        public virtual void OnTriggerExit(Collider hitCollider)
        {
            if (hitCollider.CompareTag("PlayerDetector") && enabled)
            {
                endlessVolume.SetActive(true); // activates object when inside the trigger
            }
        }
    }
}