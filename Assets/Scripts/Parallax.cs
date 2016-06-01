using UnityEngine;
using System.Collections;
//moving back the plane makes the plane goes backwards
public class Parallax : MonoBehaviour 
{
    public Transform[] backgrounds;  //array of all the back and foregrounds to be parallaxed
    private float[] _parallaxScales; //proportion of the camera's movement to move the backgrounds by
    public float smoothing = 1f;     //how smooth the parallax is going to be

    private Transform _cam;         //reference to main camera transform
    private Vector3 _previousCamPos; //store position of the camera n the previous frame 

    void Awake() //great for references
    {
        _cam = Camera.main.transform;
    }

	void Start () 
	{
	    //previous frame had the current frames camera pos
        _previousCamPos = _cam.position;
        //parallaxScales length is = to backgrounds[]
        _parallaxScales = new float[backgrounds.Length]; 
        //assigning corresponding parallaxScales
        for (int i = 0; i < backgrounds.Length; i++)
        {   //negative the value because closer object have negative z axis. 
            _parallaxScales[i] = backgrounds[i].position.z * -1;
        }
	}
	
	void Update () 
	{
	    //for each background
        for(int i = 0; i < backgrounds.Length; i++)
        {
            //gets parallax value by multiplying the difference by the backgrounds z axis
            float parallax = (_previousCamPos.x - _cam.position.x) * _parallaxScales[i];
            //add the parallax amount to a vector3
            Vector3 backgroundTargetPos = new Vector3 ((backgrounds[i].position.x + parallax),backgrounds[i].position.y, backgrounds[i].position.z);
            //fade between current pos and target pos. Only difference being + parallax on x value
            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
        }
        //set previousCamPos to the camera pos at the end of the frame
        _previousCamPos = _cam.position;
	}
}