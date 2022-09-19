using UnityEngine;

// Include the namespace required to use Unity UI and Input System
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{

    // Create public variables for player speed, and for the Text UI game objects
    public float speed;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public GameObject loseTextObject;
    public TextMeshProUGUI livesText;
    private float movementX;
    private float movementY;

    private Rigidbody rb;
    private int count;
    private int lives;
    // At the start of the game..
    void Start()
    {
        // Assign the Rigidbody component to our private rb variable
        rb = GetComponent<Rigidbody>();

        // Set the count to zero
        
        count = 0;
        lives = 3;
        SetCountText();
        SetLivesText();

        // Set the text property of the Win Text UI to an empty string, making the 'You Win' (game over message) blank
        winTextObject.SetActive(false);
        loseTextObject.SetActive(false);
    }

    void FixedUpdate()
    {
        // Create a Vector3 variable, and assign X and Z to feature the horizontal and vertical float variables above
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        // ..and if the GameObject you intersect has the tag 'Pick Up' assigned to it..
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);

            // Add one to the score variable 'count'
            count = count + 1;

            // Run the 'SetCountText()' function (see below)
            SetCountText();
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.SetActive(false);
            lives = lives - 1;
            SetLivesText();
        }
    }

    void OnMove(InputValue value)
    {
        Vector2 v = value.Get<Vector2>();

        movementX = v.x;
        movementY = v.y;
    }

    void SetLivesText()
    {
        livesText.text = "Lives: " + lives.ToString();
        if (lives == 0)
        { // Set the text value of your 'winText'
            loseTextObject.SetActive(true);
            transform.position = new Vector4(100.0f, 0.0f, 100.0f);

        }
    }
    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();

        if (count == 12)
        {
            transform.position = new Vector3(50.0f, 0.0f, 50.0f);
        }
        else if (count >= 20)
        {
            // Set the text value of your 'winText'
            winTextObject.SetActive(true);
            transform.position = new Vector4(100.0f, 0.0f, 100.0f);
        }
    }
}