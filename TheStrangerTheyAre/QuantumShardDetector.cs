using UnityEngine;
using OWML.Common;
using NewHorizons.Utility;
using NewHorizons.Utility.Files;

namespace TheStrangerTheyAre
{
    public class QuantumShardDetector : MonoBehaviour
    {
        private GameObject ice; // creates variable to store the enigma state 3 ice
        private GameObject iceBroken; // creates variable to store the enigma state 3 broken ice
        private GameObject shard; // creates variable to store the weak shard
        private GameObject activateSocket; // creates variable to store the weak shard

        private GameObject planetState; // creates variable to store quantum planet
        private GameObject planetState3; // creates variable to store quantum planet
        private GameObject barkShip; // creates vairable to store ship

        private OWAudioSource audio;
        private bool hasBroken; // creates boolean to check if the ice broke
        private bool stateActiveOnce; // creates boolean to check if the player saw state 3 or not.

        private Vector3 shardPos = new Vector3(-179.4065f, 60.6461f, -57.375f); // creates variable to store shard position
        private Quaternion shardRot = new Quaternion(11.4458f, 345.3546f, 74.4897f, -0.0001f); // creates variable to store shard rotation
        private Vector3 moveShipPos = new Vector3(-126.8444f, 51.2016f, -48.3741f); // creates variable to store coordinates to move the ship

        private VisibilityObject _shardVisibilityObject;
        private OWCamera probeCam; // creates variable to store probe camera

        void Start()
        {
            hasBroken = false; // sets activation checking ice to false every loop
            stateActiveOnce = false; // sets state 3 activation checking volume to false every loop

            planetState = TheStrangerTheyAre.NewHorizonsAPI.GetPlanet("Distant Enigma").transform.Find("Sector").gameObject; // gets state 1 of the planet
            planetState3 = TheStrangerTheyAre.NewHorizonsAPI.GetPlanet("Distant Enigma").transform.Find("Sector-3").gameObject; // gets state 3 of the planet

            ice = planetState3.transform.Find("EnigmaThinIce").gameObject; // gets the ice
            iceBroken = planetState3.transform.Find("EnigmaThinIceBroken").gameObject; // gets the broken ice
            shard = planetState.transform.Find("ENIGMA_SHARD_WEAK").gameObject; // gets the weak shard
            audio = shard.GetComponent<OWAudioSource>(); // gets the weak shard
            activateSocket = planetState.transform.Find("Quantum Sockets - WeakenedQuantum/Socket 0").gameObject; // gets the weak shard
            probeCam = Locator.GetProbe().GetForwardCamera().GetOWCamera();
            barkShip = SearchUtilities.Find("Sector-3/State3_Ship");

            _shardVisibilityObject = shard.GetComponent<VisibilityObject>();

            AudioUtilities.SetAudioClip(audio, "assets/Audio/enigmabreak.ogg", TheStrangerTheyAre.Instance); // sets audio clip
        }

        void Update()
        {
            // handle breaking
            if (planetState.activeSelf && shard.activeSelf
                && activateSocket.GetComponent<QuantumSocket>().IsOccupied()
                && _shardVisibilityObject.CheckVisibilityFromProbe(probeCam))
            {
                if (!hasBroken) {
                    audio.Play();
                    hasBroken = true; // sets break-checking boolean to true when weakened shard is active, player is in state 1, and in the right spot.
                }
            }
            if (planetState3.activeSelf)
            {
                // run when broken
                if (hasBroken)
                {
                    barkShip.transform.localPosition = moveShipPos; // moves ship if glass is broken
                    ice.SetActive(false); //  disables normal ice when broken and player is at state 3
                    iceBroken.SetActive(true); // enables broken ice when broken and player is at state 3
                }
                else
                {
                    ice.SetActive(true); // enables normal ice at the start of the loop
                    iceBroken.SetActive(false); // disables broken ice at the start of the loop
                }
            }
        }
    }
}