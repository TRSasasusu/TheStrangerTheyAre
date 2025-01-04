using UnityEngine;
using NewHorizons.Utility;

namespace TheStrangerTheyAre
{
    public class LoadPreBrambleVol : MonoBehaviour
    {
        private GameObject[] sim = new GameObject[4];

        public void Start()
        {
            sim[0] = SearchUtilities.Find("PreBramble_Body/Sector");
            sim[1] = SearchUtilities.Find("PreBramble_Body/GravityWell");
            sim[2] = SearchUtilities.Find("PreBramble_Body/RFVolume");
            sim[3] = SearchUtilities.Find("PreBramble_Body/[1] Volumes");
        }

        public virtual void OnTriggerEnter(Collider hitCollider)
        {
            //checks if player collides with the trigger volume
            if (hitCollider.CompareTag("PlayerDetector") && enabled)
            {
                foreach (GameObject go in sim)
                {
                    if (go.activeSelf == false)
                    {
                        go.SetActive(true);
                    }
                }
            }
        }
    }
}