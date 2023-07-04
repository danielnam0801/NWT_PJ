using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGuide : MonoBehaviour
{
    public bool HasPair = false;
    [SerializeField]
    private GuideLine guide;

    public void SetPair(GuideLine _guide)
    {
        guide = _guide;
        HasPair = true;
    }

    public void ClearPair()
    {
        HasPair = false;
    }
}
