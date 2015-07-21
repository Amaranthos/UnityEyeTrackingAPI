using UnityEngine;
using System.Collections;

public class Example : MonoBehaviour {

	public BoxCollider2D focusArea;

	public Session session;

	public Mode inputMode;

	// Use this for initialization
	void Start () 
	{
		session = gameObject.AddComponent<Session>();
			
		session.StartSession("N20000", "Derp Smith", "Mrs Doubtfire", "Student Sessions/");

		session.SetMode(inputMode);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.Keypad1))
		{
			session.StartExperiment("Exp001");
		}
		if (Input.GetKeyDown(KeyCode.Keypad2))
		{

			//session.StartTask("Tsk001", bb, aa);
			session.StartTask("Tsk001", focusArea);
		}
		if (Input.GetKeyDown(KeyCode.Keypad3))
		{
			session.EndTask("Tsk001");
		}
		if (Input.GetKeyDown(KeyCode.Keypad4))
		{
			session.EndExperiment("Exp001");
		}
		if (Input.GetKeyDown(KeyCode.Keypad5))
		{
			session.EndSession();
		}
	}
}
