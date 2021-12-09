using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float enemySpeed = 1f;
    [SerializeField] private float chargeSpeedBoost = 2f;
    [SerializeField] private float distanceForCharge = 5f;
    [SerializeField] private Sprite[] moveSprites = new Sprite[4]; // up, down, right, left
    [SerializeField] private Sprite[] chargeSprites = new Sprite[4];
    [SerializeField] private Sprite[] dirtySprites = new Sprite[4];
    [SerializeField] private GameObject player;

    private int moveDirection; // 0 = up, 1 = down, 2 = right. 3 = left
    private bool isAttacking = false;
    private float offset = 0.12f;

    private bool isDirtyStunned = false;

    public void DoDirtyStun(float stunDuration)
    {
        isDirtyStunned = true;
        StartCoroutine(DirtyStunCountdown(stunDuration));
    }

    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        moveDirection = Random.Range(0, 4);
        StartCoroutine(TurnRandom());
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < distanceForCharge) 
        {
            isAttacking = true;
        }

        if (isDirtyStunned)
        {
            spriteRenderer.sprite = dirtySprites[moveDirection];
        }
        else if (isAttacking)
        {
            spriteRenderer.sprite = chargeSprites[moveDirection];
        }
        else
        {
            spriteRenderer.sprite = moveSprites[moveDirection];
        }
    }

    void FixedUpdate()
    {
        if (isAttacking && !isDirtyStunned)
        {
            Vector3 vectorToPlayer = player.transform.position - transform.position;
            float xAbs = Mathf.Abs(vectorToPlayer.x);
            float yAbs = Mathf.Abs(vectorToPlayer.y);

            if (xAbs > yAbs)
            {
                if (vectorToPlayer.x > 0)
                {
                    moveDirection = 2;
                }
                else
                {
                    moveDirection = 3;
                }
            }
            else if (xAbs < yAbs)
            {
                if (vectorToPlayer.y > 0)
                {
                    moveDirection = 0;
                }
                else
                {
                    moveDirection = 1;
                }
            }
        }

        float actualSpeed = enemySpeed;
        if (isAttacking) 
        {
            actualSpeed += chargeSpeedBoost;
        }
        
        if (!isDirtyStunned)
        {
            switch (moveDirection)
            {
                case 0: 
                    transform.Translate((Vector3.up + new Vector3(offset, 0, 0)) * actualSpeed * Time.deltaTime);
                    break;
                case 1:
                    transform.Translate((Vector3.down - new Vector3(offset, 0, 0)) * actualSpeed * Time.deltaTime);
                    break;
                case 2:
                    transform.Translate((Vector3.right) * actualSpeed * Time.deltaTime);
                    break;
                case 3:
                    transform.Translate((Vector3.left) * actualSpeed * Time.deltaTime);
                    break;
            }
        }
    }

    void OnCollisionStay2D(Collision2D collision) // avoid stuck in game borders
    {
        if (collision.gameObject.CompareTag("GameBorders") && !isDirtyStunned)
        {
            switch (collision.gameObject.name)
            {
                case "Top":
                    moveDirection = Random.Range(1, 4);
                    break;
                case "Bot":
                    moveDirection = new int[]{0, 2, 3}[Random.Range(0, 3)];
                    break;
                case "Right":
                    moveDirection = new int[]{0, 1, 3}[Random.Range(0, 3)];
                    break;
                case "Left":
                    moveDirection = Random.Range(0, 3);
                    break;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().KillPlayer();
        }
    }

    IEnumerator TurnRandom()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.5f + Random.Range(-1f, 1f));

            isAttacking = false;
            moveDirection = Random.Range(0, 4);
        }
    }

    IEnumerator DirtyStunCountdown(float time)
    {
        yield return new WaitForSeconds(time);

        isDirtyStunned = false;
    }
}
