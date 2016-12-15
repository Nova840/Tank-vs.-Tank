using UnityEngine;
using System.Collections;

public class LevelSize : MonoBehaviour {

    [SerializeField]
    private float sizeX = 500, sizeZ = 500;
    [SerializeField]
    private float centerX = 0, centerZ = 0;

    public float SizeX { get { return sizeX; } }
    public float SizeZ { get { return sizeZ; } }
    public float CenterX { get { return centerX; } }
    public float CenterZ { get { return centerZ; } }

}
