using UnityEngine;

public class AnimationManager : MonoBehaviour, IAnimationManager
{
    public static IAnimationManager Instance { get; private set; }

    private Animator animator;

    public void Awake()
    {

        if (Instance != null )
        {
            Destroy(gameObject);
        }
        else
        {

            Instance = this;
        }
        animator = transform.GetComponent<Animator>();
    }


    public void SetAnimator(Animator animator_)
    {
        animator = animator_;
    }

    public void SetReloadTrigger()
    {
        animator.SetInteger("Reload", 1);
    }

    public void ResetReloadTrigger()
    {
        animator.SetInteger("Reload", 0);
    }

    public void SetAimTrigger()
    {
        animator.SetBool("Aiming", true);
    }

    public void ResetAimTrigger()
    {
        animator.SetBool("Aiming", false);
    }

    public void SetMeleeAttackTrigger()
    {
        animator.SetTrigger("MeleeAttack");
    }
}