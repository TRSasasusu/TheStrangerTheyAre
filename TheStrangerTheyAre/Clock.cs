using UnityEngine;

namespace TheStrangerTheyAre;

public class Clock : MonoBehaviour
{
    [SerializeField]
    private Transform[] _rotatingElements;

    [SerializeField]
    private float _rotationSpeed = 0.13636f;

    [Space]
    [Header("Audio")]
    [SerializeField]
    private OWAudioSource _rotationAudio;

    private void FixedUpdate()
    {
        for (int i = 0; i < _rotatingElements.Length; i++)
        {
            _rotatingElements[i].Rotate(new Vector3(0f, 0f, _rotationSpeed * Time.deltaTime));
        }

        if (_rotationAudio != null)
        {
            if (!_rotationAudio.isPlaying || _rotationAudio.IsFadingOut())
            {
                _rotationAudio.FadeIn(0.2f);
            }
            else if (_rotationAudio.isPlaying && !_rotationAudio.IsFadingOut())
            {
                _rotationAudio.FadeOut(0.2f);
            }
        }
    }
}


