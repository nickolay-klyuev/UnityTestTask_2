using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowScoreScript : MonoBehaviour
{
    private TextMeshPro scoreText;
    private bool isShowingScore = false;
    public void ShowScore(int score)
    {
        scoreText.text = score.ToString();
        isShowingScore = true;
        scoreText.enabled = true;
        StartCoroutine(ShowScoreCountdown());
    }

    IEnumerator ShowScoreCountdown()
    {
        yield return new WaitForSeconds(0.5f);
        isShowingScore = false;
        scoreText.enabled = false;
        scoreText.transform.position = transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponentInChildren<TextMeshPro>();
        scoreText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // score text
        if (isShowingScore)
        {
            scoreText.transform.Translate(Vector3.up * Time.deltaTime * 2);
        }
    }
}
