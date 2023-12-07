using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List <AirplaneScriptableObject> airplaneData = new List<AirplaneScriptableObject>(3);
    private List<Airplaine> airplanes = new List<Airplaine>();
    public Airplaine airplanePrefab;
    private Vector3 newPosition = new Vector3(512, 384, 0);
    public List<GameObject> hangers = new List<GameObject>();
    private int asignedNumber;
    void Start()
    {
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
}
