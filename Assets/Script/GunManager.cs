using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private Transform bulletSpawn;
    [SerializeField]
    private float fireRate;
    [SerializeField]
    private float bulletSpeed;
    [SerializeField]
    private float timeBeforeDestruction;
    [SerializeField]
    private PlayerStateManager playerStateManager;
    private bool canFire = true;
    
    void Update()
    {
        if (playerStateManager.actionAsset.Player.Dialogue.ReadValue<float>() >= 0.9f && canFire)
        {
            StartCoroutine(Fire());
        }
    }

    private IEnumerator Fire()
    {
        canFire = false;
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        Debug.Log("bullet fired");
        bulletRb.linearVelocity = playerStateManager.gameObject.transform.forward * bulletSpeed;
        Destroy(bullet, timeBeforeDestruction);
        yield return new WaitForSeconds(fireRate);
        canFire = true;
    }
}
