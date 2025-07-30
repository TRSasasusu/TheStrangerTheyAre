using HarmonyLib;
using NewHorizons.Utility;
using UnityEngine;

namespace TheStrangerTheyAre;

[HarmonyPatch]
public class PrisonerPatch
{
    [HarmonyPrefix]
    [HarmonyPatch(typeof(PrisonerDirector), nameof(PrisonerDirector.OnPrisonerEmerged))]
    public static void PrisonerDirector_OnPrisonerEmerged_Patch()
    {
        if (PlayerData.GetPersistentCondition("CYPRESS_BOARDVESSEL"))
        {
            GameObject prisDialogue;
            prisDialogue = SearchUtilities.Find("TSTA_Prisoner_Dialogue");
            prisDialogue.SetActive(true);
        }
    }
}