using UnityEngine;

namespace TheStrangerTheyAre
{
    public class StrangerSystemAchievements : MonoBehaviour
    {
        [SerializeField]
        private SealSocket[] sockets;
        
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
