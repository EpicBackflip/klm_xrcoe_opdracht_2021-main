using UnityEngine;
using UnityEngine.AI;

namespace planes
{
    public class Airplaine : MonoBehaviour
    {
        [HideInInspector]
        public AirplaneScriptableObject airplaneData;

        private string type;
        private string merk;
        public int number;
        
        private NavMeshAgent agent;
        
        public float wanderRadius;
        private Transform target;

        private Vector3 newPos;
        
        private Light light;
        private bool lightIsOn;
    
        [HideInInspector]
        public bool isParking;
        private bool isParked;

        void Start()
        {
            type = airplaneData.type;
            merk = airplaneData.merk;
            name = type;
            
            agent = GetComponent<NavMeshAgent>();
    
            light = transform.GetChild(0).transform.GetChild(3).GetComponent<Light>();
            
        }
        void Update()
        {
            Debug.Log(agent.pathStatus);
            if (agent.velocity.magnitude <= 0 && !isParking)
            {
                newPos = RandomNavSphere(transform.position, wanderRadius, -1);
                agent.SetDestination(newPos);
            }
            
            if (agent.velocity.magnitude <= 0 && isParking && !isParked)
            {
                GameManager.Instance.ParkingCompleted();
                isParked = true;
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
            isParking = true;
            agent.SetDestination(Hangerpos);
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
}

