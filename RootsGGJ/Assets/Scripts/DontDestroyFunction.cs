using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyFunction : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
