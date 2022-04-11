using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Daynight : MonoBehaviour
{
	[SerializeField]
	public Light directionalLight; // основной источник света

	void Start()
	{

	}
	private float nextActionTime = 0.0f;
	public float period = 0.1f;
	public float d = 0.01f;
	int reverse = 0;

	void Update()
	{
		if (Time.time > nextActionTime)
		{
			nextActionTime += period;
			if (reverse < 170 && reverse >= 0)
			{ 
				directionalLight.intensity = directionalLight.intensity + d;
				reverse++;
			}
			if (reverse < 340 && reverse >= 170)
			{
				directionalLight.intensity = directionalLight.intensity - d;
				reverse++;
			}
			if (reverse == 340) { reverse = 0; }
		}
	}
}
