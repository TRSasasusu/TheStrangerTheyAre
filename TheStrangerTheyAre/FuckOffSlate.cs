using NewHorizons.Utility;
using UnityEngine;

namespace TheStrangerTheyAre
{
    public class FuckOffSlate : MonoBehaviour
    {
        // variables to store the slate remote dialogues
        private GameObject evilDialogue;
        private GameObject evilDialogue2;

        void Start()
        {
            evilDialogue = SearchUtilities.Find("TimberHearth_Body/Sector_TH/Sector_Village/Sector_StartingCamp/Characters_StartingCamp/Villager_HEA_Slate/TSTA_SlateRemote"); // dialogue used to tell the player about a strange signal in the village
            evilDialogue2 = SearchUtilities.Find("TimberHearth_Body/Sector_TH/Sector_Village/Sector_StartingCamp/Characters_StartingCamp/Villager_HEA_Slate/TSTA_SlateRemote2"); // dialogue used to tell the player about using the scout to land on angler's eye
        }

        void Update()
        {
            if (Locator.GetShipLogManager().IsFactRevealed("HEARTH_VISION") && evilDialogue != null) // checks if the player saw the vision on timber hearth and if the dialogue volume is null
            {
                Destroy(evilDialogue); // destroys the signal hint dialogue
            }

            if (evilDialogue2 != null) // checks if dialogue volume is null
            {
                if (!Locator.GetShipLogManager().IsFactRevealed("ANGLERS_EYE_MAIN_E") || PlayerData.GetPersistentCondition("SLATE_HASYELLED")) // checks if the player hasn't been to angler's eye yet or hasn't already been yelled at by slate about using the scout to land.
                {
                    Destroy(evilDialogue2); // destroys the scout-landing dialogue
                }
            }
        }
    }
}