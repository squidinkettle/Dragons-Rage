using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("General")]
    [SerializeField] Transform cam;
    [SerializeField] CharacterController controller;
    Vector3 moveVector;
    [SerializeField] Animator playerAnimation;
    [SerializeField] GameObject attackHitBox;

    [Header("Movement Tuning")]
    [SerializeField] float speed = 6.0f;
    [SerializeField] float smoothTurn = 0.1f;
    [SerializeField] float speedMod=1;
    float turnSmoothVelocity;
    float originalSpeed;

    //Angle modifiers
    private float groundSlopeAngle = 0f;
    private Vector3 groundSlopeDir;

    void Start()
    {
        speed = speed * transform.GetComponent<Player>().playerSpeed;
        originalSpeed = speed;

    }

    public void SetSpeedModifier(float num)
    {
        speedMod = num;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
        UpdateStats();

        FallControl();
        MovementControl();

        PhysicalAttack();
    }

    private void MovementControl()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;


        playerAnimation.SetFloat("anim_speed", speed / 9);

        CheckIfDragonIsBerserk(ref horizontal, vertical, ref direction);

        if (direction.magnitude >= 0.1f)
        {
            playerAnimation.SetBool("is_walking", true);
            SetMovement(direction);

        }
        else
        {
            playerAnimation.SetBool("is_walking", false);
        }
    }

    private void SetMovement(Vector3 direction)
    {
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;

        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, smoothTurn);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);

        Vector3 moveDir = Quaternion.Euler(0f, angle, 0f) * Vector3.forward;


        controller.Move(moveDir.normalized * speed * Time.deltaTime);
    }

    private void CheckIfDragonIsBerserk(ref float horizontal, float vertical, ref Vector3 direction)
    {
        if (gameObject.GetComponent<Player>().GetRage())
        {

            horizontal = RageMovement(horizontal);
            direction = new Vector3(horizontal, 0, vertical).normalized;

        }
    }

    private void FallControl()
    {
        moveVector = Vector3.zero;
        if (controller.isGrounded == false)
        {

            moveVector += Physics.gravity;
        }

        controller.Move(moveVector * Time.deltaTime);
    }

    float RageMovement(float horizontal)
    {

        horizontal=Random.Range(-1f, 1f);
        return horizontal;

    }


    void PhysicalAttack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            float x = gameObject.transform.position.x;
            float y = gameObject.transform.position.y;
            float z = gameObject.transform.position.z;
            Vector3 location1 = new Vector3(x, y, z);
            GameObject attack = Instantiate(attackHitBox, transform.position + (transform.forward * 10), gameObject.transform.rotation);
            attack.transform.parent = gameObject.transform;

            location1 = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, gameObject.transform.localPosition.z);
            attack.transform.position = location1 + (gameObject.transform.right * 1)+(gameObject.transform.forward *5);




        }
    }



    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Vector3 temp = Vector3.Cross(hit.normal, Vector3.down);
        groundSlopeDir = Vector3.Cross(temp, hit.normal);
        groundSlopeAngle = Vector3.Angle(hit.normal, Vector3.up);
    }

    void UpdateStats() {

        speed = originalSpeed * speedMod;
    
    }


}

