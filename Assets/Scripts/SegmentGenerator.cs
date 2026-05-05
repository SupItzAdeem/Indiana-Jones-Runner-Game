using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SegmentGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] segment;
    [SerializeField] private int xPos = 50;
    [SerializeField] private float maxSpawnDistance = 100f;
    [SerializeField] private float bufferDistance = 60f;
    
    private List<GameObject> spawnedSegments = new List<GameObject>();
    public Transform player;
    
    void Start()
    {
        SpawnSegment();
    }
    
    void Update()
    {
        // makes sure there is always a segment ahead of the player
        if (player.position.x + bufferDistance > xPos) 
        {
            SpawnSegment();
        }
    }
    
    private void SpawnSegment()
    {
        int segmentNum = Random.Range(0, segment.Length);
        GameObject newSegment = Instantiate(segment[segmentNum], new Vector3(xPos, 0, 0), Quaternion.identity);
        spawnedSegments.Add(newSegment);
        xPos += 50;
        
        if (spawnedSegments.Count > 3)
        {
            GameObject oldSegment = spawnedSegments[0];
            if (player.position.x > oldSegment.transform.position.x + maxSpawnDistance)
            {
                Destroy(oldSegment);
                spawnedSegments.RemoveAt(0);
            }
        }
    }
}