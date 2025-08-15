using NewHorizons.Components;
using NewHorizons.Utility;
using System.Collections;
using UnityEngine;

namespace TheStrangerTheyAre
{
    public class ArchivesShortcutHandler : MonoBehaviour
    {
        [SerializeField]
        private InteractReceiver _interactReceiver;
        [SerializeField]
        private SpawnPoint brambleSpawn;
        private SpawnPoint archivesSpawn;

        // spawn stuff
        protected PlayerSpawner _spawner; // for spawning the player
        public const float blinkTime = 0.5f; // constant for blink time
        public const float animTime = blinkTime / 2f; // constant for blink animation time

        private void Start()
        {
            archivesSpawn = SearchUtilities.Find("PreBramble_Archives_Body/Sector/PreBrambleBase/Interactibles/BrambleArchivesSpawn").GetComponent<SpawnPoint>();
            if (_interactReceiver != null)
            {
                _interactReceiver.OnPressInteract += OnPressInteract;
                _interactReceiver.SetPromptText(UITextType.RotateGearPrompt);
            }
        }
        private void OnPressInteract()
        {
            StartCoroutine(UseShortcut());
        }

        private IEnumerator UseShortcut()
        {
            yield return new WaitForSeconds(0.5f); // let the animation play first
            OWInput.ChangeInputMode(InputMode.None); // stop player input for a while
            var cameraEffectController = FindObjectOfType<PlayerCameraEffectController>(); // gets camera controller

            // close eyes
            cameraEffectController.CloseEyes(animTime); // closes eyes
            yield return new WaitForSeconds(animTime);  // waits until animation stops to proceed to next line
            GlobalMessenger.FireEvent("PlayerBlink"); // fires an event for the player blinking

            // warp to archives
            _spawner = GameObject.FindGameObjectWithTag("Player").GetRequiredComponent<PlayerSpawner>(); // gets player spawner
            _spawner.DebugWarp(brambleSpawn); // warps you to bramble seed
            yield return new WaitForSeconds(2.5f); // waits for player to enter dimension
            _spawner.DebugWarp(archivesSpawn); // warps you to archives

            // open eyes
            cameraEffectController.OpenEyes(animTime, false); // open eyes
            yield return new WaitForSeconds(animTime); //  waits until animation stops to proceed to next line
            OWInput.ChangeInputMode(InputMode.Character); // stop player input for a while
            Locator.GetShipLogManager().RevealFact("PREBRAMBLE_SHORTCUT_E"); // reveal shiplog
        }
    }
}
