using UnityEngine;

public class PlayerInteract : Actor
{
    public Camera sceneCamera;

    public override void OnConfirm()
    {
        Ray ray = sceneCamera.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if(Physics.Raycast(ray, out hit)){
            GameObject hitObject = hit.collider.gameObject;
            Interactable interactable = hitObject.GetComponent<Interactable>();
            if(interactable != null) {
                interactable.Interact();
            }
        }
    }
}
