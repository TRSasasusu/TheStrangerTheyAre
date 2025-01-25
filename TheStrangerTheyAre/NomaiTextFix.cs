using UnityEngine;
using NewHorizons.Utility;

namespace TheStrangerTheyAre
{
    public class NomaiTextFix : MonoBehaviour
    {
        private BoxCollider nomText;
        private Vector3 textSize = new Vector3(6.1424f, 7.6508f, 0.50f);

        private void Start()
        {
            nomText = SearchUtilities.Find("StrandedVessel_Body/Sector/NOM_INCOMING").GetComponent<BoxCollider>();
            nomText.size = textSize;
        }
    }
}
