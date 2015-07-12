using UnityEngine;
using System.Collections;
using System.IO;

public class Task : MonoBehaviour 
{
	public bool isRunning;

	public float timer = 0f; // timer to record how long the student takes to complete.

	public float[,] heatmap; // holds a float value added each fram based on delta time

	public Experiment experiment; //Knows what experiment it is in.


	private int screenWidth; //Holds screen width. Screen res should never change
	private int screenHeight; //Holds screen height. Screen res should never change
	private int heatmapRange = 100; //Radius of the focus area for each frame
	private float maxValue;//stores the maximum valua of the heatmap to scale at the end.
	private float timeLookingAway;


	private void UpdateHeatmap()
	{
		Vector2 fPos = experiment.session.focusPos;

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
		print("HeatmapToColorArray");
		Color[] outArray = new Color[screenWidth*screenHeight];

		for (int y = 0; y < screenHeight; y++) 
		{
			for (int x = 0; x < screenWidth; x++) 
			{
				outArray[((y * screenWidth) + x)] = FloatToColor(heatmap[x,y] / maxValue);
			}
		}

		return outArray;

	}

	Color FloatToColor(float f)
	{
		return Color.Lerp(Color.blue, Color.red, f);
		//return Color.red;
	}

	void WritePNG(Color[] c, string filepath)
	{
		print("WritePNG");
		Texture2D outTex = new Texture2D(screenWidth, screenHeight);

		outTex.SetPixels(c);

		// Encode texture into PNG
		byte[] bytes = outTex.EncodeToPNG();
		//Object.Destroy(outTex);
		
		// For testing purposes, also write to a file in the project folder
		File.WriteAllBytes(Application.dataPath + filepath, bytes);

	}
	
	// Use this for initialization
	void Start () 
	{
		print("Start");
		screenWidth = Screen.width;
		screenHeight = Screen.height;
		heatmap = new float[screenWidth, screenHeight];
		print(screenWidth);
		print(screenHeight);
		isRunning = false;

		timeLookingAway = 0;
		maxValue = 0;
	}
	
	// Update is called once per frame
	void Update () 
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
		print("Start Task");
		isRunning = true;
	}

	public void EndTask()
	{
		print("End Task");
		isRunning = false;
		WritePNG(HeatmapToColorArray(heatmap), "test.png");
	}
}
