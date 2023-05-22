using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : Character
{
    public Joystick joystick;
    private Rigidbody2D rb;
    public int ammo = 0;
    public int invCount = 0;
    public int ammoSlot = -1;
    public int makarovSlot = -1;
    private int makarovAmount = 1;
    public int akSlot = -1;
    private int akAmount = 1;
    public int helmSlot = -1;
    private int helmAmount = 1;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    protected override void Start()
    {
        base.Start();
        GameManager.instance.LoadGame();
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
            RecieveDamage();
        if (collision.collider.CompareTag("Wall"))
            rb.velocity = Vector2.zero;
    }

    protected override void RecieveDamage()
    {
        hitpoint -= 5;
        if (hitpoint <= 0)
            Death();
    }

    protected override void Death()
    {
        base.Death();
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
    }
    
    private void Update()
    {
        if (joystick.IsJoystickActive())
        {
            Vector2 direction = joystick.GetJoystickDirection();
            if (direction.x > 0)
                transform.localScale = Vector3.one;
            else if (direction.x < 0)
                transform.localScale = new Vector3(-1, 1, 1);
            rb.velocity = direction * moveSpeed;
        }
        else
            rb.velocity = Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {   
        GameObject item = other.gameObject;
        Item itemAmount = other.gameObject.GetComponent<Item>();        
        PickUp(item, itemAmount.amount);
    }

    private void PickUp(GameObject item ,int amount)
    {       
        if (invCount < 6)
        {
            if (item.CompareTag("Ammo"))
            {
                if (ammoSlot < 0 )
                {
                    GameManager.instance.inventorySlots[invCount].SetItem(item.GetComponent<SpriteRenderer>().sprite, amount + ammo);
                    ammo += amount;
                    ammoSlot = invCount;
                    Destroy(item);
                    invCount++;
                }
                else
                {
                    GameManager.instance.inventorySlots[ammoSlot].SetItem(item.GetComponent<SpriteRenderer>().sprite, amount + ammo);
                    ammo += amount;
                    Destroy(item);
                }
            }
            else if (item.CompareTag("Makarov"))
            {
               
                if (makarovSlot<0)
                {
                    GameManager.instance.inventorySlots[invCount].SetItem(item.GetComponent<SpriteRenderer>().sprite, amount);
                    Destroy(item);
                    makarovSlot = invCount;
                    invCount++;
                }
                else
                {   
                    makarovAmount += 1;
                    GameManager.instance.inventorySlots[makarovSlot].SetItem(item.GetComponent<SpriteRenderer>().sprite, makarovAmount);
                    Destroy(item);
                }
            } 
            else if (item.CompareTag("Ak"))
            {
               
                if (akSlot < 0)
                {
                    GameManager.instance.inventorySlots[invCount].SetItem(item.GetComponent<SpriteRenderer>().sprite, amount);
                    Destroy(item);
                    akSlot = invCount;
                    invCount++;
                }
                else
                {   
                    akAmount += 1;
                    GameManager.instance.inventorySlots[akSlot].SetItem(item.GetComponent<SpriteRenderer>().sprite, akAmount);
                    Destroy(item);
                }
            } 
            
        }
        return;
    }

}
