using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController controller;
    private Transform cam;
    private Animator anim;

    public float speed;
    private Vector3 MoveDirecion;
    public float gravity;

    public float smoothRotTime;

    private float turnSmoothVelocity;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        cam = Camera.main.transform;

    }

    // Update is called once per frame
    void Update()
    {
        Move();
        GetMouseInput();

    }

    void Move()
    {
        if (controller.isGrounded)
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            Vector3 direcion = new Vector3(horizontal, 0f, vertical);

            if (direcion.magnitude > 0)
            {
                float angle = Mathf.Atan2(direcion.x, direcion.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, angle, ref turnSmoothVelocity, smoothRotTime);
                transform.rotation = Quaternion.Euler(0f, smoothAngle, 0f);
                MoveDirecion = Quaternion.Euler(0f, angle, 0f) * Vector3.forward;
                
                anim.SetInteger("transition", 1);
            }
            else
            {
                anim.SetInteger("transition", 0);
                MoveDirecion = Vector3.zero;

            }
        }

        MoveDirecion.y -= gravity * Time.deltaTime;
        
        controller.Move(MoveDirecion * speed * Time.deltaTime);

    }

    void GetMouseInput()
    {
        if (controller.isGrounded)
        {
            if (Input.GetMouseButtonDown(0))
            {
                anim.SetInteger("transition", 2);
            }
        }
    }
}
