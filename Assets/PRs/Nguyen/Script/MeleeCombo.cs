using UnityEngine;

public class MeleeCombo : MonoBehaviour
{
    [Header("Animator Setup")]
    [SerializeField] private Animator animator;
    [SerializeField] private string attackTriggerName = "Attack";
    [SerializeField] private string attackCountParamName = "AttackCount";

    [Header("Combo Settings")]
    [SerializeField] private int maxComboSteps = 3;
    [SerializeField] private float comboResetTime = 1.2f;

    private int currentComboStep = 0;
    private float lastAttackTime = 0f;

    private void Update()
    {
        if (currentComboStep > 0 && Time.time - lastAttackTime > comboResetTime)
        {
            ResetCombo();
        }

        if (Input.GetMouseButtonDown(0))
        {
            PerformAttack();
        }
    }

    private void PerformAttack()
    {
        if (animator == null) return;

        animator.SetInteger(attackCountParamName, currentComboStep);
        animator.SetTrigger(attackTriggerName);

        lastAttackTime = Time.time;

        currentComboStep++;
        if (currentComboStep >= maxComboSteps)
        {
            currentComboStep = 0;
        }
    }

    public void ResetCombo()
    {
        currentComboStep = 0;
        if (animator != null)
        {
            animator.SetInteger(attackCountParamName, 0);
        }
    }
}