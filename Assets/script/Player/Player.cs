using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(MoveController))]
public class Player : MonoBehaviour
{
    [System.Serializable]
    public class MouseInput
    {
        public Vector2 Damping;
        public Vector2 Sensitivity;
        public bool LockMouse;
    }

    [SerializeField] float runSpeed;
    [SerializeField] float walkSpeed;
    [SerializeField] float sprintSpeed;
    [SerializeField] MouseInput MouseControl;


    public PlayerAim playerAim;
    public PlayerStats playerStats;

    private MoveController m_MoveController;
    public MoveController MoveController
    {
        get
        {
            if(m_MoveController == null)
            {
                m_MoveController = GetComponent<MoveController>();
            }
            return m_MoveController;
        }
    }
    private CrossHair m_CrossHair;
    private CrossHair CrossHair
    {
        get
        {
            if(m_CrossHair == null)
            {
                m_CrossHair = GetComponentInChildren<CrossHair>();
            }
            return m_CrossHair;
        }
    }
    InputController playerInput;
    Vector2 mouseInput;
    public Interactable focus;
    void Awake()
    {
        playerInput = GameManager.Instance.inputController;
        playerStats = GetComponent<PlayerStats>();
        GameManager.Instance.LocalPlayer = this;
        runSpeed = playerStats.speed.GetPlayerValue();
        if (MouseControl.LockMouse)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
    
    void Update()
    {
        Move();
        LookAround();
    }

    private void LookAround()
    {
        mouseInput.x = Mathf.Lerp(mouseInput.x, playerInput.MouseInput.x, 1f / MouseControl.Damping.x);
        mouseInput.y = Mathf.Lerp(mouseInput.y, playerInput.MouseInput.y, 1f / MouseControl.Damping.y);
        transform.Rotate(Vector3.up * mouseInput.x * MouseControl.Sensitivity.x);

        CrossHair.LookHeight(mouseInput.y * MouseControl.Sensitivity.y);
        playerAim.SetRotation(mouseInput.y * MouseControl.Sensitivity.y);
    }

    private void Move()
    {
        runSpeed = playerStats.speed.GetPlayerValue();

        float moveSpeed = runSpeed;

        if (playerInput.IsWalking)
        {
            moveSpeed = walkSpeed;
        }

        if (playerInput.IsSprinting)
        {
            moveSpeed = sprintSpeed;
        }

        Vector2 direction = new Vector2(playerInput.Vertical*moveSpeed, playerInput.Horizontal*moveSpeed);
        MoveController.Move(direction);
    }
}
