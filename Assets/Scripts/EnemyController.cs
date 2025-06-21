using UnityEngine;
using UnityEngine.AI;

public class EnemyRootMotionController : MonoBehaviour
{
    public NavMeshAgent agent;    
    public Animator animator;     
    public Transform target; 

    void Start()
    {
        //DISABLE AUTO MOVE 
        agent.updatePosition = false;
        agent.updateRotation = false;
    }

    void Update()
    {
        Debug.Log("Vertical: " + animator.GetFloat("Vertical"));
        Debug.Log("Has path: " + agent.hasPath + " | Velocity: " + agent.velocity);

        if (target != null)
            agent.SetDestination(target.position);

        Vector3 localVel = transform.InverseTransformDirection(agent.velocity);
        animator.SetFloat("Horizontal", localVel.x);
        animator.SetFloat("Vertical", localVel.z);

        // Face direction of movement
        Vector3 lookDir = agent.steeringTarget - transform.position;
        lookDir.y = 0f;
        if (lookDir != Vector3.zero)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookDir), Time.deltaTime * 5f);
    }

    void OnAnimatorMove()
    {
        Debug.Log("✅ OnAnimatorMove running");

        // Apply root motion from the Animator's object to the parent with the NavMeshAgent
        if (animator != null && agent.enabled)
        {
            transform.position = animator.rootPosition;
            transform.rotation = animator.rootRotation;
            agent.nextPosition = transform.position;
        }
    }
}
