using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Experiment : MonoBehaviour 
{
	public List<Task> allTasks;

	public Session session;

	public int curTask = -1;

	void Awake()
	{
		allTasks = new List<Task>();
	}

	public void StartTask()
	{
		//Create Task
		print("CreateTask");
		curTask++;
        Task temp = gameObject.AddComponent<Task>();
        temp.experiment = this;
		allTasks.Add(temp);

		//Start Task
		allTasks[curTask].StartTask();

	}

	public void EndTask()
	{


	}



	

}
