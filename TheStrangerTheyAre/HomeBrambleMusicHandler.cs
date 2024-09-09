using UnityEngine;

namespace TheStrangerTheyAre
{
    public class HomeBrambleMusicHandler : MonoBehaviour
    {
        [SerializeField]
        public GameObject music; // to store the child gameobject, the music music volume.

        void Awake()
        {
            music.SetActive(false); // sets headed home volume inactive at the start of each loop
        }
        private void Update()
        {
            if (Check() && !Check2())
            {
                music.SetActive(true);  // sets headed home volume active when the player has both read the text and didn't yet find the planet
            }
            else
            {
                music.SetActive(false); // sets headed home volume inactive when intro is active, and either when player never read the text, player left the volume, or player has already found planet
            }
        }

        private bool Check()
        {
            return Locator.GetShipLogManager().IsFactRevealed("LAB_TEXT_TERRA1"); // shiplog entry for reading text
        }
        private bool Check2()
        {
            return Locator.GetShipLogManager().IsFactRevealed("HOME_REVEAL");  // shiplog entry for homeworld reveal
        }
    }
}