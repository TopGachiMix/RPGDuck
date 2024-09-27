using UnityEngine;

public class ToolsSwitch : MonoBehaviour
{
    public GameObject Axe;
    public GameObject Sword;

    void Start()
    {
        
    }

  
    private void Update()
    {
            if(Input.GetKeyDown(KeyCode.Alpha1) &Sword.activeInHierarchy)
            {
                    Axe.SetActive(true);
                    Sword.SetActive(false);
            }
            else if(Input.GetKeyDown(KeyCode.Alpha2) && Axe.activeInHierarchy)
            {
                    Axe.SetActive(false);
                    Sword.SetActive(true);
            }
    }   
}
