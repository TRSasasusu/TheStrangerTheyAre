using NAudio.SoundFont;
using NewHorizons.Components;
using NewHorizons.Utility;
using UnityEngine;

public class QuantumInstrumentTSTA : MonoBehaviour
{
    public delegate void GatherEvent(float flickerOutDuration);

    public delegate void FinishGatherEvent();

    GameObject scientist;
    GameObject sciSector;

    [SerializeField]
    private bool _gatherWithScope;

    private InteractReceiver _interactReceiver;

    private bool _waitToFlickerOut;

    private float _flickerOutTime;

    private ScreenPrompt _scopeGatherPrompt;

    public event GatherEvent OnGather;

    public event FinishGatherEvent OnFinishGather;

    // warp stuff
    private static VesselSpawnPoint campfireSpawn; // to store vessel spawn point
    private static PlayerSpawner _spawner; // for spawning the player

    private void Awake()
    {
        _interactReceiver = GetComponent<InteractReceiver>();
        if (_interactReceiver != null)
        {
            _interactReceiver.OnPressInteract += OnPressInteract;
        }
    }

    private void Start()
    {
        if (_interactReceiver != null)
        {
            _interactReceiver.SetPromptText(UITextType.GatherPrompt);
        }
        else if (_gatherWithScope)
        {
            _scopeGatherPrompt = new ScreenPrompt(InputLibrary.interact, "<CMD> " + UITextLibrary.GetString(UITextType.GatherPrompt));
            Locator.GetPromptManager().AddScreenPrompt(_scopeGatherPrompt, PromptPosition.Center);
        }
        scientist = SearchUtilities.Find("EyeOfTheUniverse_Body/Sector_EyeOfTheUniverse/Sector_Campfire/Campsite/Prefab_IP_GhostBird_Scientist_Eye").gameObject;
        sciSector = SearchUtilities.Find("EyeOfTheUniverse_Body/Sector_EyeOfTheUniverse/Sector_Campfire/InstrumentZones/ScientistSector").gameObject;
    }

    private void OnDestroy()
    {
        if (_interactReceiver != null)
        {
            _interactReceiver.OnPressInteract -= OnPressInteract;
        }
    }

    private void OnPressInteract()
    {
        Gather();
        _interactReceiver.DisableInteraction();
    }

    private void Gather()
    {
        float num = 1f;
        GlobalMessenger<float, float>.FireEvent("FlickerOffAndOn", num, 2f);
        _flickerOutTime = Time.time + num;
        _waitToFlickerOut = true;
        if (this.OnGather != null)
        {
            this.OnGather(_flickerOutTime);
        }
    }

    private void Update()
    {
        if (_gatherWithScope && !_waitToFlickerOut)
        {
            _scopeGatherPrompt.SetVisibility(isVisible: false);
            if (Locator.GetToolModeSwapper().GetSignalScope().InZoomMode() && Vector3.Angle(base.transform.position - Locator.GetPlayerCamera().transform.position, Locator.GetPlayerCamera().transform.forward) < 1f)
            {
                _scopeGatherPrompt.SetVisibility(isVisible: true);
                if (OWInput.IsNewlyPressed(InputLibrary.interact))
                {
                    Gather();
                    Locator.GetPromptManager().RemoveScreenPrompt(_scopeGatherPrompt);
                }
            }
        }
        if (_waitToFlickerOut && Time.time > _flickerOutTime)
        {
            FinishGather();
        }
    }

    private void FinishGather()
    {
        if (this.OnFinishGather != null)
        {
            this.OnFinishGather();
        }

        //Re-enable the distance thing
        SearchUtilities.Find("Volumes_Campfire/EndlessCylinder_Forest").GetComponent<EndlessCylinder>().SetActivation(true);

        //Teleport the player
        campfireSpawn = SearchUtilities.Find("RingedGiant_Body/Sector/Vessel_Body/SPAWN_Vessel").GetComponent<VesselSpawnPoint>(); // gets campfire spawn point
        _spawner = GameObject.FindGameObjectWithTag("Player").GetRequiredComponent<PlayerSpawner>(); // gets player spawner
        _spawner.DebugWarp(campfireSpawn); // warps you to campfire

        //disable/enable things
        scientist.SetActive(true);
        sciSector.SetActive(false);
        base.enabled = false;
    }
}
