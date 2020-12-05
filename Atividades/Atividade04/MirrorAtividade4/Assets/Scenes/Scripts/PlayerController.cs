using UnityEngine;
using Mirror;

public class PlayerController : NetworkBehaviour
{
    [SyncVar]

    public Color color;

    public Material material;

    public GameObject bala;

    public Transform arma;

    float moveSeep = 0.1f;

    float moveRotation = 1f;
    
    override public void OnStartServer()
    {
        base.OnStartServer();
        color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
    }

    override public void OnStartClient()
    {

    }

    void Start()
    { 
        material = GetComponent<Renderer>().material;
        material.color = color;
    }

    // Update is called once per frame
    void Update()
    {
        //if (isServer) Debug.Log("Somente no Servidor");
        //if (isClient) Debug.Log("Somente no Cliente");

        if (!isLocalPlayer) return;

        if (Input.GetKeyDown(KeyCode.X))
        {
            CmdChangeColor(color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)));           
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject tiro = Instantiate(bala, arma.position, transform.rotation);
            NetworkServer.Spawn(tiro);
        }

        transform.Translate(0, 0, Input.GetAxis("Vertical") * moveSeep);
        transform.Rotate(0, Input.GetAxis("Horizontal") * moveRotation, 0);
    }

    [Command]

    public void CmdChangeColor(Color newColor)
    {
        color = newColor;
        RpcChangeColor(newColor);
    }

    
    [ClientRpc]

    public void RpcChangeColor(Color newColor)
    {
        material.color = newColor;
    }
}
