using HarmonyLib;
using NewHorizons.Utility;
using UnityEngine;

namespace TheStrangerTheyAre;

[HarmonyPatch]
public class QuantumCampsiteControllerPatch
{
    protected static GameObject scientistZone;
    private static GameObject cypress;
    private static Transform cypressOldParent;

    private static bool Check()
    {
        return PlayerData.GetPersistentCondition("CYPRESS_BOARDVESSEL");
    }

    [HarmonyPostfix]
    [HarmonyPatch(typeof(CosmicInflationController), nameof(CosmicInflationController.Start))]
    private static void Start_Patch(CosmicInflationController __instance)
    {
        if (Check())
        {
            cypress = SearchUtilities.Find("EyeOfTheUniverse_Body/Sector_EyeOfTheUniverse/Prefab_IP_GhostBird_ScientistDescendant_Vessel1");
            scientistZone = SearchUtilities.Find("EyeOfTheUniverse_Body/Sector_EyeOfTheUniverse/Sector_Campfire/InstrumentZones/ScientistSector");
        }
    }

    [HarmonyPostfix]
    [HarmonyPatch(typeof(CosmicInflationController), nameof(CosmicInflationController.StartCollapse))]
    private static void StartCollapse_Patch(CosmicInflationController __instance)
    {
        if (Check() && cypress.gameObject != null)
        {
            Vector3 newPos = new Vector3(-0.9387f, 0.0888f, 7501.938f);
            cypress.transform.localPosition = newPos;
            cypress.transform.rotation = Quaternion.Euler(0, 90, 0);
        }
    }

    [HarmonyPostfix]
    [HarmonyPatch(typeof(CosmicInflationController), nameof(CosmicInflationController.StartInflation))]
    private static void StartInflation_Patch(CosmicInflationController __instance)
    {
        if (Check() && cypress.gameObject != null)
        {
            Vector3 newPos = new Vector3(-2.1178f, -0.9368f, 2.5623f);
            cypress.transform.parent = Locator.GetPlayerTransform();
            cypress.transform.localPosition = newPos;
            //cypress.transform.rotation = Quaternion.Euler(0, 90, 0);
        }
    }

    [HarmonyPostfix]
    [HarmonyPatch(typeof(CosmicInflationController), nameof(CosmicInflationController.StartHotBigBang))]
    private static void StartHotBigBang_Patch(CosmicInflationController __instance)
    {
        if (Check() && cypress.gameObject != null)
        {
            cypress.transform.parent = SearchUtilities.Find("EyeOfTheUniverse_Body/Sector_EyeOfTheUniverse").transform;
        }
    }

    [HarmonyPostfix]
    [HarmonyPatch(typeof(PostCreditsManager), nameof(PostCreditsManager.Update))]
    public static void ActivateLeviathan(PostCreditsManager __instance)
    {
        if (Check() && __instance._fadeOutAfterDelay &&
            EndSceneAddition.instance != null && !EndSceneAddition.instance.activated)
        {
            EndSceneAddition.instance.Activate();
        }

        if (__instance._lanternLit)
        {
            float time2 = Mathf.Max(Time.timeSinceLevelLoad - __instance._lanternLightTime, 0f);
            float num2 = __instance._lanternLightCurve.Evaluate(time2);
            Color color3 = new Color(num2, num2, num2, 1f);
            for (int i = 0; i < 4; i++)
            {
                EndSceneAddition.crabSprites[i].color = color3;
            }
        }
    }
}