using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using UnityEngine.UI;

public class Movimentation : MonoBehaviour
{
    private Vector3 targetPos;
    public float maxSpeed = 10;
    private float speed;
    private Animator anim;

    private Vector3 mousePos;
    private bool isRunning = false;
    private float rotationZ;
    private Vector3 difference;
    private Text playerUsernameLabel;

    [SerializeField]
    private DetectAreaMouse detectAreaMouse;

    PhotonView view;

    public static GameObject LocalPlayerInstance;

    void Awake() {
        view = GetComponent<PhotonView>();
        if(view.IsMine) {
            Movimentation.LocalPlayerInstance = this.gameObject;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    { 
        //targetPos = new Vector2(0, -4);
        StartCoroutine(FindDetectAreaMouse());
        playerUsernameLabel = transform.GetChild(3).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>();
        anim = GetComponent<Animator>();
        speed = maxSpeed;
    }

    IEnumerator FindDetectAreaMouse() {
        yield return new WaitForSeconds(0.4f);
        detectAreaMouse = GameObject.Find("AreaMouse").GetComponent<DetectAreaMouse>();
        //yield return new WaitForSeconds(0.9f);
        Server.canMove = true;
        Debug.Log("unlocked");
    }

    void Animation() {
        isRunning = transform.position != targetPos;
        
        difference = mousePos - transform.position;
        difference.Normalize();    
        
        if(!isRunning && Server.canMove)
            rotationZ = Mathf.Atan2(difference.x, difference.y) * Mathf.Rad2Deg;

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

            if (Server.canMove && detectAreaMouse != null)
            {
                if(Input.GetMouseButtonDown(0) && detectAreaMouse.getIsDetected()) {
                    targetPos = new Vector3(mousePos.x, mousePos.y);
                    rotationZ = Mathf.Atan2(difference.x, difference.y) * Mathf.Rad2Deg;
                }
                transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * speed);
            } else targetPos = transform.position;
            //transform.rotation = Quaternion.LookRotation(Vector3.forward, targetPos);
            Animation();
        }
    }
    /*
    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if(stream.IsWriting) {
            stream.SendNext(anim);
            stream.SendNext(isRunning);
            stream.SendNext(difference);
            stream.SendNext(rotationZ);
        }
        else {
            anim = (Animator)stream.ReceiveNext();
            isRunning = (bool)stream.ReceiveNext();
            difference = (Vector3)stream.ReceiveNext();
            rotationZ = (float)stream.ReceiveNext();
        }
    }
    */
}