using UnityEngine;

namespace TheStrangerTheyAre
{
    [RequireComponent(typeof(OWTriggerVolume))]
    public class QuantumEntanglementVolume : MonoBehaviour
    {
        [SerializeField]
        EclipseDoorController door; // creates variable to store the door, declared via unity.
        [SerializeField]
        GameObject[] doorFlaps = new GameObject[2];

        const int numStates = 3; // creates int value for number of states
        GameObject[] states = new GameObject[numStates]; // creates variable to store each state of the planet.

        bool flashlight = false; // creates boolean to store whether player flashlight is on/off
        bool isSequential = true; // boolean to determine if sequential or random.
        System.Random rnd = new System.Random(); // random number generator
        bool stateChanged = false; // boolean to determine if the state has changed when the player enters
        private bool isInTrigger; // checks if player is in trigger
        private bool isTakingPictures = false; // checks if player has took pictures

        private OWTriggerVolume _triggerVolume; // variable to store trigger volume stuff

        public void Awake()
        {
            _triggerVolume = this.GetRequiredComponent<OWTriggerVolume>(); // getting trigger volume component
            _triggerVolume.OnEntry += OnTriggerVolumeEntry; // link to entry method
            _triggerVolume.OnExit += OnTriggerVolumeExit; // link to exit method
        }

        void Start()
        {
            GlobalMessenger<ProbeCamera>.AddListener("ProbeSnapshot", new Callback<ProbeCamera>(this.OnProbeSnapshot));
            GlobalMessenger.AddListener("Probe Snapshot Removed", new Callback(this.OnProbeSnapshotRemoved));

            var distantEnigma = TheStrangerTheyAre.NewHorizonsAPI.GetPlanet("Distant Enigma"); // gets the quantum planet with nh

            states[0] = distantEnigma.transform.Find("Sector").gameObject; // gets the quantum planet's first state
            states[1] = distantEnigma.transform.Find("Sector-2").gameObject; // gets the quantum planet's second state
            states[2] = distantEnigma.transform.Find("Sector-3").gameObject; // gets the quantum planet's third state

            for (int i = 0; i < states.Length; i++)
            {
                ChangeState();
            }

            flashlight = Locator.GetFlashlight(); // gets the player flashlight
        }

        void OnDestroy()
        {
            GlobalMessenger<ProbeCamera>.RemoveListener("ProbeSnapshot", new Callback<ProbeCamera>(this.OnProbeSnapshot));
            GlobalMessenger.RemoveListener("Probe Snapshot Removed", new Callback(this.OnProbeSnapshotRemoved));
        }

        void OnProbeSnapshot(ProbeCamera probeCam)
        {
            isTakingPictures = true;
        }

        void OnProbeSnapshotRemoved()
        {
            isTakingPictures = false;
        }


        void Update()
        {
            // enable/disable flashlight boolean, check player's flashlight
            if (Locator.GetFlashlight().IsFlashlightOn())
            {
                flashlight = true;
            }
            else
            {
                flashlight = false;
            }

            /*----------------------------------------------------------------------------------------0
            |    this if statement should run if:                                                     |
            |                                                                                         |
            |    - the state hasn't changed yet                                                       |
            |    - the left door's rotation is equal to zero (represented by Quaternion.identity)     |
            |    - the right door's rotation is equal to zero (represented by Quaternion.identity)    |
            |    - player flashlight is off                                                           |
            |    - the player is not taking pictures with their scout                                 |
            |    - the player is in the trigger volume                                                |
            0-----------------------------------------------------------------------------------------0
            */

            if (!stateChanged && doorFlaps[0].transform.localRotation == Quaternion.identity && doorFlaps[1].transform.localRotation == Quaternion.identity && !flashlight && !isTakingPictures && isInTrigger)
            {
                ChangeState(); // calls change state method
                stateChanged = true;
            }
        }

        // change state method
        public void ChangeState()
        {
            int random;

            for (int i = 0; i < states.Length; i++)
            {
                if (states[i].activeSelf) // checks for active state
                {
                    states[i].SetActive(false); // sets current state false
                    if (isSequential) // sequential state change
                    {
                        if (i == states.Length - 1)
                        {
                            states[0].SetActive(true); // activates 0 to loopback to beginning if state is at max length
                            break; // breaks for loop
                        }
                        else
                        {
                            states[i + 1].SetActive(true); // activates the next state in order
                            break; // breaks for loop
                        }
                    }
                    else // randomizer stuff
                    {
                        do
                        {
                            random = rnd.Next(0, states.Length); // randomize
                        }
                        while (random == i); // should randomize until number aside current state is picked.
                        states[random].SetActive(true); // sets random state to true

                        // not sure if i need this, but for loop for disabling all other objects.
                        /*for (int j = 0; j < states.Length; j++)
                        {
                            if (j != random && !states[random].activeSelf)
                            {
                                states[random].SetActive(false);
                            }
                        }*/
                    }
                }
            }
        }
        public void OnTriggerVolumeEntry(GameObject hitObj)
        {
            //checks if player collides with the trigger volume
            if (hitObj.CompareTag("PlayerDetector") && enabled) // commented out because it won't check things properly
            {
                isInTrigger = true;
            }
        }

        public void OnTriggerVolumeExit(GameObject hitObj)
        {
            if (hitObj.CompareTag("PlayerDetector") && enabled)
            {
                isInTrigger = false;
                stateChanged = false;
            }
        }
    }
}