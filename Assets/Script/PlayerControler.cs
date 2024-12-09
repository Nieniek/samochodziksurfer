using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Dodajemy UI dla punktów

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 direction;

    private int desiredLane = 0;//0 = lewy, 1 = œrodek, 2 = prawy
    private float targetLane; // Zmienna dla docelowego pasa

    public float laneDistance = 2f; // Odleg³oœæ miêdzy pasami (wartoœci: -2, 0, 2)
    public float laneChangeSpeed = 10f; // Szybkoœæ zmiany pasa
    private bool isReversingControls = false; // Czy sterowanie jest odwrócone?

    // Zmienne dla punktów
    public Text scoreText; // Komponent UI Text do wyœwietlania punktów
    private float score = 0f; // Wynik
    private float distanceTraveled = 0f; // Dystans

    void Start()
    {
        controller = GetComponent<CharacterController>();
        targetLane = desiredLane; // Pocz¹tkowa pozycja pasa
        if (scoreText != null)
        {
            scoreText.text = "Punkty: " + score.ToString("F0"); // Wyœwietlenie pocz¹tkowych punktów
        }
    }

    void Update()
    {
        // Sterowanie gracza do zmiany pasów
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (isReversingControls)
                desiredLane -= 1; // Odwrócone sterowanie
            else
                desiredLane += 1;

            desiredLane = Mathf.Clamp(desiredLane, 0, 2); // Ogranicz do trzech pasów
            targetLane = desiredLane - 1; // Ustawienie targetLane w oparciu o pas
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (isReversingControls)
                desiredLane += 1;
            else
                desiredLane -= 1;

            desiredLane = Mathf.Clamp(desiredLane, 0, 2); // Ogranicz do trzech pasów
            targetLane = desiredLane - 1; // Ustawienie targetLane w oparciu o pas
        }

        Move(); // Wywo³anie metody do p³ynnej zmiany pasów

        // Naliczanie punktów na podstawie przebytego dystansu
        //distanceTraveled += Mathf.Abs(transform.position.z) * Time.deltaTime;
        //score = Mathf.Floor(distanceTraveled); // Punkty równaj¹ siê przebytej odleg³oœci (ca³kowite)
        score = Time.timeSinceLevelLoad;
        // Aktualizowanie wyœwietlania punktów
        if (scoreText != null)
        {
            scoreText.text = "Punkty: " + score.ToString("F0");
        }
    }

    public void Move()
    {
        // Obliczenie odleg³oœci miêdzy aktualn¹ pozycj¹ gracza a docelowym pasem
        if (Mathf.Abs(transform.position.x - targetLane * laneDistance) > Mathf.Epsilon)
        {
            // Je¿eli gracz nie jest na docelowym pasie, zaczynamy ruch
            Vector3 targetPosition = new Vector3(targetLane * laneDistance, transform.position.y, transform.position.z);

            // Przemieszczanie gracza w kierunku docelowego pasa
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, laneChangeSpeed * Time.deltaTime);
        }
    }

   

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            StartCoroutine(ReverseControlsForFiveSeconds()); // Odwrócone sterowanie
            Debug.Log("Wjecha³eœ w wodê, sterowanie odwrócone!");
        }
    }

    private IEnumerator ReverseControlsForFiveSeconds()
    {
        isReversingControls = true;
        yield return new WaitForSeconds(5f);
        isReversingControls = false;
        Debug.Log("Sterowanie wróci³o do normy.");
    }
}
