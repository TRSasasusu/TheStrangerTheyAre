using NewHorizons.Utility;
using UnityEngine;

namespace TheStrangerTheyAre
{
    public class MineralGlassHandler : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] objectsOn;
        [SerializeField]
        private GameObject[] objectsOff;
        private CodeTotemPuzzleHandler codeTotemPuzzle;
        private bool areObjectsOn;

        private void Start()
        {
            areObjectsOn = false;
            codeTotemPuzzle = SearchUtilities.Find("Sector/PreBrambleBase/Interactibles/CodePuzzle").GetComponent<CodeTotemPuzzleHandler>();
            ToggleObjects(true);
        }

        private void Update()
        {
            if (codeTotemPuzzle.areAllCodesMatched)
            {
                ToggleObjects(false);
            }
            else if (!codeTotemPuzzle.areAllCodesMatched && areObjectsOn)
            {
                ToggleObjects(true);
            }
        }

        private void ToggleObjects(bool isOn)
        {
            if (isOn)
            {
                areObjectsOn = true;
                foreach (GameObject objOn in objectsOn)
                {
                    objOn.SetActive(true);
                }
                foreach (GameObject objOff in objectsOff)
                {
                    objOff.SetActive(false);
                }
            } else
            {
                areObjectsOn = false;
                foreach (GameObject objOn in objectsOn)
                {
                    objOn.SetActive(false);
                }
                foreach (GameObject objOff in objectsOff)
                {
                    objOff.SetActive(true);
                }
            }
        }
    }
}
