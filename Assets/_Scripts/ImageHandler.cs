// Save, Delete, Load and Upload image files.

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;

public class ImageHandler : MonoBehaviour
{	
	public int startX = 0;
	public int startY = 0;
	public int endX = 1050;
	public int endY = 700;
	[HideInInspector] public string fileName;
	[HideInInspector] public string filePath;
	[HideInInspector] public string tempFilePath;
	
	bool tempFile;

	public Texture2D tex;
	public Image previewImage;

	byte[] bytes;

	void Start ()
	{
		tempFile = false;
	}

/* STORE SCREENSHOT IN A TEXTURE2D */

	public void StoreJPG () {
		StartCoroutine(StoreIt());
	}
	
	IEnumerator StoreIt()
	{	
		// Read the screen buffer after rendering is complete
		yield return new WaitForEndOfFrame();

		// Create a texture the size of the screen, RGB24 format
		tex = new Texture2D(endX, endY, TextureFormat.RGB24, false);
		tex.ReadPixels(new Rect(startX,startY,endX,endY),0,0);
		tex.Apply();

		if (tex != null)
		{
			Debug.Log ("Texture stored in memory.");
		}
	}

/* SAVE JPEG TO LOCAL DISK */

	public void SaveJPG () {
		StartCoroutine(SaveIt());
	}

	IEnumerator SaveIt()
	{	
		GameObject gameController = GameObject.FindGameObjectWithTag("gc");
		GameSetup gameSetup = gameController.GetComponent<GameSetup>();

		// We should only read the screen buffer after rendering is complete
		yield return new WaitForEndOfFrame();

		// Encode texture into JPG
		bytes = tex.EncodeToJPG(60);
		
		// Get file Prefix and subDir from GameSetup array index and concat into a filename and a filepath

			string prefix = gameSetup.filePrefix;
			// string subDir = gameSetup.subDir;
			
			string dtString = System.DateTime.Now.ToString("MM-dd-yyyy_HH-mm-ssfff");
			fileName = prefix+dtString+".jpg";
			filePath = gameSetup.galleryFilePath;
			//filePath = gameSetup.galleryFilePath+subDir;

			Debug.Log("Path = " + filePath);

		// If screenshot is > 0 x 0 write bytes
		if(endX > 0 && endY > 0)
		{
			File.WriteAllBytes(filePath+fileName, bytes);
			Debug.Log("SAVE JPEG : Your file was saved at " + filePath+fileName);
		}
	}

/* SAVE TEMPORARY JPEG TO LOCAL DISK */
	
	public void SaveTempJPG () {
		StartCoroutine(SaveItTemp());
	}
	
	IEnumerator SaveItTemp()
	{	
		GameObject gameController = GameObject.FindGameObjectWithTag("gc");
		GameSetup gameSetup = gameController.GetComponent<GameSetup>();
		// GameSetup gameSetup = GetComponent<GameSetup>();

		// We should only read the screen buffer after rendering is complete
		yield return new WaitForEndOfFrame();
		
		// Encode texture into JPG
		bytes = tex.EncodeToJPG(50);
		// Object.Destroy(tex);
		
		// Get file Prefix and subDir from GameSetup array index and concat into a filename and a filepath
		string prefix = gameSetup.filePrefix;
		string subDir = gameSetup.subDir;
		
		string dtString = System.DateTime.Now.ToString("MM-dd-yyyy_HH-mm-ssfff");
		fileName = prefix+dtString+".jpg";
		tempFilePath = gameSetup.tempFilePath;
		
		// If screenshot is > 0 x 0 write bytes
		if(endX > 0 && endY > 0)
		{
			File.WriteAllBytes(tempFilePath+fileName, bytes);
			Debug.Log("SAVE TEMP: Your temp file was saved at " + tempFilePath+fileName);

			tempFile = true;
		}
	}

/* DELETE JPEG FROM LOCAL DISK */
	
	public void DeleteJPG () {
		if(tempFile != false)
		{
			StartCoroutine(DeleteIt());
		}
		else{
			return;
		}
	}
	
	IEnumerator DeleteIt()
	{	
		GameObject gameController = GameObject.FindGameObjectWithTag("gc");
		GameSetup gameSetup = gameController.GetComponent<GameSetup>();

		// We should only read the screen buffer after rendering is complete
		yield return new WaitForEndOfFrame();

		File.Delete(gameSetup.tempFilePath+fileName);
		Debug.Log("Your file was deleted at " + gameSetup.tempFilePath+fileName);
	}

/* UPLOAD JPEG TO SERVER */

	public void UploadJPG () {
		StartCoroutine(UploadIt());
	}
	
	IEnumerator UploadIt()
	{	
		GameObject gameController = GameObject.FindGameObjectWithTag("gc");
		GameSetup gameSetup = gameController.GetComponent<GameSetup>();

		// We should only read the screen buffer after rendering is complete
		yield return new WaitForEndOfFrame();

		// Create a Web Form
		WWWForm form = new WWWForm();
		// form.AddField("frameCount", Time.frameCount.ToString());
		form.AddBinaryData("fileUpload", bytes, fileName, "image/jpg");
		
		// Upload to a cgi script
		WWW w = new WWW(gameSetup.uploaderCGI, form);
		yield return w;
		if (!string.IsNullOrEmpty(w.error)) {
			print(w.error + " " + fileName + " > UPLOAD FAIL!");
		}
		else {
			print(fileName + " > UPLOAD SUCCESS!");

			// Delete the Temp File
			DeleteJPG();
		}
	}

/* LOAD SCREENSHOT PREVIEW */

	public void LoadPreview () {
		StartCoroutine(LoadIt(tex));
	}

	IEnumerator LoadIt(Texture2D texture)
	{
		yield return new WaitForEndOfFrame();

		Rect rct = new Rect(0, 0, tex.width, tex.height);				// Define Rect
		Vector2 pvt = new Vector2(0.5f, 0.5f);							// Define Pivot
		previewImage.sprite = Sprite.Create(texture, rct, pvt);			// Assign Sprite
	}
}