using UnityEngine;
using UnityEngine.AI;
using RPG.Core;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction
    {
        [SerializeField] Transform target;

        // Private fields
        NavMeshAgent navMeshAgent;
        Animator animator;
        Health health;

        // Start is called before the first frame update
        void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            health = GetComponent<Health>();
        }

        // Update is called once per frame
        void Update()
        {
            // Enable the navmesh if the player or enemy is not dead
            EnableNavMesh();

            // Update the movement animator
            UpdateAnimator();
        }

        /**
         * This method enable the navmesh if the player or enemy is not dead
         */
        private void EnableNavMesh()
        {
            navMeshAgent.enabled = !health.IsDead();
        }

        /**
         * This method changes the animator according the forward player speed
         */
        private void UpdateAnimator()
        {
            // Get Nav Mesh Agent velicity
            Vector3 velocity = navMeshAgent.velocity;

            // Get the player inverse velocity
            Vector3 localvelocity = transform.InverseTransformDirection(velocity);

            // Get the player speed
            float speed = localvelocity.z;

            // Update animator blending speed
            animator.SetFloat("ForwardSpeed", speed);
        }

        /**
         * This method start movement player action 
         * */
        public void StartMoveAction(Vector3 destination)
        {
            // Tells the action scheduler to make a movement
            GetComponent<ActionScheduler>().StartAction(this);
            // Move to new destination
            MoveTo(destination);
        }

        /**
         * This method moves the player into target destination
         * */
        public void MoveTo(Vector3 destination)
        {
            navMeshAgent.SetDestination(destination);
            navMeshAgent.isStopped = false;
        }

        /**
         * This method cancel NavMesh Agent movement
         * */
        public void Cancel()
        {
            navMeshAgent.isStopped = true;
        }
    }
}