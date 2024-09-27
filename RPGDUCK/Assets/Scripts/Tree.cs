using UnityEngine;

public class Tree : MonoBehaviour
{
   public int Health = 5;
   public GameObject stick;







   private void Update()
   {
        if(Health <= 0)
        {
                Destroy(gameObject);
                Instantiate(stick , transform.position , Quaternion.identity);
        }
   }


   public void  TakeDamage(int damage)
   {
        Health -= damage;
   }
}
