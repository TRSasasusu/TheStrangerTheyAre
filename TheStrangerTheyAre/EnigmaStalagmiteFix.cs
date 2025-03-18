using NewHorizons.Utility;
using System.Collections.Generic;
using UnityEngine;

namespace TheStrangerTheyAre
{
    public class EnigmaStalagmiteFix : MonoBehaviour
    {
        // list variables to store each stalagmite in distant enigmas
        private List<Transform> stalagmitesState1;
        private List<Transform> stalagmitesState2;
        private List<Transform> stalagmitesState3;

        void Start()
        {
            // gets all scattered stalagmites placed with new horizons on each state, stores in a list.
            stalagmitesState1 = FindObjectsWithNameWildcard(SearchUtilities.Find("DistantEnigma_Body/Sector").transform, "ENIGMAROCK_");
            stalagmitesState3 = FindObjectsWithNameWildcard(SearchUtilities.Find("DistantEnigma_Body/Sector-3").transform, "ENIGMAROCK_");

            // iterates through the list to set the transforms of the third state to match the first state. this should prevent ships from blowing up, and it expects the length of all states to be the same.
            for (int i = 0; i < stalagmitesState1.Count; i++) {
                stalagmitesState3[i] = stalagmitesState1[i];
            }
        }

        List<Transform> FindObjectsWithNameWildcard(Transform parent, string wildcard)
        {
            List<Transform> matchingObjects = new List<Transform>(); // creates temp list to 

            // look through all children of the desired parent (which in this case is each state of enigma)
            foreach (Transform child in parent)
            {
                if (child.name.StartsWith(wildcard))
                {
                    matchingObjects.Add(child); // if a child's name matches wildcard "ENIGMAROCK_", adds it to the temp list.
                }
            }

            return matchingObjects; // returns temp list to define the global lists for each state.
        }
    }
}