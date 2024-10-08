using UnityEngine;

// credit to Hawkbar for writing this.

namespace TheStrangerTheyAre
{
   /* public class PreBrambleSpawn : MonoBehaviour
    {
        GameObject preBramble; // creates variable to store gameobject name

        private INewHorizons _newHorizonsAPI;
        private void Start()
        {
            _newHorizonsAPI = TheStrangerTheyAre.Instance.ModHelper.Interaction.TryGetModApi<INewHorizons>("xen.NewHorizons");
        }
        void Awake()
        {
            preBramble = GameObject.Find("PreBramble_Body"); // sets gameobject to prebramble
            SetPreBrambleActive(false); // prevents it from spawning at the start of the loop
            GlobalMessenger.AddListener("EnterDreamWorld", OnEnterDreamWorld); // checks if player enters the sim
            GlobalMessenger.AddListener("ExitDreamWorld", OnExitDreamWorld); // checks if player leaves the sim
        }

        // when item despawns, do the following:
        void OnDestroy()
        {
            GlobalMessenger.RemoveListener("EnterDreamWorld", OnEnterDreamWorld); // stops checking if player enters the sim
            GlobalMessenger.RemoveListener("ExitDreamWorld", OnExitDreamWorld); // stops checking if player leaves the sim
        }

        void OnEnterDreamWorld()
        {
            SetPreBrambleActive(true); // activates object when in the volume
        }

        void OnExitDreamWorld()
        {
            SetPreBrambleActive(false);  // deactivates object when not in the volume
        }

        private void SetPreBrambleActive(bool active)
        {
            // We set the children active/inactive so we can still find the body (cant find inactive objects) also base game does this with the Dreamworld so why not
            foreach (Transform child in preBramble.transform)
            {
                child.gameObject.SetActive(active);
            }
        }
    }*/
}