using UnityEngine;
using System.Collections;
using DentedPixel;
using System.Collections.Generic;

// This project demonstrates how you can use the spline behaviour for a multi-track game (like an endless runner style)

public class PathSplineTrack : MonoBehaviour {

	public GameObject car;
	public GameObject carInternal;
	public GameObject trackTrailRenderers;
	public float speed;
	public Transform[] trackOnePoints;

	private LTSpline track;
	private int trackIter = 1;
	private float trackPosition; // ratio 0,1 of the avatars position on the track

	void Start () {
		Vector3[] points=new Vector3[trackOnePoints.Length];
        for (int i = 0; i < trackOnePoints.Length; i++)
        {
			points[i] = trackOnePoints[i].position;
        }
		//track = new LTSpline( new Vector3[] {trackOnePoints[0].position, trackOnePoints[1].position, trackOnePoints[2].position, trackOnePoints[3].position, trackOnePoints[4].position, trackOnePoints[5].position, trackOnePoints[6].position, trackOnePoints[7].position, trackOnePoints[8].position, trackOnePoints[9].position, trackOnePoints[10].position, trackOnePoints[11].position, trackOnePoints[12].position, trackOnePoints[13].position, trackOnePoints[14].position, trackOnePoints[15].position, trackOnePoints[16].position, trackOnePoints[17].position, trackOnePoints[18].position, trackOnePoints[19].position, trackOnePoints[20].position, trackOnePoints[21].position, trackOnePoints[22].position, trackOnePoints[23].position, trackOnePoints[24].position, trackOnePoints[25].position, trackOnePoints[26].position, trackOnePoints[27].position, trackOnePoints[28].position, trackOnePoints[29].position, trackOnePoints[31].position, trackOnePoints[32].position, trackOnePoints[33].position, trackOnePoints[34].position, trackOnePoints[35].position, trackOnePoints[35].position, trackOnePoints[36].position, trackOnePoints[37].position, trackOnePoints[38].position, trackOnePoints[39].position, trackOnePoints[40].position, trackOnePoints[41].position, trackOnePoints[42].position, trackOnePoints[43].position, trackOnePoints[44].position, trackOnePoints[45].position, trackOnePoints[46].position, trackOnePoints[47].position, trackOnePoints[48].position, trackOnePoints[49].position, trackOnePoints[50].position, trackOnePoints[51].position, trackOnePoints[52].position, trackOnePoints[53].position, trackOnePoints[54].position, trackOnePoints[55].position, trackOnePoints[56].position, trackOnePoints[57].position, trackOnePoints[58].position, trackOnePoints[59].position, trackOnePoints[60].position, trackOnePoints[61].position, trackOnePoints[62].position, trackOnePoints[63].position, trackOnePoints[64].position, trackOnePoints[65].position, trackOnePoints[66].position, trackOnePoints[67].position, trackOnePoints[68].position, trackOnePoints[69].position} );
		//track = new LTSpline(new Vector3[] { trackOnePoints[0].position, trackOnePoints[1].position, trackOnePoints[2].position, trackOnePoints[3].position, trackOnePoints[4].position, trackOnePoints[5].position, trackOnePoints[6].position });
		track = new LTSpline(points);
	}
	
	void Update () {
		// Switch tracks on keyboard input
		//float turn = Input.GetAxis("Horizontal");
		//if(Input.anyKeyDown){
		//	if(turn<0f && trackIter>0){
		//		//trackIter--;
		//		playSwish();
		//	}else if(turn>0f && trackIter < 2){ // We have three track "rails" so stopping it from going above 3
		//		//trackIter++;
		//		playSwish();
		//	}
		//	// Move the internal local x of the car to simulate changing tracks
		//	LeanTween.moveLocalX(carInternal, (trackIter-1)*6f, 0.3f).setEase(LeanTweenType.easeOutBack);

		//}

		// Update avatar's position on correct track
		track.place( car.transform, trackPosition );

		trackPosition += Time.deltaTime * speed;// * Input.GetAxis("Vertical"); // Uncomment to have the forward and backwards controlled by the directional arrows

		if (trackPosition < 0f) // We need to keep the ratio between 0-1 so after one we will loop back to the beginning of the track
			trackPosition = 1f;
		else if(trackPosition>1f)
			trackPosition = 0f; 
	}

	// Use this for visualizing what the track looks like in the editor (for a full suite of spline tools check out the LeanTween Editor)
	void OnDrawGizmos(){
		LTSpline.drawGizmo( trackOnePoints, Color.red);
	}
}
