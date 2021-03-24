using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator animator;

    private PlayerAim m_PlayerAim;
    private PlayerAim PlayerAim
    {
        get
        {
            if(m_PlayerAim == null)
            {
                m_PlayerAim = GameManager.Instance.LocalPlayer.playerAim;
            }
            return m_PlayerAim;
        }
    }
    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        animator.SetFloat("Vertical", GameManager.Instance.inputController.Vertical);
        animator.SetFloat("Horizontal", GameManager.Instance.inputController.Horizontal);

        animator.SetBool("IsWalking", GameManager.Instance.inputController.IsWalking);
        animator.SetBool("IsSprinting", GameManager.Instance.inputController.IsSprinting);

        animator.SetFloat("Aim Angle", PlayerAim.GetAngle());
    }
}
