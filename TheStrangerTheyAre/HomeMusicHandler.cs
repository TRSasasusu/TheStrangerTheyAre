using UnityEngine;
using OWML.Common;
using NewHorizons.Utility;

namespace TheStrangerTheyAre;

public class HomeMusicHandler : MonoBehaviour
{
    [SerializeField]
    public GameObject homeMusic; // to store the child gameobject, the main music volume.
    [SerializeField]
    public GameObject homeIntroMusic; // to store the child gameobject, the intro music volume.

    void Awake()
    {
        homeMusic.SetActive(true); // sets headed home volume inactive at the start of each loop
    }
    private void Update()
    {
        if (Check() && !Check2())
        {
            homeMusic.SetActive(false);  // sets main music volume to false when the player has both read the text and didn't yet find the planet
            homeIntroMusic.SetActive(true); // sets home intro music volume to true when the player has both read the text and didn't yet find the planet
            if (homeIntroMusic.FindChild("AudioVolume").GetComponent<OWAudioSource>().isPlaying)
            {
                if (!homeIntroMusic.FindChild("AudioVolume").GetComponent<OWAudioSource>().isPlaying)
                {
                    homeIntroMusic.SetActive(false); // sets home intro music volume to true when the player has both read the text and didn't yet find the planet
                }
            }
        }
        else
        {
            homeMusic.SetActive(true);  // sets main music volume to  when the player saw the vision
            homeIntroMusic.SetActive(false); // sets home intro music volume to false when the player saw the vision
        }
    }
    private bool Check()
    {
        return Locator.GetShipLogManager().IsFactRevealed("LAB_TEXT_TERRA1"); // shiplog entry for reading text
    }
    private bool Check2()
    {
        return Locator.GetShipLogManager().IsFactRevealed("HOME_VISION");  // shiplog entry for homeworld vision
    }
}