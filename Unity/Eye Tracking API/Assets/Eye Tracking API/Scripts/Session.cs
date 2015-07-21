using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(GazePointDataComponent))]
public class Session : MonoBehaviour 
{
	private string fileOut;
	private string studentNo;
	private string studentName;
	private string teacherName;


	public List<Experiment> allExperients;

	public Vector2 focusPos;

	public string file;
	
	public Mode mode;

	int curExperiment = -1;

	GazePointDataComponent gazeData;

	// Use this for initialization
	void Awake () 
	{
		gazeData = GetComponent<GazePointDataComponent> ();
		allExperients = new List<Experiment>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		switch(mode) {
		case Mode.EyeTracking:
			if (gazeData.LastGazePoint.IsValid){
				focusPos = gazeData.LastGazePoint.GUI;
				Debug.Log ("X: " + focusPos.x + " Y: " + focusPos.y);
			}
			
			break;

		case Mode.MouseTracking:
			focusPos = Input.mousePosition;
			break;
		}
	}

	public void StartSession(string mStudentNumber, string mStudentName, string mTeacherName, string mOutlocation)
	{
		studentNo = mStudentNumber;
		studentName = mStudentName;
		teacherName = mTeacherName;
		fileOut = mOutlocation;
	}


	public void StartExperiment(string name)
	{
		allExperients = new List<Experiment>();
		curExperiment ++;
		print("CreateExperiment: " + curExperiment.ToString());
		Experiment temp = new Experiment();
		temp.session = this;
		temp.name = name;
		allExperients.Add(temp);
	}
	public void EndExperiment()
	{
		allExperients[curExperiment].EndExperiment();

	}

	public void StartTask()
	{
		allExperients[curExperiment].StartTask();
	}
	public void EndTask()
	{
		allExperients[curExperiment].EndTask();

	}

	public void EndSessions()
	{
		//TODO
		//Gather all data
		//Save to CVS

	}
}

public enum Mode {
	EyeTracking,
	MouseTracking	
}