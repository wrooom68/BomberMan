using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    private Rigidbody2D rb2D;
    private float thrust =  10.0f;
    // Start is called before the first frame update
    void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        direction = transform.right;
    }

    Vector3 direction;

    // Update is called once per frame
    void Update()
    {
        rb2D.AddForce(direction * thrust);
    }

    void SwitchDirection()
    {
        int n = Random.Range(1, 50);

        rb2D.velocity = Vector2.zero;
        rb2D.angularVelocity = 0;

        if (direction == transform.right || direction == -transform.right)
        {
            if (n % 2 == 0)
            {
                direction = -transform.up;
            }
            else
            {
                direction = transform.up;
            }

            //GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;

        }
        else

        if (direction == -transform.up || direction == transform.up)
        {
            if (n % 2 == 0)
            {
                direction = -transform.right;
            }
            else
            {
                direction = transform.right;
            }

            //GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
        }
    }

    void OnCollisionStay2D()
    {
        SwitchDirection();
    }
    bool onlyOnce = false;
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name.Contains("explosion") && !onlyOnce)
        {
            onlyOnce = true;
            Destroy(gameObject);
            FindObjectOfType<PlayerController>().Counter();
        }
    }

}
