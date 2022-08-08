using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is used to not destroy the background music when switching scenes
/// </summary>

public class DoNotDestroyMusic : MonoBehaviour
{
    private static DoNotDestroyMusic instance = null;
    
    public static DoNotDestroyMusic Instance {
        get { return instance; }
    }

    void Awake()
    {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
            return;
        } 
        else {
            instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }

}
