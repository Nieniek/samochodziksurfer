using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 direction;

    private int desiredLane = 1; // 0 = lewy, 1 = œrodek, 2 = prawy
    public float laneDistance = 2f; // Odleg³oœæ miêdzy pasami (wartoœci: -2, 0, 2)

    public float laneSwitchSpeed = 10f; // Szybkoœæ zmiany pasa
    private bool isReversingControls = false; // Czy sterowanie jest odwrócone?

    void Start()
    {
        controller = GetComponent<CharacterController>();
        if (controller == null)
        {
            Debug.LogError("Brak komponentu CharacterController na obiekcie gracza!");
        }
    }

    void Update()
    {
        // Sterowanie gracza do zmiany pasów
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (isReversingControls)
                desiredLane-=1; // Odwrócone sterowanie
            else
                desiredLane+=1;

            desiredLane = Mathf.Clamp(desiredLane, 0,2); // Ogranicz do trzech pasów
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (isReversingControls)
                desiredLane+=1;
            else
                desiredLane-=1;

            desiredLane = Mathf.Clamp(desiredLane, 0,2); // Ogranicz do trzech pasów
        }
    }

    void FixedUpdate()
    {
        // Obliczenie pozycji docelowej w oparciu o pas
        float targetX = (desiredLane - 1) * laneDistance; // Pozycje pasów: -2, 0, 2
        Vector3 targetPosition = new Vector3(targetX, transform.position.y, transform.position.z);

        // P³ynna interpolacja do docelowej pozycji
        Vector3 moveVector = Vector3.Lerp(transform.position, targetPosition, Time.fixedDeltaTime * laneSwitchSpeed);

        // Przesuniêcie za pomoc¹ CharacterController
        Vector3 deltaMove = moveVector - transform.position; // Oblicz ró¿nicê przesuniêcia
        controller.Move(deltaMove);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle")) // Zak³adamy, ¿e samochody maj¹ tag "Obstacle"
        {
            Debug.Log("Kolizja z samochodem! Gra zatrzymana.");
            Time.timeScale = 0; // Zatrzymanie gry
        }

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
