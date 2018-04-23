using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast : MonoBehaviour
{

	private Player player;
	private bool canShowHoverText = false;
	private bool lookingAtWitch = false;
	public float maxPickUpDistance = 10.0f;
	public float maxWitchDistance = 50.0f;
	private float timeLookingAtWitch = 0.0f;
	public float maxTimeToLook = 4f;
    public LayerMask whatToHit;
    private WizardFollow wizard;
    private VoiceHandler voice;

	void Awake()
	{
		player = GetComponent<Player>();
        wizard = GameObject.FindGameObjectWithTag("Overlord").GetComponent<WizardFollow>();
        voice = GameObject.FindGameObjectWithTag("Voice").GetComponent<VoiceHandler>();
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit hit;
		Vector3 forward = transform.TransformDirection(Vector3.forward);
		
		//checks if we see an object
		if (Physics.Raycast(transform.position, forward, out hit, whatToHit))
		{

			if (hit.distance <= maxWitchDistance && hit.collider.gameObject.CompareTag("Witch") && !hit.collider.isTrigger)
			{
				lookingAtWitch = true;
			}
			else
			{
				lookingAtWitch = false;
			}
			
			//checks if we're close enough to pick it up
			if (hit.distance <= maxPickUpDistance && hit.collider.gameObject.CompareTag("Pick Up") && !hit.collider.isTrigger)
			{

				canShowHoverText = true;
				if (Input.GetMouseButtonDown(0))
				{
					StartCoroutine(voice.PlaySoundBite(hit.collider.gameObject));
                    StorytellingEngine.ItemCollected();
					hit.collider.gameObject.SetActive(false);		// can probably do other stuff later
				}
			}
			else
			{
				canShowHoverText = false;
			}
		}

        //adds time to how long you can look at the witch for
        //resets if you turn away
        if (lookingAtWitch && !wizard.HasDoneDamage())
		{
			timeLookingAtWitch += Time.deltaTime;
			if (timeLookingAtWitch > maxTimeToLook)		//look too long it hurts you (In text how frightening)
			{
                player.Hit();
				timeLookingAtWitch = 0.0f;
                wizard.LookedAt();
			}
		}
		else
		{
			timeLookingAtWitch = 0.0f;
		}
	}
	
	private void OnGUI()
	{
		if (canShowHoverText)
		{
            GUI.skin.box.fontSize = 30;
            GUI.Box(new Rect(Screen.width / 2 - 100, 5 * Screen.height / 6, 300, 40), "Click to pick up");
		}
	}

    public bool LookingAtWitch()
    {
        return lookingAtWitch;
    }

	
}
