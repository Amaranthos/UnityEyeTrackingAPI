using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//[SerializeField]
[System.Serializable]
public class Experiment
{
	public string name;
	[SerializeField]
	public List<Task> allTasks;

	[System.NonSerialized]
	public Session session;

	public int curTask = -1;

	/// <summary>
	/// Called to initialise the variables
	/// </summary>
	public void Awake()
	{
		allTasks = new List<Task>();
	}

	/// <summary>
	/// Creates a new task and starts recording
	/// </summary>
	/// <param name="taskNumber"></param>
	/// <param name="aa"></param> The Top Left corner of Focus Area
	/// <param name="bb"></param> The Bop Right corner of Focus Area
	public void StartTask(string taskName, Vector2 aa, Vector2 bb)
	{
		//TODO Designer Proof check to see if task is still running
		//Create Task
		curTask++;
		allTasks.Add(new Task());
		allTasks[curTask].experiment = this;
		allTasks[curTask].name = taskName;
		allTasks[curTask].aa = aa;
		allTasks[curTask].bb = bb;

		//Start Task
		allTasks[curTask].StartTask();

	}

	public string ReturnData()
	{
		string tempOut = "";
		tempOut += "Experiment: " + name + "\n";
		tempOut += "Task" + "," + "Time to Complete" + "," + "Correct Focus" + "," + "Heatmap" + "\n";
		for (int i = 0; i < allTasks.Count; i++)
		{
			tempOut += allTasks[i].ReturnData();
		}

		return tempOut;
	}

	public void EndTask(string taskName)
	{
		//TODO Designer Proof check to see if task is still running
		if (allTasks[curTask].name == taskName)
			allTasks[curTask].EndTask();
	}

	public void EndExperiment()
	{


	}

	/// <summary>
	/// Runs the update function on the current task
	/// </summary>
	public void UpdateTask()
	{
		if (curTask >= 0)
			allTasks[curTask].Update();

	}



	

}
