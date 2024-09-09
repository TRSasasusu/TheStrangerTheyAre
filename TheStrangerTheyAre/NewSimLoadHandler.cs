using UnityEngine;

namespace TheStrangerTheyAre
{
    public class NewSimLoadHandler : MonoBehaviour
    {
        /*GameObject newSim; // creates variable to store the new sim sector

        private INewHorizons _newHorizonsAPI;

        private void Start()
        {
            _newHorizonsAPI = TheStrangerTheyAre.Instance.ModHelper.Interaction.TryGetModApi<INewHorizons>("xen.NewHorizons");
        }
        void Awake()
        {
            newSim = GameObject.Find("Sector_NewSim"); // gets the underwater floor in the fourth sector of the simulation
            TheStrangerTheyAre.WriteLine("NEW SECTOR FOUND!", MessageType.Success); // debug message
            newSim.SetActive(false); // deactivates object when outside the trigger
            TheStrangerTheyAre.WriteLine("DEACTIVATED THE NEW SIM SECTOR", MessageType.Success); // debug message
        }

        public virtual void OnTriggerEnter(Collider hitCollider)
        {
            TheStrangerTheyAre.WriteLine("SOMETHING ENTERED THE NEW SIM TRIGGER VOLUME", MessageType.Success); // debug message
            //checks if player collides with the trigger volume
            if (hitCollider.CompareTag("PlayerDetector") && enabled)
            {
                TheStrangerTheyAre.WriteLine("PLAYER ENTERED THE NEW SIM TRIGGER VOLUME", MessageType.Success); // debug message
                newSim.SetActive(true); // activates object when inside the trigger
                TheStrangerTheyAre.WriteLine("ACTIVATED THE NEW SIM SECTOR", MessageType.Success); // debug messageger
            }
        }*/
    }
}