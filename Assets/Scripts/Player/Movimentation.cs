using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class Movimentation : MonoBehaviour
{
    private Vector3 targetPos;
    public float speed = 10;
    private Animator anim;

    private Vector3 mousePos;
    private bool isRunning = false;
    private float rotationZ;
    private Vector3 difference;

    PhotonView view;

    void Start()
    { 
        //targetPos = new Vector2(0, -4);
        view = GetComponent<PhotonView>();
        anim = GetComponent<Animator>();
    }

    void Animation() {
        isRunning = transform.position != targetPos;
        
        difference = mousePos - transform.position;
        difference.Normalize();    
        
        if(rotationZ >= -45 && rotationZ <= 45)
            anim.SetInteger("direction", 3);
        else if(rotationZ > 45 && rotationZ <= 135)
            anim.SetInteger("direction", 1);
        else if(rotationZ < -45 && rotationZ >= -135)
            anim.SetInteger("direction", 2);
        else
            anim.SetInteger("direction", 0);
        
        anim.SetBool("isRunning", isRunning);
    }

    void Update()
    {
        if(view.IsMine) {

            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (Input.GetMouseButton(0))
            {
                targetPos = new Vector3(mousePos.x, mousePos.y);
                rotationZ = Mathf.Atan2(difference.x, difference.y) * Mathf.Rad2Deg;
            }

            transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * speed);

            //transform.rotation = Quaternion.LookRotation(Vector3.forward, targetPos);

            Animation();
        }
    }

    void OnCollisionEnter2D()
    {
        speed = 0.1f;
        transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * speed);
    }

    void OnCollisionExit2D()
    {
        speed = 10;
        transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * speed);
    }
}
