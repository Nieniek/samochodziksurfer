using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        // Je�li gracz uderzy w samoch�d
        if (collision.gameObject.CompareTag("Player"))
        {
            // Zatrzymanie gry
            Time.timeScale = 0;
            Debug.Log("Game Over");
        }
    }
}
