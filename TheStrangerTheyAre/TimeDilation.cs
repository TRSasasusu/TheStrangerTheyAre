using UnityEngine;
using NewHorizons.Utility.OuterWilds;
using OWML.Common;
using OWML.ModHelper;

namespace TheStrangerTheyAre
{
    public class TimeDilation : MonoBehaviour
    {
        private const float DEFAULT_LENGTH = 22;
        
        public void Start()
        {
            float currentLoopValueTSTA = TheStrangerTheyAre.Instance.ModHelper.Storage.Load<float>("save.json"); // loads the current loop from the save.json
            if (Check() && TheStrangerTheyAre.NewHorizonsAPI.GetCurrentStarSystem() == "AnonymousStrangerOW.StrangerSystem" && currentLoopValueTSTA < 34)
            {
                if (currentLoopValueTSTA <= DEFAULT_LENGTH)
                {
                    currentLoopValueTSTA = DEFAULT_LENGTH; // if less than the default, set it equal to default.
                } else
                {
                    TimeLoopUtilities.SetLoopDuration(currentLoopValueTSTA + 2); // save data
                    TheStrangerTheyAre.Instance.ModHelper.Storage.Save<float>((currentLoopValueTSTA + 2), "save.json"); // save data to savefile
                }
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