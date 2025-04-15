using HarmonyLib;
using UnityEngine;

namespace TheStrangerTheyAre;

[HarmonyPatch]
public class InsulationPatch
{
    [HarmonyPrefix]
    [HarmonyPatch(typeof(InsulatingVolume), nameof(InsulatingVolume.OnEffectVolumeEnter))]
    public static void InsulatingVolume_OnEffectVolumeEnter_Patch(InsulatingVolume __instance, GameObject hitObj)
    {
        HazardDetector component = hitObj.GetComponent<HazardDetector>();
        if (component != null)
        {
            component.AddInsulatingVolume(__instance);
        }
    }

    [HarmonyPrefix]
    [HarmonyPatch(typeof(InsulatingVolume), nameof(InsulatingVolume.OnEffectVolumeExit))]
    public static void InsulatingVolume_OnEffectVolumeExit_Patch(InsulatingVolume __instance, GameObject hitObj)
    {
        HazardDetector component = hitObj.GetComponent<HazardDetector>();
        if (component != null)
        {
            component.RemoveInsulatingVolume(__instance);
        }
    }
}