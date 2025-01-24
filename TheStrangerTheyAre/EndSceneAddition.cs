using NewHorizons.Utility;
using NewHorizons.Utility.Files;
using OWML.ModHelper;
using UnityEngine;

namespace TheStrangerTheyAre
{
    public class EndSceneAddition : MonoBehaviour
    {
        //Allows easy editing, should be removed
        public static Vector3 newAxolotlPos = new Vector3(130, 100, 0);
        public static Quaternion newAxolotlRot = Quaternion.Euler(0, 180, 329.2757f);
        public static EndSceneAddition instance;
        public bool activated = false;
        private static GameObject[] axolotl = new GameObject[3];
        public static SpriteRenderer[] crabSprites = new SpriteRenderer[4];

        private void Awake()
        {
            instance = this;
            Activate();
        }

        public void Activate()
        {
            activated = true;
            gameObject.SetActive(true);
        } 

        public static void LoadEndingAdditions(AssetBundle endingBundle)
        {
            // define axolotl
            axolotl[0] = SearchUtilities.Find("PostCreditsScene/Canvas/PostCreditsPainting_7a");
            axolotl[1] = SearchUtilities.Find("PostCreditsScene/Canvas/PostCreditsPainting_7b");
            axolotl[2] = SearchUtilities.Find("PostCreditsScene/Canvas/PostCreditsPainting_7c");

            // turn axolotl
            foreach (GameObject axo in axolotl)
            {
                axo.transform.localPosition = newAxolotlPos;
                axo.transform.localRotation = newAxolotlRot;
            }

            // load objects, define objects and parent
            GameObject endingPlanet = endingBundle.LoadAsset<GameObject>("Assets/PostCredits/PostCreditsPlanet.prefab");
            GameObject endingCrab1 = endingBundle.LoadAsset<GameObject>("Assets/PostCredits/PostCreditsCrab1.prefab");
            GameObject endingCrab2 = endingBundle.LoadAsset<GameObject>("Assets/PostCredits/PostCreditsCrab2.prefab");
            GameObject endingCrab3 = endingBundle.LoadAsset<GameObject>("Assets/PostCredits/PostCreditsCrab3.prefab");
            GameObject spaceShip = endingBundle.LoadAsset<GameObject>("Assets/PostCredits/PostCreditsShip.prefab");
            Transform endingParent = GameObject.Find("PostCreditsScene/Canvas").transform;

            // instantiate objects
            endingPlanet = GameObject.Instantiate(endingPlanet, endingParent);
            endingCrab1 = GameObject.Instantiate(endingCrab1, endingParent);
            endingCrab2 = GameObject.Instantiate(endingCrab2, endingParent);
            endingCrab3 = GameObject.Instantiate(endingCrab3, endingParent);
            spaceShip = GameObject.Instantiate(spaceShip, endingParent);

            // rename stuff
            endingPlanet.name = "EndingPlanet";
            endingCrab1.name = "EndingCrab1";
            endingCrab2.name = "EndingCrab2";
            endingCrab3.name = "EndingCrab3";
            spaceShip.name = "EndingShip";

            //Make sure it's visible and in the right location
            AssetBundleUtilities.ReplaceShaders(endingPlanet);
            AssetBundleUtilities.ReplaceShaders(endingCrab1);
            AssetBundleUtilities.ReplaceShaders(endingCrab2);
            AssetBundleUtilities.ReplaceShaders(endingCrab3);
            AssetBundleUtilities.ReplaceShaders(spaceShip);

            // position stuff
            endingPlanet.transform.localPosition = new Vector3(-500, 400, 2000);
            endingCrab1.transform.localPosition = new Vector3(-100, -360, 0);
            endingCrab2.transform.localPosition = new Vector3(-430, -390, 0);
            endingCrab3.transform.localPosition = new Vector3(-550, -410, 0);
            spaceShip.transform.localPosition = new Vector3(-965, -563.5699f, 400);

            //Need to make sure it's in the right spot of the hierachy to render properly
            endingPlanet.transform.SetSiblingIndex(0);
            endingCrab1.transform.SetSiblingIndex(5);
            endingCrab2.transform.SetSiblingIndex(6);
            endingCrab3.transform.SetSiblingIndex(7);
            spaceShip.transform.SetSiblingIndex(8);

            // get ending crab sprite renderers
            crabSprites[0] = endingCrab1.GetComponent<SpriteRenderer>();
            crabSprites[1] = endingCrab2.GetComponent<SpriteRenderer>();
            crabSprites[2] = endingCrab3.GetComponent<SpriteRenderer>();
            crabSprites[3] = spaceShip.GetComponent<SpriteRenderer>();

            // make them black for now
            for (int i = 0; i < crabSprites.Length; i++)
            {
                crabSprites[i].color = Color.black;
            }

            //Add the component
            endingPlanet.AddComponent<EndSceneAddition>();
        }
    }
}