using UnityEngine;
using NewHorizons.Utility;

namespace TheStrangerTheyAre
{
    public class NewSimLoadHandler : MonoBehaviour
    {
        GameObject[] simulations = new GameObject[2]; // all custom simulations
        void Start()
        {
            if (TheStrangerTheyAre.NewHorizonsAPI.GetCurrentStarSystem() == "SolarSystem")
            {
                // define custom simulations
                simulations[0] = SearchUtilities.Find("PreBramble_SIM"); // gets pre bramble
                simulations[1] = SearchUtilities.Find("Sector_NewSim"); // gets new sim section

                // sets dreamworld false each loop
                foreach (GameObject sim in simulations)
                {
                    sim.SetActive(false);
                }

                // add listeners
                GlobalMessenger.AddListener("ExitDreamWorld", OnExitDreamWorld); // checks if player leaves the sim
                GlobalMessenger.AddListener("EnterDreamWorld", OnEnterDreamWorld); // checks if player leaves the sim
            }

        }

        void OnEnterDreamWorld()
        {
            // custom simulations enabled
            foreach (GameObject sim in simulations)
            {
                sim.SetActive(true);
            }
        }

        void OnExitDreamWorld()
        {
            // custom simulations disabled
            foreach (GameObject sim in simulations)
            {
                sim.SetActive(false);
            }
        }
    }
}