using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public Rigidbody2D theRB;
    public float moveSpeed;

    public Animator myAnim;

    public string areaTransitionName;

    private Vector3 bottomLeftLimit, topRightLimit;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        } else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        theRB.velocity = new Vector2(moveX, moveY) * moveSpeed;

        myAnim.SetFloat("moveX", theRB.velocity.x);
        myAnim.SetFloat("moveY", theRB.velocity.y);

        if (moveX == 1 || moveX == -1 || moveY == 1 || moveY == -1)
        {
            myAnim.SetFloat("lastMoveX", moveX);
            myAnim.SetFloat("lastMoveY", moveY);
        }

        transform.position = new Vector3(
                                Mathf.Clamp(transform.position.x, bottomLeftLimit.x, topRightLimit.x),
                                Mathf.Clamp(transform.position.y, bottomLeftLimit.y, topRightLimit.y),
                                transform.position.z);
    }

    public void setBounds(Vector3 botLeft, Vector3 topRight)
    {
        bottomLeftLimit = botLeft + new Vector3(.5f, .75f, 0f);
        topRightLimit = topRight - new Vector3(.5f, .75f, 0f);
    }
}
