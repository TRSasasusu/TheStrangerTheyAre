using OWML.Common;
using UnityEngine;

namespace TheStrangerTheyAre
{
    public class QuantumDoorChecker : MonoBehaviour
    {
        [SerializeField]
        private GameObject qDoorClosed;
        [SerializeField]
        private GameObject qDoorOpen;

        EclipseInterface eclipse;
        private void Start()
        {
            var distantEnigma = TheStrangerTheyAre.NewHorizonsAPI.GetPlanet("Distant Enigma"); // gets the quantum planet with nh

            eclipse = distantEnigma.transform.Find("Sector-2/EnigmaInterface/Prefab_IP_Door_Interface").GetComponent<EclipseInterface>();
        }

        private void Update()
        {
            if (eclipse.isOpen)
            {
                qDoorClosed.SetActive(false);
                qDoorOpen.SetActive(true);
            }
            else
            {
                qDoorClosed.SetActive(true);
                qDoorOpen.SetActive(false);
            }
        }
    }
}
