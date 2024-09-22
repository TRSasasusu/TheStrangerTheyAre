using UnityEngine;
using OWML.Common;

namespace TheStrangerTheyAre
{
    public class RingedLabIntroMusicHandler : MonoBehaviour
    {
        [SerializeField]
        public GameObject Intro; // to store the child gameobject, the intro music volume.
        [SerializeField]
        public GameObject Main; // to store the child gameobject, the main music volume.

        GameObject defaultAudio; // to store the default audio for the station.
        void Awake()
        {
            //Intro.SetActive(false); // sets headed home intro volume inactive at the start of each loop
            //Main.SetActive(false); // sets headed home volume inactive at the start of each loop
            defaultAudio = GameObject.Find("RingedLaboratory_Body/Sector/AudioVolume");
        }
        private void Update()
        {
            if (Check() && !Check2())
            {
                if (Check3())
                {
                    Intro.SetActive(false); // sets headed home intro volume to true
                    Main.SetActive(true); // sets headed home volume to true
                    defaultAudio.SetActive(false); // sets default to false
                } else
                {
                    Intro.SetActive(true); // sets headed home intro volume to true
                    Main.SetActive(false); // sets headed home volume to true
                    defaultAudio.SetActive(false); // sets default to false
                }
            } else
            {
                Intro.SetActive(false); // sets headed home intro volume to true
                Main.SetActive(false); // sets headed home volume to true
                defaultAudio.SetActive(true); // sets default to false
            }
        }

        private bool Check()
        {
            return Locator.GetShipLogManager().IsFactRevealed("LAB_TERRA_E");
        }

        private bool Check3()
        {
            if (Locator.GetShipLogManager().IsFactRevealed("LAB_TEXT_TERRA2") && Locator.GetShipLogManager().IsFactRevealed("LAB_TEXT_TERRA1"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool Check2()
        {
            return Locator.GetShipLogManager().IsFactRevealed("HOME_VISION");
        }
    }
    }