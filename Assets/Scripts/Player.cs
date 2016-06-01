using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour 
{
    public static Animator _anim;
    public static bool IsGameoverTextVisible, alive;

    public float speed, jumpPower, maxSpeed;
    public Transform jumpEnd;
    private ParticleSystem _dustParticle;

    private Rigidbody2D _rb2d;
    private bool _isWalking,_isFacingRight, _isGrounded, _jump, _landed;
    private StatManager _statManager;
    private float _timerTemp, _fallVelocity;
    private SoundManager _soundManager;
    void Awake () 
    {
        //_timerTemp = 0.1f;
        speed *= 100;
        jumpPower *= 100;
            
        _dustParticle = Resources.Load<ParticleSystem>("Prefabs/DustParticle");
        _statManager = GameObject.FindGameObjectWithTag("StatManager").GetComponent<StatManager>();
        _soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        _anim = GetComponent<Animator>();
        _rb2d = GetComponent<Rigidbody2D>();
        alive = true;
    }
        
    void Update()
    {   
        _fallVelocity = Mathf.Abs(_rb2d.velocity.y);  //check the impact when landing
        int mask1 = 1 << LayerMask.NameToLayer("Ground");
        int mask2 = 1 << LayerMask.NameToLayer("Obstacle");
        //int combinedMask = mask1 | mask2;

        _isGrounded = Physics2D.Linecast(transform.position,jumpEnd.position, mask1 | mask2);
        if(Input.GetButtonDown("Jump") && _isGrounded)
            Jump();
    }

    void FixedUpdate () 
    {
        if(alive)
        {
            float h = (Input.GetAxisRaw("Horizontal"));

            //movement
           //_rb2d.AddForce((Vector2.right * speed) * h);
            //_rb2d.velocity = new Vector2(h * speed, _rb2d.velocity.y);
            transform.Translate(Vector2.right * h * speed * Time.deltaTime);
            //limiting the speed of the player
            if(_rb2d.velocity.x > maxSpeed)
                _rb2d.velocity = new Vector2(maxSpeed, _rb2d.velocity.y);
            if(_rb2d.velocity.x < -maxSpeed)
                _rb2d.velocity = new Vector2(-maxSpeed, _rb2d.velocity.y);
            
            //calls animation
            _isWalking = Mathf.Abs(h) > 0;
            _anim.SetBool("IsRunning", _isWalking);

            //make the character face the right direction
            if(h < 0 && !_isFacingRight) 
                Flip();
            else if (h > 0 && _isFacingRight)
                Flip();
            
            //dust particle
            if(_isWalking && _isGrounded)
            {
                ParticleSystem instance = (ParticleSystem)Instantiate(_dustParticle, jumpEnd.position, Quaternion.identity); 
                Destroy(instance, instance.duration);
                _soundManager.RunningSound(true);
            }
            else
            {
                _soundManager.RunningSound(false);
            }

            //reduce health while moving
            if(_isWalking)
            {
                StatManager.ToxinReduce(_statManager.ToxinReductionValue()); 
            }
            StatManager.HungerReduce(_statManager.HungerReductionValue());
        }          
    }


    //check how fast it lands 
    void OnCollisionEnter2D(Collision2D collision) 
    {
        if (Mathf.Abs(collision.relativeVelocity.y) > 150)
        {
            ParticleSystem instance = (ParticleSystem)Instantiate(_dustParticle, jumpEnd.position, Quaternion.identity); 
            instance.Play();
            Destroy(instance, instance.duration);
            _soundManager.LandingSound();
        }    
    }
    void Flip()
    {
        _isFacingRight = !_isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
    public void Jump()
    {
        _rb2d.AddForce(new Vector2(_rb2d.velocity.x,jumpPower)); 
       //_rb2d.velocity = new Vector2(_rb2d.velocity.x, jumpPower);
    }


}
