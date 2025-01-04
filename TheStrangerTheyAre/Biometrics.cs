using UnityEngine;
using OWML.Common;
using System.Collections;

namespace TheStrangerTheyAre
{
    public class Biometrics : MonoBehaviour
    {
        [SerializeField]
        GameObject scanner;
        [SerializeField]
        AudioSource scanAudio;
        [SerializeField]
        AudioSource stopAudio;
        [SerializeField]
        GameObject pass;
        [SerializeField]
        AudioSource passAudio;
        [SerializeField]
        GameObject fail;
        [SerializeField]
        AudioSource failAudio;
        [SerializeField]
        RotatingDoor door;

        Animator scanAnim;
        bool isInTrigger;
        bool isScanning;

        public void Start()
        {
            scanAnim = scanner.GetComponent<Animator>();
            scanner.SetActive(false);
            pass.SetActive(false);
            fail.SetActive(true);
        }

        public IEnumerator DelayedCheck()
        {
            isScanning = true;
            yield return new WaitForSeconds(5); // wait until scan is done

            if (isInTrigger)
            {
                if (Locator.GetToolModeSwapper().GetItemCarryTool().GetHeldItemType() == ItemType.ConversationStone &&
                    Locator.GetToolModeSwapper().GetItemCarryTool().GetHeldItem().GetComponent<SkullItem>().isSkull == true)
                {
                    scanner.SetActive(false);
                    pass.SetActive(true);
                    fail.SetActive(false);
                    passAudio.Play();
                    door.Open();
                    Locator.GetShipLogManager().RevealFact("CRIMSON_SPINLAB_BIO");
                    isScanning = false;
                }
                else
                {
                    scanner.SetActive(false);
                    pass.SetActive(false);
                    fail.SetActive(true);
                    failAudio.Play();
                    door.Close();
                    isScanning = false;
                }
            }
        }

        public virtual void OnTriggerEnter(Collider hitCollider)
        {
            //checks if player collides with the trigger volume
            if (hitCollider.CompareTag("PlayerDetector") && enabled)
            {
                isInTrigger = true;
                scanner.SetActive(true);
                scanAudio.Play();
                scanAnim.Play("scan", 0);
                StartCoroutine(DelayedCheck());
            }
        }

        public virtual void OnTriggerExit(Collider hitCollider)
        {
            //checks if player exits with the trigger volume
            if (hitCollider.CompareTag("PlayerDetector") && enabled)
            {
                isInTrigger = false;
                if (isScanning)
                {
                    StopCoroutine(DelayedCheck());
                    scanAnim.Play("scanstop", 0);
                    scanAudio.Stop();
                    stopAudio.Play();
                }
            }
        }
    }
}