using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Cursor")]
    public Camera playerCam;
    public Texture2D idleCursor;
    public Texture2D interactionCursor;
    RaycastHit mouseHit;

    [Header("Interaction")]
    public LayerMask interactionMask;
    bool detectInteraction = false;
    Interactable interactableComponent;
    InteractableGroup interactableGroupComponent;
    string componentName = string.Empty;
    string lastComponentName = string.Empty;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.isInInteractionMenu || GameManager.isInIsoMode || GameManager.isInMenu || CharacterMove.isMoving || GameManager.isInTutorial)
        {
            Cursor.SetCursor(idleCursor, Vector2.zero, CursorMode.Auto);
            return;
        }

        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject(Input.touches[0].fingerId))
            {
                Ray touchRay = playerCam.ScreenPointToRay(Input.touches[0].position);
                if (Physics.Raycast(touchRay, out mouseHit, 20.0f, interactionMask))
                {
                    componentName = mouseHit.transform.name;
                    if (componentName != lastComponentName)
                    {
                        ResetElements();
                        interactableComponent = mouseHit.transform.GetComponent<Interactable>();
                        interactableGroupComponent = mouseHit.transform.GetComponent<InteractableGroup>();
                        lastComponentName = componentName;
                    }
                }
                //
                if (interactableComponent)
                {
                    interactableComponent.Interact();
                    interactableComponent.outlineEffect.enabled = true;
                }
                else if (interactableGroupComponent)
                {
                    interactableGroupComponent.Interact();
                    interactableGroupComponent.outlineEffect.enabled = true;
                    interactableGroupComponent.ToggleOthersOutline(true);
                }
            }
        }
        else
        {
            Ray mouseRay;
            mouseRay = playerCam.ScreenPointToRay(Input.mousePosition);
            //
            if (Physics.Raycast(mouseRay, out mouseHit, 20.0f, interactionMask) && !EventSystem.current.IsPointerOverGameObject())
            {
                componentName = mouseHit.transform.name;

                if (componentName != lastComponentName)
                {
                    ResetElements();
                    interactableComponent = mouseHit.transform.GetComponent<Interactable>();
                    interactableGroupComponent = mouseHit.transform.GetComponent<InteractableGroup>();

                    if (interactableComponent || interactableGroupComponent)
                    {
                        if(interactableComponent)
                            interactableComponent.outlineEffect.enabled = true;
                        if (interactableGroupComponent)
                        {
                            interactableGroupComponent.outlineEffect.enabled = true;
                            interactableGroupComponent.ToggleOthersOutline(true);
                        }
                        //
                        detectInteraction = true;
                    }
                    lastComponentName = componentName;
                }
            }
            else
            {
                ResetDetection();
            }
            //
            if (detectInteraction)
            {
                Cursor.SetCursor(interactionCursor, Vector2.zero, CursorMode.Auto);
                if (Input.GetMouseButtonDown(0))
                {
                    if (interactableComponent)
                    {
                        interactableComponent.Interact();
                    }
                    else if (interactableGroupComponent)
                    {
                        interactableGroupComponent.Interact();
                    }
                }
            }
            else
            {
                Cursor.SetCursor(idleCursor, Vector2.zero, CursorMode.Auto);
            }
        }
    }

    public void ResetDetection()
    {
        mouseHit = new RaycastHit();
        detectInteraction = false;
        //
        ResetElements();
        componentName = string.Empty;
    }

    void ResetElements()
    {
        if (interactableComponent)
        {
            interactableComponent.outlineEffect.enabled = false;
            interactableComponent = null;
        }
        if (interactableGroupComponent)
        {
            interactableGroupComponent.outlineEffect.enabled = false;
            interactableGroupComponent.ToggleOthersOutline(false);
            interactableGroupComponent = null;
        }
    }
}
