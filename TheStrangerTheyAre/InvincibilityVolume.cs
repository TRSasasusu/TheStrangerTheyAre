using UnityEngine;
using OWML.Common;

namespace TheStrangerTheyAre
{
    public class InvincibilityVolume : MonoBehaviour
    {
        public virtual void OnTriggerEnter(Collider hitCollider)
        {
            //checks if player collides with the trigger volume
            if (hitCollider.CompareTag("PlayerDetector") && enabled)
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