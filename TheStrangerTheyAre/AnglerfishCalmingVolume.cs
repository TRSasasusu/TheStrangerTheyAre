using UnityEngine;
using NewHorizons.Utility;

namespace TheStrangerTheyAre
{
    public class AnglerfishCalmingVolume : MonoBehaviour
    {
        private AnglerfishController[] anglerControllers = new AnglerfishController[2];
        private SocketedQuantumObject[] anglerQuantumControllers = new SocketedQuantumObject[2];

        void Start()
        {
            for (int i = 0; i < 2; i++)
            {
                anglerControllers[i] = SearchUtilities.Find("BASE_ANGLER" + (i + 1)).GetComponent<AnglerfishController>();
                anglerQuantumControllers[i] = SearchUtilities.Find("BASE_ANGLER" + (i + 1)).GetComponent<SocketedQuantumObject>();
            }
            
        }

        public virtual void OnTriggerEnter(Collider hitCollider)
        {
            //checks if player collides with the trigger volume
            if (hitCollider.CompareTag("PlayerDetector") && enabled)
            {
                for (int i = 0; i < 2; i++)
                {
                    anglerQuantumControllers[i].ChangeQuantumState(true);
                    anglerControllers[i].ChangeState(AnglerfishController.AnglerState.Lurking);
                }
            }
        }
    }
}