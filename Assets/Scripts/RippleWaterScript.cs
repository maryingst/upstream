using UnityEngine;
using System.Collections.Generic;
using System;

public class RippleWaterScript : MonoBehaviour {
	
	public Material material;
	
	//Mesh Data
	public int width;
	public int height;
	private Vector3 size;
	private Vector3[] vertices;
	private Vector2[] uv;
	private Vector4[] tangents;
	private int[] triangles;
	private Vector2 uvScale;
	private Vector3 sizeScale;
	
	//Ripple Data
	private List<Vector3> RippleOrigins;
	private List<DateTime> RippleBirth;
	
	protected float scrollSpeed;
	ObjectManagerScript getSpeedScript;

	
    void Start() {
		
		getSpeedScript = Camera.mainCamera.GetComponent("ObjectManagerScript") as ObjectManagerScript;	
		if(getSpeedScript)
			scrollSpeed = getSpeedScript.GetGameSpeed()*0.3f;
		else
			scrollSpeed = 0.3f;

		
		RippleBirth = new List<DateTime>();
		RippleOrigins = new List<Vector3>();
		
		gameObject.AddComponent<MeshFilter>();
		gameObject.AddComponent("MeshRenderer");
		if (material)
			renderer.material = material;
		else
			renderer.material.color = Color.blue;
		
		
		Mesh mesh = GetComponent<MeshFilter>().mesh;
		
		size = new Vector3();
		size.x = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width,0,50)).x - Camera.main.ScreenToWorldPoint(new Vector3(0,0,50)).x;
		size.z = Camera.main.ScreenToWorldPoint(new Vector3(0,Screen.height,50)).y - Camera.main.ScreenToWorldPoint(new Vector3(0,0,50)).y;
		size.y = 100;
	
		vertices = new Vector3[height * width];
		uv = new Vector2[height * width];
		tangents = new Vector4[height * width];
	
	    uvScale = new Vector2(1.0f / (width - 1.0f), 1.0f / (height - 1.0f));
		sizeScale = new Vector3 (size.x / (width - 1), size.y, size.z / (height - 1));
		
		//make vertices
		for (int y=0;y<height;y++)
		{
			for (int x=0;x<width;x++)
			{
			
				Vector3 vertex = new Vector3 (x, 0, y);
				vertices[y*width + x] = Vector3.Scale(sizeScale, vertex);
				uv[y*width + x] = Vector2.Scale(new Vector2 (x, y), uvScale);

				Vector3 tan = Vector3.Scale(sizeScale, new Vector3(2,0,0)).normalized;
				tangents[y*width + x] = new Vector4( tan.x, tan.y, tan.z, -1.0f );
			}
		}
		
		//load vertices and uv
		mesh.vertices = vertices;
		mesh.uv = uv;
		
		//make triangles
		triangles = new int[(height - 1) * (width - 1) * 6];
		int index = 0;
		for (int y=0;y<height-1;y++)
		{
			for (int x=0;x<width-1;x++)
			{
			
				triangles[index++] = (y     * width) + x;
				triangles[index++] = ((y+1) * width) + x;
				triangles[index++] = (y     * width) + x + 1;

				triangles[index++] = ((y+1) * width) + x;
				triangles[index++] = ((y+1) * width) + x + 1;
				triangles[index++] = (y     * width) + x + 1;
			}
		}
	
		//load triangles normals and tangents
		mesh.triangles = triangles;
		mesh.RecalculateNormals();
		mesh.tangents = tangents;
		
		//Move to center of screen
		gameObject.transform.position = new Vector3(Camera.main.ScreenToWorldPoint(new Vector3(Screen.width,0,75)).x,Camera.main.ScreenToWorldPoint(new Vector3(0,Screen.height,75)).y,75);
    }
	
	// Update is called once per frame
	void Update () {
		ResetWater();
		LoadRipples();
		LoadMesh();
		CullRipple();
		
		if(getSpeedScript)
			scrollSpeed = getSpeedScript.GetGameSpeed()*0.3f;
		else
			scrollSpeed = 0.3f;
		
		Vector2 newOffset;
		newOffset = gameObject.renderer.material.GetTextureOffset("_BumpMap");
		newOffset.y -= scrollSpeed * Time.deltaTime;
		
		gameObject.renderer.material.SetTextureOffset("_BumpMap", newOffset);

	}
	
	void LoadRipples(){

		for (int k=0;k<height*width;k++)
		{
		List<Vector3>.Enumerator rippleEnum = RippleOrigins.GetEnumerator();
		List<DateTime>.Enumerator birthEnum = RippleBirth.GetEnumerator();

			while(rippleEnum.MoveNext()){
				birthEnum.MoveNext();
				Vector4 vertex = transform.localToWorldMatrix*new Vector4(vertices[k].x,vertices[k].y,vertices[k].z,1);
				TimeSpan dif = DateTime.Now-birthEnum.Current;
				float dist = Vector2.Distance(new Vector2(rippleEnum.Current.x,rippleEnum.Current.y-(scrollSpeed/7f)*(dif.Milliseconds+dif.Seconds*1000)) , new Vector2(vertex.x,vertex.y));
				//if(dist<=(DateTime.Now-birthEnum.Current).Milliseconds/20 && dist>=(DateTime.Now-birthEnum.Current).Milliseconds/100)
				if(dist < ((dif.Milliseconds+dif.Seconds*1000)/25))
				vertices[k].y+=(int)((100/Mathf.Sqrt(dist))*Mathf.Cos(dist/7-(dif.Milliseconds+dif.Seconds*1000)/(200)));
			}	
		
			//vertices[k].y/=(RippleBirth.Count+1);
		}
		
		//todo calculate tangents;
	}
	
	
	void LoadMesh(){
		Mesh mesh = GetComponent<MeshFilter>().mesh;
		mesh.Clear();
		 
		//load vertices and uv
		mesh.vertices = vertices;
		mesh.uv = uv;
		
		//load triangles normals and tangents
		mesh.triangles = triangles;
		mesh.RecalculateNormals();
		mesh.tangents = tangents;		
	}
	
	//Adds Ripple to List
	public void AddRipple(Vector3 position,DateTime birth){
		RippleOrigins.Add(position);
		RippleBirth.Add(birth);
	}
	
	//Removes a stale ripple
	void CullRipple(){
		List<Vector3>.Enumerator rippleEnum = RippleOrigins.GetEnumerator();
		List<DateTime>.Enumerator birthEnum = RippleBirth.GetEnumerator();
		int count=0;

		while(rippleEnum.MoveNext()){
			birthEnum.MoveNext();
			TimeSpan dif = DateTime.Now-birthEnum.Current;
			if(rippleEnum.Current.y-(scrollSpeed/7f)*(dif.Milliseconds+dif.Seconds*1000)<-500){
				RippleOrigins.RemoveAt(count);
				RippleBirth.RemoveAt(count);
				break;
			}
			count++;
		}
					
		
	}
	
	void ResetWater(){
		for (int y=0;y<height;y++)
		{
			for (int x=0;x<width;x++)
			{
			
				vertices[y*width + x].y = 0;

				Vector3 tan = Vector3.Scale(sizeScale, new Vector3(2,0,0)).normalized;
				tangents[y*width + x] = new Vector4( tan.x, tan.y, tan.z, -1.0f );
			}
		}
	}
}
