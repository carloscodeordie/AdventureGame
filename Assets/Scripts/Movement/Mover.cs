using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
{
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
            // Update the movement animator
            UpdateAnimator();
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
        public void MoveTo(Vector3 destination)
        {
            navMeshAgent.SetDestination(destination);
        }
    }
}