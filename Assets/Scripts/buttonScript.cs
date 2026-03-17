using UnityEngine;

public class buttonScript : MonoBehaviour
{
    public Rigidbody gooseBody;
    bool gooseFreed = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gooseBody = GameObject.FindGameObjectWithTag("gooseButton").GetComponent<Rigidbody>();
        gooseFreed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if ((gooseFreed == false) && (Input.GetKey(KeyCode.E)))
        {
            gooseBody.useGravity = true;
            gooseFreed = true;
        }

        //if (Input.GetMouseButtonDown(0))
        //{
        //    gooseBody.useGravity = true;
        //}
    }
}
