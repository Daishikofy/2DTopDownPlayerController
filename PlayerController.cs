using UnityEngine;

public class playerContrroller : MonoBehaviour {

    [SerializeField]
    private float speed;
    private Rigidbody2D rigidbody2D;
    private Vector2 playerMovement;

	// Use this for initialization
	void Start () {
        rigidbody2D = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 playerInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        playerMovement = playerInput * speed;
	}

    private void FixedUpdate()
    {
        rigidbody2D.MovePosition(rigidbody2D.position + playerMovement * Time.deltaTime);
    }
}
