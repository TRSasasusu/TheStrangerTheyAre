using UnityEngine;

namespace TheStrangerTheyAre
{
    public class StrangerSystemAchievements : MonoBehaviour
    {
        [SerializeField]
        private SealSocket[] sockets;
        private bool isIlliterate;
        private bool hasFoundHome;

        void Start()
        {
            isIlliterate = PlayerData.GetPersistentCondition("LANGUAGE_LEARNED");
            hasFoundHome = Locator.GetShipLogManager().IsFactRevealed("HOME_REVEAL");
            // follow your dreams achievement
            if (hasFoundHome && !isIlliterate)
            {
                DialogueConditionManager.SharedInstance.SetConditionState("tsta_followyourdreams", true);
            }
        }
        
        void Update()
        {
            // all or nothing achievement
            int temp = 0;
            foreach (var socket in sockets)
            {
                if (socket.itemPlaced)
                {
                    temp++;
                }
            }
            if (temp >= sockets.Length)
            {
                DialogueConditionManager.SharedInstance.SetConditionState("tsta_allornothing", true);
            }
        }
    }
}
