using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace planes
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        
        public List <AirplaneScriptableObject> airplaneData = new List<AirplaneScriptableObject>(3);
        private List<Airplaine> airplanes = new List<Airplaine>();
        public List<GameObject> hangers = new List<GameObject>();

        public GameObject winText;
    
        public Airplaine airplanePrefab;
    
        private Vector3 newPosition = new Vector3(512, 384, 0);
        private int asignedNumber;

        private int planesParked;
        
        void Start()
        {
            Instance = this;
            for (int i = 0; i < airplaneData.Count; i++)
            {
                airplanes.Add(Instantiate(airplanePrefab,transform));
                airplanes[i].airplaneData = airplaneData[i];
                airplanes[i].transform.position = newPosition;

                asignedNumber = i + 1;
                airplanes[i].number = asignedNumber;
                hangers[i].GetComponent<Text>().text = asignedNumber.ToString();
            }
        }

        public void Park()
        {
            for (int i = 0; i < airplanes.Count; i++)
            {
                airplanes[i].Park(hangers[i].transform.position);
            }
        }
        public void ToggleLights()
        {

            foreach (Airplaine airplaine in airplanes)
            {
                airplaine.ToggleLights();
            }
        }
        public void ParkingCompleted()
        {
            planesParked++;
            if (planesParked == 3)
            {
                winText.SetActive(true);
            }
        }
    } 
}

