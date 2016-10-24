using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
    // Use this for initialization
    public float horizontalKeyboard = 0;
    public float verticalKeybaord = 0;
    public float moveSpeed = 5;
    Rigidbody rb;
    //Things used for moving our object
    Vector3 forwardMovement;    //this could be forwards or backwards. With forwad movement, we will only use the Z component.
    Vector3 sidewaysMovement;   //this could be left or right. We will only use the X component.
    public Vector3 totalMovement;      //Sum of forwardMovement and sidewaysMovement
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////
    public float yaw = 0;           //Mouse X
    
	void Start () {
        rb = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update() {
        playerMovement();
        playerLook();
        playerJump();
    }
    bool isGrounded() {
        Ray ray = new Ray(transform.position,Vector3.down);
        RaycastHit raycastHit;
        Physics.Raycast(ray, out raycastHit, 1.1f);

        if (raycastHit.collider != null && raycastHit.collider.tag == "floor") {
            print("isGrounded is true");
            return true;
        }
        else {
            print("isGrounded is false");
            return false;
        }
    }

    void playerJump()
    {
        if (Input.GetKey("space") && isGrounded())
        {
            rb.AddForce(new Vector3(0,10,0),ForceMode.Impulse);
        }
    }

    void playerLook() {
        yaw += Input.GetAxis("Mouse X");
        transform.localRotation = Quaternion.Euler(0, yaw, 0);
    }
    void playerMovement() {
        horizontalKeyboard = Input.GetAxis("Horizontal");
        verticalKeybaord = Input.GetAxis("Vertical");

        forwardMovement = verticalKeybaord * transform.forward;
        sidewaysMovement = horizontalKeyboard * transform.right;

        totalMovement = sidewaysMovement + forwardMovement;
        //totalMovement = transform.TransformDirection(totalMovement);

        rb.AddForce(totalMovement * moveSpeed);

        if (Input.GetKey("q"))
        {
            print("Q is pressed");
            moveSpeed = 10;
        }
        else
        {
            moveSpeed = 5;
        }
    }
}

//void playerMovement()
//{
//    //runs whatever is in here every frame
//    //so if we we had a game that runs at 60FPS (frames per second)
//    //the code within Update() will execute 60 times a second
//    horizontalKeyboard = Input.GetAxis("Horizontal");
//    verticalKeybaord = Input.GetAxis("Vertical");
//    //movement 
//    forwardMovement = new Vector3(0, 0, verticalKeybaord);
//    sidewaysMovement = new Vector3(horizontalKeyboard, 0, 0);
//    //add forward and sideways movement and set that to total movement
//    totalMovement = sidewaysMovement + forwardMovement;
//    //Remember to uncomment 2 lines below
//    totalMovement = transform.TransformDirection(totalMovement);
//    totalMovement = new Vector3(totalMovement.x, rb.velocity.y, totalMovement.z);
//    //set rb.velocity = totalMovement
//    rb.velocity = totalMovement * moveSpeed;

//    if (Input.GetKey("q"))
//    {
//        print("Q is pressed");
//        moveSpeed = 10;
//    }
//    else
//    {
//        moveSpeed = 5;
//    }
//}
