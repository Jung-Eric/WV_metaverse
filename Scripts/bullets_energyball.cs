using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullets_energyball : MonoBehaviour
{
    public GameObject explosion;

    public float velocity_x = 3;

    Vector3 position;

    // Start is called before the first frame update
    void Start()
    {
        //transform.position;
        Invoke("Destory_self", 3);
    }

    void Update()
    {
        position = transform.position;

        position.x += velocity_x;

        transform.position = position;
        //------------------------------------
        //������ �浹 �� �����Ѵ�.

    }

    private void OnTriggerEnter2D(Collider2D col)
    {

        Instantiate(explosion, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
        Destroy(this.gameObject);

    }

    void Destory_self()
    {
        Instantiate(explosion, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
        Destroy(this.gameObject);
    }

    void way_change()
    {
        velocity_x = -velocity_x;
    }

}
