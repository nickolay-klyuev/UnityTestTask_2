using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    static private int score = 0;
    static public void AddScore(int amount)
    {
        score += amount;
    }
    static public int GetScore()
    {
        return score;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
