using HarmonyLib;
using NewHorizons.Utility;
using UnityEngine;

namespace TheStrangerTheyAre;

[HarmonyPatch]
public class QuantumCampsiteControllerPatch
{
    /*private GameObject[] _instrumentZones;
    private TravelerEyeController[] _travelerControllers;
    private Transform[] _travelerRoots;
    private bool _areInstrumentsActive;
    private bool _hasMetSolanum;
    private bool _hasMetPrisoner;
    private bool _hasErasedPrisoner;
    private bool _hasJamSessionStarted;

    
    private static GameObject scientist;
    private static GameObject scientistSignal;
    private static Animator scientistAnim;
    private static TravelerEyeController scientistController;
    private static Transform scientistRoot;*/
    protected static GameObject scientistZone;
    private static GameObject cypress;
    private static Transform cypressOldParent;

    /*[HarmonyPostfix]
    [HarmonyPatch(typeof(QuantumCampsiteController), nameof(QuantumCampsiteController.ActivateRemainingInstrumentZones))]
    private static void QuantumCampsiteController_ActivateRemainingInstrumentZones_Postfix(QuantumCampsiteController __instance)
    {
        if (Check())
        {
            if (__instance._instrumentZones.Length > 6)
            {
                __instance._instrumentZones[6] = scientistZone;
                __instance._instrumentZones[6].SetActive(true);
            }
        }
    }

    [HarmonyPrefix]
    [HarmonyPatch(typeof(QuantumCampsiteController), nameof(QuantumCampsiteController.GetTravelerMusicEndClip))]
    public static bool GetEndMusic(QuantumCampsiteController __instance, ref AudioClip __result)
    {
        bool flag = __instance._hasMetPrisoner && !__instance._hasErasedPrisoner;
        if (Check())
        {
            if (Check() && flag && __instance._hasMetSolanum)
            {
                __result = AudioUtilities.LoadAudio(Path.Combine(TheStrangerTheyAre.Instance.ModHelper.Manifest.ModFolderPath, "assets", "Audio", "NewTraveler_wSwP.ogg"));
            }
            else if (Check() && flag)
            {
                __result = AudioUtilities.LoadAudio(Path.Combine(TheStrangerTheyAre.Instance.ModHelper.Manifest.ModFolderPath, "assets", "Audio", "NewTraveler_nSwP.ogg"));
            }
            else if (Check() && __instance._hasMetSolanum)
            {
                __result = AudioUtilities.LoadAudio(Path.Combine(TheStrangerTheyAre.Instance.ModHelper.Manifest.ModFolderPath, "assets", "Audio", "NewTraveler_wSnP.ogg"));
            }
            else if (Check())
            {
                __result = AudioUtilities.LoadAudio(Path.Combine(TheStrangerTheyAre.Instance.ModHelper.Manifest.ModFolderPath, "assets", "Audio", "NewTraveler_nSnP.ogg"));
            }
            return false;
        }
        else
        {
            return true;
        }
    }

    [HarmonyPostfix]
    [HarmonyPatch(typeof(QuantumCampsiteController), nameof(QuantumCampsiteController.Start))]
    private static void Start_Patch(QuantumCampsiteController __instance)
    {
        // new shit
        scientist = SearchUtilities.Find("EyeOfTheUniverse_Body/Sector_EyeOfTheUniverse/Sector_Campfire/Campsite/Prefab_IP_GhostBird_Scientist_Eye");
        scientistZone = SearchUtilities.Find("EyeOfTheUniverse_Body/Sector_EyeOfTheUniverse/Sector_Campfire/InstrumentZones/ScientistSector");
        scientistController = scientist.GetComponent<TravelerEyeController>();
        scientistSignal = SearchUtilities.Find("EyeOfTheUniverse_Body/Sector_EyeOfTheUniverse/Sector_Campfire/Campsite/Prefab_IP_GhostBird_Scientist_Eye/ScientistSolo"); 
        scientistAnim = SearchUtilities.Find("EyeOfTheUniverse_Body/Sector_EyeOfTheUniverse/Sector_Campfire/Campsite/Prefab_IP_GhostBird_Scientist_Eye/Ghostbird_IP_ANIM").GetComponent<Animator>();
        cypress = SearchUtilities.Find("EyeOfTheUniverse_Body/Sector_EyeOfTheUniverse/Sector_Campfire/Campsite/Prefab_IP_GhostBird_ScientistDescendant_Vessel1");
        if (Check())
        {
            __instance._instrumentZones = __instance._instrumentZones.Concat(new GameObject[] { scientistZone }).ToArray();
            __instance._travelerControllers = __instance._travelerControllers.Concat(new TravelerEyeController[] { scientistController }).ToArray();
        }
        scientistZone.SetActive(false);
        scientistSignal.SetActive(false);
        scientist.SetActive(false);

        scientistController._dialogueTree = scientistController.gameObject.GetComponentInChildren<CharacterDialogueTree>();
        scientistController._signal = scientistSignal.GetComponent<AudioSignal>();
        scientistController._dialogueTree.OnStartConversation += scientistController.OnStartConversation;
        scientistController._dialogueTree.OnEndConversation += scientistController.OnEndConversation;
    }*/

    private static bool Check()
    {
        return PlayerData.GetPersistentCondition("CYPRESS_BOARDVESSEL");
    }
    /*
    [HarmonyPrefix]
    [HarmonyPatch(typeof(QuantumCampsiteController), nameof(QuantumCampsiteController.OnTravelerStartPlaying))]
    private static bool OnTravelerStartPlaying_Patch(QuantumCampsiteController __instance)
    {
        if (!__instance._hasJamSessionStarted)
        {
            __instance._hasJamSessionStarted = true;
            for (int i = 0; i < __instance._travelerControllers.Length; i++)
            {
                __instance._travelerControllers[i].OnStartCosmicJamSession();
            }
            if (Check())
            {
                scientistSignal.SetActive(true);
            }
        }
        return false;
    }

    [HarmonyPrefix]
    [HarmonyPatch(typeof(QuantumCampsiteController), nameof(QuantumCampsiteController.AreAllTravelersGathered))]
    private static bool AreAllTravelersGathered_Patch(QuantumCampsiteController __instance, out bool __result)
    {
        for (int i = 0; i < __instance._travelerControllers.Length; i++)
        {
            if (!__instance._travelerControllers[i].gameObject.activeInHierarchy && 
                (i != 4 || __instance._hasMetSolanum) && 
                (i != 5 || (__instance._hasMetPrisoner && !__instance._hasErasedPrisoner)) && 
                (i != 6 || Check()))
            {
                __result = false;
                return false;
            }
        }
        __result = true;
        return false;
    }

    [HarmonyPrefix]
    [HarmonyPatch(typeof(CosmicInflationController), nameof(CosmicInflationController.Awake))]
    private static bool Awake_Cosmic_Patch(CosmicInflationController __instance)
    {
        if (Check())
        {
            __instance._travelers = __instance._travelers.Concat(new TravelerEyeController[] { scientistController }).ToArray();
        }
        return true;
    }*/

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
            GameObject.Destroy(scientistZone);
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