using UnityEngine;

namespace TheStrangerTheyAre
{
   public class EclipseInterface : AbstractGhostDoorInterface
    {
        [SerializeField]
        private SingleLightSensor[] _lightSensors;

        [SerializeField]
        private Transform[] _rotatingElements;

        [Space]
        [SerializeField]
        private float _rotationSpeed = 180f;

        [SerializeField]
        private float _startingRotation = 270f;

        [SerializeField]
        private float _angleAccuracy = 10f;

        [Space]
        [SerializeField]
        private bool _canRotateWhileOpen = true;

        [SerializeField]
        private float _timeToClosure = 0.3f;

        [Header("Audio")]
        [SerializeField]
        private OWAudioSource _rotationAudio;

        private float _timeSinceClosure;
        private GameObject qDoorClosed;
        private GameObject qDoorOpen;

        private void Awake()
        {
            if (_rotatingElements.Length < 1 || _lightSensors.Length < 1)
            {
                Debug.LogError("No Light sensor or rotating dial.");
            }
            for (int i = 0; i < _lightSensors.Length; i++)
            {
                _lightSensors[i].OnDetectLight += new OWEvent.OWCallback(OnDetectLight);
                _lightSensors[i].OnDetectDarkness += new OWEvent.OWCallback(OnDetectDarkness);
            }
        }

        private void Start()
        {
            base.enabled = false;
            qDoorClosed = GameObject.Find("StaticDoor_Closed");
            qDoorOpen = GameObject.Find("StaticDoor_Open");
            qDoorClosed.SetActive(true);
            qDoorOpen.SetActive(false);
        }

        private void OnDestroy()
        {
            for (int i = 0; i < _lightSensors.Length; i++)
            {
                _lightSensors[i].OnDetectLight -= new OWEvent.OWCallback(OnDetectLight);
                _lightSensors[i].OnDetectDarkness -= new OWEvent.OWCallback(OnDetectDarkness);
            }
        }

        public override void SetStartingPosition(bool IsActivated)
        {
            if (IsActivated)
            {
                for (int i = 0; i < _rotatingElements.Length; i++)
                {
                    float z = _rotatingElements[i].localRotation.eulerAngles.z;
                    _rotatingElements[i].Rotate(new Vector3(0f, 0f, 0f - z));
                }
            }
            else
            {
                for (int j = 0; j < _rotatingElements.Length; j++)
                {
                    float z2 = _rotatingElements[j].localRotation.eulerAngles.z;
                    _rotatingElements[j].Rotate(new Vector3(0f, 0f, _startingRotation - z2));
                }
            }
        }

        private void FixedUpdate()
        {
            _timeSinceClosure += Time.deltaTime;
            bool flag = _canRotateWhileOpen;
            if (flag)
            {
                for (int i = 0; i < _rotatingElements.Length; i++)
                {
                    _rotatingElements[i].Rotate(new Vector3(0f, 0f, _rotationSpeed * Time.deltaTime));
                }
            }
            if (_rotationAudio != null)
            {
                if (flag && (!_rotationAudio.isPlaying || _rotationAudio.IsFadingOut()))
                {
                    _rotationAudio.FadeIn(0.2f);
                }
                else if (!flag && _rotationAudio.isPlaying && !_rotationAudio.IsFadingOut())
                {
                    _rotationAudio.FadeOut(0.2f);
                }
            }
        }

        private void OnDetectLight()
        {
            base.enabled = true;
        }

        private void OnDetectDarkness()
        {
            float num = _rotatingElements[0].localRotation.eulerAngles.z % 360f;
            if (!_canRotateWhileOpen)
            {
                if (_timeSinceClosure < _timeToClosure)
                {
                    bool flag = true;
                    for (int i = 0; i < _lightSensors.Length; i++)
                    {
                        if (_lightSensors[i].IsIlluminated())
                        {
                            flag = false;
                        }
                    }
                    if (flag)
                    {
                        CallOpenEvent();
                    }
                }
            }
            else if (num < _angleAccuracy || 360f - num < _angleAccuracy)
            {
                CallOpenEvent();
            }
            base.enabled = false;
            if (_rotationAudio != null)
            {
                _rotationAudio.FadeOut(0.2f);
            }
        }

        private void OnDoorOpen()
        {
            qDoorClosed.SetActive(false);
            qDoorOpen.SetActive(true);
            for (int i = 0; i < _lightSensors.Length; i++)
            {
                _lightSensors[i].SetDetectorActive(active: false);
            }

        }

        private void OnDoorClose()
        {
            qDoorClosed.SetActive(true);
            qDoorOpen.SetActive(false);
            for (int i = 0; i < _lightSensors.Length; i++)
            {
                _lightSensors[i].SetDetectorActive(active: true);
            }
        }
    }

}