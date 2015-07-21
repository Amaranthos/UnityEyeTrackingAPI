using UnityEngine;
using System.Collections;

public class Example : MonoBehaviour {

	public GameObject focusArea;

	public Session session;

	// Use this for initialization
	void Start () 
	{
		session = gameObject.AddComponent<Session>();
			
		session.StartSession("N20000", "Derp Smith", "Mrs Doubtfire", "Student Sessions/");
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.Keypad1))
		{
			session.StartExperiment("Exp001");
		}
		if (Input.GetKeyDown(KeyCode.Keypad1))
		{
			session.StartTask();
		}
		if (Input.GetKeyDown(KeyCode.Keypad1))
		{
			session.EndTask();
		}
		if (Input.GetKeyDown(KeyCode.Keypad1))
		{
			session.EndExperiment();
		}
		if (Input.GetKeyDown(KeyCode.Keypad1))
		{
			session.EndExperiment();
		}
	}
}
