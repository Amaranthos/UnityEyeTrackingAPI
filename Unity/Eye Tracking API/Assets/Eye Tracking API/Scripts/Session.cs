using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Session : MonoBehaviour 
{
	public List<Experiment> allExperients;

	public Vector2 focusPos;

	int curExperiment = -1;




	// Use this for initialization
	void Awake () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
		focusPos = Input.mousePosition;

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
























