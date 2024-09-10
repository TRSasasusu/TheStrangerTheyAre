using UnityEngine;
using OWML.Common;

namespace TheStrangerTheyAre
{
    public class QuantumEntanglementVolume : MonoBehaviour
    {
        [SerializeField]
        EclipseDoorController door; // creates variable to store the door, declared via unity.

        const int numStates = 3; // creates int value for number of states
        GameObject[] states = new GameObject[numStates]; // creates variable to store each state of the planet.

        Flashlight flashlight; // creates variable to store the player flashlight
        int currentState; // creates integer to store the current state
        bool isSequential = true; // boolean to determine if sequential or random.
        System.Random rnd = new System.Random(); // random number generator
        bool stateChanged = false; // boolean to determine if the state has changed when the player enters


        void Start()
        {
            var distantEnigma = TheStrangerTheyAre.NewHorizonsAPI.GetPlanet("Distant Enigma");

            states[0] = distantEnigma.transform.Find("Sector").gameObject; // gets the quantum planet's first state
            states[1] = distantEnigma.transform.Find("Sector-2").gameObject; // gets the quantum planet's second state
            states[2] = distantEnigma.transform.Find("Sector-3").gameObject; // gets the quantum planet's third state

            flashlight = Locator.GetFlashlight(); // gets the player flashlight
            if (states[0].activeSelf)
            {
                currentState = 0;
            }
            else if (states[1].activeSelf)
            {
                currentState = 1;
            }
            else if (states[2].activeSelf)
            {
                currentState = 2;
            }
        }

        // change state method
        public void ChangeState()
        {
            int random = rnd.Next(0, states.Length);
            for (int i = 0; i < states.Length; i++)
            {
                TheStrangerTheyAre.WriteLine("Index is currently: " + i, MessageType.Success); // debug message
                if (i == currentState)
                {
                    states[i].SetActive(false);
                    TheStrangerTheyAre.WriteLine("Current State Disabled! ("+i+")", MessageType.Success); // debug message
                } else
                {
                    if (isSequential == true)
                    {
                        if (i == currentState + 1)
                        {
                            TheStrangerTheyAre.WriteLine("State should be changing...", MessageType.Success); // debug message
                            if (i == states.Length - 1)
                            {
                                currentState = 0;
                                states[0].SetActive(true);
                                TheStrangerTheyAre.WriteLine("State " + 0 + " Active!", MessageType.Success); // debug message
                            }
                            else
                            {
                                currentState = i;
                                states[i].SetActive(true);
                                TheStrangerTheyAre.WriteLine("State " + i + " Active!", MessageType.Success); // debug message
                            }
                        }
                        else
                        {
                            TheStrangerTheyAre.WriteLine("State shouldn't be changing...", MessageType.Success); // debug message
                            states[i].SetActive(false);
                            TheStrangerTheyAre.WriteLine("State " + i + " Inactive!", MessageType.Success); // debug message
                        }
                    }
                    else
                    {
                        if (i == random && i != currentState)
                        {
                            currentState = i;
                            states[i].SetActive(true);
                            TheStrangerTheyAre.WriteLine("State " + i + "Active!", MessageType.Success); // debug message
                        }
                        else
                        {
                            states[i].SetActive(false);
                            TheStrangerTheyAre.WriteLine("State " + i + "Inactive!", MessageType.Success); // debug message
                        }
                    }
                }
            }
        }
        private void OnTriggerEnter(Collider hitCollider)
        {
            //checks if player collides with the trigger volume
            if (hitCollider.CompareTag("PlayerDetector") && enabled /*&& flashlight.IsFlashlightOn() == false && !door._frontDoor.IsOpen()*/) // commented out because it won't check things properly
            {
                if (stateChanged == false){
                    ChangeState(); // calls change state method
                    stateChanged = true;
                }
            }
        }

       private void OnTriggerExit(Collider hitCollider)
        {
            if (hitCollider.CompareTag("PlayerDetector") && enabled)
            {
                stateChanged = false;
            }
        }
    }
}