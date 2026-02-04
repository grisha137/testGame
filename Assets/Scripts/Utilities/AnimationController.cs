using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private void Awake()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

    public void Play(string stateName)
    {
        if (animator == null || string.IsNullOrEmpty(stateName))
        {
            return;
        }

        animator.Play(stateName);
    }

    public void SetBool(string parameter, bool value)
    {
        if (animator == null)
        {
            return;
        }

        animator.SetBool(parameter, value);
    }

    public void SetFloat(string parameter, float value)
    {
        if (animator == null)
        {
            return;
        }

        animator.SetFloat(parameter, value);
    }
}
