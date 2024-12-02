using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP_Bar : MonoBehaviour
{
    public GameObject HpBar;
    public float maxHP;
    public float HP;
    
    // Start is called before the first frame update
    void Start()
    {
        HP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        float p;
        p = HP/maxHP; 

        if (HP >= 0){
            HpBar.transform.localScale = new Vector3(1*p, 1, 0);
        }else if (HP < 0){
            HpBar.transform.localScale = new Vector3(0, 1, 0);
        }else if (p >= 1){
            HpBar.transform.localScale = new Vector3(1, 1, 0);
        }
    }
}
