using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class MapType
{
    public GameObject MapPrefab;
    public int MapCode;
    public Vector2 MapPosition;
}

[CreateAssetMenu(menuName = "SO/Map")]
public class MapInformation : ScriptableObject
{
    //public List<MapType>
}
