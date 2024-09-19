using UnityEngine;
using NewHorizons.Utility.OuterWilds;

namespace TheStrangerTheyAre
{
    public class TimeDilation : MonoBehaviour
    {
        private const float DEFAULT_LENGTH = 22;
        
        public static void TimeDilator()
        {
            float currentLoopValueTSTA = TheStrangerTheyAre.Instance.ModHelper.Storage.Load<float>("save.json");
            if (Check() && TheStrangerTheyAre.NewHorizonsAPI.GetCurrentStarSystem() == "AnonymousStrangerOW.StrangerSystem" && currentLoopValueTSTA < 34)
            {
                TimeLoopUtilities.SetLoopDuration(currentLoopValueTSTA + 2); // save data
                TheStrangerTheyAre.Instance.ModHelper.Storage.Save<float>(currentLoopValueTSTA + 2, "save.json"); // save data to savefile
            } else
            {
                TimeLoopUtilities.SetLoopDuration(DEFAULT_LENGTH);
            }
        }
        private static bool Check()
        {
            return Locator.GetShipLogManager().IsFactRevealed("HOME_VISION");
        }
    }
}