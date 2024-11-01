using NewHorizons.Utility;
using UnityEngine;

namespace TheStrangerTheyAre
{
    public class LightningDisableTrigger : MonoBehaviour
    {
        GameObject lightning; // to store lightning

        public void Start()
        {
            lightning = SearchUtilities.Find("AnglersEye_Body/Sector/Clouds/LightningGenerator"); // get lightning
        }

        public virtual void OnTriggerEnter(Collider hitCollider)
        {
            //checks if player collides with the trigger volume
            if (hitCollider.CompareTag("PlayerDetector") && enabled)
            {
                lightning.SetActive(false); // disables lightning when in trigger
            }
        }

        public virtual void OnTriggerExit(Collider hitCollider)
        {
            //checks if player exits with the trigger volume
            if (hitCollider.CompareTag("PlayerDetector") && enabled)
            {
                lightning.SetActive(true); // enables lightning when outside of trigger
            }
        }
    }
}