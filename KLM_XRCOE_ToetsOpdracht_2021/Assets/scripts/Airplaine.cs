using UnityEngine;
using UnityEngine.AI;

public class Airplaine : MonoBehaviour
{
    [HideInInspector]
    public AirplaneScriptableObject airplaneData;

    private string type;
    private string merk;
    
    private NavMeshAgent agent;
    public int number;

    public float wanderRadius;

    private Transform target;
    private float timer;

    private Vector3 newPos;

    private bool isParked = false;

    private Light light;

    private bool lightIsOn;

    void Start()
    {
        type = airplaneData.type;
        merk = airplaneData.merk;
        name = type;
        agent = GetComponent<NavMeshAgent>();

        light = transform.GetChild(0).transform.GetChild(3).GetComponent<Light>();

        newPos = RandomNavSphere(transform.position, wanderRadius, -1);
        agent.SetDestination(newPos);
    }
    void Update()
    {
        if (agent.velocity.magnitude <= 0 && !isParked)
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

    public void Park(Vector3 Hangerpos)
    {
        agent.SetDestination(Hangerpos);
        isParked = true;
    }

    public void ToggleLights()
    {
        if (lightIsOn)
        {
            light.intensity = 0;
            lightIsOn = false;
        }
        else
        {
            light.intensity = 10;
            lightIsOn = true;
        }
    }
}
