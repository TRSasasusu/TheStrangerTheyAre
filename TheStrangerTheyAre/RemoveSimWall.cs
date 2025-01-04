using NewHorizons.Utility;
using UnityEngine;

namespace TheStrangerTheyAre
{
    public class RemoveSimWall : MonoBehaviour
    {
        GameObject simWall; // to store simWall
        private bool isInTrigger = false;

        public void Start()
        {
            simWall = SearchUtilities.Find("DreamWorld_Body/Sector_DreamWorld/Sector_DreamZone_4/Simulation_DreamZone_4/Effects_DreamZone_4_Upper/Effects_IP_SIM_BoundaryCylinder"); // get simulation wall
        }

        public void Update()
        {
            if (isInTrigger)
            {
                simWall.SetActive(false); // disables sim wall when in trigger
            }
            else
            {
                simWall.SetActive(true); // enables sim wall  when outside of trigger
            }
        }
        public virtual void OnTriggerEnter(Collider hitCollider)
        {
            //checks if player collides with the trigger volume
            if (hitCollider.CompareTag("PlayerDetector") && enabled)
            {
                isInTrigger = true;                
            }
        }

        public virtual void OnTriggerExit(Collider hitCollider)
        {
            //checks if player exits with the trigger volume
            if (hitCollider.CompareTag("PlayerDetector") && enabled)
            {
                isInTrigger = false;
            }
        }
    }
}