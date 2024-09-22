using HarmonyLib;
using OWML.ModHelper;
using System.IO;
using TheStrangerTheyAre;
using UnityEngine;
using OWML.Common;
using OWML.ModHelper.Events;
using NewHorizons.Components.Stars;

namespace TheStrangerTheyAre;

[HarmonyPatch]
public class QuantumCampsiteControllerPatch
{
    private GameObject[] _instrumentZones;
    private TravelerEyeController[] _travelerControllers;
    private Transform[] _travelerRoots;
    private bool _areInstrumentsActive;
    private bool _hasMetSolanum;
    private bool _hasMetPrisoner;
    private bool _hasErasedPrisoner;

    protected GameObject scientistZone;
    private GameObject scientist;
    private GameObject scientistSignal;
    private Animator scientistAnim;
    private TravelerEyeController scientistController;
    private Transform scientistRoot;

    [HarmonyPrefix] // this method causes mod to not load
    [HarmonyPatch(typeof(QuantumCampsiteController), nameof(QuantumCampsiteController.GetTravelerMusicEndClip))]
    private static bool QuantumCampsiteController_GetTravelerMusicEndClip_Prefix(QuantumCampsiteController __instance, ref AudioClip __result)
    {
        bool flag = __instance._hasMetPrisoner && !__instance._hasErasedPrisoner;
        //AudioClip newAudioType; // haven't implemented this yet, so ignore it.
        if (Check() && flag && __instance._hasMetSolanum)
        {
            Path.Combine(TheStrangerTheyAre.Instance.ModHelper.Manifest.ModFolderPath, "assets", "Audio", "NewTraveler_wSwP.ogg"); // still don't know how i can convert this to audioclip so it doesn't set itself equal to anything.
        } else if (Check() && flag)
        {
            Path.Combine(TheStrangerTheyAre.Instance.ModHelper.Manifest.ModFolderPath, "assets", "Audio", "NewTraveler_nSwP.ogg");
        } else if (Check() && __instance._hasMetSolanum)
        {
            Path.Combine(TheStrangerTheyAre.Instance.ModHelper.Manifest.ModFolderPath, "assets", "Audio", "NewTraveler_wSnP.ogg");
        } else if (Check())
        {
            Path.Combine(TheStrangerTheyAre.Instance.ModHelper.Manifest.ModFolderPath, "assets", "Audio", "NewTraveler_nSnP.ogg");
        }
        
        if (Check())
        {
            return false;
        } else
        {
            return true;
        }
    }

    [HarmonyPostfix] // this method causes mod to not load
    [HarmonyPatch(typeof(QuantumCampsiteController), nameof(QuantumCampsiteController.ActivateRemainingInstrumentZones))]
    private static void QuantumCampsiteController_ActivateRemainingInstrumentZones_Postfix(QuantumCampsiteController __instance)
    {
        if (Check())
        {
            if (__instance._instrumentZones.Length > 6)
            {
                __instance._instrumentZones[6] = GameObject.Find("ScientistSector");
                __instance._instrumentZones[6].SetActive(true);
            }
        }
    }

    // everything below allows the mod to run, if i comment the two problematic methods out.
    private void Awake(QuantumCampsiteController __instance)
    {
        // new shit
        scientist = GameObject.Find("Prefab_IP_GhostBird_Scientist_Eye");
        scientistZone = GameObject.Find("ScientistSector");
        scientistController = GameObject.Find("Prefab_IP_GhostBird_Scientist_Eye").GetComponent<TravelerEyeController>();
        scientistSignal = scientist.transform.Find("ScientistSolo").gameObject;
        scientistAnim = scientist.transform.Find("Ghostbird_IP_ANIM").GetComponent<Animator>();
        AddInstrumentZone(scientistZone, scientistController);
        scientistZone.SetActive(false);
        scientist.SetActive(false);

        scientistController._dialogueTree = scientistController.gameObject.GetComponentInChildren<CharacterDialogueTree>();
        scientistController._dialogueTree.OnStartConversation += scientistController.OnStartConversation;
        scientistController._dialogueTree.OnEndConversation += scientistController.OnEndConversation;

        // existing shit
        __instance._trigger.OnEntry += __instance.OnEntry;
        __instance._trigger.OnExit += __instance.OnExit;
        __instance._campfire.OnCampfireStateChange += __instance.OnCampfireStateChange;
        __instance._eskerDialogue.OnStartConversation += __instance.OnStartEskerConversation;
        __instance._riebeckDialogue.OnEndConversation += __instance.OnEndRiebeckConversation;
        for (int i = 0; i < __instance._travelerControllers.Length; i++)
        {
            __instance._travelerControllers[i].OnStartPlaying += __instance.OnTravelerStartPlaying;
        }
    }

    private static bool Check()
    {
        return Locator.GetShipLogManager().IsFactRevealed("NEWSIM_SCIENTIST_CLONE");
    }

    public void Update()
    {
        if (scientist.activeSelf)
        {
            if (DialogueConditionManager.SharedInstance.GetConditionState("SCI_TRIGGERPLAY"))
            {
                scientistSignal.SetActive(true);
                scientistAnim.Play("TSTA_PlayInstrument", 0);
            }
            else
            {
                scientistSignal.SetActive(false);
                scientistAnim.Play("Prisoner_LeanOnTree_HoldInstrument", 0);
            }
        }
    }
    public void AddInstrumentZone(GameObject newZone, TravelerEyeController newController)
    {
        GameObject[] newInstrumentZones = new GameObject[_instrumentZones.Length + 1];
        TravelerEyeController[] newTravelerControllers = new TravelerEyeController[_travelerControllers.Length + 1];

        for (int i = 0; i < _instrumentZones.Length; i++)
        {
            newInstrumentZones[i] = _instrumentZones[i];
        }
        for (int i = 0; i < _travelerControllers.Length; i++)
        {
            newTravelerControllers[i] = _travelerControllers[i];
        }

        newInstrumentZones[_instrumentZones.Length] = newZone;
        newTravelerControllers[_instrumentZones.Length] = newController;

        _instrumentZones = newInstrumentZones;
        _travelerControllers = newTravelerControllers;
    }
}