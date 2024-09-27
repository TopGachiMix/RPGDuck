using UnityEngine;

public class Enemy : MonoBehaviour
{   
    [Header("Times")]
        private Transform _player;
        public float  Speed = 2f;
        public int Health;
        public int Damage = 1;
        float nextTime;
        public float DamageTime = 1f;
        private Animator _anim;

        private void Start()
        {
            _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            _anim = GetComponent<Animator>();
            
        }

        private void Update()
        {   
            
            if(Health <= 0)
            {
                Destroy(gameObject);
            }
        }


        private void FixedUpdate()
        {
          
            if(_player.transform.position.x > transform.position.x)
            {
                transform.eulerAngles = new Vector3(0 , 180 , 0);
            }

            else if(_player.position.x < 0)
            {
                transform.eulerAngles = new Vector3(0 , 0 , 0);
            }


           transform.position = Vector2.MoveTowards(transform.position , _player.transform.position , Speed * Time.fixedDeltaTime);
        }



        public void TakeDamage(int damage)
        {
            Health -= damage;
        }


        private void OnTriggerStay2D(Collider2D coll)
        {   
            if(Time.time > nextTime)
            {

            if(coll.CompareTag("Player"))
            {
                _anim.SetTrigger("attack");
                nextTime = Time.time + DamageTime;
              
                
            }
            
            }
        }


        private void OnAttack()
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().TakeDamage(Damage);
        }


}
