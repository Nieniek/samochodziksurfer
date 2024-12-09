using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawnerScript : MonoBehaviour
{
    public GameObject samochodPrefab; // Prefab samochodu
    public float spawnInterval = 1f; // Co ile sekund spawnujemy samochód

    private float[] lanePositions = { -2, 0, 2 }; // Współrzędne X pasów (lewy, środek, prawy)

    void Start()
    {
        if (samochodPrefab == null)
        {
            Debug.LogError("Prefab samochodu nie został przypisany!");
            return;
        }

        StartCoroutine(SpawnCar());
        StartCoroutine(IncreaseSpeed());
    }

    IEnumerator SpawnCar()
    {
        while (true)
        {
            // Wybieramy losowy pas
            int randomLaneIndex = Random.Range(0, lanePositions.Length);
            float spawnX = lanePositions[randomLaneIndex];

            // Tworzymy pozycję spawnu na danym pasie
            Vector3 spawnPosition = new Vector3(spawnX, transform.position.y, transform.position.z);

            // Tworzymy samochód
            GameObject car = Instantiate(samochodPrefab, spawnPosition, Quaternion.identity);

            // Dodajemy siłę, aby samochód poruszał się w stronę gracza
            Rigidbody carRigidbody = car.GetComponent<Rigidbody>();
            if (carRigidbody != null)
            {
                carRigidbody.AddForce(Vector3.back * 1000);
            }
            else
            {
                Debug.LogError("Prefab samochodu nie ma komponentu Rigidbody!");
            }

            // Usuwamy samochód po 10 sekundach
            Destroy(car, 10);

            // Czekamy określony czas przed spawnem kolejnego samochodu
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    IEnumerator IncreaseSpeed()
    {
        while (true)
        {
            yield return new WaitForSeconds(10); // Co 10 sekund
            spawnInterval *= 0.95f; // Zmniejsz czas spawnowania o 5%
        }
    }
}
