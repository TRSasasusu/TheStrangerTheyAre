using UnityEngine;
using NewHorizons.Utility;

namespace TheStrangerTheyAre
{
    public class AnglerfishHandlerTSTA : MonoBehaviour
    {
        private SocketedQuantumObject[] anglerQuantumControllers = new SocketedQuantumObject[7];

        void Start()
        {
            anglerQuantumControllers[0] = SearchUtilities.Find("ANGLER_MAIN_1").GetComponent<SocketedQuantumObject>();
            anglerQuantumControllers[1] = SearchUtilities.Find("ANGLER_MAIN_2").GetComponent<SocketedQuantumObject>();
            anglerQuantumControllers[2] = SearchUtilities.Find("BASE_ANGLER1").GetComponent<SocketedQuantumObject>();
            anglerQuantumControllers[3] = SearchUtilities.Find("BASE_ANGLER2").GetComponent<SocketedQuantumObject>();
            anglerQuantumControllers[4] = SearchUtilities.Find("NEST_ANGLER1").GetComponent<SocketedQuantumObject>();
            anglerQuantumControllers[5] = SearchUtilities.Find("NEST_ANGLER2").GetComponent<SocketedQuantumObject>();
            anglerQuantumControllers[6] = SearchUtilities.Find("NEST_ANGLER3").GetComponent<SocketedQuantumObject>();

            for (int i = 0; i < 2; i++)
            {   
                anglerQuantumControllers[i]._maxSnapshotLockRange = 1000; // amplifies snapshot range to fit the anglerfish, so snapshots can be made
                anglerQuantumControllers[i]._visibilityTrackers[0] = anglerQuantumControllers[i]._visibilityTrackers[1]; // fixes the visibility trackers so the player can look away easier
            }   
        }
    }
}