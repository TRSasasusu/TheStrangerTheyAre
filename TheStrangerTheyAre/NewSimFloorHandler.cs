using UnityEngine;
using NewHorizons.Utility;

namespace TheStrangerTheyAre
{
    public class NewSimFloorHandler : MonoBehaviour
    {
        [SerializeField]
        NewSimFloorHandler[] waterTriggers; // all dreamzone water triggers, obtained from unity.

        GameObject floorDZ4; // creates variable to store the floor of dz4
        GameObject waterDZ4; // creates variable to store the water in dz4
        GameObject liquidDZ4; // creates variable to store the liquid volume of dz4
        public bool isInside = false;

        void Start()
        {
            GlobalMessenger.AddListener("ExitDreamWorld", OnExitDreamWorld); // checks if player leaves the sim
            waterDZ4 = SearchUtilities.Find("WaterPlane_DreamZone4"); // gets the underwater floor in the fourth sector of the simulation
            floorDZ4 = SearchUtilities.Find("DreamWorld_Body/Sector_DreamWorld/Sector_DreamZone_4/Geo_DreamZone_4_Upper/Terrain_IP_Dreamworld_Floorbed"); // gets the underwater floor in the fourth sector of the simulation
            liquidDZ4 = SearchUtilities.Find("DreamWorld_Body/Sector_DreamWorld/Volumes_DreamWorld/DreamRiverFluidVolume"); // gets the underwater floor in the fourth sector of the simulation
        }

        void OnExitDreamWorld()
        {
            // dreamzone water enabling
            waterDZ4.SetActive(true); // activates object when player leaves the trigger
            floorDZ4.SetActive(true); // activates object when player leaves the trigger
            liquidDZ4.SetActive(true); // activates object when player leaves the trigger
        }

        public virtual void OnTriggerEnter(Collider hitCollider)
        {
            if (hitCollider.CompareTag("PlayerDetector") && enabled)
            {
                isInside = true; // enable trigger boolean
                // dreamzone water disabling
                waterDZ4.SetActive(false); // deactivates object when inside the trigger
                floorDZ4.SetActive(false); // deactivates object when inside the trigger
                liquidDZ4.SetActive(false); // deactivates object when inside the trigger
            }
        }

        public virtual void OnTriggerExit(Collider hitCollider)
        {
            if (hitCollider.CompareTag("PlayerDetector") && enabled)
            {
                isInside = false; // disable trigger boolean
                int temp = 0; // temporary variable to count active triggers, set to 0 each time before incrementing.
                foreach (NewSimFloorHandler trigger in waterTriggers)
                {
                    if (trigger.isInside)
                    {
                        temp++; // increment temp variable if inside any of the triggers
                    }   
                }

                // runs if the temporary variable is less than 1 (0 or less).
                if (temp < 1)
                {
                    // dreamzone water enabling
                    waterDZ4.SetActive(true); // activates object when player leaves the trigger
                    floorDZ4.SetActive(true); // activates object when player leaves the trigger
                    liquidDZ4.SetActive(true); // activates object when player leaves the trigger
                }
            }
        }
    }
}