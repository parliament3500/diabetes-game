using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    public Transform sightEnd, groundStart, groundEnd, sightBegin, jumpEnd;
    public float speed,jumpPower;
    private Rigidbody2D _rb2d;
    private bool _avoid,_isGrounded, _isFacingRight, _jumpToAvoid, _canJump;

    void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        int mask1 = 1 << LayerMask.NameToLayer("Ground");
        int mask2 = 1 << LayerMask.NameToLayer("Obstacle");
        int mask3 = 1 << LayerMask.NameToLayer("Player");
        int combinedMask = mask1 | mask2 | mask3;

        Debug.DrawLine(transform.position, sightEnd.position, Color.red);
        _isGrounded = Physics2D.Linecast(groundStart.position, groundEnd.position, mask1 | mask2);
        _avoid = Physics2D.Linecast(sightBegin.position, sightEnd.position, mask3 | mask2 | mask3);
        _jumpToAvoid = Physics2D.Linecast(sightBegin.position, jumpEnd.position, mask1 | mask2);
    }

    void FixedUpdate()
    {
        if (_isGrounded)
        {               
            _rb2d.velocity = new Vector2(speed, _rb2d.velocity.y);
            _canJump = true;

            if (_avoid)
            {
                speed = -speed;
                //_avoid = false;
            }
        }
        //====================================
        if (speed < 0 && !_isFacingRight)
            Flip();
        else if (speed > 0 && _isFacingRight)
            Flip();
        //====================================
        if (_jumpToAvoid && _canJump && _isGrounded)
        {
            _rb2d.AddForce(new Vector2(0, jumpPower));
            _canJump = false;
        }
    }

    public void Flip()
    {
        _isFacingRight = !_isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
