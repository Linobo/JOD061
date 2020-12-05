using UnityEngine;
using Photon.Pun;

public class PlayerController : MonoBehaviour
{
    PhotonView photonView; 
    public Color color;
    public GameObject bala;
    public Transform arma;

    // Start is called before the first frame update
    void Awake()
    {
        photonView = GetComponent<PhotonView>();
        if (photonView.IsMine)
        {
            photonView.RPC(
                    "ChangeColor",
                    RpcTarget.AllBuffered,
                    Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)
                );

            Camera.main.transform.SetParent(transform);
            Camera.main.transform.localPosition = new Vector3(0, 3, -6);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!photonView.IsMine) return;

        transform.Translate(0f, 0f, Input.GetAxis("Vertical") * 10.2f *Time.deltaTime);
        transform.Rotate(0f, Input.GetAxis("Horizontal") * 100f * Time.deltaTime, 0f);

        if (Input.GetKeyDown(KeyCode.X))
        {
            photonView.RPC(
                "ChangeColor", 
                RpcTarget.AllBuffered, 
                Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)
            );
        }

        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            photonView.RPC("Atirar", RpcTarget.AllBuffered);
        }
    }

    public void OnCollisionEnter(Collision other) 
    {
        if (other.gameObject.CompareTag("Bala"))
        {
            if (photonView.IsMine)
            {
                PhotonNetwork.Destroy(gameObject);
            }
        }
    }

    [PunRPC]
    void Atirar() {
        Instantiate(bala, arma.position, transform.rotation);
    }

    [PunRPC]
    void ChangeColor(float red, float green, float blue)
    {
        color = new Color(red, green, blue);
        Material material = GetComponent<Renderer>().material;
        material.color = color;
    }
}
