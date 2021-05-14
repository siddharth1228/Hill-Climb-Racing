using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public UnityEngine.UI.Image image;
    public float fuel=1;
    public float fuelconsumption=0.1f;
    public Rigidbody2D frontTireRb, backTireRb, carRb;
    public float speed = 800f;
    public float carTorque = 55f;
    float horizontalInput;
    public LayerMask ground;
    
    public float jumpForce=5f;
    public ParticleSystem dust;
   
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        image.fillAmount=fuel;
        if(Input.GetButtonDown("Jump") && (frontTireRb.IsTouchingLayers(ground)|| backTireRb.IsTouchingLayers(ground)))
        {
            Jump();
        }
        if(horizontalInput!=0 &&(frontTireRb.IsTouchingLayers(ground)|| backTireRb.IsTouchingLayers(ground)))
        {
            CreateDust();
        }
        else 
        {
            dust.Pause();
        }      
    }
    private void FixedUpdate()
    {
        if(fuel>0)
        {
            frontTireRb.AddTorque(-horizontalInput * speed * Time.fixedDeltaTime);
            backTireRb.AddTorque(-horizontalInput * speed * Time.fixedDeltaTime);
            carRb.AddTorque(-horizontalInput * carTorque * Time.fixedDeltaTime);
        }
    

        fuel-=fuelconsumption*Mathf.Abs(horizontalInput)*Time.fixedDeltaTime;
    }
    private void Jump()
    {
        backTireRb.velocity= new Vector2(backTireRb.velocity.x,jumpForce);
        frontTireRb.velocity= new Vector2(frontTireRb.velocity.x,jumpForce);
        carRb.velocity= new Vector2(carRb.velocity.x,jumpForce);
        
        
    }
    private void CreateDust()
    {
        dust.Play();
    } 
}
