using NewHorizons.Utility;
using UnityEngine;

namespace TheStrangerTheyAre
{
    public class DevCodeTotem : AbstractGhostDoorInterface
    {
        [SerializeField]
        private InteractReceiver _interactReceiver;

        [SerializeField]
        private GearInterfaceEffects _gearInterfaceHorizontal;

        [SerializeField]
        private GearInterfaceEffects _gearInterfaceVertical;

        [SerializeField]
        private RotaryDial[] _dials;

        [SerializeField]
        private Transform _lockOnTransform;

        [SerializeField]
        private Transform[] _selectors;

        [Space]
        [SerializeField]
        private int[] _secretCode1;

        [SerializeField]
        private int[] _secretCode2;

        [SerializeField]
        private int[] _secretCode3;


        [SerializeField]
        private string[] _factIDs = new string[0];

        [Space]
        [SerializeField]
        private OWAudioSource _oneShotAudio;

        private ScreenPrompt _leftRightPrompt;

        private ScreenPrompt _upDownPrompt;

        private ScreenPrompt _leavePrompt;

        private int _selectedDial;

        private bool _codeCheckDirty;

        private bool _playerInteracting;

        private bool _movingSelector;

        private float _currentSelectorPosY;

        private float _targetSelectorPosY;

        private bool isInSimMode = false;

        private DreamCampfire[] dreamFires = new DreamCampfire[3];
        private DreamCampfire[] dreamFiresSpecial = new DreamCampfire[2];

        private GameObject prisFlame;
        private void Awake()
        {
            if (_interactReceiver != null)
            {
                _interactReceiver.OnPressInteract += OnPressInteract;
            }
            _selectedDial = 0;
        }

        private void Start()
        {
            prisFlame = SearchUtilities.Find("RingWorld_Body/Sector_RingInterior/Sector_Zone4/Sector_PrisonDocks/Sector_PrisonInterior/Interactibles_PrisonInterior/Prefab_IP_Sarcophagus/Prefab_IP_SleepingMummy_v2 (PRISONER)/Mummy_IP_ArtifactAnim/ArtifactPivot/Flame");
            if (_interactReceiver != null)
            {
                _interactReceiver.SetPromptText(UITextType.UnknownInterfacePrompt);
            }
            _currentSelectorPosY = (_targetSelectorPosY = _dials[_selectedDial].transform.localPosition.y);
            for (int i = 0; i < _selectors.Length; i++)
            {
                _selectors[i].SetLocalPositionY(_currentSelectorPosY);
            }
            _leftRightPrompt = new ScreenPrompt(InputLibrary.left, InputLibrary.right, UITextLibrary.GetString(UITextType.RotateGearLeftRightPrompt) + "   <CMD>", ScreenPrompt.MultiCommandType.POS_NEG);
            _upDownPrompt = new ScreenPrompt(InputLibrary.up, InputLibrary.down, UITextLibrary.GetString(UITextType.RotateGearUpDownPrompt) + "   <CMD>", ScreenPrompt.MultiCommandType.POS_NEG);
            _leavePrompt = new ScreenPrompt(InputLibrary.cancel, UITextLibrary.GetString(UITextType.LeavePrompt) + "   <CMD>");
            base.enabled = false;

            // setup dream campfires
            dreamFires[0] = SearchUtilities.Find("RingWorld_Body/Sector_RingInterior/Sector_Zone1/Sector_DreamFireHouse_Zone1/Interactables_DreamFireHouse_Zone1/DreamFireChamber/Prefab_IP_DreamCampfire/Controller_Campfire").GetComponent<DreamCampfire>();
            dreamFires[1] = SearchUtilities.Find("RingWorld_Body/Sector_RingInterior/Sector_Zone2/Sector_DreamFireLighthouse_Zone2_AnimRoot/Interactibles_DreamFireLighthouse_Zone2/DreamFireChamber/Prefab_IP_DreamCampfire/Controller_Campfire").GetComponent<DreamCampfire>();
            dreamFires[2] = SearchUtilities.Find("RingWorld_Body/Sector_RingInterior/Sector_Zone3/Sector_HiddenGorge/Sector_DreamFireHouse_Zone3/Interactables_DreamFireHouse_Zone3/DreamFireChamber_DFH_Zone3/Prefab_IP_DreamCampfire/Controller_Campfire").GetComponent<DreamCampfire>();
            
            dreamFiresSpecial[0] = SearchUtilities.Find("RingWorld_Body/Sector_RingInterior/Sector_Zone4/Sector_PrisonDocks/Sector_PrisonInterior/Interactibles_PrisonInterior/Prefab_IP_DreamCampfire/Controller_Campfire").GetComponent<DreamCampfire>();
            dreamFiresSpecial[1] = SearchUtilities.Find("AnglersEye_Body/Sector/BrambleMuseum/Interactables/FIRE/PREBRAMBLE_ENTRY/Controller_Campfire").GetComponent<DreamCampfire>();
        }

        private void OnDestroy()
        {
            if (_interactReceiver != null)
            {
                _interactReceiver.OnPressInteract -= OnPressInteract;
            }
        }

        private bool MoveSelectorToLocalPositionY(float yPos)
        {
            if (OWMath.ApproxEquals(yPos, _targetSelectorPosY, 0.01f))
            {
                return false;
            }
            _oneShotAudio.PlayOneShot(AudioType.CodeTotem_Vertical);
            _targetSelectorPosY = yPos;
            _movingSelector = true;
            return true;
        }

        public void FireExtinguishSpecial(DreamCampfire fire)
        {
            fire.SetState(Campfire.State.UNLIT);
            fire.StopSleeping(sudden: true);
            fire.SetInteractionEnabled(enabled: false);
            if (fire._mummyCircleFlameController != null)
            {
                fire._mummyCircleFlameController.FadeTo(0f, 0.2f);
            }
            if (fire._houseLightController != null)
            {
                fire._houseLightController.FadeTo(0f, 0.2f);
            }
            fire.OnDreamCampfireExtinguished.Invoke();
        }

        private void Update()
        {
            if (_movingSelector)
            {
                _currentSelectorPosY = Mathf.MoveTowards(_currentSelectorPosY, _targetSelectorPosY, Time.deltaTime * 1.5f);
                if (OWMath.ApproxEquals(_currentSelectorPosY, _targetSelectorPosY))
                {
                    _currentSelectorPosY = _targetSelectorPosY;
                    _movingSelector = false;
                }
                for (int i = 0; i < _selectors.Length; i++)
                {
                    _selectors[i].SetLocalPositionY(_currentSelectorPosY);
                }
            }
            if (!_playerInteracting && !_movingSelector)
            {
                base.enabled = false;
            }
            bool flag = OWInput.IsInputMode(InputMode.SatelliteCam);
            _leftRightPrompt.SetVisibility(flag);
            _upDownPrompt.SetVisibility(flag);
            _leavePrompt.SetVisibility(flag);
            if (!flag)
            {
                return;
            }
            if ((OWInput.IsNewlyPressed(InputLibrary.right) || OWInput.IsNewlyPressed(InputLibrary.right2) || OWInput.IsNewlyPressed(InputLibrary.toolActionPrimary)) && !_gearInterfaceVertical.IsRotating())
            {
                _dials[_selectedDial].Rotate(positive: true);
                _gearInterfaceHorizontal.AddRotation(-45f, 0f);
                _oneShotAudio.PlayOneShot(AudioType.CodeTotem_Horizontal);
                _codeCheckDirty = true;
            }
            else if ((OWInput.IsNewlyPressed(InputLibrary.left) || OWInput.IsNewlyPressed(InputLibrary.left2) || OWInput.IsNewlyPressed(InputLibrary.toolActionSecondary)) && !_gearInterfaceVertical.IsRotating())
            {
                _dials[_selectedDial].Rotate(positive: false);
                _gearInterfaceHorizontal.AddRotation(45f, 0f);
                _oneShotAudio.PlayOneShot(AudioType.CodeTotem_Horizontal);
                _codeCheckDirty = true;
            }
            else if (OWInput.IsNewlyPressed(InputLibrary.up) || OWInput.IsNewlyPressed(InputLibrary.up2))
            {
                _selectedDial = Mathf.Max(_selectedDial - 1, 0);
                if (MoveSelectorToLocalPositionY(_dials[_selectedDial].transform.localPosition.y))
                {
                    _gearInterfaceVertical.AddRotation(45f, 0f);
                }
                else
                {
                    _gearInterfaceVertical.PlayFailure(forward: true, 0.5f);
                }
            }
            else if (OWInput.IsNewlyPressed(InputLibrary.down) || OWInput.IsNewlyPressed(InputLibrary.down2))
            {
                _selectedDial = Mathf.Min(_selectedDial + 1, _dials.Length - 1);
                if (MoveSelectorToLocalPositionY(_dials[_selectedDial].transform.localPosition.y))
                {
                    _gearInterfaceVertical.AddRotation(-45f, 0f);
                }
                else
                {
                    _gearInterfaceVertical.PlayFailure(forward: false);
                }
            }
            else if (OWInput.IsNewlyPressed(InputLibrary.cancel))
            {
                CancelInteraction();
            }
            for (int j = 0; j < _dials.Length; j++)
            {
                if (_dials[j].IsRotating())
                {
                    return;
                }
            }
            if (_codeCheckDirty)
            {
                CheckForCode();
                _codeCheckDirty = false;
            }
        }

        private void CheckForCode()
        {
            bool flag1 = true;
            bool flag2 = true;
            bool flag3 = true;
            for (int i = 0; i < _dials.Length; i++)
            {
                flag1 = flag1 && _dials[i].GetSymbolSelected() == _secretCode1[i];
                flag2 = flag2 && _dials[i].GetSymbolSelected() == _secretCode2[i];
                flag3 = flag3 && _dials[i].GetSymbolSelected() == _secretCode3[i];
            }

            if (flag1)
            {
                DialogueConditionManager.SharedInstance.SetConditionState("tsta_redpilled", true);
                Locator.GetDreamWorldController().ExitLanternBounds();
                Locator.GetDreamWorldController().UpdateSimulationSphereRadius(-20);
                isInSimMode = true;
            } else {
                // disable sim mode if code not set
                if (isInSimMode)
                {
                    Locator.GetDreamWorldController().EnterLanternBounds();
                    Locator.GetDreamWorldController().UpdateSimulationSphereRadius(0);
                    isInSimMode = false;
                }

                if (flag2)
                {
                    DialogueConditionManager.SharedInstance.SetConditionState("tsta_ghostbusters", true);
                    GhostBrain[] ghostBrains = FindObjectsOfType<GhostBrain>();
                    GameObject tempObj;

                    foreach (GhostBrain ghost in ghostBrains)
                    {
                        ghost.Die();
                    }
                }
                else if (flag3)
                {
                    DialogueConditionManager.SharedInstance.SetConditionState("tsta_sudormrf", true);

                    CancelInteraction();
                    foreach (DreamCampfire fire in dreamFires)
                    {
                        fire.OnEnterCustomCollider();
                    }
                    prisFlame.SetActive(false);
                    foreach (DreamCampfire fireSpecial in dreamFiresSpecial)
                    {
                        FireExtinguishSpecial(fireSpecial);
                    }
                }
            }
        }

        private void OnPressInteract()
        {
            Locator.GetToolModeSwapper().UnequipTool();
            Locator.GetPlayerTransform().GetComponent<PlayerLockOnTargeting>().LockOn(_lockOnTransform, Vector3.zero);
            GlobalMessenger.FireEvent("EnterSatelliteCameraMode");
            Locator.GetPromptManager().AddScreenPrompt(_upDownPrompt, PromptPosition.UpperRight);
            Locator.GetPromptManager().AddScreenPrompt(_leftRightPrompt, PromptPosition.UpperRight);
            Locator.GetPromptManager().AddScreenPrompt(_leavePrompt, PromptPosition.UpperRight);
            for (int i = 0; i < _factIDs.Length; i++)
            {
                Locator.GetShipLogManager().RevealFact(_factIDs[i]);
            }
            base.enabled = true;
            _playerInteracting = true;
        }

        private void CancelInteraction()
        {
            Locator.GetPromptManager().RemoveScreenPrompt(_leftRightPrompt);
            Locator.GetPromptManager().RemoveScreenPrompt(_upDownPrompt);
            Locator.GetPromptManager().RemoveScreenPrompt(_leavePrompt);
            Locator.GetPlayerTransform().GetComponent<PlayerLockOnTargeting>().BreakLock();
            _interactReceiver.ResetInteraction();
            GlobalMessenger.FireEvent("ExitSatelliteCameraMode");
            _playerInteracting = false;
        }

        public override void SetStartingPosition(bool IsActivated)
        {
        }
    }

}
