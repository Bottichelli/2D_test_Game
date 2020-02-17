using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : Creature
{

    [SerializeField] public float def_speed;

    [SerializeField] private float cr_speed; 

    [SerializeField] private float stairSpeed;

    [SerializeField] private Transform feetPos;

    [SerializeField] private Transform crouchPos1;

    [SerializeField] private Transform crouchPos2;

    [SerializeField] private BoxCollider2D box;

    [SerializeField] private CircleCollider2D circle;

    [SerializeField] public Transform _player;

    [SerializeField] private float checkRadius;

    [SerializeField] private float coyoteTimer;

    [SerializeField] private float feetRangeX;

    [SerializeField] private float feetRangeY;

    [SerializeField] private float jumpForce;

    [SerializeField] private float crouchDis;

    private float jumpTimeCounter;

    [SerializeField] private float jumpTime;

    [SerializeField] private bool isGrounded;
     
    [SerializeField] private bool isCrouch = false;

    [SerializeField] private bool isAttack = false;

    public bool isJumping;

    public bool OnStair;

    public bool isClimb;

    public bool isDamaged = false;

    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private LayerMask whatIsBar;


    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        speed = def_speed;
    }

    void Update()
    {
  
        Jump();

        Crouch();

        Flip();

        if (OnStair)
        {
            if (Input.GetButton("Vertical"))
            {
                isClimb = true;
            }
        }

    }

    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(crouchPos1.position, crouchPos1.position + Vector3.up * crouchDis);
        Gizmos.DrawLine(crouchPos2.position, crouchPos2.position + Vector3.up * crouchDis);

        Gizmos.DrawCube(feetPos.position, new Vector3(feetRangeX, feetRangeY, 1));

    }

    // Учим микрочелика ползать
    private void Crouch()
    {
        //пускаю лучи света, шоб чекнуть, есть ли над головой препятствия
        if (Physics2D.Raycast(crouchPos1.transform.position,  Vector2.up, crouchDis, whatIsBar) || Physics2D.Raycast(crouchPos2.transform.position, Vector2.up, crouchDis, whatIsBar))
        {
            isCrouch = true;
        }
        else
        {
            isCrouch = false;
        }

        //если все ок, вИрубаю верхний коллайдер
        if ((Input.GetKey(KeyCode.S)) && (isClimb == false) && (OnStair == false))
        {
            isCrouch = true;
            box.enabled = false;
            speed = cr_speed;
            animator.SetBool("crouch", true);
            
        }

        else if (isCrouch == false)
        {
            box.enabled = true;
            speed = def_speed;
            animator.SetBool("crouch", false);
            
        }
    
    }
        
    private void FixedUpdate()
    {
        Walking();

        Climbing();
    }

    // Учим микрочелика пользоваться лестницей
    private void Climbing()
    {
        if (isClimb)
        {
            rb.velocity = new Vector2(rb.velocity.x, Input.GetAxis("Vertical") * cr_speed);

            rb.gravityScale = 0;

            animator.SetBool("climb_bool", true);

        }

        else
        {
            rb.gravityScale = 8;
            animator.SetBool("climb_bool", false);

        }

        animator.SetFloat("cilmb", Mathf.Abs(Input.GetAxis("Vertical")));
    }

    // Учим микрочелика поворачивать тушку в нужном направлении 
    private void Flip()
    {
        // поворачиваем спрайт микрочела потем изменения значения Scale по оси х
        Vector2 _player = transform.localScale;
        if (Input.GetAxis("Horizontal") > 0)
        {
            _player.x = 1;
        }

        if (Input.GetAxis("Horizontal") < 0)
        {
            _player.x = -1;
        }
        transform.localScale = _player;
    }

    private void Jump()
    {
        //Проверяем стоит ли микрочелик на земле путем проверки соприкосновения ОверлапБокса с 
        //объектами, которые помечены слоем whatIsGround

        if (Physics2D.OverlapBox(feetPos.position, new Vector2(feetRangeX, feetRangeY), 0, whatIsGround))
        {
            isGrounded = true;
        }
        else
        {
            //запускаем карутину, отвечающую за "эффект Койота"
            StartCoroutine(Coyote());           
        }

        //Проверяем, стоит ли микрочелик на земле, чтобы он мог прыгнуть
        if ((isGrounded == true) && (Input.GetButtonDown("Jump")) && (isCrouch == false))
        {
            Jumping();
        }

        //проверяем, находится ли микрочелик на лестнице, чтобы он мог с нее прыгнуть
        else if (isGrounded == false && Input.GetButtonDown("Jump") && isClimb == true)
        {
            Jumping();
        }

        //Реализовываем удерживание кнопки прыжка, для более высокого прыжка микрочелика
        if (Input.GetButton("Jump") && isJumping == true)
        {
            //Если таймер прыжка не равен 0, то подбрасываем микрочела вверх до тех пор,
            //пока таймер прыжка не будет равен 0
            if (jumpTimeCounter > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                jumpTimeCounter -= Time.deltaTime;

                animator.SetBool("jumping", true);
            }

            //Если таймер прыжка равен 0, то микрочелик начикает падать и подрубается
            //анимация падения
            else 
            {
                isJumping = false;
                animator.SetBool("jumping", false);
                animator.SetBool("falling", true);

            }

        }

        //Раелизовываем 
        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
            animator.SetBool("jumping", false);
            animator.SetBool("falling", true); 

        }

        if (isGrounded == true)
        {
            animator.SetBool("falling", false);
        }
        else
        {
            
            animator.SetBool("falling", true);
        }
    }

    private void Jumping()
    {
        isJumping = true;

        jumpTimeCounter = jumpTime;
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

        if (OnStair == true)
        {
            isClimb = false;
        }
        
    }

    private void Walk(Vector2 dir)
    { 
        rb.velocity = new Vector2(dir.x * speed, rb.velocity.y);        
    }

    private void Walking()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector2 dir = new Vector2(x, y);

        Walk(dir);

        Vector3 _player = transform.localScale;
        if (x > 0)
        {
            _player.x = 1;
        }

        if (x < 0)
        {
            _player.x = -1;
        }

        animator.SetFloat("speed", Mathf.Abs(x));
    }

    IEnumerator Coyote()
    {
        yield return new WaitForSeconds(coyoteTimer);

        isGrounded = false;
    }
}
