using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private void Update()
    {
        // Verify if the user click on mouse left button
        if (Input.GetMouseButton(0))
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
            GetComponent<Mover>().MoveTo(hit.point);
        }
    }
}
