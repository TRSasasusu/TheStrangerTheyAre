using NewHorizons.Utility;
using UnityEngine;

namespace TheStrangerTheyAre
{
    public class FuckOffSlate : MonoBehaviour
    {
        GameObject evilDialogue;

        void Start()
        {
            evilDialogue = SearchUtilities.Find("TimberHearth_Body/Sector_TH/Sector_Village/Sector_StartingCamp/Characters_StartingCamp/Villager_HEA_Slate/TSTA_SlateRemote");
        }

        void Update()
        {
            if (Check() && evilDialogue != null)
            {
                Destroy(evilDialogue);
            }
        }

        private bool Check()
        {
            return Locator.GetShipLogManager().IsFactRevealed("HEARTH_VISION");
        }
    }
}