using UnityEngine;

public class buttonScript : MonoBehaviour
{
    public Rigidbody gooseBody;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gooseBody = GameObject.FindGameObjectWithTag("gooseButton").GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            gooseBody.useGravity = true;
        } 

        //if (Input.GetMouseButtonDown(0))
        //{
        //    gooseBody.useGravity = true;
        //}
    }
}
