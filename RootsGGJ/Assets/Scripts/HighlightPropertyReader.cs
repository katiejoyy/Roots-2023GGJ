using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HighlightPropertyReader : MonoBehaviour
{
    bool _hasTargetedStateProperty;
    List<Interactable> _interactables;
    List<Renderer> _highlightRenderers;

    void Start()
    {
        _hasTargetedStateProperty = false;
        _interactables = GetComponents<Interactable>().ToList();
        _highlightRenderers = new List<Renderer>();
        CacheRenderers();
    }

    void Update()
    {
        bool isTargeted = false;

        _interactables.ForEach((interactable) => {
            if(interactable.IsTargeted)
            {
                isTargeted = true;
            }
        });

        if (isTargeted != _hasTargetedStateProperty)
        {
            ChangeHighlight(isTargeted ? 1.0f : 0.0f);
        }

        _hasTargetedStateProperty = isTargeted;
    }

    private void CacheRenderers()
    {
        if (GetComponent<Renderer>() != null)
        {
            _highlightRenderers.Add(GetComponent<Renderer>());
        }
        _highlightRenderers.AddRange(GetComponentsInChildren<Renderer>());
    }

    private void ChangeHighlight(float highlightAmount)
    {
        foreach (Renderer highlightRenderer in _highlightRenderers)
        {
            foreach (Material material in highlightRenderer.materials)
            {
                if (material.HasProperty("_HighlightedEnabled"))
                {
                    material.SetFloat("_HighlightedEnabled", highlightAmount);
                }
            }
        }
    }
}