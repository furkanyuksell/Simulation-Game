using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public class TestMove : NetworkBehaviour
{
    float moveSpeed = 2f;
    Rigidbody2D rb;
    public TextMesh textMesh;    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        PlayerData playerData = NetworkConnection.Instance.GetPlayerDataFromClientId(OwnerClientId);
        textMesh.text = playerData.playerName.ToString();
    }
    // Update is called once per frame
    void Update()
    {
        if (!IsOwner)
        {
            return;
        }
        
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Vector2 inputDir = input.normalized;
        rb.velocity = inputDir * moveSpeed;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SendMessageToClientRpc();
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            SendMessageToServerRpc();
        }
    }

    [ClientRpc]
    private void SendMessageToClientRpc()
    {
        TestManager.Instance.testText.text = "Bir mesaj gonderildi / " + OwnerClientId;

    }

    [ServerRpc]
    private void SendMessageToServerRpc()
    {
        TestManager.Instance.testText.text = "Bir mesaj gonderildi / " + OwnerClientId;
    }
}
