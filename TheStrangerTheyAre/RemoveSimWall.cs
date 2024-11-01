using NewHorizons.Utility;
using UnityEngine;

namespace TheStrangerTheyAre
{
    public class RemoveSimWall : MonoBehaviour
    {
        GameObject simWall; // to store simWall

        public void Start()
        {
            simWall = SearchUtilities.Find("DreamWorld_Body/Sector_DreamWorld/Sector_DreamZone_4/Simulation_DreamZone_4/Effects_DreamZone_4_Upper/Effects_IP_SIM_BoundaryCylinder"); // get simulation wall
        }

        public virtual void OnTriggerEnter(Collider hitCollider)
        {
            //checks if player collides with the trigger volume
            if (hitCollider.CompareTag("PlayerDetector") && enabled)
            {
                simWall.SetActive(false); // disables sim wall when in trigger
            }
        }

        public virtual void OnTriggerExit(Collider hitCollider)
        {
            //checks if player exits with the trigger volume
            if (hitCollider.CompareTag("PlayerDetector") && enabled)
            {
                simWall.SetActive(true); // enables sim wall  when outside of trigger
            }
        }
    }
}