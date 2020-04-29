using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Sincronizar : MonoBehaviourPun, IPunObservable
{

    public Vector3 RealPosition = Vector3.zero;
    public Quaternion RealRotation = Quaternion.identity;


    public Vector3 RealPosition1 = Vector3.zero;
    public Quaternion RealRotation1 = Quaternion.identity;

    public Vector3 RealVelocity1 = Vector3.zero;
    public Vector3 RealVelocity2 = Vector3.zero;

    public Vector3 RealScale;

    public Animator anim;

    public Rigidbody2D rb;

    void FixedUpdate()
    {
        if (!photonView.IsMine)
        {
            //rb.position = Vector3.Lerp(rb.position, RealPosition1, 0);
            //rb.velocity = Vector3.Lerp(rb.velocity, RealVelocity1, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine)
        {
            transform.position = Vector3.Lerp(transform.position, RealPosition, 0);
            transform.localScale = Vector3.Lerp(transform.localScale, RealScale, 1);
            transform.rotation = Quaternion.Lerp(transform.rotation, RealRotation, 0);
        }


    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo message)
    {
        if (stream.IsWriting)
        {

            //stream.SendNext(rb.position);
            //stream.SendNext(rb.velocity);

            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
            stream.SendNext(transform.localScale);

            stream.SendNext(anim.GetBool("Grounded"));
            stream.SendNext(anim.GetBool("Move"));
            stream.SendNext(anim.GetFloat("Speed"));

        }
        else
        {
            //RealPosition1 = (Vector3)stream.ReceiveNext();
            //RealVelocity1 = (Vector3)stream.ReceiveNext();

            RealPosition = (Vector3)stream.ReceiveNext();
            RealRotation = (Quaternion)stream.ReceiveNext();
            RealScale = (Vector3)stream.ReceiveNext();

            anim.SetBool("Grounded", (bool)stream.ReceiveNext());
            anim.SetBool("Move", (bool)stream.ReceiveNext());
            anim.SetFloat("Speed", (float)stream.ReceiveNext());


        }
    }


}
