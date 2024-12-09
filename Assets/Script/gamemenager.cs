using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class gamemenager : MonoBehaviour
{
    List<int> highScores = new List<int>();
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddHighScore(int highScore)
        { highScores.Add(highScore); }

    public List<int> GetHightScores()
    {
        highScores.Sort();
        return highScores;
    }
    public int GetBestScore()
    {
        highScores.Sort();
        return highScores.First();
    }
}
