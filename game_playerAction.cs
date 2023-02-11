using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private float horizontal;
    private float speed = 8f;
    private float jumpHeight = 10f;
    private bool fades = false;
    private Color alphaColor;
    private float fadeSpeed = 10f;
    private bool posChanged = false;
    private Color originalColor;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(transform.up * 15f, ForceMode2D.Impulse);
        originalColor = GameObject.Find("cloud").GetComponent<SpriteRenderer>().material.color;
    }

    private void move()
    {
        horizontal = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private void jump()
    {
        rb.AddForce(transform.up * jumpHeight, ForceMode2D.Impulse);
    }
    
    void Update()
    {
        move();

        // if player on cloud edgecollider, jump
        if (rb.velocity.y == 0)
        {
            jump();
           // originalColor = GameObject.Find("cloud").GetComponent<SpriteRenderer>().material.color;
            fades = true;
        }

        // fades cloud after jump
        if (fades)
        {
            alphaColor.a = 0;

            alphaColor = GameObject.Find("cloud").GetComponent<SpriteRenderer>().material.color;

            float fade = alphaColor.a - (fadeSpeed * Time.deltaTime);

            alphaColor = new Color(alphaColor.r, alphaColor.g, alphaColor.b, fade);

            GameObject.Find("cloud").GetComponent<Renderer>().material.color = alphaColor;

            // when cloud faded transfrom its position
            if (alphaColor.a <= 0)
            {
                //GameObject duplicate = Instantiate(GameObject.Find("cloud"));
                var cloudHeight = 15f;
                GameObject.Find("cloud").transform.position = new Vector3(Random.Range(4f, 7f), cloudHeight);
                cloudHeight += 5f;
            }
        }     
           
    }
}

