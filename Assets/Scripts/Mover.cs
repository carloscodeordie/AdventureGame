using System;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    // Private fields
    NavMeshAgent navMeshAgent;
    
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessMouseInput();
    }

    /**
     * Process mouse input used to move the player in the screen
     * */
    private void ProcessMouseInput()
    {
        // Verify if the user click on mouse left button
        if (Input.GetMouseButton(0))
        {
            MoveToCursor();
        }
        // Update the movement animator
        UpdateAnimator();
    }

    private void MoveToCursor()
    {
        // Create ray to mouse position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        // Variable that contains the click position
        RaycastHit hit;

        // Triggers a ray in mouse position
        bool hasHit = Physics.Raycast(ray, out hit);

        // Validate if the ray hit some valid terrain
        if (hasHit)
        {
            // Move player to new destination
            SetTargetDestination(hit.point);
        }
    }

    // This method changes the animator according the forward player speed
    private void UpdateAnimator()
    {
        // Get Nav Mesh Agent velicity
        Vector3 velocity = GetComponent<NavMeshAgent>().velocity;

        // Get the player inverse velocity
        Vector3 localvelocity = transform.InverseTransformDirection(velocity);

        // Get the player speed
        float speed = localvelocity.z;

        // Update animator blending speed
        GetComponent<Animator>().SetFloat("ForwardSpeed", speed);
    }

    /**
     * Move player into target destination
     * */
    private void SetTargetDestination(Vector3 destination)
    {
        navMeshAgent.SetDestination(destination);
    }
}
