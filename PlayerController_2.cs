using UnityEngine;
using System.Collections;

public class playerContrroller : MonoBehaviour {

    [SerializeField]
    private float speed;
    [SerializeField]
    private float dashFactor;
    [SerializeField]
    private float dashDuration;
    [SerializeField]
    private float dashCoolDown;
    

    private Rigidbody2D rigidbody2D;
    private Vector2 playerMovement;

    private IEnumerator dashCorroutine;
    private bool canDash;

    // Use this for initialization
    void Start () {
        rigidbody2D = GetComponent<Rigidbody2D>();
        canDash = true;
	}
	
	// Update is called once per frame
	void Update () {
        /*
         * PlayerInput receives values between -1 and 1 and is then normalized
         * PlayerMovement representes those values multiplied by the player's speed
         * The speed is only applied after going throught the dash test.
         */
        Vector2 playerInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        //We use corroutine for the dash so the update function stays clean
        if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            canDash = false;
            dashCorroutine = Dash();
            StartCoroutine( dashCorroutine );
        }
            
        playerMovement = playerInput * speed;
	}

    private void FixedUpdate()
    {
        rigidbody2D.MovePosition(rigidbody2D.position + playerMovement * Time.deltaTime);
    }

    IEnumerator Dash()
    {
        /*
         * The speed is multiply by the dash 
         * factor during the dashDuration
         */
        speed = speed * dashFactor;
        float normalizedTime = 0;
        while (normalizedTime <= 1f)
        {
            normalizedTime += 1 / dashDuration;
            yield return null;
        }
        
        // It is then set to the original speed
         
        speed = speed / dashFactor;

        // then the coolDown happens, the player can only dash again after it.
        normalizedTime = 0;
        while (normalizedTime <= 1f)
        {
            normalizedTime += 1 / dashCoolDown;
            yield return null;
        }

        canDash = true;
    }
}
