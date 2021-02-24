using UnityEngine;

public class Character : Unit
{
    [SerializeField] private float _speed = 5.0f;
    [SerializeField] private int _livesNumber = 5;
    [SerializeField] private float _jumpForce = 15f;

    new private Rigidbody2D rigidbody;

    CharacterController controller;

    private Animator animator;
    private SpriteRenderer sprite;
    private bool _isGrounded = false;

    private Bullet bullet;

    public int Lives
    {
        get
        {
            return _livesNumber;
        }

        set
        {
            if (value < 5) _livesNumber = value;
            livesBar.Refresh();
        }
    }

   private LivesBar livesBar;

    private CharacterState State
    {
        get
        {
            return (CharacterState)animator.GetInteger("State");
        }

        set
        {
            animator.SetInteger("State", (int)value);
        }
    }

    private void Awake()
    {

        livesBar = FindObjectOfType<LivesBar>();

        controller = GetComponent<CharacterController>();


        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();

        bullet = Resources.Load<Bullet>("Bullet");
    }

    /* private void Start()
     {
         animator = GetComponent<Animator>();
         rigidbody = GetComponent<Rigidbody2D>();
         sprite = GetComponentInChildren<SpriteRenderer>();
     }*/

    private void FixedUpdate()
    {
        CheckGround();
    }

    private void Update()
    {
        if (_isGrounded)
        {
            State = CharacterState.Idle;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }

        if (Input.GetButton("Horizontal"))
        {
            Run();
        }

        if (_isGrounded && Input.GetButtonDown("Jump"))

        {
            Jump();
        }
    }

    private void Run()
    {
        Vector3 direction = transform.right * Input.GetAxis("Horizontal");

        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, _speed * Time.deltaTime);

        sprite.flipX = direction.x < 0;

        if (_isGrounded)
        {
            State = CharacterState.Run;
        }
    }

    private void Jump()
    {
        rigidbody.AddForce(transform.up * _jumpForce, ForceMode2D.Impulse);

    }

    private void Shoot()
    {
        Vector3 position = transform.position;
        position.y += 0.5f;
        Bullet newBullet = Instantiate(bullet, position, bullet.transform.rotation) as Bullet;

        newBullet.Parent = gameObject;
        newBullet.Direction = newBullet.transform.right * (sprite.flipX ? -1f : 1f);
    }

    public override void ReceiveDamage()
    {
        Lives--;
        rigidbody.velocity = Vector3.zero;
        rigidbody.AddForce(transform.up * 8f, ForceMode2D.Impulse);



        Debug.Log(_livesNumber);
    }

    private void CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.3f);

        _isGrounded = colliders.Length > 1;

        if (!_isGrounded)
        {
            State = CharacterState.Jump;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Bullet bullet = collider.gameObject.GetComponent<Bullet>();

        if (bullet && bullet.Parent != gameObject)
        {
            ReceiveDamage();
        }


    }
}

public enum CharacterState
{
    Idle, Run, Jump
}
