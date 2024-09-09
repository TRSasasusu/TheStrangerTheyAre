using UnityEngine;

namespace TheStrangerTheyAre
{
    public class WaterHazardScaler : MonoBehaviour
    {
        void Awake()
        {
            GameObject.Find("VelvetVortex_Body/Sector/HazardVolume").transform.parent = GameObject.Find("VelvetVortex_Body/Sector/Water").transform; // gets the water scale        
        }
    }
}