using UnityEngine;

public class MonsterAnimation : MonoBehaviour
{
    private Animator animator;

    public void PlayWinAnimation()
    {
        animator.SetBool("IsWin", true);
    }
}
