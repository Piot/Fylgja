using UnityEngine;
using System.Collections;

public class PlayerIndoorsTrigger : MonoBehaviour {

	private CharacterWalking characterWalking;
	[HideInInspector]
	public LogicCameraInfoApplicator targetCamera;
	public float targetWalkSpeed = 1;
	private float originalWalkSpeed;
	public float indoorsFOV = 60;
	public float outdoorsFOV = 40;
	public iTween.EaseType easeTypeFOV = iTween.EaseType.linear;
	public float tweenTime = 0.5f;
	
	void OnTriggerEnter(Collider collider)
	{	
		if(characterWalking == null)
		{
			AssignCharacterWalking(collider.gameObject);
		}
		
		iTween.ValueTo(gameObject, iTween.Hash("from", targetCamera.currentFov, "to", indoorsFOV, "onupdate", "FadeFieldOfView", "onupdatetarget", gameObject, "time", tweenTime, "easetype", easeTypeFOV));
		
		originalWalkSpeed = characterWalking.maxWalkSpeed;
		characterWalking.maxWalkSpeed = targetWalkSpeed;
	}
	
	void OnTriggerExit()
	{
		iTween.ValueTo(gameObject, iTween.Hash("from", targetCamera.currentFov, "to", outdoorsFOV, "onupdate", "FadeFieldOfView", "onupdatetarget", gameObject, "time", tweenTime, "easetype", easeTypeFOV));
		
		characterWalking.maxWalkSpeed = originalWalkSpeed;
	}
	
	void AssignCharacterWalking(GameObject go)
	{
		characterWalking = go.GetComponent<CharacterWalking>();
	}
	
	void FadeFieldOfView(float fieldOfView)
	{
		targetCamera.currentFov = fieldOfView;
	}
}