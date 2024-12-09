using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 direction;

    private int desiredLane = 1; // 0 = lewy, 1 = �rodek, 2 = prawy
    public float laneDistance = 2f; // Odleg�o�� mi�dzy pasami (warto�ci: -2, 0, 2)

    public float laneSwitchSpeed = 10f; // Szybko�� zmiany pasa
    private bool isReversingControls = false; // Czy sterowanie jest odwr�cone?

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
        // Sterowanie gracza do zmiany pas�w
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (isReversingControls)
                desiredLane-=1; // Odwr�cone sterowanie
            else
                desiredLane+=1;

            desiredLane = Mathf.Clamp(desiredLane, 0,2); // Ogranicz do trzech pas�w
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (isReversingControls)
                desiredLane+=1;
            else
                desiredLane-=1;

            desiredLane = Mathf.Clamp(desiredLane, 0,2); // Ogranicz do trzech pas�w
        }
    }

    void FixedUpdate()
    {
        // Obliczenie pozycji docelowej w oparciu o pas
        float targetX = (desiredLane - 1) * laneDistance; // Pozycje pas�w: -2, 0, 2
        Vector3 targetPosition = new Vector3(targetX, transform.position.y, transform.position.z);

        // P�ynna interpolacja do docelowej pozycji
        Vector3 moveVector = Vector3.Lerp(transform.position, targetPosition, Time.fixedDeltaTime * laneSwitchSpeed);

        // Przesuni�cie za pomoc� CharacterController
        Vector3 deltaMove = moveVector - transform.position; // Oblicz r�nic� przesuni�cia
        controller.Move(deltaMove);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle")) // Zak�adamy, �e samochody maj� tag "Obstacle"
        {
            Debug.Log("Kolizja z samochodem! Gra zatrzymana.");
            Time.timeScale = 0; // Zatrzymanie gry
        }

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
