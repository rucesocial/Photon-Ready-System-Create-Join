using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float playerSpeed = 2.0f;
    public float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;
    public bool isGrounded;
    
    void Start()
    {
       
    }

 
    private void OnEnable()
    {

        //transform.GetChild(1).GetComponent<MeshRenderer>().material.color = new Color(Random.Range(0, 255), Random.Range(0, 255), Random.Range(0, 255));
    }

    public float speed = 10.0f;
    public float rotationSpeed = 100.0f;

    void Update()
    {
       
        // Get the horizontal and vertical axis.
        // By default they are mapped to the arrow keys.
        // The value is in the range -1 to 1
        float translation = Input.GetAxis("Vertical") * speed;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;

        // Make it move 10 meters per second instead of 10 meters per frame...
        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;

        // Move translation along the object's z-axis
        transform.Translate(0, 0, translation);

        // Rotate around our y-axis
        transform.Rotate(0, rotation, 0);
        if (Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.Escape))
        {
            if (!CanvasManager.instance.CurrentRoomPanel.activeInHierarchy)
            {
                CanvasManager.instance.CloseAllPanels();
                CanvasManager.instance.CurrentRoomPanel.SetActive(true);
            }
            else
                CanvasManager.instance.CloseAllPanels();
        }
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            GetComponent<Rigidbody>().AddForce(0, jumpHeight, 0);
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }

    }
    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }
}
