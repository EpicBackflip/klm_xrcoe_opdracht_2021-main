using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace planes
{
    public class Airplaine : MonoBehaviour
    {
        [HideInInspector]
        public AirplaneScriptableObject airplaneData;

        private string type;
        private string merk;
        [HideInInspector]
        public int number;

        private Material trailMaterial;

        private NavMeshAgent agent;
        [Tooltip("you can set the max wander radius for the planes here meaning they won't move to a point outside of this range")]
        public float wanderRadius;
        private Transform target;

        private Vector3 newPos;
        
        private Light light;
        private bool lightIsOn;
        
        private TrailRenderer trailRenderer;
    
        [HideInInspector]
        public bool isParking;
        private bool isParked;

        //setting the plane variables to be that of the given plane data
        void Start()
        {
            type = airplaneData.type;
            merk = airplaneData.brand;
            name = type;
            trailMaterial = airplaneData.trailMaterial;
            
            agent = GetComponent<NavMeshAgent>();
    
            //this looks a bit weird but that's because the light is 2 child layers into the plane prefab
            light = transform.GetChild(0).transform.GetChild(3).GetComponent<Light>();
            //The same counts for the trail renderer
            trailRenderer = transform.GetChild(0).transform.GetChild(4).GetComponent<TrailRenderer>();
            trailRenderer.material = trailMaterial;
        }
        void Update()
        {
            // if the agent reached it's destination and stopped moving and is not busy parking set a new pos
            if (agent.velocity.magnitude <= 0 && !isParking)
            {
                newPos = RandomNavSphere(transform.position, wanderRadius, -1);
                agent.SetDestination(newPos);
            }
            // if the agent reached it's destination and stopped moving and is busy parking but hasn't parked yet
            // that means it reached it parking spot and is now parked
            if (agent.velocity.magnitude <= 0 && isParking && !isParked)
            {
                GameManager.Instance.ParkingCompleted();
                isParked = true;
            }
        }

        //method to create a new random sphere for the plane to move towards
        public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
        {
            NavMeshHit navHit;
            
            //random point within a sphere within the range of the distance
            Vector3 randomDirection = Random.insideUnitSphere * dist;
            //adding to the origin so it starts from that point
            randomDirection += origin;
            //setting the navmesh position to that of the random point
            NavMesh.SamplePosition (randomDirection, out navHit, dist, layermask);
     
            return navHit.position;
        }
        
        //set the destination to that of the given hanger
        public void Park(Vector3 Hangerpos)
        {
            isParking = true;
            agent.SetDestination(Hangerpos);
        }
    
        //toggles the light and sets it's intensity
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

