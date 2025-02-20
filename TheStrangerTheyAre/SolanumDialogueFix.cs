using UnityEngine;
using NewHorizons.Utility;
using NewHorizons.Components.Props;

namespace TheStrangerTheyAre;

public class SolanumDialogueFix : MonoBehaviour
{
    private GameObject solanumDialogue;

    private void Start()
    {
        solanumDialogue = SearchUtilities.Find("QuantumMoon_Body/Sector_QuantumMoon/State_EYE/Interactables_EYEState/ConversationPivot/Character_NOM_Solanum/TSTA_Solanum");
    }

    private void Update()
    {
        if (DialogueConditionManager.SharedInstance.GetConditionState("TSTA_SOL_SCANNED") == true)
        {
            solanumDialogue.SetActive(true);
        } else
        {
            solanumDialogue.SetActive(false);
        }
    }
}
