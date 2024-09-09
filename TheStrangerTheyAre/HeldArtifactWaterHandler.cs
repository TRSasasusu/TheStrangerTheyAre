using UnityEngine;

namespace TheStrangerTheyAre
{
    public class HeldArtifactWaterHandler : MonoBehaviour
    {
        public void Awake()
        {
            GlobalMessenger<float>.AddListener("PlayerCameraEnterWater", OnCameraEnterWater);
        }

        public void Destroy()
        {
            GlobalMessenger<float>.RemoveListener("PlayerCameraEnterWater", OnCameraEnterWater);
        }

        private void OnCameraEnterWater(float _)
        {
            if (Locator.GetDreamWorldController() != null
                && Locator.GetDreamWorldController().IsInDream()
                && Locator.GetToolModeSwapper()?.GetItemCarryTool()?.GetHeldItem() is DreamLanternItem lantern
                && lantern.GetLanternController().IsLit())
            {
                Locator.GetDreamWorldController().ExitDreamWorld(DreamWakeType.LanternSubmerged); // extinguishes lantern when in other forms of water
            }
        }
    }
}
