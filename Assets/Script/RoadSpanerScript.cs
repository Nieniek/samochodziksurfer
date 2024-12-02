
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoadSpanerScript : MonoBehaviour
{
    public float RoadMoveSpeed = 15;

    // Nowa lista prefabrykatów, które bêd¹ losowane
    public List<GameObject> RoadPrefabs;

    private List<GameObject> RoadPieces;

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
        if (distanceToLastPiece > 6)
        {
            Vector3 newSpawnPosition = RoadPieces.Last().transform.position + new Vector3(0, 0, 6);
            SpawnRoadPiece(newSpawnPosition);
        }
        if (RoadPieces.Count > 20)
        {
            Destroy(RoadPieces[0]);
            RoadPieces.RemoveAt(0);
        }
    }

    void SpawnRoadPiece(Vector3 position)
    {
        // Losuj prefab z listy RoadPrefabs
        if (RoadPrefabs != null && RoadPrefabs.Count > 0)
        {
            int randomIndex = Random.Range(0, RoadPrefabs.Count);
            GameObject selectedPrefab = RoadPrefabs[randomIndex];
            GameObject newRoadPiece = Instantiate(selectedPrefab, position, Quaternion.identity);
            newRoadPiece.transform.parent = transform;
            RoadPieces.Add(newRoadPiece);
        }
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
        for (int i = 0; i > -20; i--)
        {
            Vector3 newSpawnPosition = transform.position + new Vector3(0, 0, 6 * i);
            SpawnRoadPiece(newSpawnPosition);
        }
    }
}
