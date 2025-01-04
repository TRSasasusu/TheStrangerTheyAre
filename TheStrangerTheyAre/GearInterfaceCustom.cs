using NewHorizons.Utility;
using UnityEngine;

namespace TheStrangerTheyAre
{
   public class GearInterfaceCustom : MonoBehaviour
    {
        [SerializeField]
        private InteractReceiver _interactReceiver;

        [SerializeField]
        private GearInterfaceEffects _gearInterface;

        [SerializeField]
        private GameObject monitorOn;

        [SerializeField]
        private GameObject monitorOff;

        private GameObject vesselSignal;

        private bool isOn = false;

        public bool hasControls => _interactReceiver != null;

        private void Start()
        {
            if (_interactReceiver != null)
            {
                _interactReceiver.OnPressInteract += OnPressInteract;
                _interactReceiver.SetPromptText(UITextType.RotateGearPrompt);
            }
            vesselSignal = SearchUtilities.Find("STRANDED_VESSEL_SIGNAL");
            vesselSignal.SetActive(false);
            monitorOff.SetActive(true);
            monitorOn.SetActive(false);
        }

        private void OnDestroy()
        {
            if (_interactReceiver != null)
            {
                _interactReceiver.OnPressInteract -= OnPressInteract;
            }
        }

        private void OnPressInteract()
        {
            if (_gearInterface != null)
            {
                _gearInterface.AddRotation(90f);
                isOn ^= true; // toggles is on
                vesselSignal.SetActive(isOn);
                if (isOn)
                {
                    monitorOff.SetActive(false);
                    monitorOn.SetActive(true);
                } else
                {
                    monitorOff.SetActive(true);
                    monitorOn.SetActive(false);
                }
            }
            _interactReceiver.ResetInteraction();
        }
    }
}
