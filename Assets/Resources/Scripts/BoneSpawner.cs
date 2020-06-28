using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneSpawner : MonoBehaviour
{
    public GameObject bonePrefab;
    public GameObject boneHolderPrefab;
    public Vector3 spawnPosition;
    [HideInInspector]
    public bool isSpawned;
}
