using Epic.OnlineServices;
using HarmonyLib;
using NewHorizons.Utility;
using OWML.Common;
using OWML.ModHelper;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace TheStrangerTheyAre
{
    public class TheStrangerTheyAre : ModBehaviour
    {
        public static INewHorizons NewHorizonsAPI { get; private set; }
        private GameObject oldTitlePlanet;
        private GameObject titleRigidBody;
        private AssetBundle bundle;
        

        private void Awake()
        {
            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());
        }
        
        //DEBUG
        public static TheStrangerTheyAre Instance
        {
            get
            {
                if (instance == null) instance = FindObjectOfType<TheStrangerTheyAre>();
                return instance;
            }
        }

        private static TheStrangerTheyAre instance;

        public static void WriteLine(string text, MessageType messageType = MessageType.Message)
        {
            Instance.ModHelper.Console.WriteLine(text, messageType);
        }

        private void Start()
        {
            // Starting here, you'll have access to OWML's mod helper.
            ModHelper.Console.WriteLine($"My mod {nameof(TheStrangerTheyAre)} is loaded!", MessageType.Success);


            // Get the New Horizons API and load configs
            NewHorizonsAPI = ModHelper.Interaction.TryGetModApi<INewHorizons>("xen.NewHorizons");
            NewHorizonsAPI.LoadConfigs(this);

            // Example of accessing game code.
            LoadManager.OnCompleteSceneLoad += (scene, loadScene) =>
            {
                if (loadScene != OWScene.SolarSystem) return;

                // Wait a few frames to make sure its done
                if (NewHorizonsAPI.GetCurrentStarSystem() == "SolarSystem")
                {
                    ModHelper.Events.Unity.FireInNUpdates(OnSolarSystemLoaded, 5);
                }

                ModHelper.Console.WriteLine("Loaded into solar system!", MessageType.Success);
            };

            /*bundle = ModHelper.Assets.LoadBundle("assets/AssetBundle/strangerbundle");
            LoadManager.OnCompleteSceneLoad += Stuff;
            Stuff(OWScene.TitleScreen, OWScene.TitleScreen);*/
        }

        /*private void Stuff(OWScene scene, OWScene loadScene)
        {
            if (loadScene != OWScene.TitleScreen) return;

            titleRigidBody = SearchUtilities.Find("Scene/Background/PlanetPivot");
            oldTitlePlanet = SearchUtilities.Find("Scene/Background/PlanetPivot/PlanetRoot");

            var prefab = bundle.LoadAsset<GameObject>("Assets/NewTitlePlanet.prefab");
            GameObject.Instantiate(oldTitlePlanet, titleRigidBody.transform);
            prefab.transform.SetParent(titleRigidBody.transform);
            prefab.transform.position = oldTitlePlanet.transform.position;
            prefab.transform.rotation = oldTitlePlanet.transform.rotation;
            oldTitlePlanet.SetActive(false);
            ModHelper.Console.WriteLine("Disable Old Planet", MessageType.Success);
        }*/

        private void OnSolarSystemLoaded()
        {
            var preBramble = GameObject.Find("PreBramble_Body");
            var preBrambleSector = GameObject.Find("PreBramble_Body").transform.Find("Sector");

            // Offset all children of the planet to match the ground model (includes GravityWell here)
            var offset = new Vector3(-10.1f, 246.8f, 99.9f);
            foreach (Transform child in preBramble.transform)
            {
                // Skip the sector because some of its children need to move and some don't
                if (child.name != "Sector")
                {
                    child.localPosition += offset;
                }
            }

            // Everything NH made under Sector (Fog, Air, AmbientLight) is centered so we offset them just like the ground model
            var childrenToOffset = new string[] { "AmbientLight", "Air", "FogSphere", "Atmosphere", "GroundSphere", "Water" };
            foreach (Transform child in preBrambleSector.transform)
            {
                if (childrenToOffset.Any(x => x == child.name))
                {
                    child.localPosition += offset;
                }
            }

            // Makes sure that artifacts get blown out when going under water
            Locator.GetPlayerBody().gameObject.AddComponent<HeldArtifactWaterHandler>();
        }
    }
}
