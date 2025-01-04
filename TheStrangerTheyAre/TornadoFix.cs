using NewHorizons.Utility;
using UnityEngine;

namespace TheStrangerTheyAre
{
    public class TornadoFix : MonoBehaviour
    {
        private GameObject[] tornadoes = new GameObject[7]; // create new array of gameobjects to store all tornadoes

        public virtual void OnTriggerEnter(Collider hitCollider)
        {
            //checks if player collides with the trigger volume
            if (hitCollider.CompareTag("PlayerDetector") && enabled)
            {
                for (int i = 0; i < 7; i++)
                {
                    tornadoes[i] = SearchUtilities.Find("CRIMSONTORNADO_" + (i + 1)); // gets all tornadoes
                    tornadoes[i].gameObject.SetActive(false); // disables when player enters trigger volume
                }
            }
        }

        public virtual void OnTriggerExit(Collider hitCollider)
        {
            if (hitCollider.CompareTag("PlayerDetector") && enabled)
            {
                for (int i = 0; i < 7; i++)
                {
                    tornadoes[i] = SearchUtilities.Find("CRIMSONTORNADO_" + (i + 1)); // gets all tornadoes
                    tornadoes[i].gameObject.SetActive(true); // disables when player leaves trigger volume
                }
            }
        }
    }
}
