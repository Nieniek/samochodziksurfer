using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Dodajemy UI dla punkt�w

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 direction;

    private int desiredLane = 0;//0 = lewy, 1 = �rodek, 2 = prawy
    private float targetLane; // Zmienna dla docelowego pasa

    public float laneDistance = 2f; // Odleg�o�� mi�dzy pasami (warto�ci: -2, 0, 2)
    public float laneChangeSpeed = 10f; // Szybko�� zmiany pasa
    private bool isReversingControls = false; // Czy sterowanie jest odwr�cone?

    // Zmienne dla punkt�w
    public Text scoreText; // Komponent UI Text do wy�wietlania punkt�w
    private float score = 0f; // Wynik
    private float distanceTraveled = 0f; // Dystans

    void Start()
    {
        controller = GetComponent<CharacterController>();
        targetLane = desiredLane; // Pocz�tkowa pozycja pasa
        if (scoreText != null)
        {
            scoreText.text = "Punkty: " + score.ToString("F0"); // Wy�wietlenie pocz�tkowych punkt�w
        }
    }

    void Update()
    {
        // Sterowanie gracza do zmiany pas�w
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (isReversingControls)
                desiredLane -= 1; // Odwr�cone sterowanie
            else
                desiredLane += 1;

            desiredLane = Mathf.Clamp(desiredLane, 0, 2); // Ogranicz do trzech pas�w
            targetLane = desiredLane - 1; // Ustawienie targetLane w oparciu o pas
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (isReversingControls)
                desiredLane += 1;
            else
                desiredLane -= 1;

            desiredLane = Mathf.Clamp(desiredLane, 0, 2); // Ogranicz do trzech pas�w
            targetLane = desiredLane - 1; // Ustawienie targetLane w oparciu o pas
        }

        Move(); // Wywo�anie metody do p�ynnej zmiany pas�w

        // Naliczanie punkt�w na podstawie przebytego dystansu
        //distanceTraveled += Mathf.Abs(transform.position.z) * Time.deltaTime;
        //score = Mathf.Floor(distanceTraveled); // Punkty r�wnaj� si� przebytej odleg�o�ci (ca�kowite)
        score = Time.timeSinceLevelLoad;
        // Aktualizowanie wy�wietlania punkt�w
        if (scoreText != null)
        {
            scoreText.text = "Punkty: " + score.ToString("F0");
        }
    }

    public void Move()
    {
        // Obliczenie odleg�o�ci mi�dzy aktualn� pozycj� gracza a docelowym pasem
        if (Mathf.Abs(transform.position.x - targetLane * laneDistance) > Mathf.Epsilon)
        {
            // Je�eli gracz nie jest na docelowym pasie, zaczynamy ruch
            Vector3 targetPosition = new Vector3(targetLane * laneDistance, transform.position.y, transform.position.z);

            // Przemieszczanie gracza w kierunku docelowego pasa
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, laneChangeSpeed * Time.deltaTime);
        }
    }

   

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            StartCoroutine(ReverseControlsForFiveSeconds()); // Odwr�cone sterowanie
            Debug.Log("Wjecha�e� w wod�, sterowanie odwr�cone!");
        }
    }

    private IEnumerator ReverseControlsForFiveSeconds()
    {
        isReversingControls = true;
        yield return new WaitForSeconds(5f);
        isReversingControls = false;
        Debug.Log("Sterowanie wr�ci�o do normy.");
    }
}
