using NUnit;
using UnityEngine;
using UnityEngine.AI;

public class aiScript : MonoBehaviour
{
    public Camera cam;
    public NavMeshAgent agent;



    void Start()
    {
        agent = GetComponent<NavMeshAgent>();   
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Vector3 mousePos = new Vector3(hit.point.x, 0, hit.point.z);
                agent.SetDestination(mousePos);
            }
        }
    }
}
