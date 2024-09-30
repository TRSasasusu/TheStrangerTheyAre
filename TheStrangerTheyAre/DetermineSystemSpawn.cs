using NewHorizons.Utility;
using UnityEngine;

namespace TheStrangerTheyAre
{
    public class DetermineSystemSpawn : MonoBehaviour
    {
        // variables
        static int warpID = 0; // creates variable to store the id of the warp
        private static GameObject dimension; // creates variable to store the bramble warp node
        public static bool hasWarpedWithBramble; // creates boolean to check if the warp has been done with bramble.
        // spawn point variables
        public GameObject shipSpawnHome;
        public GameObject shipSpawnBramble;
        public GameObject playerSpawnHome;
        public GameObject playerSpawnBramble;

        public void OnSolarSystemLoad()
        {
            if (TheStrangerTheyAre.NewHorizonsAPI.GetCurrentStarSystem() == "AnonymousStrangerOW.StrangerSystem")
            {
                // get spawn points
                playerSpawnHome = SearchUtilities.Find("SpawnPlayer_Home"); // finds the player homeworld spawn
                shipSpawnHome = SearchUtilities.Find("SpawnShip_Home"); // finds the ship homeworld spawn
                playerSpawnBramble = SearchUtilities.Find("SpawnPlayer_Bramble"); // finds the player bramble spawn
                shipSpawnBramble = SearchUtilities.Find("SpawnShip_Bramble"); // finds the ship bramble spawn


                // set active spawn if player warped to new system with bramble seed
                if (Check2())
               {
                    // deactivate bramble spawns
                    playerSpawnBramble.SetActive(false);
                    shipSpawnBramble.SetActive(false);

                    // activate homeworld spawns
                    playerSpawnHome.SetActive(true);
                    shipSpawnHome.SetActive(true);
                } else {
                    // activate bramble spawns
                    playerSpawnBramble.SetActive(true);
                    shipSpawnBramble.SetActive(true);

                    // deactivate homeworld spawns
                    playerSpawnHome.SetActive(false);
                    shipSpawnHome.SetActive(false);
                }
            } /*else if (TheStrangerTheyAre.NewHorizonsAPI.GetCurrentStarSystem() == "SolarSystem")
            {
                hasWarpedWithBramble = false; // sets to false every solar system loop
                dimension = SearchUtilities.Find("DarkerBramble_StrangerSystemWarp_Body"); // finds the warp bramble dimension
                GlobalMessenger.AddListener("PlayerEnterBrambleDimension", OnPlayerEnterBrambleDimension); // adds global listener for player entry
            }*/
        }
        private bool Check2()
        {
            return Locator.GetShipLogManager().IsFactRevealed("HOME_VISION");
        }
        /*public void OnDestroy()
        {
            if (TheStrangerTheyAre.NewHorizonsAPI.GetCurrentStarSystem() == "SolarSystem")
            {
                GlobalMessenger.RemoveListener("PlayerEnterBrambleDimension", OnPlayerEnterBrambleDimension);
            }
        }

        public void OnPlayerEnterBrambleDimension()
        {
            var dimensionBody = Locator.GetPlayerSectorDetector().GetLastEnteredSector().GetOWRigidbody();
            if (dimensionBody == dimension)
            {
                hasWarpedWithBramble = true; // sets to true if player entered the specified dimension
            }
        }*/
    }
}