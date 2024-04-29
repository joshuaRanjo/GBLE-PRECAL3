using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class Clickhandler : MonoBehaviour
{   
    #region Variables

    private Camera _mainCamera;

    #endregion

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if (!context.started) return;

        if (EventSystem.current.IsPointerOverGameObject())
        {
            
            return; // Exit early if the click originated from a UI element
        }

        // Check if hit interference layer
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, LayerMask.GetMask("InterferenceLayer"));
        if (hit.collider != null) return; // exit if interferencelayer was clicked

        var rayHit = Physics2D.GetRayIntersection(_mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue()));
        if (!rayHit.collider) return;
        

        ClickableObject component = rayHit.collider.gameObject.GetComponent<ClickableObject>();
        if(component != null)
        {
            //Debug.Log("Clicked clickable object ");
            component.DoAction();
        }
        
    }
}