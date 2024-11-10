using HarmonyLib;
using UnityEngine;

namespace TheStrangerTheyAre;

[HarmonyPatch]
public class QuantumCampsiteControllerPatch
{
    //################################# Eye Things #################################
    /**
     * When other instrument zone's are activated, also activate Ditylum's
     * 
     * @param __instance The calling campsite controller
     */
    [HarmonyPostfix]
    [HarmonyPatch(typeof(QuantumCampsiteController), nameof(QuantumCampsiteController.ActivateRemainingInstrumentZones))]
    public static void ScientistZoneFix(QuantumCampsiteController __instance)
    {
        if (EyeHandlerTSTA.doEyeStuff)
        {
            __instance._instrumentZones[6].SetActive(true);
        }
    }

    /**
     * Make sure that we fetch the correct audio clip
     */
    [HarmonyPrefix]
    [HarmonyPatch(typeof(QuantumCampsiteController), nameof(QuantumCampsiteController.GetTravelerMusicEndClip))]
    public static bool GetEndMusic(QuantumCampsiteController __instance, ref AudioClip __result)
    {
        //Ditylum isn't there, don't change anything
        if (!EyeHandlerTSTA.doEyeStuff)
        {
            return true;
        }

        //Otherwise, use flags to determine what clip to use
        bool prisonerPresent = __instance._hasMetPrisoner && !__instance._hasErasedPrisoner;
        __result = EyeHandlerTSTA.sciOnly; //Default is only Ditylum is there
        if (__instance._hasMetSolanum && prisonerPresent) //Both others are there
        {
            __result = EyeHandlerTSTA.withBoth;
        }
        else if (__instance._hasMetSolanum) //Only Solanum made it
        {
            __result = EyeHandlerTSTA.withSol;
        }
        else if (prisonerPresent) //Only prisoner made it
        {
            __result = EyeHandlerTSTA.withPrisoner;
        }

        //Don't run the original method
        return false;
    }

    /*
 * Have the end scene creature come in
 */
    [HarmonyPostfix]
    [HarmonyPatch(typeof(PostCreditsManager), nameof(PostCreditsManager.Update))]
    public static void ActivateLeviathan(PostCreditsManager __instance)
    {
        //Only show the leviathan if it hasn't shown up and we've met Ditylum
        if (PlayerData.GetPersistentCondition("CYPRESS_BOARDVESSEL") && __instance._fadeOutAfterDelay &&
            EndSceneAddition.instance != null && !EndSceneAddition.instance.activated)
        {
            EndSceneAddition.instance.Activate();
            __instance._delayedFadeTime = Time.time + EndSceneAddition.totalTime;
        }
    }
}