using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ReviveButton : MonoBehaviour,IPointerClickHandler {
	#region IPointerClickHandler implementation

	public void OnPointerClick (PointerEventData eventData)
	{
		BearScript.RevivePlayer ();
	}

	#endregion




}
