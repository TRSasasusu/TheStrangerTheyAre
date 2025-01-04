using NewHorizons.Utility.Files;
using NewHorizons.Utility;
using System.IO;
using UnityEngine;
using NewHorizons.Components;

namespace TheStrangerTheyAre
{
    public class EyeHandlerTSTA : MonoBehaviour
    {
        public static bool doEyeStuff = false;
        public static AudioClip sciOnly = null;
        public static AudioClip withSol = null;
        public static AudioClip withPrisoner = null;
        public static AudioClip withBoth = null;

        /**
         * Loads the different songs for the ending
         */
        public static void LoadSongs()
        {
            sciOnly = AudioUtilities.LoadAudio(Path.Combine(TheStrangerTheyAre.Instance.ModHelper.Manifest.ModFolderPath, "assets", "Audio", "NewTraveler_wSwP.ogg"));
            withSol = AudioUtilities.LoadAudio(Path.Combine(TheStrangerTheyAre.Instance.ModHelper.Manifest.ModFolderPath, "assets", "Audio", "NewTraveler_nSwP.ogg"));
            withPrisoner = AudioUtilities.LoadAudio(Path.Combine(TheStrangerTheyAre.Instance.ModHelper.Manifest.ModFolderPath, "assets", "Audio", "NewTraveler_wSnP.ogg"));
            withBoth = AudioUtilities.LoadAudio(Path.Combine(TheStrangerTheyAre.Instance.ModHelper.Manifest.ModFolderPath, "assets", "Audio", "NewTraveler_nSnP.ogg"));
        }

        /**
         * Does the fixes for the eye system
         */
        public static void FixEyeSystem()
        {
            //Find the campsite root transform
            Transform campRoot = SearchUtilities.Find("EyeOfTheUniverse_Body/Sector_EyeOfTheUniverse/Sector_Campfire").gameObject.transform;

            //If Scientist isn't here, delete our additions and exit early
            if (!PlayerData.GetPersistentCondition("CYPRESS_BOARDVESSEL"))
            {
                GameObject[] tempArr = new GameObject[12];

                // debug thing here?
                doEyeStuff = false;

                // destroy all eye stuff if condition not met
                // cypress game objects
                GameObject.Destroy(SearchUtilities.Find("EyeOfTheUniverse_Body/Sector_VesselBridge/Prefab_IP_GhostBird_ScientistDescendant_Vessel2").gameObject);
                GameObject.Destroy(SearchUtilities.Find("EyeOfTheUniverse_Body/Sector_VesselBridge/Prefab_IP_GhostBird_ScientistDescendant_Vessel2").gameObject);
                GameObject.Destroy(SearchUtilities.Find("EyeOfTheUniverse_Body/Sector_EyeOfTheUniverse/Prefab_IP_GhostBird_ScientistDescendant_EyeSurface").gameObject);
                GameObject.Destroy(SearchUtilities.Find("EyeOfTheUniverse_Body/Sector_EyeOfTheUniverse/Sector_Campfire/Campsite/Prefab_IP_GhostBird_ScientistDescendant_Vessel1").gameObject);

                // scientist stuff
                GameObject.Destroy(campRoot.Find("Campsite/Prefab_IP_GhostBird_Scientist_Eye").gameObject);
                GameObject.Destroy(campRoot.Find("InstrumentZones/ScientistSector").gameObject);

                // observatory stuff
                GameObject.Destroy(campRoot.parent.Find("Sector_Observatory/SystemModel").gameObject);
                GameObject.Destroy(campRoot.parent.Find("Sector_Observatory/Tube_Mineral").gameObject);
                GameObject.Destroy(campRoot.parent.Find("Sector_Observatory/Mineral_Sign").gameObject);
                GameObject.Destroy(campRoot.parent.Find("Sector_Observatory/System_Sign").gameObject);

                // surface audio stuff
                GameObject.Destroy(campRoot.parent.Find("EyeAudio_Intro1").gameObject);
                GameObject.Destroy(campRoot.parent.Find("EyeAudio_Intro2").gameObject);
                return;
            }

            doEyeStuff = true;

            //Load the custom songs
            LoadSongs();

            //Do stuff for the campsite
            FixCampsite(campRoot);

            //Do stuff for Scientist
            FixScientist(campRoot);

            //Do stuff to the inflation controller
            FixInflationController(campRoot);
        }

        /**
         * Fixes things associated with the campsite controller
         * 
         * @param campRoot the root of the campsite
         */
        private static void FixCampsite(Transform campRoot)
        {
            //Give information to the campsite controller
            QuantumCampsiteController campController = campRoot.GetComponent<QuantumCampsiteController>();

            //Scientist's traveler script
            campController._travelerControllers = AddToArray<TravelerEyeController>(campRoot.Find("Campsite/Prefab_IP_GhostBird_Scientist_Eye").GetComponent<TravelerEyeController>(),
                campController._travelerControllers);
            campController._travelerControllers[campController._travelerControllers.Length - 1].OnStartPlaying += campController.OnTravelerStartPlaying;

            //Scientist's zone
            campController._instrumentZones = AddToArray<GameObject>(campRoot.Find("InstrumentZones/ScientistSector").gameObject,
                campController._instrumentZones);

            //Scientist's object
            campController._travelerRoots = AddToArray<Transform>(campRoot.Find("Campsite/Prefab_IP_GhostBird_Scientist_Eye"),
                campController._travelerRoots);
        }

        /**
         * Fixes things relating directly to Scientist
         * 
         * @param campRoot the root of the campsite
         */
        private static void FixScientist(Transform campRoot)
        {
            //Give Scientist the stuff that he needs to work
            TravelerEyeController scientistController = campRoot.Find("Campsite/Prefab_IP_GhostBird_Scientist_Eye").GetComponent<TravelerEyeController>();

            //Give him the dialogue
            scientistController._dialogueTree = scientistController.transform.Find("EYE_ScientistIntro").gameObject.GetComponent<CharacterDialogueTree>();
            scientistController._dialogueTree.OnStartConversation += scientistController.OnStartConversation;
            scientistController._dialogueTree.OnEndConversation += scientistController.OnEndConversation;

            //Give him the signal
            scientistController._signal = scientistController.transform.Find("ScientistSolo").gameObject.GetComponent<AudioSignal>();
            scientistController._signal._startActive = false;
            scientistController._signal.GetOWAudioSource().playOnAwake = false;
            scientistController._signal.SetSector(campRoot.GetComponent<Sector>());
            scientistController._signal.SetSignalActivation(false, 0);

            //Finally, turn him off for now
            scientistController.gameObject.SetActive(false);
        }

        /**
         * Fixes the inflation controller
         * 
         * @param campRoot The root of the campsite sector
         */
        private static void FixInflationController(Transform campRoot)
        {
            CosmicInflationController inflator = campRoot.GetComponentInChildren<CosmicInflationController>();

            //First, give it Scientist's traveler controller
            inflator._travelers = AddToArray<TravelerEyeController>(campRoot.Find("Campsite/Prefab_IP_GhostBird_Scientist_Eye").GetComponent<TravelerEyeController>(),
                inflator._travelers);
            inflator._travelers[inflator._travelers.Length - 1].OnStartPlaying += inflator.OnTravelerStartPlaying;

            //Next, give it his transform
            inflator._inflationObjects = AddToArray<Transform>(campRoot.Find("Campsite/Prefab_IP_GhostBird_Scientist_Eye"),
                inflator._inflationObjects);
        }

        /**
         * Adds the given element to the given array
         * 
         * @param element The thing to add
         * @param arr The array to add to
         * @return A copy of arr with the added element
         */
        private static T[] AddToArray<T>(T element, T[] arr)
        {
            T[] temp = new T[arr.Length + 1];
            arr.CopyTo(temp, 0);
            temp[temp.Length - 1] = element;
            return temp;
        }
    }
}
