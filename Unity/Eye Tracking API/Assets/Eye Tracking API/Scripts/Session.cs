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


	public Vector2 focusPos;

	public string file;
	
	public Mode mode;

	public int curExperiment = -1;

	GazePointDataComponent gazeData;

	[SerializeField]
	public List<Experiment> allExperients;




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

		UpdateTask();
	}

	public void StartSession(string mStudentNumber, string mStudentName, string mTeacherName, string mOutlocation)
	{
		studentNo = mStudentNumber;
		studentName = mStudentName;
		teacherName = mTeacherName;
		fileOut = mOutlocation;
	}
	/// <summary>
	/// Runs the update function on the current task
	/// </summary>
	void UpdateTask()
	{
		if (curExperiment >= 0)
			allExperients[curExperiment].UpdateTask();

	}

	/// <summary>
	///	Creates a new experiment ready for Start Task;
	/// </summary>
	/// <param name="experimentName"></param>
	public void StartExperiment(string experimentName)
	{
		//Creates
		curExperiment ++;
		print("CreateExperiment: " + curExperiment.ToString());
		Experiment temp = new Experiment();
		temp.session = this;
		temp.name = experimentName;
		temp.Awake();
		allExperients.Add(temp);
	}

	/// <summary>
	/// Tells the current experiment to Stop
	/// Must be called before another Experiment is Started
	/// </summary>
	/// <param name="experimentName"></param> 
	public void EndExperiment(string experimentName)
	{
		if (allExperients[curExperiment].name == experimentName)
			allExperients[curExperiment].EndExperiment();
	}

	/// <summary>
	/// Creates a new task and starts recording
	/// </summary>
	/// <param name="taskName"></param>
	/// <param name="aa"></param> The Bottom Left corner of Focus Area
	/// <param name="bb"></param> The Top Right corner of Focus Area
	public void StartTask(string taskName, Vector2 aa, Vector2 bb)
	{
		allExperients[curExperiment].StartTask(taskName, aa, bb);
	}

	public void StartTask(string taskName, BoxCollider2D focusArea)
	{
		Vector2 aa = Camera.main.WorldToScreenPoint(focusArea.GetComponent<BoxCollider2D>().bounds.min);
		Vector2 bb = Camera.main.WorldToScreenPoint(focusArea.GetComponent<BoxCollider2D>().bounds.max);

		Debug.Log("AA" + aa.x);
		Debug.Log("BB" + bb.x);

		allExperients[curExperiment].StartTask(taskName, new Vector2((int)aa.x, (int)aa.y), new Vector2((int)bb.x, (int)bb.y));
	}




	//Tells the current Task to Stop
	//Must be called before another Task is Started
	/// <summary>
	/// Tells the current Task to Stop
	/// -Creates heatmap
	/// -Collates Data
	/// </summary>
	/// <param name="taskName"></param>
	public void EndTask(string taskName)
	{
		allExperients[curExperiment].EndTask(taskName);
	}

	public void EndSession()
	{
		//TODO
		//Gather all data
		//Save to CVS

	}

	public void SetMode(Mode mm)
	{
		mode = mm;
	}
}

public enum Mode {
	MouseTracking,	
	EyeTracking
}