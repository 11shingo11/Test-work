using System.Collections;
using System.Collections.Generic;
using System.Net.Security;
using UnityEngine;

public class Weapon : Item
{ 
    public GameObject bulletPrefab; 
    public Transform firePoint; 
    public float bulletSpeed = 10f; 
    

    protected override void Start()
    {
        base.Start();
    }
    public void Shoot()
    {
        if (GameManager.instance.player.ammo != 0)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            Vector2 bulletDirection = (GameManager.instance.player.transform.localScale.x > 0) ? firePoint.right : -firePoint.right;
            bulletRb.velocity = Vector2.Scale(bulletDirection, new Vector2(bulletSpeed, bulletSpeed));
            if (GameManager.instance.player.transform.localScale.x > 0)
                bullet.transform.localScale = new Vector3(-0.2f, 0.2f, 0.2f);
            else
                bullet.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            GameManager.instance.player.ammo -= 1;
            if (GameManager.instance.player.ammoSlot>=0)
                GameManager.instance.inventorySlots[GameManager.instance.player.ammoSlot].itemCountText.text = GameManager.instance.player.ammo.ToString();
            Destroy(bullet,5f);
        }
        else
            Debug.Log("No ammo!!");
    }
}
