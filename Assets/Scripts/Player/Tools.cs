using Mono.Cecil.Cil;
using Unity.Mathematics;
using UnityEditor;
using UnityEditor.ProjectWindowCallback;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.ProBuilder.MeshOperations;

public class Tools : MonoBehaviour
{
    [SerializeField] InputActionReference tool1Key;
    [SerializeField] InputActionReference tool2Key;
    [SerializeField] Camera raycastOrigin;

    private GameObject currHover = null;
    private Outline currOutline = null;
    private int currentTool = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tool1Key.action.performed += Tool1;
        tool2Key.action.performed += Tool2;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTool == 1 || currentTool == 2)
        {
            RaycastHit result = NewRaycast(raycastOrigin.transform.position, raycastOrigin.transform.forward);
            if (result.transform.gameObject == null)
            {
                DestroyCurrOutline();
            }
            else if (result.transform.gameObject.tag == "Hoverable")
            {
                if (currHover == null || currHover != result.collider.gameObject)
                {
                    currHover = result.collider.gameObject;
                    DestroyCurrOutline();
                    currOutline = currHover.AddComponent<Outline>();
                    currOutline.OutlineColor = Color.black;
                    currOutline.OutlineWidth = 10f;
                    //it would be very cool and epic if we added a sound effect here at some point
                }
            }
            else
            {
                DestroyCurrOutline();
                currHover = null;
            }
        }
        else
        {
            DestroyCurrOutline();
        }
    }

    void DestroyCurrOutline()
    {
        if (currOutline != null)
            Object.Destroy(currOutline);
    }
    void SwitchTool(int toSwitch)
    {
        if (currentTool == toSwitch)
        {
            //unequip the currently held tool
            currentTool = 0;
        }
        else
        {
            //equip the new tool
            currentTool = toSwitch;
        }
    }

    RaycastHit NewRaycast(Vector3 start, Vector3 direction)
    {
        RaycastHit hit;
        if (Physics.Raycast(start, direction, out hit, 500f))
        {
            // Debug.Log(hit.collider); // for testing purposes hehehehaw
        }
        return hit;
    }
    void Tool1(InputAction.CallbackContext callbackContext)
    {
        SwitchTool(1);
    }

    void Tool2(InputAction.CallbackContext callbackContext)
    {
        SwitchTool(2);
    }
}
