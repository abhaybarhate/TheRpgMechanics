using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Core;
namespace RPG.Movement 
{
    public class Mover : MonoBehaviour, IAction
    {
        Ray lastRay;
        NavMeshAgent playerAgent;

        // Start is called before the first frame update
        void Start()
        {
            playerAgent = GetComponent<NavMeshAgent>();
        }

        // Update is called once per frame
        void Update()
        {

            UpdateAnimator();
        }
        
        public void startMoveAction(Vector3 destination)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            moveToHitPoint(destination);
        }

        public void moveToHitPoint(Vector3 destination)
        {
            playerAgent.SetDestination(destination);
            playerAgent.isStopped = false;
        }

        public void Cancel()
        {
            playerAgent.isStopped = true;
        }

        void UpdateAnimator()
        {
            Vector3 velocity = playerAgent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;
            GetComponent<Animator>().SetFloat("forwardSpeed", speed);

        }

    }

}

