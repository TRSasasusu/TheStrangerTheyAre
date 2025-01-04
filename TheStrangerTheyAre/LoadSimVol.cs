using UnityEngine;
using OWML.Common;
using NewHorizons.Utility;

namespace TheStrangerTheyAre
{
    public class LoadSimVol : MonoBehaviour
    {
        private DreamWorldController sim;

        public void Start()
        {
            sim = SearchUtilities.Find("DreamWorld_Body").GetComponent<DreamWorldController>();
        }

        public virtual void OnTriggerEnter(Collider hitCollider)
        {
            //checks if player collides with the trigger volume
            if (hitCollider.CompareTag("PlayerDetector") && enabled)
            {
                sim.IsInDream();
            }
        }
    }
}