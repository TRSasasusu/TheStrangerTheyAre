using UnityEngine;
using NewHorizons.Utility;
using OWML.ModHelper;

namespace TheStrangerTheyAre
{
    public class LoadSimVol : MonoBehaviour
    {
        // variables
        private PlayerSectorDetector player; // for storing the player's sector detector
        private Sector simSector; // for storing the sim sector
        private Sector dz4Sector; // for storing the dz4 sector
        private DreamCampfireStreaming dz4Stream; // for storing the dream campfire streaming script
        public bool hasEnteredVolume;

        void Start()
        {
            hasEnteredVolume = false;
            player = Locator.GetPlayerSectorDetector(); // get player's sector detector
            GlobalMessenger.AddListener("ExitDreamWorld", OnExitDreamWorld); // checks if player leaves the sim
            simSector = SearchUtilities.Find("DreamWorld_Body/Sector_DreamWorld").GetComponent<Sector>(); // get sim root sector
            dz4Sector = SearchUtilities.Find("DreamWorld_Body/Sector_DreamWorld/Sector_DreamZone_4").GetComponent<Sector>(); // get dz4 root sector
            dz4Stream = SearchUtilities.Find("RingWorld_Body/Sector_RingInterior/Sector_Zone4/PrisonDocks/Sector_PrisonInterior/Interactibles_PrisonInterior/Prefab_IP_DreamCampfire/DreamCampfire_Streaming").GetComponent<DreamCampfireStreaming>(); // get dream campfire streaming script
        }

        public virtual void OnTriggerEnter(Collider hitCollider)
        {
            //checks if player collides with the trigger volume
            if (hitCollider.CompareTag("PlayerDetector") && enabled)
            {
                hasEnteredVolume = true;
                simSector.AddOccupant(player); // adds player to sim root sector
                dz4Sector.AddOccupant(player); // adds player to sim dz4 sector
                dz4Stream.UpdatePreloadingState(true); // runs the streamer for dz4 manually
            }
        }

        void OnExitDreamWorld()
        {
            hasEnteredVolume = false;
        }
    }
}