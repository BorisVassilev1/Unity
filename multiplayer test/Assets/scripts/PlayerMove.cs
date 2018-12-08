using UnityEngine;
using UnityEngine.Networking;

public class PlayerMove : NetworkBehaviour
{
    public GameObject bulletPrefab;

    public override void OnStartLocalPlayer()
    {
        GetComponent<MeshRenderer>().material.color = Color.red;

        this.transform.GetChild(0).gameObject.GetComponent<Camera>().enabled = true;
    }

    [Command]
    void CmdFire()
    {
        // This [Command] code is run on the server!

        // create the bullet object locally
        var bullet = (GameObject)Instantiate(
             bulletPrefab,
             transform.position + transform.forward,
             Quaternion.identity);

        bullet.GetComponent<Rigidbody>().velocity = this.transform.forward * 10;

        // spawn the bullet on the clients
        NetworkServer.Spawn(bullet);

        // when the bullet is destroyed on the server it will automaticaly be destroyed on clients
        Destroy(bullet, 2.0f);
    }

    void Update()
    {
        if (!isLocalPlayer)
            return;

        var rotY = Input.GetAxis("Horizontal") * 1.0f;
        var forward = Input.GetAxis("Vertical") * 0.1f;

        transform.Rotate(0, rotY, 0);
        Vector3 tr = Vector3.forward;
        Vector3 pos = this.transform.position;
        //this.GetComponent<Rigidbody>().MovePosition(new Vector3(pos.x + tr.x * forward,pos.y + tr.y * forward,pos.z + tr.z * forward));
        //this.GetComponent<Rigidbody>().velocity.Set(tr.x * forward * 10, tr.y * forward * 10, tr.z * forward  *10);
        this.transform.Translate(new Vector3(tr.x * forward, tr.y * forward, tr.z * forward));

        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Command function is called from the client, but invoked on the server
            CmdFire();
        }
    }
}