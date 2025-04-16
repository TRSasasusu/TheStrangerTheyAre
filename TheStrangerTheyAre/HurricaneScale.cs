using NewHorizons.Utility;
using UnityEngine;

namespace TheStrangerTheyAre
{
    public class HurricaneScale : MonoBehaviour
    {
        // variables
        private GameObject hurricane; // to store the hurricane gameobject
        private Vector3 scale = new Vector3 (0.2f, 0.301f, 0.2f); // set scale amount in vector3

        void Start()
        {
            // find object
            hurricane = SearchUtilities.Find("AnglersEye_Body/Sector/Hurricane");

            // set scale
            hurricane.transform.localScale = scale; // set scale equal to the vector3
        }
    }
}
