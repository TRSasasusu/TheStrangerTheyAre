using UnityEngine;
using OWML.Common;

namespace TheStrangerTheyAre
{
    public class InvincibilityVolumeMineralNeeded : MonoBehaviour
    {
        public virtual void OnTriggerEnter(Collider hitCollider)
        {
            //checks if player collides with the trigger volume
            if (hitCollider.CompareTag("PlayerDetector") && enabled && 
                Locator.GetToolModeSwapper().GetItemCarryTool().GetHeldItemType() == ItemType.Scroll && 
                Locator.GetToolModeSwapper().GetItemCarryTool().GetHeldItem().GetComponent<CustomItem>().isCloakMineral == true)
            {
                Locator.GetPlayerTransform().GetComponent<PlayerResources>().ToggleInvincibility(); // sets invincibility for player to true
                Locator.GetDeathManager().ToggleInvincibility(); // sets invincibility for death manager to true
            }
        }

        public virtual void OnTriggerExit(Collider hitCollider)
        {
            //checks if player exits with the trigger volume
            if (hitCollider.CompareTag("PlayerDetector") && enabled &&
                Locator.GetToolModeSwapper().GetItemCarryTool().GetHeldItemType() == ItemType.Scroll &&
                Locator.GetToolModeSwapper().GetItemCarryTool().GetHeldItem().GetComponent<CustomItem>().isCloakMineral == true)
            {
                Locator.GetPlayerTransform().GetComponent<PlayerResources>().ToggleInvincibility(); // sets invincibility for player to false
                Locator.GetDeathManager().ToggleInvincibility(); // sets invincibility for death manager to false
            }
        }
    }
}