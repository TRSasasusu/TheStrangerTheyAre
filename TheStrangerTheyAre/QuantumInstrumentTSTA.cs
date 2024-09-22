using UnityEngine;

namespace TheStrangerTheyAre;
public class QuantumInstrumentTSTA : MonoBehaviour
{
    public delegate void GatherEvent(float flickerOutDuration);

    public delegate void FinishGatherEvent();

    private GameObject[] _activateObjects;

    private GameObject[] _deactivateObjects;

    private bool _gatherWithScope = false;

    private InteractReceiver _interactReceiver;

    private bool _waitToFlickerOut;

    private float _flickerOutTime;

    private ScreenPrompt _scopeGatherPrompt;

    public event GatherEvent OnGather;

    public event FinishGatherEvent OnFinishGather;

    GameObject scientist;
    private GameObject endlessVolume;

    private void Awake()
    {
        var fire = GameObject.Find("EyeOfTheUniverse_Body/Sector_EyeOfTheUniverse/Sector_Campfire"); // gets the quantum planet with nh
        
        _activateObjects[0] = fire.transform.Find("Volumes_Campfire/EndlessCylinder_Forest").gameObject; // gets the endless eye volume
        _activateObjects[1] = fire.transform.Find("Campsite/Prefab_IP_GhostBird_Scientist_Eye").gameObject; // gets the quantum planet's first state
        _deactivateObjects[0] = fire.transform.Find("InstrumentZones/ScientistSector").gameObject; // gets the scientist sector

        _activateObjects[1].SetActive(false);

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
        for (int i = 0; i < _activateObjects.Length; i++)
        {
            _activateObjects[i].SetActive(value: true);
        }
        for (int j = 0; j < _deactivateObjects.Length; j++)
        {
            _deactivateObjects[j].SetActive(value: false);
        }
        base.enabled = false;
    }
}