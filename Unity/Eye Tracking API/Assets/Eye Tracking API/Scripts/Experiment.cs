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

	public void CreateTask()
	{
		print("CreateTask");
		curTask++;
		allTasks.Add(gameObject.AddComponent<Task>());
		allTasks[curTask].experiment = this;
	}



	

}
