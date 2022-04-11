using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Entity
{
    public static Hero Instance { get; set; }

    [SerializeField] private float speed = 3f; // скорость движения
    [SerializeField] private int lives = 5; // HP
    [SerializeField] private float jumpForce = 10f; // сила прыжка
    

    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator anim;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponent<Animator>();
        Instance = this;
    }

    private void FixedUpdate()
    {
        CheckGround(); // Проверка земли под ногами
        SquatCheck(); // Проверка уперлись ли мы во что-то головой
       
    }

    private void Update()
    {
        if (onGround)
        {
            anim.SetInteger("moveX", 0);
        } // Анимация когда персонаж стоит на месте
            
        Run();
        if(Input.GetKeyDown(KeyCode.Space))
            Jump();
        Crawl(); // Ползанье
        Lunge(); // Подкат
    }
    private void Run()
    {
        if (Input.GetButton("Horizontal"))
        {
            Vector3 dir = transform.right * Input.GetAxisRaw("Horizontal");
            transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);
            sprite.flipX = dir.x < 0.0f;
            if (onGround) anim.SetInteger("moveX", 1);
        }   
    }
    private int JumpCount = 0;
    public int maxJumpValue = 1;
    private void Jump()
    {
        if ((onGround || JumpCount++ < maxJumpValue) && !SquatCheck())
        {
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        }
    }
    public bool onGround;
    public Transform GroundCheck;
    public float CheckRadius = 0.25f;
    public LayerMask Ground;
    private void CheckGround() // Проверка наличия земли под ногами
    {
        onGround = Physics2D.OverlapCircle(GroundCheck.position, CheckRadius, Ground);
        if (!onGround) anim.SetInteger("moveX", 2);
        if (onGround) { JumpCount = 0; }
    }
    public Transform TopCheck;
    public float TopCheckRadius = 0.25f;
    public LayerMask TopGround;
    //public Collider2D poseStand;
    //public Collider2D poseSquat;
    
    private bool SquatCheck() // Приседание
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            anim.SetBool("Squat", true);
            //poseStand.enabled = false;
            //poseSquat.enabled = true;
        }
        else if(!Physics2D.OverlapCircle(TopCheck.position, TopCheckRadius, TopGround)) // Не дает встать если над TopCheck что-то мешает
        {
            anim.SetBool("Squat", false);
            //poseStand.enabled = true;
            //poseSquat.enabled = false;  
        }
        return anim.GetBool("Squat");
    }
    private void Crawl() // Ползанье(анимация)
    {
        if(Input.GetButton("Horizontal") && Input.GetKey(KeyCode.LeftShift))
        {
            anim.SetInteger("Crawl", 1);
        }
        else { anim.SetInteger("Crawl", 0); }
    }
    [SerializeField] private int lungeImpulse = 150;
    private void Lunge() // Рывок
    {
        if (Input.GetKey(KeyCode.LeftControl) && onGround && !lungeLock)
        {
            rb.velocity = new Vector2(0, 0);
            if (Input.GetKey(KeyCode.A))
            {
                rb.AddForce(Vector2.left * lungeImpulse);
            }
            else if(Input.GetKey(KeyCode.D)){ rb.AddForce(Vector2.right * lungeImpulse); }
            lungeLock = true;
            Invoke("LockLunge", 2f); // Разблокировка рывка через 2 секунды
        }
    }
    private bool lungeLock = false;
    private void LockLunge()
    {
        lungeLock = false;
    }
    public override void GetDamage(int damage)
    {
        lives -= damage;
        Debug.Log(lives);
        //if(lives < 1)
        //{
        //    Die();
        //a}
    }
    public int GetLive()
    {
        return lives;
    }
}
