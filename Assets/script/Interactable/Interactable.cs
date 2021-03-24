using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius;
    public Transform interactionTransform;

    bool isFocus = false;
    Transform playerTransform;

    bool hasInteracted = false;

    private void Start()
    {
        isFocus = false;
        hasInteracted = false;
        playerTransform = GameManager.Instance.LocalPlayer.transform;
    }
    void Update()
    {

        float distance = Vector3.Distance(playerTransform.position, interactionTransform.position);
        if (!hasInteracted && distance <= radius)
        {
            OnFocused();
        }
        else
        {
            OnDefocused();
        }
        if (GameManager.Instance.inputController.Interact&&isFocus == true)
        {
            if (!hasInteracted)
            {
                OnInteract();
            }
        }
    }

    public void OnFocused()
    {
        isFocus = true;
        print("Focus" + gameObject.name);
    }

    public void OnDefocused()
    {
        isFocus = false;
    }
    public virtual void OnInteract()
    {
        print("Interacted");
        hasInteracted = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }
}
