using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(GazePointDataComponent))]
public class Session : MonoBehaviour 
{
	public List<Experiment> allExperients;

	public Vector2 focusPos;
	
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
			CreateExperiment();
		}
		if (Input.GetKeyDown(KeyCode.T))
		{
			CreateTask();
		}
	}

	void CreateExperiment()
	{
		print("CreateExperiment");
		curExperiment ++;
		allExperients.Add(gameObject.AddComponent<Experiment>());
		allExperients[curExperiment].session = this;
	}

	void CreateTask()
	{
		allExperients[curExperiment].CreateTask();
	}
}

public enum Mode {
	EyeTracking,
	MouseTracking	
}