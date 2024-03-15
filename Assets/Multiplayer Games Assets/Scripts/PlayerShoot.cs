using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerShoot : NetworkBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private float shootSpeed;
    [SerializeField] private Transform shootPoint;

    private Rigidbody tankRB;

    public override void OnNetworkSpawn()
    {
        tankRB = GetComponent<Rigidbody>();
    }
   
    void Update()
    {
        if (!IsOwner) return;

        if (Input.GetButtonDown("Jump"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject bulletObject = Instantiate(bullet, shootPoint.position, shootPoint.rotation);

        bulletObject.GetComponent<NetworkObject>().Spawn();
        bullet.GetComponent<Rigidbody>().AddForce(tankRB.velocity + bulletObject.transform.forward * shootSpeed, ForceMode.VelocityChange);

        Destroy(bulletObject, 5.0f);
    }
}
