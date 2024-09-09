using UnityEngine;
using OWML.Common;
using NewHorizons.Components.Volumes;

namespace TheStrangerTheyAre
{
    public class DetermineSystemSpawn : MonoBehaviour
    {
        // variables
        //static int warpID = 0; // creates variable to store the id of the warp
        //static GameObject warpNode; // creates variable to store the bramble warp node
        void Awake()
        {
            if (TheStrangerTheyAre.NewHorizonsAPI.GetCurrentStarSystem() == "AnonymousStrangerOW.StrangerSystem")
            {
                if (Check())
                {
                    // home spawn enable here, bramble spawn disable too
                } else
                {
                    // home spawn disable here, bramble spawn enable too
                }
                //warpNode = GameObject.Find("DarkerBramble_StrangerSystemWarp_Body/Sector/WarpVolume"); // gets the underwater floor in the fourth sector of the simulation
            }
        }

        static void Update()
        {

        }

        private bool Check()
        {
            return Locator.GetShipLogManager().IsFactRevealed("HOME_REVEAL"); // shiplog entry for when planet discovered
        }

        private bool Check2()
        {
            return Locator.GetShipLogManager().IsFactRevealed("LAB_TEXT_TERRA1"); // shiplog entry for reading text
        }
    }
}