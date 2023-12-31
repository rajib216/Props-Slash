using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody targetRb; // Declare a Rigidbody variable to access individual targets
    private GameManager gameManager; // Communicate with the GameManager.cs script

    private float minForce = 14;
    private float maxForce = 18;
    private float xMaxTorque = 10;
    private float yMaxTorque = 20;
    private float xPosRange = 7;
    private float xNegRange = -7;
    private float yPosition = -1;

    public int pointValue; // Custom pointvalue for each of the targets
    public ParticleSystem explosionparticle; // Custom effect for each of the targets

    // Start is called before the first frame update
    void Start()
    {
        targetRb = GetComponent<Rigidbody>(); // Get the Rigidbody component through the declared variable
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        // Spawn Rigidbody from random poistion, with random force and torque
        targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        targetRb.AddTorque(xzRandomTorque(), yRandomTorque(), xzRandomTorque(), ForceMode.Impulse);
        transform.position = RandomPosition();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
    private void OnMouseDown()
    {
        if(gameManager.isGameActive)
        {
            Destroy(gameObject);
            gameManager.UpdateScore(pointValue);
            Instantiate(explosionparticle, transform.position, explosionparticle.transform.rotation);
        }
        
    } */

    private void OnTriggerEnter(Collider other)
    {
        if(!(gameObject.CompareTag("Skull") || gameObject.CompareTag("Bomb")))
        {
            gameManager.UpdateLives(-1);
        }
        Destroy(gameObject);
    }

    public void DestroyTarget()
    {
        if (gameManager.isGameActive)
        {
            Destroy(gameObject);
            gameManager.UpdateScore(pointValue);
            Instantiate(explosionparticle, transform.position, explosionparticle.transform.rotation);
        }
    }

    // Helper Functions
    Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minForce, maxForce);
    }

    float xzRandomTorque()
    {
        return Random.Range(-xMaxTorque, xMaxTorque);
    }

    float yRandomTorque()
    {
        return Random.Range(-yMaxTorque, yMaxTorque);
    }

    Vector3 RandomPosition()
    {
        return new Vector3(Random.Range(xNegRange, xPosRange), yPosition);
    }
}
