using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Sincronizar : MonoBehaviourPun, IPunObservable
{

    public Vector3 RealPosition;
    public Quaternion RealRotation;
    public Vector3 RealScale;

    public Animator anim;

    void FixedUpdate()
    {
        Update();
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine)
        {
            transform.position = Vector3.Lerp(transform.position, RealPosition, 0);
            //transform.localScale = Vector3.Lerp(transform.localScale, RealScale, 0.04f);
            transform.rotation = Quaternion.Lerp(transform.rotation, RealRotation, 0);
        }


    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo message)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
           // stream.SendNext(transform.localScale);

            stream.SendNext(anim.GetBool("Grounded"));
            stream.SendNext(anim.GetBool("Move"));
            stream.SendNext(anim.GetFloat("Speed"));
        }
        else
        {
            RealPosition = (Vector3)stream.ReceiveNext();
            RealRotation = (Quaternion)stream.ReceiveNext();
          //  RealScale = (Vector3)stream.ReceiveNext();

            anim.SetBool("Grounded", (bool)stream.ReceiveNext());
            anim.SetBool("Move", (bool)stream.ReceiveNext());
            anim.SetFloat("Speed", (float)stream.ReceiveNext());
        }
    }


}
