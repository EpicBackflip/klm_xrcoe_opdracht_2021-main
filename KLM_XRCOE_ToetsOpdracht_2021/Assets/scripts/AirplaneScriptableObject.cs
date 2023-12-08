using UnityEngine;
using UnityEngine.Serialization;


namespace planes
{
    [CreateAssetMenu(fileName = "AirplaneScriptableObject", menuName = "Airplane", order = 1)]
    public class AirplaneScriptableObject : ScriptableObject
    {
        [Tooltip("set the type of the plane here")]
        public string type;
        [Tooltip("set the brand of the plane here")]
        public string brand;
    }
}
