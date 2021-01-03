using System;
using UnityEngine;

namespace RPG.Control
{
    public class PatrolPath : MonoBehaviour
    {
        const float waypointGizmosRadius = 0.3f;

        // Draw Gizmos
        private void OnDrawGizmos()
        {
            DrawPatrolPathLines();
        }

        /**
         * This methods draw lines between patrol paths in Unity Editor window
         */ 
        private void DrawPatrolPathLines()
        {
            // Iterates the child objects
            for (int i = 0; i < transform.childCount; i++)
            {
                // Get the next waipoint index
                int j = GetNextIndex(i);

                // Draw a sphere in current waypoint
                Gizmos.DrawSphere(GetWaypoint(i), waypointGizmosRadius);
                // Draw a line between current and previous waypoints
                Gizmos.DrawLine(GetWaypoint(i), GetWaypoint(j)); ;
            }
        }

        /**
         * Get the next index for child objects
         */
        public int GetNextIndex(int index)
        {
            // Return zero if it is the last waypoint
            if (index + 1 == transform.childCount) { return 0; }
            // Return the next waipoint index
            return index + 1;
        }

        /**
         * Get the waipoint passed in the index
         */ 
        public Vector3 GetWaypoint(int index)
        {
            return transform.GetChild(index).position;
        }
    }
}