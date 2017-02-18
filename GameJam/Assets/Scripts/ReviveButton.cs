using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ReviveButton : MonoBehaviour,IPointerClickHandler {
	[SerializeField]
	Image image;
	[SerializeField]
	Text text;
	void Awake()
	{
		image.enabled = text.enabled = false;
		BearScript.PlayerFallDown += () => image.enabled = text.enabled = true;
	}

	public void OnPointerClick (PointerEventData eventData)
	{
		image.enabled = text.enabled = false;
		BearScript.RevivePlayer ();
	}




}
