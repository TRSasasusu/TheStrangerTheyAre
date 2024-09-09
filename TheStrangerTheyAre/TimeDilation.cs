using UnityEngine;
using NewHorizons.Utility.OuterWilds;
using OWML.ModHelper;
using Epic.OnlineServices.Stats;
using NewHorizons.Components.Stars;

namespace TheStrangerTheyAre
{
    public class TimeDilation : ModBehaviour
    {
        private const float DEFAULT_LENGTH = 22;
        
        private void Start()
        {
            float currentLoopValueTSTA = ModHelper.Storage.Load<float>("save.json");
            if (Check() && TheStrangerTheyAre.NewHorizonsAPI.GetCurrentStarSystem() == "AnonymousStrangerOW.StrangerSystem" && currentLoopValueTSTA < 44)
            {
                TimeLoopUtilities.SetLoopDuration(currentLoopValueTSTA + 2); // save data
                ModHelper.Storage.Save<float>(currentLoopValueTSTA + 2, "save.json"); // save data to savefile
            } else
            {
                TimeLoopUtilities.SetLoopDuration(DEFAULT_LENGTH);
            }
        }
        private bool Check()
        {
            return Locator.GetShipLogManager().IsFactRevealed("HOME_VISION");
        }
    }
}