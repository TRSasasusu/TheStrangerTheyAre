using NewHorizons.Utility;
using UnityEngine;

namespace TheStrangerTheyAre
{
    public class TornadoFix : MonoBehaviour
    {
        private GameObject[] tornadoes = new GameObject[3]; // create new array of gameobjects to store all tornadoes
        private TornadoFluidVolume[] tornadoVolumes = new TornadoFluidVolume[3]; // create new array of all tornado fluid volumes

        private void Start()
        {
            for (int i = 0; i < tornadoes.Length; i++)
            {
                tornadoVolumes[i] = SearchUtilities.Find("CRIMSONTORNADO_" + (i + 1)).GetComponentInChildren<TornadoFluidVolume>();
                tornadoVolumes[i]._density = 1;
            }
        }
    }
}
