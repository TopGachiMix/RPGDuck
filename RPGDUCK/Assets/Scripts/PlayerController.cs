using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class PlayerController : MonoBehaviour
{   
    [Header("Base")]
    public float Speed;
    private int Sticks;




    [Header("Control")]
    private Vector2 _movePositions;
    private Vector2 _move;
    private Rigidbody2D _rb;
    private bool _fasingRight = false;
    private Animator _anim;



    [Header("Time")]
    float nextTime;
    public float ReloadTime = 1f;

    [Header("Text")]
    public Text StickText;


    [Header("Attack")]
    public int Damage = 1;
    public Transform AttackPos;
    public float AttackRange = 0.5f;
    public LayerMask EnemyLayers;
    private ToolsSwitch _toolsSwitch;
    public LayerMask TreeLayer;


    [Header("Health")]
    public int Health = 3;
    public int NumOfHearts  = 3;
    public int MaxHealth = 3;
    public Image[] Hearts;
    public Sprite FullHearts;
    public Sprite EmptyHearts;




    void Start()
    {
            _rb = GetComponent<Rigidbody2D>();
            _anim = GetComponent<Animator>();
            _toolsSwitch = GetComponent<ToolsSwitch>();
   
            
    }


        

    private void Update()
    { 
            Vector3 _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _movePositions = new Vector2(Input.GetAxisRaw("Horizontal") , Input.GetAxisRaw("Vertical"));
            _move = _movePositions.normalized * Speed;


        if(_move.x > 0  || _move.x < 0)
        {
            _anim.SetBool("isRun" , true);
        }
        else if(_move.x == 0)
        {
                     _anim.SetBool("isRun" , false);

        }


            if(_mousePosition.x >  transform.position.x && _fasingRight == true )
            {
                    Flip();
                    
            }

            else if(_mousePosition.x <   transform.position.x && _fasingRight == false )
            {
                    Flip();
            }


        if(Health > NumOfHearts)
            Health = NumOfHearts;
        
        for(int i = 0 ; i < Hearts.Length ; i++)
        {
            if(i < NumOfHearts)
                Hearts[i].enabled = true;
            else
                Hearts[i].enabled = false;
            if(i < Health)
                Hearts[i].sprite = FullHearts;
            else
                Hearts[i].sprite = EmptyHearts;
        }

        if(Health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }


        
            if(Time.time > nextTime  )
            {
                if(Input.GetMouseButtonDown(0) && _toolsSwitch.Axe.activeInHierarchy)
                {  
                     _anim.SetTrigger("Axe");
                    nextTime = Time.time + ReloadTime;
                    GetComponent<ToolsSwitch>().enabled = false;
                }

                else  if(Input.GetMouseButtonDown(0) && _toolsSwitch.Sword.activeInHierarchy)
                {
                    _anim.SetTrigger("Sword");
                    nextTime = Time.time + ReloadTime;
                    GetComponent<ToolsSwitch>().enabled = false;
                }
            }
                StickText.text = $"{Sticks}";


    }       


    private void FixedUpdate()
    {
            _rb.MovePosition(_rb.position + _move * Time.fixedDeltaTime);
    }


    private void Flip()
    {
        _fasingRight =!_fasingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *=-1;
        transform.localScale = Scaler;

    }



    public void TakeDamage(int damage)
    {
        Health -= damage;
    }


    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.CompareTag("Stick"))
        {
                Sticks++;
                Destroy(coll.gameObject);
        }
    }


    public void OnChop()
    {
       GetComponent<ToolsSwitch>().enabled = true;
        Collider2D[] hit2D = Physics2D.OverlapCircleAll(AttackPos.position , AttackRange , TreeLayer);

        for(int i = 0 ; i < hit2D.Length ; i++)
        {
            hit2D[i].GetComponent<Tree>().TakeDamage(1);
        }
    }

    public void OnAttack()
    {   
        GetComponent<ToolsSwitch>().enabled = true;
        Collider2D[] hitAttack2D = Physics2D.OverlapCircleAll(AttackPos.position , AttackRange , EnemyLayers);
        
        foreach(Collider2D hitAttack in hitAttack2D)
        {
            hitAttack.GetComponent<Enemy>().TakeDamage(Damage);
        }
    }


    void OnDrawGizmos()
    {
            if(AttackPos == null)
                    return;
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(AttackPos.position , AttackRange);
}



}


