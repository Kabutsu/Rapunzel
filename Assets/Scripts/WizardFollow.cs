using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardFollow : MonoBehaviour {

    private bool lookedAt = false;

    public GameObject player;
    private Raycast playerScript;

    public GameObject wizard;
    private Transform wizardTransform;

    private const float MINIMUM_Z = 155f;
    private const float MAXIMUM_Z = 330f;
    private const float MINIMUM_X = 162f;
    private const float MAXIMUM_X = 360f;

    public float moveSpeed = 3f;
    public float rotationSpeed = 3f;
    private int whatToHit;

    private Transform myTransform;

	// Use this for initialization
	void Start () {
        myTransform = gameObject.transform;
        playerScript = player.GetComponent<Raycast>();
        wizardTransform = wizard.transform;
        whatToHit = 1 << 10;
	}
	
	// Update is called once per frame
	void Update () {
        if (lookedAt && !playerScript.LookingAtWitch())
        {
            Teleport();
        } else if (!playerScript.LookingAtWitch() && WithinBounds())
        {
            AdvanceTowardsPlayer(); 
        }
	}

    private void Teleport()
    {
        lookedAt = false;
        float newX = Random.Range(MINIMUM_X, MAXIMUM_Z);
        float newZ = Random.Range(MINIMUM_Z, MAXIMUM_Z);

        myTransform.position = new Vector3(newX, myTransform.position.y, newZ);

        RaycastHit hit;
        Vector3 down = myTransform.TransformDirection(Vector3.down);
        if (Physics.Raycast(myTransform.position, down, out hit, 100f, whatToHit))
        {
            wizardTransform.position = new Vector3(myTransform.position.x, hit.point.y + 2.5f, myTransform.position.z);
        }
    }

    private void AdvanceTowardsPlayer()
    {
        myTransform.position += myTransform.forward * moveSpeed * Time.deltaTime;
        
        myTransform.rotation = Quaternion.Slerp(myTransform.rotation,
        Quaternion.LookRotation(player.transform.position - myTransform.position), rotationSpeed * Time.deltaTime);
        myTransform.eulerAngles = new Vector3(0f, myTransform.rotation.eulerAngles.y - 5f, 0f);
        wizardTransform.rotation = myTransform.rotation;

        RaycastHit hit;
        Vector3 down = myTransform.TransformDirection(Vector3.down);
        if (Physics.Raycast(myTransform.position, down, out hit, 100f, whatToHit)) {
            wizardTransform.position = new Vector3(myTransform.position.x, hit.point.y + 2.5f, myTransform.position.z);
        }
    }

    private bool WithinBounds()
    {
        if (myTransform.position.x < MINIMUM_X)
        {
            myTransform.position = new Vector3(MINIMUM_X, myTransform.position.y, myTransform.position.z);
            return false;
        }

        if (myTransform.position.x > MAXIMUM_X)
        {
            myTransform.position = new Vector3(MAXIMUM_X, myTransform.position.y, myTransform.position.z);
            return false;
        }

        if (myTransform.position.z > MAXIMUM_Z)
        {
            myTransform.position = new Vector3(myTransform.position.x, myTransform.position.y, MAXIMUM_Z);
            return false;
        }

        if (myTransform.position.z < MINIMUM_Z)
        {
            myTransform.position = new Vector3(myTransform.position.x, myTransform.position.y, MINIMUM_Z);
            return false;
        }

        return true;
    }

    public void LookedAt()
    {
        lookedAt = true;
    }

    public bool HasDoneDamage()
    {
        return lookedAt;
    }
}
