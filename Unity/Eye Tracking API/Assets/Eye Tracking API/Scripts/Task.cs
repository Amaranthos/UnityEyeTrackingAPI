using UnityEngine;
using System.Collections;
using System.IO;

[System.Serializable]
public class Task
{

	public string name; //Nmae of the taks
	public Vector2 aa; // Top Left of Focus Area
	public Vector2 bb; // Bottom Right of Focus Area

	public bool isRunning;

	public float timer = 0f; // timer to record how long the student takes to complete.

	public float[,] heatmap; // holds a float value added each fram based on delta time

	[System.NonSerialized]
	public Experiment experiment; //Knows what experiment it is in.


	private int screenWidth; //Holds screen width. Screen res should never change
	private int screenHeight; //Holds screen height. Screen res should never change
	private int heatmapRange = 20; //Radius of the focus area for each frame
	private float maxValue;//stores the maximum valua of the heatmap to scale at the end.
	private float timeLookingAway;


	private void UpdateHeatmap()
	{
		Vector2 fPos = experiment.session.focusPos;

		//Debug.Log(aa);

		//Apply a percentage of delta time to area around focus point.
		for (int i = ((int)fPos.x - heatmapRange); i < ((int)fPos.x + heatmapRange + 1) ; i++) 
		{
			if (i >= 0 && i < screenWidth)// Ensure points are within the screen area
			{
				for (int j = ((int)fPos.y - heatmapRange); j < ((int)fPos.y + heatmapRange + 1) ; j++) 
				{
					if (j >= 0 && j < screenHeight)// Ensure points are within the screen area
					{
						float dist = (new Vector2(i,j) - fPos).magnitude;

						if (dist < heatmapRange) //ensure area is circular.
						{
							heatmap[i,j] += ((heatmapRange - dist) / heatmapRange) * Time.deltaTime;

							maxValue = Mathf.Max(maxValue, heatmap[i,j]);
							//print (maxValue);
						}
					}
				}
			}
		}
	}

	Color[] HeatmapToColorArray(float[,] hm)
	{
		Debug.Log("HeatmapToColorArray");
		Color[] outArray = new Color[screenWidth*screenHeight];

		for (int y = 0; y < screenHeight; y++) 
		{
			for (int x = 0; x < screenWidth; x++) 
			{
				outArray[((y * screenWidth) + x)] = FloatToColor(heatmap[x,y] / maxValue);

				if (y == aa.y && x > aa.x && x < bb.x)
				{
					outArray[((y * screenWidth) + x)] += new Color(1, 1, 1);
				}
				else if (y == bb.y && x > aa.x && x < bb.x)
				{
					outArray[((y * screenWidth) + x)] += new Color(1, 1, 1);
				}

				else if (x == aa.x && y > aa.y && y < bb.y)
				{
					outArray[((y * screenWidth) + x)] += new Color(1, 1, 1);
				}
				else if (x == bb.x && y > aa.y && y < bb.y)
				{
					outArray[((y * screenWidth) + x)] += new Color(1, 1, 1);
				}

			}
		}

		return outArray;

	}

	Color FloatToColor(float f)
	{
		float ff = f * 5;

		if (ff < 1)
			return Color.Lerp(Color.black, Color.blue, ff);
		else if (ff < 2)
			return Color.Lerp(Color.blue, Color.magenta, ff - 1);
		else if (ff < 3)
			return Color.Lerp(Color.magenta, Color.red, ff - 2);
		else if (ff < 4)
			return Color.Lerp(Color.red, Color.yellow, ff - 3);
		else if (ff < 5)
			return Color.Lerp(Color.yellow, Color.green, ff - 4);
		else
			return Color.green;

		//return Color.red;
	}

	void WritePNG(Color[] c, string filepath)
	{
		Debug.Log("WritePNG");
		Texture2D outTex = new Texture2D(screenWidth, screenHeight);

		outTex.SetPixels(c);

		// Encode texture into PNG
		byte[] bytes = outTex.EncodeToPNG();
		//Object.Destroy(outTex);
		
		// For testing purposes, also write to a file in the project folder
		File.WriteAllBytes(Application.dataPath + "/" + filepath, bytes);

	}
	
	// Use this for initialization
	void Start () 
	{
		Debug.Log("Start");
		screenWidth = Screen.width;
		screenHeight = Screen.height;
		heatmap = new float[screenWidth, screenHeight];
		Debug.Log(screenWidth);
		Debug.Log(screenHeight);
		isRunning = false;

		timeLookingAway = 0;
		maxValue = 0;
	}
	
	// Update is called once per frame
	public void Update () 
	{
		if (isRunning)
		{
			//Get Data and create heatmap
			timer += Time.deltaTime;
			UpdateHeatmap();
		}
	}


	public void StartTask()
	{
		Start();
		Debug.Log("Start Task");
		isRunning = true;
	}

	public void EndTask()
	{
		Debug.Log("End Task");
		isRunning = false;
		WritePNG(HeatmapToColorArray(heatmap), "test.png");
		
	}

	public int Width {
		get {return screenWidth;}
	}

	public int Height {
		get {return screenWidth;}
	}
}
