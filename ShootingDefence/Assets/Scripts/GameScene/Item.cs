using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 vec = new Vector3(0, 1, 0);
        this.transform.Rotate(vec);
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerCharacter pChar))
        {
            pChar.getHealthPack();
            Destroy(this.gameObject);
        }
    }

}
