using UnityEngine;
using GhostEnums;
using NewHorizons.Utility;

namespace TheStrangerTheyAre
{
    public class DataCloneTrigger : MonoBehaviour
    {
        // variables
        private GameObject scientist1; // creates variable to store the walking around scientist
        private GameObject scientist2; // creates variable to store the pre-vision scientist
        private GameObject visionTorchCone; // creates variable to store the vision torch cone
        private Vector3 scale = new Vector3(0.9845f, 1.2517f, 6.7005f); // set scale amount in vector3
        
        void Start()
        {
            // get all gameobjects as needed
            scientist1 = SearchUtilities.Find("DreamWorld_Body/Sector_DreamWorld/Sector_DreamZone_4/Interactibles_DreamZone_4_Upper/Sector_NewSim/New_SimSector/FourthArchive/GhostBirds/CUTSCENE_GHOSTBIRDS/NODE_SCIENTIST/Prefab_IP_GhostBird_SCIENTIST"); // gets the ghostbird ai scientist
            scientist2 = SearchUtilities.Find("DreamWorld_Body/Sector_DreamWorld/Sector_DreamZone_4/Interactibles_DreamZone_4_Upper/Sector_NewSim/New_SimSector/FourthArchive/GhostBirds/CUTSCENE_GHOSTBIRDS/NODE_SCIENTIST/Prefab_IP_GhostBird_Scientist2"); // gets the pre-vision scientist
            visionTorchCone = scientist2.transform.Find("Ghostbird_IP_ANIM/Ghostbird_Skin_01:Ghostbird_Rig_V01:Base/Ghostbird_Skin_01:Ghostbird_Rig_V01:Root/Ghostbird_Skin_01:Ghostbird_Rig_V01:Spine01/Ghostbird_Skin_01:Ghostbird_Rig_V01:Spine02/Ghostbird_Skin_01:Ghostbird_Rig_V01:Spine03/Ghostbird_Skin_01:Ghostbird_Rig_V01:Spine04/Ghostbird_Skin_01:Ghostbird_Rig_V01:ClavicleL/Ghostbird_Skin_01:Ghostbird_Rig_V01:ShoulderL/Ghostbird_Skin_01:Ghostbird_Rig_V01:ElbowL/Ghostbird_Skin_01:Ghostbird_Rig_V01:WristL/Ghostbird_Skin_01:Ghostbird_Rig_V01:HandAttachL/SCIENTIST_VISIONTORCH/VisionBeam/TriggerVolume_MindProjector").gameObject;
        }

        public virtual void OnTriggerEnter(Collider hitCollider)
        {
            //checks if player collides with the trigger volume
            if (hitCollider.CompareTag("PlayerDetector") && enabled)
            {
                scientist1.GetComponent<GhostController>().FacePlayer(TurnSpeed.FASTEST); // faces scientist to player
                scientist2.SetActive(true); // enables pre-vision scientist when player interacts with trigger
                scientist2.transform.position = scientist1.transform.position; // sets the position of pre-vision scientist equal to ghostbird ai
                scientist2.transform.rotation = scientist1.transform.rotation; // sets the rotation of pre-vision scientist equal to ghostbird ai
                visionTorchCone.GetComponent<ConeShape>().enabled = true; // enables vision torch cone so it can work!!!!
                visionTorchCone.transform.localScale = scale; // scales the coneshape so it can be accessible even from the stairs
            }
        }
    }
}