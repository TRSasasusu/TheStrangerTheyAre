using UnityEngine;

namespace TheStrangerTheyAre
{
    public class LightPuzzle : MonoBehaviour
    {
        [SerializeField]
        private MeshRenderer lightbulbRenderer; // get lightbulb game object
        [SerializeField]
        private Light lightbulbLight; //get reference to lightbulb's light
        [SerializeField]
        private EclipseDoorController door; // get hidden door

        private readonly Color maxColor = new Color(1.489617f, 1.513387f, 1.228142f);
        private readonly Color minColor = new Color(0.9354541f, 0.9530618f, 0.7417698f);
        private Material lightbulbMat; //get reference to lightbulb, then check its Meshrenderer, then get the material (not sharedMaterial) in the appropriate slot)
        
        float openValue; //calculate distance to open state, it'll need to be between 0 (farthest) and 1 (closest).
        private bool isLit;
        
        void Start()
        {
            DoLights();
        }

        void Update()
        {
            for (int i = 0; i < door._lightSensors.Length; i++)
            {
                if (door._lightSensors[i].IsIlluminated())
                {
                    isLit = true;
                    break;
                } else
                {
                    isLit = false;
                }
            }

            if (isLit)
            {
                DoLights();
            }
        }

        void DoLights()
        {
            Material[] mats = lightbulbRenderer.materials;
            Material lightbulbMat = mats[1];

            float angle = door._rotatingElements[1].localRotation.eulerAngles.z;
            if (angle > 180) angle = 180 - (angle - 180);
            openValue = Mathf.InverseLerp(0, 180, angle);

            Color currentColor = Color.Lerp(minColor, maxColor, openValue);
            lightbulbMat.SetColor("_EmissionColor", currentColor);
            lightbulbLight.intensity = openValue;
            lightbulbRenderer.materials = mats;
        }
    }
}
