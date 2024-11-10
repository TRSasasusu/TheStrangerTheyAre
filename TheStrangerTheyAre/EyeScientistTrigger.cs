using UnityEngine;

namespace TheStrangerTheyAre
{
    public class EyeScientistTrigger : MonoBehaviour
    {
        [SerializeField]
        GameObject sci1; // creates variable to store scientist1
        [SerializeField]
        GameObject sci2; // creates variable to store scientist2
        bool triggerHit;

        void Awake()
        {
            triggerHit = false;
        }
        void Update()
        {
            if (this.gameObject.activeSelf && !triggerHit)
            {
                sci1.SetActive(true); // activates object when inside the trigger
                sci2.SetActive(false); // activates object when inside the trigger
            }
        }

        public virtual void OnTriggerEnter(Collider hitCollider)
        {
            //checks if player collides with the trigger volume
            if (hitCollider.CompareTag("PlayerDetector") && enabled)
            {
                triggerHit=true;
                sci1.SetActive(false); // activates object when inside the trigger
                sci2.SetActive(true); // activates object when inside the trigger
            }
        }
    }
}