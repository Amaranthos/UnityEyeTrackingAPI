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


		if (Input.GetKeyDown(KeyCode.Space))
		{
			allExperients[curExperiment].allTasks[allExperients[curExperiment].curTask].StartTask();
		}
		if (Input.GetKeyDown(KeyCode.LeftShift))
		{
			allExperients[curExperiment].allTasks[allExperients[curExperiment].curTask].EndTask();
		}
		if (Input.GetKeyDown(KeyCode.E))
		{
			StartExperiment();
		}
		if (Input.GetKeyDown(KeyCode.T))
		{
			StartTask();
		}
	}

	public Session(string mStudentNumber, string mStudentName, string mTeacherName, string mOutlocation)
	{
		studentNo = mStudentNumber;
		studentName = mStudentName;
		teacherName = mTeacherName;
		fileOut = mOutlocation;
	}


	void StartExperiment()
	{
		curExperiment ++;
		print("CreateExperiment: " + curExperiment.ToString());
        Experiment temp = gameObject.AddComponent<Experiment>();
        temp.session = this;
        allExperients.Add(temp);
	}

	void StartTask()
	{
		allExperients[curExperiment].StartTask();
	}

	void EndExperiment()
	{


	}

	void EndTask()
	{


	}
}

public enum Mode {
	EyeTracking,
	MouseTracking	
}