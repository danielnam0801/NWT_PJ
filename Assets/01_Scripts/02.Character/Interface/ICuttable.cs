using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICuttable
{
    public void SpriteCutting(Vector2 InputVec, Vector2 OutputVec, int layerMask = Physics2D.AllLayers);
}
