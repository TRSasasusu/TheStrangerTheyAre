using UnityEngine;

namespace TheStrangerTheyAre
{
    public class NewSimFloorHandler : MonoBehaviour
    {
        GameObject floorDZ4; // creates variable to store the floor of dz4
        GameObject waterDZ4; // creates variable to store the water in dz4

        void Awake()
        {
            waterDZ4 = GameObject.Find("WaterPlane_DreamZone4"); // gets the underwater floor in the fourth sector of the simulation
            floorDZ4 = GameObject.Find("DreamWorld_Body/Sector_DreamWorld/Sector_DreamZone_4/Geo_DreamZone_4_Upper/Terrain_IP_Dreamworld_Floorbed"); // gets the underwater floor in the fourth sector of the simulation
        }

        public virtual void OnTriggerEnter(Collider hitCollider)
        {
            //checks if player collides with the trigger volume
            if (hitCollider.CompareTag("PlayerDetector") && enabled)
            {
                waterDZ4.SetActive(false); // activates object when inside the trigger
                floorDZ4.SetActive(false); // activates object when inside the trigger
            }
        }

        public virtual void OnTriggerExit(Collider hitCollider)
        {
            if (hitCollider.CompareTag("PlayerDetector") && enabled)
            {
                waterDZ4.SetActive(true); // activates object when inside the trigger
                floorDZ4.SetActive(true); // activates object when inside the trigger
            }
        }
    }
}