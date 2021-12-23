using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class self_destruct : MonoBehaviour
{
    // Start is called before the first frame update

    Vector3 position;

    void Start()
    {
        Invoke("Destory_self", 3);
    }

    void Destory_self()
    {
        Destroy(this.gameObject);
    }
}
