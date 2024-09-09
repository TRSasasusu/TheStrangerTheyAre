using UnityEngine;

namespace TheStrangerTheyAre
{
    public class TextSwap : MonoBehaviour
    {
        public GameObject TranslatorText;
        public GameObject Dialogue;

        private bool isSwapped;

        private void Awake()
        {

        }

        private void Start()
        {
            if (Check())
            {
                TranslatorText.SetActive(false);
                Dialogue.SetActive(true);
                isSwapped = true;
            }
            else
            {
                TranslatorText.SetActive(true);
                Dialogue.SetActive(false);
                isSwapped = false;
            }
        }

        private void Update()
        {
            if (!isSwapped && Check())
            {
                TranslatorText.SetActive(false);
                Dialogue.SetActive(true);
            }
        }

        private bool Check()
        {
            return Locator.GetShipLogManager().IsFactRevealed("ANGLERS_EYE_ALIENTEXT_E2");
        }
    }
}