using UnityEngine;
using UnityEngine.AI;

public class Airplaine : MonoBehaviour
{
    public AirplaneScriptableObject airplaneData;

    private string type;
    private string merk;
    
    private NavMeshAgent agent;

    public float wanderRadius;
    public float wanderTimer;
 
    private Transform target;
    private float timer;

    private Vector3 newPos;


    void Start()
    {
        type = airplaneData.type;
        merk = airplaneData.merk;
        
        agent = GetComponent<NavMeshAgent>();

        newPos = RandomNavSphere(transform.position, wanderRadius, -1);
        agent.SetDestination(newPos);
    }
    void Update()
    {
        if (agent.velocity.magnitude <= 0)
        {
            newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            agent.SetDestination(newPos);
        }
    }
    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask) {
        Vector3 randDirection = Random.insideUnitSphere * dist;
 
        randDirection += origin;
        NavMeshHit navHit;
        NavMesh.SamplePosition (randDirection, out navHit, dist, layermask);
 
        return navHit.position;
    }
}
