using UnityEngine;
using OWML.Common;

namespace TheStrangerTheyAre
{
    public class InvincibilityVolume : MonoBehaviour
    {
        /*bool supernovaExists; // checks if supernova exists

        void Awake()
        {
            supernovaExists = false; // sets supernova to false at the start of the loop
        }
        void Update()
        {
            // variables for update function
            var shouldBeActive = TimeLoop.GetSecondsElapsed() > 1980;

            if (shouldBeActive == true)
            {
                supernovaExists = true; // sets to true at the time of supernova
            }
        }*/
        public virtual void OnTriggerEnter(Collider hitCollider)
        {
            //checks if player collides with the trigger volume
            if (hitCollider.CompareTag("PlayerDetector") && enabled /*&& !supernovaExists*/)
            {
                Locator.GetPlayerTransform().GetComponent<PlayerResources>().ToggleInvincibility(); // sets invincibility for player to true
                Locator.GetDeathManager().ToggleInvincibility(); // sets invincibility for death manager to true
            }
        }

        public virtual void OnTriggerExit(Collider hitCollider)
        {
            //checks if player exits with the trigger volume
            if (hitCollider.CompareTag("PlayerDetector") && enabled)
            {
                Locator.GetPlayerTransform().GetComponent<PlayerResources>().ToggleInvincibility(); // sets invincibility for player to false
                Locator.GetDeathManager().ToggleInvincibility(); // sets invincibility for death manager to false
            }
        }
    }
}