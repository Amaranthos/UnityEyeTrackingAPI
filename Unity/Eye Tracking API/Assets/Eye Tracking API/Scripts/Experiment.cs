using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//[SerializeField]
public class Experiment 
{
	public List<Task> allTasks;

	public Session session;

	public int curTask = -1;

	public string name;

	void Awake()
	{
		allTasks = new List<Task>();
	}

	public void StartTask()
	{
		//Create Task
		curTask++;
		allTasks.Add(new Task());
		allTasks[curTask].experiment = this;

		//Start Task
		allTasks[curTask].StartTask();

	}

	public void EndTask()
	{


	}

	public void EndExperiment()
	{


	}



	

}
