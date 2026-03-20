using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class Tools : MonoBehaviour
{
    [SerializeField] InputActionReference tool1Key;
    [SerializeField] InputActionReference tool2Key;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tool1Key.action.performed += Tool1;
        tool2Key.action.performed += Tool2;
    }

    // Update is called once per frame
    void Update(){
        
    }
    void Tool1(InputAction.CallbackContext callbackContext)
    {
        Debug.Log("paintbrush 👨‍🎨");
    }

    void Tool2(InputAction.CallbackContext callbackContext)
    {
        Debug.Log("hammer ☭");
    }
}
