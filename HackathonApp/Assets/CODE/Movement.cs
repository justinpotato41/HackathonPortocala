using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody2D rb;
    public PlatformEffector2D effector;
    private SpriteRenderer render;

    [Header("Run")]
    public float speed;
    public float upSpeed;

    public bool canClimb;

    Vector2 dir;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        render = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        dir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        Run();
        DropDown();
        GoUp();

        if(transform.position.y <= effector.transform.position.y)
        {
            render.sortingOrder = 6;
        }
        else
        {
            render.sortingOrder = 1;
        }
    }

    void Run()
    {
        rb.velocity = new Vector2(dir.x * speed, rb.velocity.y);
    }

    void DropDown()
    {
        if(dir.y < 0 && canClimb)
        {
            effector.rotationalOffset = 180f;
        }
        else
        {
            effector.rotationalOffset = 0f;
        }
    }

    void GoUp()
    {
        if(dir.y > 0 && canClimb)
        {
            rb.velocity = new Vector2(0f, upSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("scara"))
        {
            canClimb = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("scara"))
        {
            canClimb = false;
        }
    }
}
