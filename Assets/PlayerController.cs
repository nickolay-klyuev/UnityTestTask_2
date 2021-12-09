using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Sprite[] playerSprites = new Sprite[4]; // up, down, right, left
    [SerializeField] private GameObject bomb;

    [SerializeField] private float playerSpeed = 10f;

    private float offset = 0.12f;

    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Vertical") > 0) // up
        {
            spriteRenderer.sprite = playerSprites[0];
        }
        else if (Input.GetAxis("Vertical") < 0) // down
        {
            spriteRenderer.sprite = playerSprites[1];
        }
        else if (Input.GetAxis("Horizontal") > 0) // right
        {
            spriteRenderer.sprite = playerSprites[2];
        }
        else if (Input.GetAxis("Horizontal") < 0) // left
        {
            spriteRenderer.sprite = playerSprites[3];
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bomb, transform.position, Quaternion.identity);
        }
    }

    void FixedUpdate()
    {
        transform.Translate(Vector3.right * Input.GetAxis("Horizontal") * playerSpeed * Time.deltaTime);
        transform.Translate((Vector3.up + new Vector3(offset, 0, 0)) * Input.GetAxis("Vertical") * playerSpeed * Time.deltaTime);
    }
}
