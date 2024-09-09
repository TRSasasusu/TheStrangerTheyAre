using UnityEngine;

namespace TheStrangerTheyAre
{
    public class GlitchedTilesAlt : MonoBehaviour
    {
        // variables
        GameObject tilesAlt; // creates variable to store the alternate glitched tiles
        void Awake()
        {
            tilesAlt = GameObject.Find("GlitchedTilesAlt"); // gets the underwater floor in the fourth sector of the simulation
            tilesAlt.SetActive(false); // deactivates object at start of loop
        }

        void Update()
        {
            // variables for update function
            var shouldBeActive = TimeLoop.GetSecondsElapsed() > 790; // these tiles activate after the flood
            var isActive = tilesAlt.activeInHierarchy;

            if (shouldBeActive != isActive)
            {
                tilesAlt.SetActive(shouldBeActive); // sets active if the time loop condition is met
            }
        }
    }
}