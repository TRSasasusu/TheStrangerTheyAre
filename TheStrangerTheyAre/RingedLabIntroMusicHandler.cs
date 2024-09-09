using UnityEngine;

namespace TheStrangerTheyAre
{
    public class RingedLabIntroMusicHandler : MonoBehaviour
    {
        [SerializeField]
        public GameObject Intro; // to store the child gameobject, the intro music volume.
        [SerializeField]
        public GameObject Main; // to store the child gameobject, the main music volume.

        public bool hasActivated; // creates boolean to check if the trigger has been activated at least once

        void Awake()
        {
            Intro.SetActive(false); // sets headed home intro volume inactive at the start of each loop
            Main.SetActive(false); // sets headed home volume inactive at the start of each loop
        }
        private void Update()
        {
            if (Check() && !Check2() && Intro.GetComponent<RingedLabIntroMusicTrigger>().playerLeft == false)
            {
                Intro.SetActive(true);  // sets headed home intro volume active when the player has read the text, didn't leave the volume, and didn't yet find the planet
            }
            else if (Intro.activeSelf && !Check() || Intro.activeSelf && Check2() || Intro.GetComponent<RingedLabIntroMusicTrigger>().playerLeft == true)
            {
                Intro.SetActive(false); // sets headed home intro volume inactive when intro is active, and either when player never read the text, player left the volume, or player has already found planet
            }

            if (Check() && !Check2() && Intro.GetComponent<RingedLabIntroMusicTrigger>().playerLeft == true && Intro.GetComponent<RingedLabIntroMusicTrigger>().hasActivated == true)
            {
                Main.SetActive(true);  // sets headed home volume active when the player has both read the text and didn't yet find the planet
            }
            else if (Check() || !Check2() || Intro.GetComponent<RingedLabIntroMusicTrigger>().playerLeft == false && Intro.GetComponent<RingedLabIntroMusicTrigger>().hasActivated == false)
            {
                Main.SetActive(false); // sets headed home volume inactive when intro is active, and either when player never read the text, player left the volume, or player has already found planet
            }
        }

        private bool Check()
        {
            return Locator.GetShipLogManager().IsFactRevealed("LAB_TEXT_TERRA1");
        }
        private bool Check2()
        {
            return Locator.GetShipLogManager().IsFactRevealed("HOME_REVEAL");
        }
    }
}