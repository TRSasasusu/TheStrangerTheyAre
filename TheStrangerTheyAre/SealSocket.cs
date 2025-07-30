using System.Collections;
using UnityEngine;

namespace TheStrangerTheyAre;
public class SealSocket : OWItemSocket
{
    [SerializeField]
    GameObject activeObjects;
    [SerializeField]
    public int sealSocketID;
    [SerializeField]
    GameObject inactiveObjects;

    public bool itemPlaced;

    public override void Awake()
    {
        base.Awake();
        _acceptableType = ItemType.SharedStone;
        activeObjects.SetActive(false);
        inactiveObjects.SetActive(true);
    }

    public override bool PlaceIntoSocket(OWItem item)
    {
        if (base.PlaceIntoSocket(item))
        {
            item.transform.localScale = item.transform.localScale*2.5f;
            if (item.GetComponent<Seals>().sealID == sealSocketID)
            {
                activeObjects.SetActive(true);
                inactiveObjects.SetActive(false);
                itemPlaced = true;
                StartCoroutine(PlayAnimationAndProceed("ProjectionStart", false));
                return true;
            }
        }
        itemPlaced = false;
        return false;
    }

    private IEnumerator PlayAnimationAndProceed(string animationName, bool isEndAnim)
    {
        // Get the animator component and play the animation
        Animator animator = activeObjects.GetComponent<Animator>();
        animator.Play(animationName, 0);

        // Wait until the animation is finished
        yield return new WaitUntil(() =>
            animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 &&
            !animator.IsInTransition(0)
        );

        // Code to execute after the animation finishes
        OnAnimationComplete(isEndAnim);
    }

    private void OnAnimationComplete(bool isEndAnim)
    {
        if (isEndAnim) {
            // should disable when it ends
        }
        else {
            activeObjects.GetComponent<Animator>().Play("ProjectionLoop", 0);
        }
    }

    public override OWItem RemoveFromSocket()
    {
        OWItem oWItem = base.RemoveFromSocket();
        
        if (activeObjects.activeSelf)
        {
            StartCoroutine(PlayAnimationAndProceed("ProjectionEnd", true));
            activeObjects.SetActive(false);
            inactiveObjects.SetActive(true);
        }
        return oWItem;
    }
}
