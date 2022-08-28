using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowSkill : MonoBehaviour
{
    [SerializeField] float throwSpeed = 5f;

    GameObject playerObject;
    Vector3 positionToShoot;
    void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        positionToShoot = new Vector3(playerObject.transform.position.x, .3f, playerObject.transform.position.z);
    }

    void Update()
    {
        var step = throwSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, positionToShoot, step);

        if (transform.position == positionToShoot)
            Destroy(gameObject);
    }
}
