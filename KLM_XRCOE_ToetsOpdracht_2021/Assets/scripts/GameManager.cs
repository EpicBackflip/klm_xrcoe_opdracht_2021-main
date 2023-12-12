using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

namespace planes
{
    public class GameManager : MonoBehaviour
    {
        //I needed to call the gameManager from the airplane script so i made it a singleton so it can be called everywhere
        public static GameManager Instance { get; private set; }
        
        [Tooltip("Here you can drag the scriptable objects for the plane types you would like to spawn in order")]
        public List <AirplaneScriptableObject> airplaneData = new List<AirplaneScriptableObject>(3);
        [Tooltip("Drag the hangers from the scene here")]
        public List<GameObject> hangers = new List<GameObject>();
        private List<Airplaine> airplanes = new List<Airplaine>();
        
        [Tooltip("Drag the wintext from the canvas here")]
        public GameObject winText;
        [Tooltip("Drag the airplaneprefab from the assets here")]
        public Airplaine airplanePrefab;
    
        private Vector3 newPosition = new Vector3(512, 384, 0);

        public GameObject fireWorks;
        
        private int asignedNumber;
        private int amountOfPlanesParked;
        
        void Start()
        {
            Instance = this;
            Spawner();
        }

        public void Spawner()
        {
            //here i'm instantiated 3 planes and adding them to the list
            //after which i'm looping through them to assign each plane with the airplane data from the scriptable objects and setting their position
            for (int i = 0; i < airplaneData.Count; i++)
            {
                airplanes.Add(Instantiate(airplanePrefab, transform));
                airplanes[i].airplaneData = airplaneData[i];
                airplanes[i].transform.position = newPosition;
                //setting the numbers, I made an int i+1 so i can set the text properly as i starts at 0
                asignedNumber = i + 1;
                airplanes[i].number = asignedNumber;
                hangers[i].GetComponent<Text>().text = asignedNumber.ToString();
            }
        }

        //Here i'm parking all the planes into their given hangers based on i so the first plane gets sent to the first hanger etc.
        public void Park()
        {
            for (int i = 0; i < airplanes.Count; i++)
            {
                airplanes[i].Park(hangers[i].transform.position);
            }
        }
        
        //toggling the lights when the button is clicked
        public void ToggleLights()
        {
            foreach (Airplaine airplaine in airplanes)
            {
                airplaine.ToggleLights();
            }
        }
        
        //everytime a plane calls this method it means it parked and adds one to the counter
        //once it reaches the amount of planes it means they're all parked and the wintext is activated
        public void ParkingCompleted()
        {
            amountOfPlanesParked++;
            
            if (amountOfPlanesParked == airplanes.Count)
            {
                winText.SetActive(true);
                fireWorks.SetActive(true);
            }
        }
    } 
}

