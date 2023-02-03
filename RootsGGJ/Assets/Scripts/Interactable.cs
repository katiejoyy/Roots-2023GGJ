using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public float radius = 3f;

    public bool HasInteracted {get; protected set; }
    public bool IsTargeted {get; private set;}
    private bool IsHovered {get; set;}

    void OnDrawGizmosSelected ()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    public abstract void Interact();

    void Start()
    {
        IsTargeted = false;
        IsHovered = false;
    }

    void Update()
    {
        if(IsTargeted != IsHovered)
        {
            IsTargeted = IsHovered;
            Debug.Log($"{name} has become " + (IsTargeted ? "targeted" : "untargeted"));
        }
    }

    void OnMouseOver()
    {
        IsHovered = true;
    }

    void OnMouseExit()
    {
        IsHovered = false;
    }
}
