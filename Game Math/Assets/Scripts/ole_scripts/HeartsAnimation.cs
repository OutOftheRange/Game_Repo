using UnityEngine;

public class HeartsAnimation : MonoBehaviour
{
    public Animator animator;
    private static readonly int RunAnimation = Animator.StringToHash("run");

    public void StartAnimation()
    {
        animator.SetBool(RunAnimation, true);
    }

    public void StopAnimation()
    {
        animator.SetBool(RunAnimation, false);
    }
}