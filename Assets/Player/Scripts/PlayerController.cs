using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class used to control all the aspects of the player
/// </summary>
public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// Speed multiplier for the player
    /// </summary>
    public float movementSpeed = 1;
    
    /// <summary>
    /// Jump Multiplier for the player
    /// </summary>
    public float jumpForce = 1;

    /// <summary>
    /// The player transform object
    /// </summary>
    public Transform playerTransform;

    /// <summary>
    /// The player animator object
    /// </summary>
    public Animator playerAnimator;

    /// <summary>
    /// The player sprite renderer object
    /// </summary>
    public SpriteRenderer playerSpriteRenderer;

    /// <summary>
    /// The value of a movement keypress. Positive values are right, negative are left.
    /// </summary>
    private float _movementTrigger;

    /// <summary>
    /// The Health text located on the UI
    /// </summary>
    public Text healthDisplay;

    /// <summary>
    /// The game over text displayed when the player dies
    /// </summary>
    public Text gameOverText;

    /// <summary>
    /// The value of the players health
    /// </summary>
    private int _health;

    /// <summary>
    /// The amount of damage inflicted on the player
    /// </summary>
    private int _damage;

    /// <summary>
    /// Rigid Body component responsible for the player physics
    /// </summary>
    public Rigidbody2D rigidBody;
    
    // Start is called once before the first frame
    void Start()
    {
        _health = 100;
        healthDisplay.text = _health.ToString();
    }

    // Update is called once per frame
    private void Update()
    {
        // Delta time refers to the amount of time (delta) between each rendered frame.
        // Since framerate and the time between frames is not constant, we need to multiply our value by our framerate in order to sync our movement with the time between each frame.
        _movementTrigger = Time.smoothDeltaTime * movementSpeed * Input.GetAxis("Horizontal");
        
        // The "Horizontal" parameters measures horizontal keypresses (A, D, Left Arrow, Right Arrow)
        // Left presses trigger a negative value, Right presses are Positive. It scales exponentially up to 1 (try Debug.log to see)
        playerTransform.position += new Vector3(_movementTrigger, 0 , 0) ; // Input.getAxis represents a keypress value.
        
        playerAnimator.SetFloat("Speed", Mathf.Abs(_movementTrigger)); // Sets the speed value which is used to determine the threshold for the run animation
        playerAnimator.SetFloat("Jump Force", Mathf.Abs(rigidBody.velocity.y)); // Sets the Jump Force value which is used to determine the threshold of the Jump animation

        if(_movementTrigger > 0 && playerSpriteRenderer.flipX is true) // Flips the player model depending on what direction they are traveling in order to make sure they are facing the direction they are headed
            playerSpriteRenderer.flipX = false;

        if(_movementTrigger < 0 && playerSpriteRenderer.flipX is false)
            playerSpriteRenderer.flipX = true;

        if((Input.GetButtonDown("Jump") || (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))) && Mathf.Abs(rigidBody.velocity.y) < 0.001f) // GetButtonDown is called whenever a button is pressed down on the keyboard. Unity already has the built in "Jump" parameter, so we dont need to specify keys.
        // The and statement checks to make sure that force already isn't applied, which would mean the player is already jumping. We don't want multiple jumps.
        {
            rigidBody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse); // Adds force to the y-axis, which makes the player jump up. The impulse adds an instant forced based on the mass
        }
    }
    
    /// <summary>
    /// Function called when the object collides with the player
    /// </summary>
    /// <param name="other">
    /// The object that has collided with the player (we check to make sure its an asteroid)
    /// </param>
    public void OnCollisionEnter2D(Collision2D other)
    {
        if(!other.collider.CompareTag("Asteroid")) return; // check if the collision is from an asteroid. If not end the function. One line if reduces nesting cycle and makes code more aesthetically pleasing
        
        Destroy(other.gameObject); // Destroys the asteroid on impact with the player

        _damage = (int) (other.transform.localScale.x * 10); // Applies damage to the player relative to the size of the asteroid
        
        if (_damage >= _health) // check if an asteroid does enough damage to kill the player
        {
            gameObject.SetActive(false); // Kills the player by deactivating it (prevents errors in console from generating and allows for potential respawn in the future
            _health = 0;
            gameOverText.gameObject.SetActive(true); // shows the game over text
        }
        else
        {
            _health -= _damage; // if the damage isn't enough to kill the player, then the damage from the asteroid is inflicted
            // Note: we check for if it kills the player first, then take damage as we don't want to catch the death of the player first to ensure they don't survive a blow
        }
        
        healthDisplay.text = _health.ToString(); // Updates the health display text
    }
}
