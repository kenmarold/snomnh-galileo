// Image Gallery builder

using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;
using System.Collections;

public class ImageGallery : MonoBehaviour  
{
	Texture2D tex;
	public int textureHeight;
	public int textureWidth;
	int currentIndex = 0;

	public void buildGallery()
	{
		GameObject gameController = GameObject.FindGameObjectWithTag("gc");
		GameSetup gameSetup = gameController.GetComponent<GameSetup>();

		for(int i = 0; i < gameSetup.galleryImages.Length; i++) 			// Load the entire Gallery
		// for(int i = 0; i < 15; i++) 										// Load 15 images in the Gallery
		{
			StartCoroutine("loader", currentIndex);
			currentIndex++;
		}

		#if UNITY_EDITOR
		// Debug.Log ("The length of the gallery is: " + gameSetup.galleryImages.Length);
		#endif
	}

	IEnumerator loader(int indexNum)
	{
		GameObject gameController = GameObject.FindGameObjectWithTag("gc");
		GameSetup gameSetup = gameController.GetComponent<GameSetup>();

		WWW www = new WWW("file://" + gameSetup.galleryImages[indexNum]);	// get the first file disk from static variable galleryImages
		yield return www;                                               	// Wait until its loaded
		tex = new Texture2D(textureHeight, textureWidth);               	// create a new Texture2D
		www.LoadImageIntoTexture(tex);                           			// put the image file into the new Texture2D

		#if UNITY_EDITOR
		// Debug.Log ("The path to the gallery file is " + gameSetup.galleryImages[indexNum]);
		#endif

		createGalleryImages(tex);
	}

	public void createGalleryImages(Texture2D tex)
	{
		// Instantiate Gallery Thumb Prefab and Load in Sprite
		GameObject galleryThumb = Instantiate(GameObject.FindGameObjectWithTag("GalleryImgHolder")) as GameObject;

		Image galleryImg = galleryThumb.GetComponent<Image>();				// Access Image Component
		Rect rct = new Rect(0, 0, tex.width, tex.height);					// Define Rect arg
		Vector2 pvt = new Vector2(0.5f, 0.5f);								// Define Pivot arg
		galleryImg.sprite = Sprite.Create(tex, rct, pvt);
		
		// Set Gallery Thumb Parent
		galleryThumb.transform.SetParent(GameObject.FindGameObjectWithTag("GalleryThumbs").transform);
		galleryThumb.transform.localScale = new Vector3(1F, 1F, 1F);
	}
}

