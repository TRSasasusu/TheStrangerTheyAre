using UnityEngine;
using NewHorizons.Utility;

namespace TheStrangerTheyAre
{
    public class DarkenTheBramble : MonoBehaviour
    {
        // variables to store lights
        GameObject pre;
        GameObject darker;

        void Start()
        {
            // gets the lights
            pre = SearchUtilities.Find("PreBramble_Archives_Body/Atmosphere/AmbientLight_DB_Interior");
            darker = SearchUtilities.Find("DarkerBramble_Hideout_Body/Sector/Atmosphere/AmbientLight_DB_Interior");

            // kills the lights
            Destroy(pre);
            Destroy(darker);
        }
    }
}