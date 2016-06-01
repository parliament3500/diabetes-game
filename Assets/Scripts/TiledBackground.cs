using UnityEngine;
using System.Collections;

public class TiledBackground : MonoBehaviour 
{
    //figure out how many tiles width based on current resolution
    //essentially make the background fit to any resolution
    //powers of 2s 16/32/64/128 etc
    public int textureSize = 32;
    public bool scaleHorizontally = true;
    public bool scaleVertically = true;
	void Start () 
	{
        //ceil = Returns the smallest integer greater to or equal to f.
        var newWidth = !scaleHorizontally ? 1 : Mathf.Ceil(Screen.width/(textureSize * PixelPerfectCamera.scale));
        var newHeight = !scaleVertically ? 1 : Mathf.Ceil(Screen.height/(textureSize * PixelPerfectCamera.scale));

        //scale requires vector3
        transform.localScale = new Vector3(newWidth * textureSize, newHeight * textureSize,1);

        GetComponent<Renderer>().material.mainTextureScale = new Vector3(newWidth,newHeight,1);
	}
}
