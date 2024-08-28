using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LoadSceneLogic : MonoBehaviour
{
    public InputActionReference actionReference;

    void Update()
    {

        if(actionReference.action.IsPressed())
        {
            SceneManager.LoadScene("Gameplay");
        }
    }
}
