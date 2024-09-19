using UnityEngine;

namespace TheStrangerTheyAre
{
    public class EyeNewSignalTrigger : MonoBehaviour
    {
        [SerializeField]
        GameObject signal; // creates variable to store signal
        [SerializeField]
        GameObject parentObject; // creates variable to store parent
        bool triggerHit;

        void Awake()
        {
            triggerHit = false;
        }
        void Update()
        {
            if (parentObject.activeSelf && !triggerHit)
            {
                signal.SetActive(true); // activates object when inside the trigger
            }
        }

        public virtual void OnTriggerEnter(Collider hitCollider)
        {
            //checks if player collides with the trigger volume
            if (hitCollider.CompareTag("PlayerDetector") && enabled)
            {
                triggerHit = true;
                signal.SetActive(false); // activates object when inside the trigger
            }
        }
    }
}