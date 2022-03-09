using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    // Start is called before the first frame update
    public virtual void GetDamage(int damage)
    {

    }
    public virtual void Die()
    {
        Destroy(this.gameObject);
    }
}
