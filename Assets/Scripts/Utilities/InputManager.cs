using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {
    
    public static InputManager Instance { get; protected set; }

	void OnEnable() {
        if(Instance != null) {
            Debug.LogError("There should only be one instance of Input Manager.");
        }

        Instance = this;
    }


}
