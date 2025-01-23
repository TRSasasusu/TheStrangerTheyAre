using UnityEngine;

namespace TheStrangerTheyAre
{
    public class TravelRoomHandler : MonoBehaviour
    {
        [SerializeField]
        TravelRoomHandler[] travelTriggers; // all dreamzone water triggers, obtained from unity.
        [SerializeField]
        GameObject[] objects; // to store simWall

        public bool isInside;
        
        void Start()
        {
            GlobalMessenger.AddListener("ExitDreamWorld", OnExitDreamWorld); // checks if player leaves the sim
        }

        void OnExitDreamWorld()
        {
            // object list enabling
            foreach (GameObject objectList in objects)
            {
                objectList.SetActive(true); // enables stuff when left dream world
            }
        }

        public virtual void OnTriggerEnter(Collider hitCollider)
        {
            //checks if player collides with the trigger volume
            if (hitCollider.CompareTag("PlayerDetector") && enabled)
            {
                isInside = true;
                foreach (GameObject objectList in objects)
                {
                    objectList.SetActive(false); // disables stuff when in trigger
                }
            }
        }

        public virtual void OnTriggerExit(Collider hitCollider)
        {
            //checks if player exits with the trigger volume
            if (hitCollider.CompareTag("PlayerDetector") && enabled)
            {
                isInside = false; // disable trigger boolean
                int temp = 0; // temporary variable to count active triggers, set to 0 each time before incrementing.
                foreach (TravelRoomHandler trigger in travelTriggers)
                {
                    if (trigger.isInside)
                    {
                        temp++; // increment temp variable if inside any of the triggers
                    }
                }

                // runs if the temporary variable is less than 1 (0 or less).
                if (temp < 1)
                {
                    // objects list enabling
                    foreach (GameObject objectList in objects)
                    {
                        objectList.SetActive(true); // enables stuff when in trigger
                    }
                }
            }
        }
    }
}