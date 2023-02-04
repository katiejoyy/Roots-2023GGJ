using UnityEngine;

public class PlayerInteract : Actor
{
    public Camera sceneCamera;
    private Vector3? targetPosition = null;
    private Vector3 startPosition;
    public float speed = 0.1f;
    public float progress = 0;

    public void Update() {
        if(targetPosition.HasValue){
            progress += (speed * Time.deltaTime);
            float time = progress / Vector3.Distance(startPosition, targetPosition.Value);
            transform.position = Vector3.Lerp(startPosition,targetPosition.Value,time);
            
            if (time >= 1){
                targetPosition = null;
                progress = 0;
                return;
            }

            RaycastHit hit;
            Vector3 direction = targetPosition.Value - transform.position;
            direction.Normalize();
            if (Physics.Raycast(transform.position,direction,out hit, 1.0f)){
                GameObject hitObject = hit.collider.gameObject;
                if (hitObject.gameObject.tag != "Ground")
                {
                    targetPosition = null;
                }
            }
            
        }
        
    }

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
            if (hitObject.tag == "Ground"){
                startPosition = transform.position;
                progress = 0;
                targetPosition = new Vector3(hit.point.x,transform.position.y,hit.point.z);
            }
        }
    }
    private void OnCollisionEnter(Collision other) 
    {
        targetPosition = null;
    }
}
