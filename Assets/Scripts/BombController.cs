using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    [SerializeField] private float countdownTime = 3f;
    [SerializeField] private GameObject explosion;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartCountdown());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator StartCountdown()
    {
        yield return new WaitForSeconds(countdownTime);

        DoExplosion();
    }

    private void DoExplosion()
    {
        Instantiate(explosion, transform.position - new Vector3(0, 0, 10), Quaternion.identity);
        Destroy(gameObject);
    }
}
