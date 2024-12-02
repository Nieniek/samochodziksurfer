using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 direction;

    private int desiredLane = 1; // 0 = lewy, 1 = œrodek, 2 = prawy
    public float laneDistance = 4f; // Odleg³oœæ miêdzy pasami

    private bool isReversingControls = false; // Czy sterowanie jest odwrócone?

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Wyzerowanie ruchu w osi Z, aby samochód nie porusza³ siê do przodu
        direction.z = 0;

        // Obs³uga wejœcia gracza do zmiany pasa
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (isReversingControls)
            {
                // Odwrócone sterowanie: prawo na lewo
                desiredLane--;
                if (desiredLane < 0)
                    desiredLane = 0; // Ogranicz gracza do lewego pasa
            }
            else
            {
                // Normalne sterowanie
                desiredLane++;
                if (desiredLane > 2)
                    desiredLane = 2; // Ogranicz gracza do prawego pasa
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (isReversingControls)
            {
                // Odwrócone sterowanie: lewo na prawo
                desiredLane++;
                if (desiredLane > 2)
                    desiredLane = 2; // Ogranicz gracza do prawego pasa
            }
            else
            {
                // Normalne sterowanie
                desiredLane--;
                if (desiredLane < 0)
                    desiredLane = 0; // Ogranicz gracza do lewego pasa
            }
        }
    }

    void FixedUpdate()
    {
        // Oblicz pozycjê docelow¹
        Vector3 targetPosition = transform.position;
        targetPosition.x = (desiredLane - 1) * laneDistance; // Pozycje: -laneDistance, 0, laneDistance

        // Przesuniêcie gracza w kierunku docelowej pozycji
        Vector3 moveVector = Vector3.zero;
        moveVector.x = (targetPosition.x - transform.position.x) * 10f; // Interpolacja osi X
        moveVector.y = direction.y; // Uwzglêdnienie grawitacji lub skoków, jeœli istniej¹

        // Brak ruchu w osi Z
        moveVector.z = 0;

        controller.Move(moveVector * Time.fixedDeltaTime);
    }

    // Sprawdzenie kolizji z wod¹
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            StartCoroutine(ReverseControlsForFiveSeconds()); // Odwracamy sterowanie na 5 sek
            Debug.Log("Wjecha³eœ w wodê, sterowanie odwrócone!");
        }
    }

    // Korutyna odwracaj¹ca sterowanie na 5 sekund
    private IEnumerator ReverseControlsForFiveSeconds()
    {
        isReversingControls = true;
        yield return new WaitForSeconds(5f); // Czekaj przez 5 sekund
        isReversingControls = false; // Przywróæ normalne sterowanie po 5 sekundach
        Debug.Log("Sterowanie wróci³o do normy.");
    }
}
