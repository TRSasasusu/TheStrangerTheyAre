using HarmonyLib;
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

            // adds the spawn determine thing
            //Locator.GetPlayerBody().gameObject.AddComponent<DetermineSystemSpawn>();

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
        }

        private void OnSolarSystemLoaded()
        {
            // TODO: Remove this and prefer to add the component in Unity, Idiot doesn't have unity so he will make do
            var campfire = GameObject.Find("AnglersEye_Body/Sector/BrambleMuseum/Interactables/Prefab_IP_DreamCampfire");
            campfire.AddComponent<CustomSimSpawn>();

            // Fix offset issues on PreBramble. The terrain was placed incorrectly as were all props place onto it
            // The children are inactive so have to find them manually
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
