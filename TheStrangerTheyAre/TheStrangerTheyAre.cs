using HarmonyLib;
using NewHorizons.Utility;
using OWML.Common;
using OWML.ModHelper;
using System.Linq;
using System.Reflection;
using UnityEngine;
using NewHorizons.Utility.Files;

namespace TheStrangerTheyAre
{
    public class TheStrangerTheyAre : ModBehaviour
    {
        public static INewHorizons NewHorizonsAPI { get; private set; }
        private GameObject oldTitlePlanet;
        private GameObject titleRigidBody;
        private AssetBundle _homeMoonBundle;
        private AssetBundle endingBundle;

        private void Awake()
        {
            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());
        }

        public interface IAchievements
        {
            void RegisterAchievement(string uniqueID, bool secret, ModBehaviour mod);
            void RegisterTranslation(string uniqueID, TextTranslation.Language language, string name, string description);
            void RegisterTranslationsFromFiles(ModBehaviour mod, string folderPath);
            void EarnAchievement(string uniqueID);
            bool HasAchievement(string uniqueID);
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

        private void PlaceTitlePlanet()
        {
            var background = GameObject.Find("Background");
            var planetpivot = GameObject.Find("PlanetPivot");
            planetpivot.FindChild("Prefab_HEA_Campfire").SetActive(false);
            planetpivot.FindChild("PlanetRoot").SetActive(false);

            if (_homeMoonBundle == null)
            {
                _homeMoonBundle = ModHelper.Assets.LoadBundle("assets/AssetBundle/title");
            }
            GameObject homemoon = Instantiate(_homeMoonBundle.LoadAsset<GameObject>("Assets/NewTitlePlanet.prefab"));
            AssetBundleUtilities.ReplaceShaders(homemoon);
            homemoon.transform.parent = planetpivot.transform;
            homemoon.transform.localPosition = new Vector3(3.4349f, - 12.3667f, 3.1489f);
            homemoon.transform.localRotation = Quaternion.Euler(333.0655f, 8.4042f, 7.4983f);
        }

        private void Start()
        {
            // Starting here, you'll have access to OWML's mod helper.
            ModHelper.Console.WriteLine($"My mod {nameof(TheStrangerTheyAre)} is loaded!", MessageType.Success);
            var AchievementsAPI = ModHelper.Interaction.TryGetModApi<IAchievements>("xen.AchievementTracker");

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
            bool isMuricaOn = ModHelper.Interaction.ModExists("Hawkbar.FreedomUnits");
            if (isMuricaOn)
            {
                ModHelper.Console.WriteLine("IMPERIALISM DETECTED! SHUTTING DOWN!", MessageType.Error);
                Application.Quit();
            }
            PlaceTitlePlanet();
            LoadManager.OnCompleteSceneLoad += (scene, loadScene) =>
            {
                if (loadScene == OWScene.PostCreditsScene)
                {
                    if (endingBundle == null)
                    {
                        endingBundle = ModHelper.Assets.LoadBundle("assets/AssetBundle/postcredits");
                        if (endingBundle != null)
                        {
                            EndSceneAddition.LoadEndingAdditions(endingBundle);
                        }
                    }
                } else { 
                    // unload when not on title screen
                    if (_homeMoonBundle != null)
                    {
                        _homeMoonBundle.Unload(true);
                        _homeMoonBundle = null;
                    }
                }

                if (loadScene != OWScene.SolarSystem) return;
                ModHelper.Console.WriteLine("Loaded into solar system!", MessageType.Success);
            };
        }


        private void OnSolarSystemLoaded()
        {
            if (NewHorizonsAPI.GetCurrentStarSystem().Equals("SolarSystem"))
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
}
