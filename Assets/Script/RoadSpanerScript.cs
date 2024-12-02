
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoadSpanerScript : MonoBehaviour
{
    public GameObject RoadPiecePrefab;
    public float RoadMoveSpeed = 15;
    [SerializeField]
    public List<GameObject> RoadPieces;
    // Start is called before the first frame update
    void Start()
    {
        RoadPieces = new List<GameObject>();
        SpawnRoadPiece(transform.position);
        WarmUp();
    }

    // Update is called once per frame
    void Update()
    {
        MoveRoadPieces();
        float distanceToLastPiece = Vector3.Distance(transform.position, RoadPieces.Last().transform.position);
        if (distanceToLastPiece > 10)
        {
            Vector3 newSpawnPosition = RoadPieces.Last().transform.position + new Vector3(0, 0, 10);
            SpawnRoadPiece(newSpawnPosition);
        }
        if (RoadPieces.Count > 10)
        {
            Destroy(RoadPieces[0]);
            RoadPieces.RemoveAt(0);
        }
    }
    void SpawnRoadPiece(Vector3 position)
    {
        // dodaj losowanie drogi z prefabs
        GameObject newRoadPiece = Instantiate(RoadPiecePrefab, position, Quaternion.identity);
        newRoadPiece.transform.parent = transform;
        RoadPieces.Add(newRoadPiece);
    }
    void MoveRoadPieces()
    {
        foreach (GameObject roadPiece in RoadPieces)
        {
            roadPiece.transform.position += new Vector3(0, 0, -1)
                * Time.deltaTime
                * RoadMoveSpeed;
        }
    }
    void WarmUp()
    {
        for (int i = 0; i > -10; i--)
        {
            Vector3 newSpawnPosition = transform.position + new Vector3(0, 0, 10 * i);
            SpawnRoadPiece(newSpawnPosition);
        }
    }
}