using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Sprite[] playerSprites = new Sprite[4]; // up, down, right, left
    [SerializeField] private GameObject bomb;

    [SerializeField] private float playerSpeed = 10f;
    [SerializeField] private bl_Joystick joystick;
    [SerializeField] private int dyingScore = -1000;

    private float offset = 0.12f;
    private Vector3 innerPosition;

    private SpriteRenderer spriteRenderer;

    public void KillPlayer()
    {
        transform.position = innerPosition;
        GetComponent<ShowScoreScript>().ShowScore(dyingScore);
        ScoreSystem.AddScore(dyingScore);
    }

    public void SpawnBomb()
    {
        Instantiate(bomb, transform.position, Quaternion.identity);
    }

    // Start is called before the first frame update
    void Start()
    {
        innerPosition = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Vertical") > 0 || joystick.Vertical > 4) // up
        {
            spriteRenderer.sprite = playerSprites[0];
        }
        else if (Input.GetAxis("Vertical") < 0 || joystick.Vertical < -4) // down
        {
            spriteRenderer.sprite = playerSprites[1];
        }
        else if (Input.GetAxis("Horizontal") > 0 || joystick.Horizontal > 4) // right
        {
            spriteRenderer.sprite = playerSprites[2];
        }
        else if (Input.GetAxis("Horizontal") < 0 || joystick.Horizontal < -4) // left
        {
            spriteRenderer.sprite = playerSprites[3];
        }

        if (Input.GetKeyDown(KeyCode.Space)) // spawn bomb
        {
            SpawnBomb();
        }
    }

    void FixedUpdate()
    {
        // keyboard control
        transform.Translate(Vector3.right * Input.GetAxis("Horizontal") * playerSpeed * Time.deltaTime);
        transform.Translate((Vector3.up + new Vector3(offset, 0, 0)) * Input.GetAxis("Vertical") * playerSpeed * Time.deltaTime);

        // virtual joystick control
        transform.Translate(Vector3.right * joystick.Horizontal / 5 * playerSpeed * Time.deltaTime);
        transform.Translate((Vector3.up + new Vector3(offset, 0, 0)) * joystick.Vertical / 5 * playerSpeed * Time.deltaTime);
    }
}
