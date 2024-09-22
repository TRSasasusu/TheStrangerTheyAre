using UnityEngine;

namespace TheStrangerTheyAre
{
    public class SpoilerPlanetHider : MonoBehaviour
    {

        GameObject homeMoon; // creates variable to store planet
        void Awake()
        {
            homeMoon = GameObject.Find("StrangersHomeworld_Body/Sector"); // gets the quantum planet with nh
        }

        public virtual void OnTriggerEnter(Collider hitCollider)
        {
            //checks if player collides with the trigger volume
            if (hitCollider.CompareTag("PlayerDetector") && enabled && !Check())
            {
                homeMoon.SetActive(false); // deactivates spoiler planet for ringed lab location reveal, if homeworld is not already discovered
            }
        }

        public virtual void OnTriggerExit(Collider hitCollider)
        {
            if (hitCollider.CompareTag("PlayerDetector") && enabled && !homeMoon.activeSelf)
            {
                homeMoon.SetActive(true); // activates spoiler planet when not in ringed lab, as long as the object is inactive.
            }
        }

        private bool Check()
        {
            return Locator.GetShipLogManager().IsFactRevealed("HOME_REVEAL"); // shiplog for planet reveal
        }
    }
}