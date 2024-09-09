using UnityEngine;

namespace TheStrangerTheyAre
{
    public class RingedLabIntroMusicTrigger : MonoBehaviour
    {
        public bool hasActivated; // creates boolean to check if the trigger has been activated at least once
        public bool playerLeft; // creates boolean that checks if player left the volume

        void Awake()
        {
            hasActivated = false; // sets activation boolean to false every loop start
            playerLeft = false; // sets activation boolean 
        }

        public virtual void OnTriggerEnter(Collider hitCollider)
        {
            //checks if player collides with the trigger volume
            if (hitCollider.CompareTag("PlayerDetector") && enabled && Check() && !Check2() && !hasActivated)
            {
                hasActivated = true;
            }
        }

        public virtual void OnTriggerExit(Collider hitCollider)
        {
            if (hitCollider.CompareTag("PlayerDetector") && enabled && Check() && !Check2() && hasActivated)
            {
                playerLeft = true;
            }
        }

        private bool Check()
        {
            return Locator.GetShipLogManager().IsFactRevealed("LAB_TERRA_TEXT1");
        }
        private bool Check2()
        {
            return Locator.GetShipLogManager().IsFactRevealed("HOME_REVEAL");
        }
    }
}