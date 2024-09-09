using Epic.OnlineServices.Presence;
using HarmonyLib;
using OWML.Common;
using OWML.Utils;
using UnityEngine;


namespace TheStrangerTheyAre
{
    [HarmonyPatch]
    public class CustomSimSpawn : MonoBehaviour
    {

        const string ARRIVAL_GO_PATH = "Sector/PreBramble_SIM/Interactibles/Prefab_IP_DreamArrivalPoint_PreBramble";

        private static DreamArrivalPoint.Location PreBrambleArrivalLocation = EnumUtils.Create<DreamArrivalPoint.Location>("PreBramble");

        private static bool isInCustomDream = false;

        private static GameObject _planetGO;

        private static Sector _preBrambleSector;

        private static OWTriggerVolume _preBrambleVolume;

        private static Sector _sectorHold;

        private static OWTriggerVolume _volumeHold;

        private static OWRigidbody _bodyHold;

        private INewHorizons _newHorizonsAPI;

        [HarmonyPrefix]
        [HarmonyPatch(typeof(DreamWorldController), nameof(DreamWorldController.EnterDreamWorld))]
        private static void EnterDreamWorld(DreamWorldController __instance, DreamCampfire dreamCampfire)
        {
            if (dreamCampfire._dreamArrivalLocation == PreBrambleArrivalLocation)
            {
                _sectorHold = __instance._dreamWorldSector;
                _volumeHold = __instance._dreamWorldVolume;
                _bodyHold = __instance._dreamBody;
                __instance._dreamWorldSector = _preBrambleSector;
                __instance._dreamWorldVolume = _preBrambleVolume;
                __instance._dreamBody = _planetGO.GetComponent<OWRigidbody>();
                _planetGO.SetActive(true);
                isInCustomDream = true;
            }
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(DreamWorldController), nameof(DreamWorldController.ExitDreamWorld), new System.Type[] { typeof(DreamWakeType) })]
        private static void ExitDreamWorld(DreamWorldController __instance, DreamWakeType wakeType)
        {
            if (isInCustomDream)
            {
                __instance._dreamWorldSector = _sectorHold;
                __instance._dreamWorldVolume = _volumeHold;
                __instance._dreamBody = _bodyHold;
                _planetGO.SetActive(false);
                isInCustomDream = false;
            }
        }

        private void Start()
        {
            _newHorizonsAPI = TheStrangerTheyAre.Instance.ModHelper.Interaction.TryGetModApi<INewHorizons>("xen.NewHorizons");
            _planetGO = _newHorizonsAPI.GetPlanet("Pre Bramble");
            var simSpawn = _planetGO.transform.Find(ARRIVAL_GO_PATH).GetComponent<DreamArrivalPoint>();
            var campfireController = transform.Find("Controller_Campfire").GetComponent<DreamCampfire>();

            var oldCampfireLoc = campfireController._dreamArrivalLocation;
            campfireController._entrywayVolumes = new OWTriggerVolume[0];
            simSpawn._entrywayVolumes = new OWTriggerVolume[0];
            simSpawn._sector = _planetGO.transform.Find("Sector").GetComponent<Sector>();
            var oldArrivalLoc = simSpawn._location;

            _preBrambleSector = _planetGO.transform.Find("Sector").GetComponent<Sector>();
            _preBrambleVolume = _planetGO.transform.Find("Volumes/Ruleset").GetComponent<OWTriggerVolume>();
            simSpawn._location = PreBrambleArrivalLocation;
            campfireController._arrivalPoint = simSpawn;
            campfireController._dreamArrivalLocation = PreBrambleArrivalLocation;
            Locator.UnregisterDreamCampfire(campfireController, oldCampfireLoc);
            Locator.RegisterDreamCampfire(campfireController, PreBrambleArrivalLocation);
            Locator.UnregisterDreamArrivalPoint(simSpawn, oldArrivalLoc);
            Locator.RegisterDreamArrivalPoint(simSpawn, PreBrambleArrivalLocation);
        }
        
    }
}