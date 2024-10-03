using NewHorizons.Utility;
using UnityEngine;

namespace TheStrangerTheyAre
{
    public class QuantumFatassFix : MonoBehaviour
    {
        BoxShape lightBox;
        BoxShape anglerBox;
        void Start()
        {
            // gets the bounding box for giant angler
            lightBox = SearchUtilities.Find("DarkerBramble_BigAngler_Body/Sector/GiantAnglerfish/Scale/Beast_Anglerfish/B_angler_root/B_angler_body01/B_angler_body02/B_angler_antenna01/B_angler_antenna02/B_angler_antenna03/B_angler_antenna04/B_angler_antenna05/B_angler_antenna06/B_angler_antenna07/B_angler_antenna08/B_angler_antenna09/B_angler_antenna10/B_angler_antenna11/B_angler_antenna12_end/Lure_FogGlow").GetComponent<BoxShape>();
            anglerBox = SearchUtilities.Find("DarkerBramble_BigAngler_Body/Sector/GiantAnglerfish/Scale/Beast_Anglerfish/Beast_Anglerfish").GetComponent<BoxShape>();
        }

        void OnSolarSystemLoaded()
        {
            // divides it so it can be used as quantum socket
            lightBox.size = lightBox.size / 6;
            anglerBox.size = anglerBox.size / 6;
        }
    }
}
