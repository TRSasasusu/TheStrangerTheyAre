using NewHorizons.Utility.Files;
using UnityEngine;

namespace TheStrangerTheyAre
{
    public class EndSceneAddition : MonoBehaviour
    {
        //Allows easy editing, should be removed
        public static float speed = 50;
        public static float x = 600;
        public static float y = 20;
        public static float z = 556;
        public static float totalTime = 8;
        public static float delay = 0.5f;
        public static float zRot = -10;


        public static AssetBundle endingBundle = null;
        public static EndSceneAddition instance;
        public bool activated = false;

        private void Awake()
        {
            instance = this;
            gameObject.SetActive(false);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, zRot);
        }

        public void Activate()
        {
            activated = true;
            gameObject.SetActive(true);
        }

        private void LateUpdate()
        {
            if (activated)
            {
                //Count down if delayed
                if (delay > 0)
                {
                    delay -= Time.deltaTime;
                    return;
                }

                //If not delayed, move to the left
                transform.Translate(Vector3.left * speed * Time.deltaTime, Space.Self);
            }
        }

        public static void LoadEndingAdditions()
        {
            //Load the asset bundle
            GameObject endingObj = endingBundle.LoadAsset<GameObject>("Assets/PostCredits/PostCreditsImage.prefab");

            //Make the game object for the dragon
            Transform endingParent = GameObject.Find("PostCreditsScene/Canvas").transform;
            endingObj = GameObject.Instantiate(endingObj, endingParent);
            endingObj.name = "ending";

            //Make sure it's visible and in the right location
            AssetBundleUtilities.ReplaceShaders(endingObj);
            endingObj.transform.localPosition = new Vector3(EndSceneAddition.x, EndSceneAddition.y, EndSceneAddition.z);

            //Need to make sure it's in the right spot of the hierachy to render properly
             endingObj.transform.SetSiblingIndex(4);

            //Add the component
             endingObj.AddComponent<EndSceneAddition>();
        }
    }
}