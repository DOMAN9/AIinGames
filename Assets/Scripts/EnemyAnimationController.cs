using UnityEngine;
using UnityEngine.AI;

public class AImovement : MonoBehaviour
{
    public Transform target;
    public float moveSpeed = 3.5f;
    public float lookSpeed = 10f; // 🔁 Adjustable turn speed

    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;
        agent.updateRotation = false; // ❗ We’ll rotate manually
    }

    void Update()
    {
        if (target != null)
        {
            agent.SetDestination(target.position);
            RotateTowardsTarget();
        }
    }

    void RotateTowardsTarget()
    {
        Vector3 direction = (agent.steeringTarget - transform.position).normalized;
        if (direction.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * lookSpeed);
        }
    }

    public void SetMoveSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;
        agent.speed = moveSpeed;
    }
}
