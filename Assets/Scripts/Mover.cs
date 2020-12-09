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
        if (Input.GetMouseButtonDown(0))
        {
            MoveToCursor();
        }
        
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

    /**
     * Move player into target destination
     * */
    private void SetTargetDestination(Vector3 destination)
    {
        navMeshAgent.SetDestination(destination);
    }
}
