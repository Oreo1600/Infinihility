using UnityEngine;
using UnityEngine.AI;

public class buttonScript : MonoBehaviour
{
    public Rigidbody gooseBody;
    [SerializeField] bool gooseFreed = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gooseBody = GameObject.FindGameObjectWithTag("gooseButton").GetComponent<Rigidbody>();
        gooseFreed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if ((gooseFreed == false) && (Input.GetKeyDown(KeyCode.E))) 
        {
            Debug.Log("E");
            //gooseBody.useGravity = true;
            gooseFreed = true;
            gooseBody.GetComponent<Rigidbody>().useGravity = true;

            //yield return new WaitForSeconds(3);

            gooseBody.GetComponent<NavMeshAgent>().enabled = true;
            gooseBody.GetComponent<GooseMovementScript>().enabled = true;
            this.enabled = false;
        }

        //if (Input.GetMouseButtonDown(0))
        //{
        //    gooseBody.useGravity = true;
        //}
    }
}
