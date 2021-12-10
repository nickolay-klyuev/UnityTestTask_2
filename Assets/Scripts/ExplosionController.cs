using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    [SerializeField] private float explodePower = 10f;
    [SerializeField] private float stunDuration = 3f;
    [SerializeField] private int scoreAmount = 100;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Enemy") || collider.gameObject.CompareTag("Bomb"))
        {
            collider.GetComponent<Rigidbody2D>().AddForce((collider.transform.position - transform.position) * explodePower);
        }

        if (collider.gameObject.CompareTag("Enemy"))
        {
            ScoreSystem.AddScore(scoreAmount);

            collider.GetComponent<EnemyController>().DoDirtyStun(stunDuration);
            collider.GetComponent<ShowScoreScript>().ShowScore(scoreAmount);
        }
    }
}
