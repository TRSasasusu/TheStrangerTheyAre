using UnityEngine;
using NewHorizons.Utility;

namespace TheStrangerTheyAre;

public class SolanumDialogueFix : MonoBehaviour
{
    private GameObject solanumDialogue;
    private GameObject solanumTorch;
    private GameObject baseDialogue;
    private bool hasDestroyed;

    private void Start()
    {
        hasDestroyed = false;
        solanumDialogue = SearchUtilities.Find("QuantumMoon_Body/Sector_QuantumMoon/State_EYE/Interactables_EYEState/ConversationPivot/Character_NOM_Solanum/TSTA_Solanum");
        solanumTorch = SearchUtilities.Find("QuantumMoon_Body/Sector_QuantumMoon/State_EYE/Interactables_EYEState/ConversationPivot/Character_NOM_Solanum/TSTA_SolanumTorch");
        baseDialogue = GameObject.Find("QuantumMoon_Body/Sector_QuantumMoon/State_EYE/Interactables_EYEState/ConversationPivot/Character_NOM_Solanum/ConversationZone"); // gets the base game dialogue of solanum
    }

    private void Update()
    {
        if (DialogueConditionManager.SharedInstance.GetConditionState("TSTA_SOL_SCANNED") == true)
        {
            solanumDialogue.SetActive(true);
            if (!hasDestroyed)
            {
                Destroy(baseDialogue);
                Destroy(solanumTorch);
                hasDestroyed = true;
            }
        } else
        {
            solanumDialogue.SetActive(false);
        }
    }
}
