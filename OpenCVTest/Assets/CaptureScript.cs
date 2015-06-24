using UnityEngine;
using System.Collections;
using OpenCvSharp;

public class CaptureScript : MonoBehaviour {
	void Start() {
		CvColor[] colors = new CvColor[] {
			 new CvColor(0,0,255), 
			 new CvColor(0,128,255), 
			 new CvColor(0,255,255), 
			 new CvColor(0,255,0), 
             new CvColor(255,128,0),
             new CvColor(255,255,0),
             new CvColor(255,0,0),
             new CvColor(255,0,255),
		};

		const double scale = 1.25;
		const double scaleFactor = 2.5;
		const int minNeighbours = 2;

		CvCapture cap = CvCapture.FromCamera(2);
		CvWindow window = new CvWindow("Eye Tracker");

		while (CvWindow.WaitKey(10) < 0) {
			IplImage img = cap.QueryFrame();
			IplImage smallImg = new IplImage(new CvSize(Cv.Round(img.Width / scale), Cv.Round(img.Height / scale)), BitDepth.U8, 1);

			IplImage grey = new IplImage(img.Size, BitDepth.U8, 1);

			Cv.CvtColor(img, grey, ColorConversion.Bgr555ToGray);
			Cv.Resize(grey, smallImg, Interpolation.Linear);
			Cv.EqualizeHist(smallImg, smallImg);

			CvHaarClassifierCascade cascade = CvHaarClassifierCascade.FromFile("haarcascade_eye.xml");

			CvMemStorage storage = new CvMemStorage();

			storage.Clear();

			CvSeq<CvAvgComp> eyes = Cv.HaarDetectObjects(smallImg, cascade, storage, scaleFactor, minNeighbours, 0, new CvSize(30, 30));

			for(int  i = 0; i < eyes.Total; i++) {
				CvRect r = eyes[i].Value.Rect;
				CvPoint centre = new CvPoint {
					X = Cv.Round((r.X + r.Width) * 0.25 * scale),
					Y = Cv.Round((r.Y + r.Height) * 0.25 * scale)
				};
				int radius = Cv.Round((r.Width + r.Height) * 0.25 * scale);
				img.Circle(centre, radius, colors[i % 8], 3, LineType.AntiAlias, 0);
			}
			window.Image = img;
		}
	}
}