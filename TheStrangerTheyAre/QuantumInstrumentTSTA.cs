using UnityEngine;

namespace TheStrangerTheyAre;
public class QuantumInstrumentTSTA : MonoBehaviour
{
    public delegate void GatherEvent(float flickerOutDuration);

    public delegate void FinishGatherEvent();

    //private GameObject[] _activateObjects;

    //private GameObject[] _deactivateObjects;

    private bool _gatherWithScope = true;

    private InteractReceiver _interactReceiver;

    private bool _waitToFlickerOut;

    private float _flickerOutTime;

    private ScreenPrompt _scopeGatherPrompt;

    public event GatherEvent OnGather;

    public event FinishGatherEvent OnFinishGather;

    GameObject scientist;

    private void Awake()
    {
        /*_activateObjects[0] = GameObject.Find("Sector_EyeOfTheUniverse/Sector_Campfire/Campsite/Prefab_IP_GhostBird_Scientist_EyeIdle");
        _deactivateObjects[0] = GameObject.Find("EyeOfTheUniverse_Body/Sector_EyeOfTheUniverse/Sector_Campfire/InstrumentZones/PrisonerZone/PlanetLightController/OriginalPlacement/Prefab_IP_VisiblePlanet");
        _deactivateObjects[1] = GameObject.Find("EyeOfTheUniverse_Body/Sector_EyeOfTheUniverse/Sector_Campfire/InstrumentZones/PlanetWithLab_EYE");

        _activateObjects[0].SetActive(false);*/
        var eye = GameObject.Find("EyeOfTheUniverse_Body/Sector_EyeOfTheUniverse"); // gets the quantum planet with nh

        scientist = eye.transform.Find("Sector_Campfire/Campfire/Prefab_IP_GhostBird_Scientist_EyeIdle").gameObject; // gets the quantum planet's first state
        _interactReceiver = GetComponent<InteractReceiver>();
        if (_interactReceiver != null)
        {
            _interactReceiver.OnPressInteract += OnPressInteract;
        }

    }

    private void Start()
    {
        _scopeGatherPrompt = new ScreenPrompt(InputLibrary.interact, "<CMD> " + UITextLibrary.GetString(UITextType.GatherPrompt));
        Locator.GetPromptManager().AddScreenPrompt(_scopeGatherPrompt, PromptPosition.Center);
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
        } else if (scientist.activeSelf)
        {
            scientist.SetActive(false);
        }
    }

    private void FinishGather()
    {
        if (this.OnFinishGather != null)
        {
            this.OnFinishGather();
        }
        scientist.SetActive(true);
        base.enabled = false;
    }
}