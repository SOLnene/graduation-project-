using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class TerrainData : UpdatableData
{
    public int terrianSize;
    public bool useFalloff;
    public float meshHeighMultiplier;
    public AnimationCurve meshHeightCurve;

    public float minHeight
    {
        get
        {
            return meshHeighMultiplier * meshHeightCurve.Evaluate(0);
        }
    }

    public float maxHeight
    {
        get
        {
            return meshHeighMultiplier * meshHeightCurve.Evaluate(1);
        }
    }

}
