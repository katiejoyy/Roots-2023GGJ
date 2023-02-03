using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public float radius = 3f;

    bool hasInteracted = false;

    void OnDrawGizmosSelected ()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    public abstract void Interact();
}
