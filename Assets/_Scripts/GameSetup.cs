// Setup class for sorting the basic shit out 

using UnityEngine;
using System.IO;
using System.Linq;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameSetup : MonoBehaviour 
{
	// System.Random rand = new System.Random();

	// Root File Paths
	// public string defaultRoot;
	public string windowsRootPath = @"C:\Users\admin\Dropbox\";
	public string macRootPath = @"/Users/kenmarold/";
	
	// Gallery File Paths & Variables
	public string[] galleryImages;
	// public static FileInfo[] result;
	[HideInInspector] public string testPath;
	[HideInInspector] public string arctopithecusGalleryPath;
	[HideInInspector] public string gulonGalleryPath;
	[HideInInspector] public string scythianWolfGalleryPath;
	[HideInInspector] public string simivulpaGalleryPath;
	[HideInInspector] public string succorathGalleryPath;
	[HideInInspector] public string tatusGalleryPath;
	GameObject galleryThumbHolder;
	
	// Universal File Paths
	public string galleryFilePath;
	public string tempFilePath;
	
	// Remote Server Path
	[HideInInspector] public string uploaderCGI;
	
	// Strange Creatures Variables
	public Text titleText;
	public static Text animalName;
	public Text descriptionText;
	public Image sketchSolution;
	public Image animalPhoto;
	public string activeTag;
	public int totalPossibleCorrectParts;
	public int totalParts;

	[HideInInspector] public int randomNumber;
	[HideInInspector] public int indexNumber;
	[HideInInspector] public int arrayIndex;
	
	// Code Wheel File Paths & Variables
	public string filePrefix;
	public string subDir;

	// Restart variables
	private Vector3 prevMousePosition;
	public GameObject timeOutWarning;
	public float countdownLength = 10;
	public float timeUntilRestartWarning = 30;

	// File System List of Folders
	public List<string> folders;

	void Awake()
	{		
		// Configure Mac File System
		if (Application.platform == RuntimePlatform.OSXPlayer || Application.platform == RuntimePlatform.OSXEditor)
			BuildMacFileSystem();
		// Configure Windows File System
		if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
			BuildWindowsFileSystem();
		// Initialize Strange Creatures
		if(Application.loadedLevelName == "Strange Creatures")
			InitStrangeCreatures();
		// Initialize Code Wheel
		if(Application.loadedLevelName == "Code Wheel")
			InitCodeWheel();
	}

	void Start()
	{		
		prevMousePosition = Input.mousePosition;
		// Debug.Log (prevMousePosition + " at Game Start");
	}

	void Update()
	{
		if(Input.anyKeyDown || Input.mousePosition != prevMousePosition)
		{
			StartGameTimer();
			if (timeOutWarning != null)
			{
				timeOutWarning.SetActive(false);
			}
		}
		prevMousePosition = Input.mousePosition;
	}

	// GAME TIMER

	void StartGameTimer()
	{
		CancelInvoke ();
		Invoke ("ShowRestartWarning", timeUntilRestartWarning);
	}
	
	void ShowRestartWarning()
	{
		GameObject gameController = GameObject.FindGameObjectWithTag("gc");					// First, find the GameObject
		CountdownTimer countdownTimer = gameController.GetComponent<CountdownTimer>();		// Then, access the Script in the GameObject
		countdownTimer.startTimer(countdownLength);											// Finally, call the Script method														

		timeOutWarning.SetActive(true);
		CancelInvoke ();
		Invoke ("RestartGame", countdownLength);

		// Debug.Log ("Restart Warning Popup");
	}

	void RestartGame()
	{
		Application.LoadLevel(Application.loadedLevel);
		// Debug.Log("Game Restarted");
	}

	// BUILD FILE SYSTEM

	// Create Directory if it does not exist
	public void CreateDirectoryIfNotExists(string path)
	{
		if (System.IO.Directory.Exists(path) == false)
		{
			System.IO.Directory.CreateDirectory(path);
			// Debug.Log(path + " folder was created.");
		}
	}

	public void BuildMacFileSystem ()
	{
		// defaultRoot = macRootPath;

		if(Application.loadedLevelName == "Strange Creatures")
		{
			// Debug.Log ("You are running Strange Creatures on a Mac");
			
			// Create folder List
			folders.Add(macRootPath+@"Galileo/strangecreatures/arctopithecus/");
			folders.Add(macRootPath+@"Galileo/strangecreatures/simivulpa/");
			folders.Add(macRootPath+@"Galileo/strangecreatures/scythian-wolf/");
			folders.Add(macRootPath+@"Galileo/strangecreatures/tatus/");
			folders.Add(macRootPath+@"Galileo/strangecreatures/gulon/");
			folders.Add(macRootPath+@"Galileo/strangecreatures/succorath/");
			folders.Add(macRootPath+@"Galileo/strangecreatures/temp/");

			// If a folder doesn't exist, create it.
			foreach(string folder in folders)
			{
				CreateDirectoryIfNotExists(folder);
			}
		}
		
		if(Application.loadedLevelName == "Code Wheel")
		{
			// Debug.Log ("You are running Code Wheel on a Mac");
			
			// Create folder List
			folders.Add(macRootPath+@"Galileo/codewheel/encode/");
			folders.Add(macRootPath+@"Galileo/codewheel/temp/");
			
			// If a folder doesn't exist, create it.
			foreach(string folder in folders)
			{
				CreateDirectoryIfNotExists(folder);
			}
		}
	}
	
	public void BuildWindowsFileSystem ()
	{
		// defaultRoot = windowsRootPath;

		if(Application.loadedLevelName == "Strange Creatures")
		{
			Debug.Log ("You are running Strange Creatures on Windows");
			
			// Create folder List
			folders.Add(windowsRootPath+@"Galileo\strangecreatures\arctopithecus\");
			folders.Add(windowsRootPath+@"Galileo\strangecreatures\simivulpa\");
			folders.Add(windowsRootPath+@"Galileo\strangecreatures\scythian-wolf\");
			folders.Add(windowsRootPath+@"Galileo\strangecreatures\tatus\");
			folders.Add(windowsRootPath+@"Galileo\strangecreatures\gulon\");
			folders.Add(windowsRootPath+@"Galileo\strangecreatures\succorath\");
			folders.Add(windowsRootPath+@"Galileo\strangecreatures\temp\");
						
			// If a folder doesn't exist, create it.
			foreach(string folder in folders)
			{
				CreateDirectoryIfNotExists(folder);
			}
		}
		
		if(Application.loadedLevelName == "Code Wheel")
		{
			Debug.Log ("You are running Code Wheel on Windows");
			
			// Create folder List
			folders.Add(windowsRootPath+@"Galileo\codewheel\encode\");
			folders.Add(windowsRootPath+@"Galileo\codewheel\temp\");
			
			// If file system doesn't exist, build it.
			foreach(string folder in folders)
			{
				CreateDirectoryIfNotExists(folder);
			}
		}
	}

	public void InitStrangeCreatures ()
	{
		uploaderCGI = "http://www.locomoku.com/projects/samnoble/strangecreatures/cgi-bin/upload.cgi";

		// Set Paths from folders List generated in FileSystem methods
		arctopithecusGalleryPath = folders[0];
		simivulpaGalleryPath = folders[1];
		scythianWolfGalleryPath = folders[2];
		tatusGalleryPath = folders[3];
		gulonGalleryPath = folders[4];
		succorathGalleryPath = folders[5];

		tempFilePath = folders[6];

		// Build Gallery Arrays
		string[] arctopithecusImages = Directory.GetFiles(arctopithecusGalleryPath, "*.jpg");
		string[] gulonImages = Directory.GetFiles(gulonGalleryPath, "*.jpg");
		string[] scythianWolfImages = Directory.GetFiles(scythianWolfGalleryPath, "*.jpg");
		string[] simivulpaImages = Directory.GetFiles(simivulpaGalleryPath, "*.jpg");
		string[] succorathImages = Directory.GetFiles(succorathGalleryPath, "*.jpg");
		string[] tatusImages = Directory.GetFiles(tatusGalleryPath, "*.jpg");

		#if UNITY_EDITOR
		// DEBUG FOLDER CONTENTS
		/*foreach(string image in gulonImages)
		{
			print ("IMAGES: " + image);
		}*/
		#endif

		// Concatenate Gallery Folders into single Array
		galleryImages = 
			Directory.GetFiles(arctopithecusGalleryPath, "*.jpg")
				.Concat(gulonImages)
				.Concat(scythianWolfImages)
				.Concat(simivulpaImages)
				.Concat(succorathImages)
				.Concat(tatusImages)
				.ToArray();

#if UNITY_EDITOR

		// DEBUG GALLERY IMAGES
		/*foreach(string image in galleryImages)
		{
			print ("GALLERY IMAGE: " + image);
		}*/
#endif

		// Create Creature Sets

		string[] animalTags =
		{
			"ArctopithecusPart",
			"SimivulpaPart",
			"ScythianWolfPart",
			"TatusPart",
			"GulonPart",
			"SuccorathPart"
		};

		// Get the total count of animal parts based on tag (above)
		// testAnimalPartsCountTotal = GameObject.FindGameObjectsWithTag(animalTags[0]);
		GameObject[] arctopithecusPartsCountTotal = GameObject.FindGameObjectsWithTag(animalTags[0]);
		GameObject[] simivulpaPartsCountTotal = GameObject.FindGameObjectsWithTag(animalTags[1]);
		GameObject[] scythianWolfPartsCountTotal = GameObject.FindGameObjectsWithTag(animalTags[2]);
		GameObject[] tatusPartsCountTotal = GameObject.FindGameObjectsWithTag(animalTags[3]);
		GameObject[] gulonPartsCountTotal = GameObject.FindGameObjectsWithTag(animalTags[4]);
		GameObject[] succorathPartsCountTotal = GameObject.FindGameObjectsWithTag(animalTags[5]);

		int[] totalPartsByTag = // these are integer counts of the Gameobjects above
		{
			arctopithecusPartsCountTotal.Count(),
			simivulpaPartsCountTotal.Count(),
			scythianWolfPartsCountTotal.Count(),
			tatusPartsCountTotal.Count(),
			gulonPartsCountTotal.Count(),
			succorathPartsCountTotal.Count()
		};

		// Store the total parts count
		totalParts = totalPartsByTag[0] + totalPartsByTag[1] + totalPartsByTag[2] + totalPartsByTag[3] + totalPartsByTag[4] + totalPartsByTag[5];

		// Debug.Log ("TOTAL PARTS = " + totalParts);
			
			#if UNITY_EDITOR
		// Debug.Log ("testAnimalPartsCount = " + testAnimalPartsCountTotal.Length);
		// Debug.Log ("arctopithecusPartsCount = " + arctopithecusPartsCountTotal.Length);
		// Debug.Log ("simivulpaPartsCount = " + simivulpaPartsCountTotal.Length);
		// Debug.Log ("scythianWolfPartsCount = " + scythianWolfPartsCountTotal.Length);
		// Debug.Log ("tatusPartsCount = " + tatusPartsCountTotal.Length);
		// Debug.Log ("gulonPartsCount = " + gulonPartsCountTotal.Length);
		// Debug.Log ("succorathPartsCount = " + succorathPartsCountTotal.Length);
		#endif

		string[] animalTitles =
		{
			"Of the Bear-Ape ARCTOPITHECUS.",	// Index 0 arctopithecus
			"Of the SIMIVULPA, or Apifb-Fox.",	// Index 1 simivulpa
			"The SCYTHIAN WOLF.",				// Index 2 scythian-wolf
			"Of the TATUS, or Guinean Beast.",	// Index 3 tatus
			"Of the GULON.",					// Index 4 gulon
			"Of the SUCCORATH."					// Index 5 succorath
		};

		string[] animalDescriptions = 
		{
			// Of the Bear-Ape ARCTOPITHECUS.
			"There is in America a very deformed beast which the inhabitants call Haut or Hauti, and the Frenchmen, Guenon, as big as a great African Munkey. His belly hangeth very low, his head and face like unto a childs, as may be seen by this lively picture, and being taken it will fight like a young child. His skin is of an ash-colour, and hairy like a Bear; he hath but three claws on a foot, as long as four fingers, and like the thornes of Privet, where-by he climeth up into the highest trees, and for the most part liveth of the leaves of a certain tree being of an exceeding height, which the Americans call Amahut, and thereof this beast is called Haut. Their tail is about three fingers long, having very little hair there-on; I observed, that although it often rained, yet was that beast never wet.",
			// Of the SIMIVULPA, or Apifb-Fox.
			"…they have seen a four-footed beast, the forepart like a Fox, and in the hinder part like an Ape, except that it had a mans feet, and ears like a Bat, and underneath the common belly, there was a skin like a bag or scrip, where-in she keepeth, lodgeth, and carryeth her young ones, until they are able to provide for themselves, without the help of their dam; neither do they come forth of that receptacle, except it be to suck milk, or sport themselves, so that the same under-belly is her best remedy against the furious Hunters, and other ravening beasts, to preserve her young ones, for she is incredibly swift, running with that carriage as if she has no burden. It hath a tail like a Munkey…",
			// The SCYTHIAN WOLF.
			"There is no doubt but this Beast is of the kinde of Beavers, because it liveth both on the water and on the land, and the outward form of the parts beareth a similitude of that Beast. Their outward form is most like unto a Beaver, saving in their tail, for the tail of a Beaver is fish, but the tail of an Synthian Wolf is flesh. They are less then Beavers, some compare them unto a Cat, and some unto a Fox; but I cannot consent unto the Fox. They are bigger than a CAT and longer, but lesser then a Fox, and therefore in my opinion they are well called Dogs of the water. They exceed in length, for in Swetia, and all the Northern Rivers they are three times so long as a Beaver. They have rough skin; and the hair of it very soft and neat, like the hair of a Beaver, but different in this, that it is shorter and unequal, also of colour like a Ches-nut, or brownish, but the Beavers is white or ash-colour. It hath very sharp teeth, and is a very biting Beast, likewise short legs, and his feet and tail like a Dogs, which caused Bellonius to write, that if his tail were off, he were in all parts like a Beaver, differing in nothing but his habitation. For the Beaver goeth both to the Salt waters, and to the fresh, but the Synthian Wolf never to the salt.",
			// Of the TATUS, or Guinean Beast.
			"This is a four-footed strange Beast, it is naturally covered with a hard shell, divided and interlined like the fins of fishes, outwardly seeming buckled to the back like Coat-armor, within which the beast draweth up his body, as a Hedge-hog doth within his prickled skin; and therefore I take it to be a Brasilian Hedge-hog. It is not much greater than a little Pig, and by the snout, ears, legs, and feet thereof, it seemeth to be of that kind, saving that the snout is a little broader, and shorter than a Pigs, and the tail very long like a Lizards or Rats, and one of these being brought into France, did live upon the eating of seeds, and fruits of the Gardens, but it appeareth by that picture, or rather the stuffed, which Adriausus Mercellus the Apothecary…that the feet thereof are not cloven into two parts like Swine, but rather into many like Dogs, for upon the hinderfeet there are five toes, and upon the fore feet four, whereof two are so small that they are scarce visible. The breadth of that same skin was about seven fingers, and the length of it two spans, the shell or crust upon the back of it did not reach down unto the rump or tail, but broke off as it were upon the hips, some four fingers from the tail.",
			// Of the GULON.
			"This Beast was not known by the Ancients, but hath been since discovered in the Northern parts of the World, and because of the voracity thereof, it is called  (Gula)…is thought to be engendered by a Hyena and a Lioness, for the quality it resembleth a Hiena, and it is the same which is called (Crocuta;) it is a devouring and an unprofitable creature, having sharper teeth than other creatures. Some think it is derived of a Wolf and a Dog, for it is about the bigness of a Dog; it hath the face of Cat, the body and tail of a Fox; being black of colour; his feet and nails be most sharp, his skin rusty, the hair very sharp, and it feedeth upon dead carkases. When it hath found a dead carcass he eateth thereof so violently, that his belly standeth out like a bell; then he seeketh for some narrow passage betwixt two trees, and there draweth through his body, by pressing whereof, he driveth out the meat which he had eaten; and being so emptied returneth and devoureth as much as he did before, and goeth again and emptieth himself as in former manner; and so continueth eating and emptying till all be eaten.",
			// Of the SUCCORATH.
			"…it is of a very deformed shape, and monstrous presence, a great ravener and untamable wilde Beast. When the Hunters that desire her skin set upon her, she flyeth very swift, carrying her young ones upon her back, and covering them with her broad tail: Hunters dig several pits or great holes in the earth, which they cover with boughs, sticks, and earth, so weakly that if the Beast chance at any time to come upon it, she and her young ones fall down into the pit and are taken. This cruel, untamable, impatient, violent, ravening, and bloudy beast, perceiving that her natural strength cannot deliver her from the wit and policy of men her hunters, (for being inclosed she can never get out again.) …she destroyeth them all with her own teeth; for there was never any of them taken alive…And this is all I finde recorded of this most savage Beast."
		};
		string[] filePrefixes =
		{
			"arctopithecus_",
			"simivulpa_",
			"scythian-wolf_",
			"tatus_",
			"gulon_",
			"succorath_"
		};
		string[] subDirs =
		{
			"arctopithecus",
			"simivulpa",
			"scythian-wolf",
			"tatus",
			"gulon",
			"succorath"
		};

		/*
		string[] sketchSolutions =
		{
			"",
			"",
			"",
			"",
			"",
			""
		};
		string[] animalPhotos =
		{
			"",
			"",
			"",
			"",
			"",
			""
		};
		 */

		// Generate random index number to key all array content in to game 
		// arrayIndex = Random.Range(0,animalTitles.Length);

		// Always start at index number 0 to key all array content in to game 
		arrayIndex = 0;

		// Debug.Log("Current Animal Array Index is: " + arrayIndex);

		galleryFilePath = folders[arrayIndex];						// Set Gallery File Path
		string desc = animalDescriptions[arrayIndex];				// Set Animal Description Text
			descriptionText.text = desc;
		string title = animalTitles[arrayIndex];					// Set Animal Description Title
			titleText.text = title;
		filePrefix = filePrefixes[arrayIndex];						// Set Animal File Prefix
		subDir = subDirs[arrayIndex];								// Set Animal File Sub-Directory
		activeTag = animalTags[arrayIndex];							// Set Animal Tag
		totalPossibleCorrectParts = totalPartsByTag[arrayIndex];	// Set Active Parts Total

		// Set Animal Sketch Solution -- this is the actual sketch from galileo exhibit
		// sketchSolution = sketchSolutions[arrayIndex];
		
		// Set Animal Photo -- this is a photograph of the animal
		// animalPhoto = animalPhotos[arrayIndex];

		// Debug.Log ("Strange Creatures is intialized and ready.");
	}
	
	public void InitCodeWheel ()
	{
		// GameObject gameController = GameObject.FindGameObjectWithTag("gc");
		// InitializeFileSystem initializeFileSystem = gameController.GetComponent<InitializeFileSystem>();

		uploaderCGI = "http://www.locomoku.com/projects/samnoble/codewheel/cgi-bin/upload.cgi";

		// Set Paths from folders List
		galleryFilePath = folders[0];
		tempFilePath = folders[1];

		galleryImages = 
			Directory.GetFiles(galleryFilePath, "*.jpg");

		GameObject[] alphaKeys;
		GameObject[] symbolKeys;
		GameObject[] romanKeys;
		GameObject deleteKey;
		
		filePrefix = "secretmessage_";
		subDir = "";

		alphaKeys = GameObject.FindGameObjectsWithTag("AlphaKey");
		symbolKeys = GameObject.FindGameObjectsWithTag("SymbolKey");
		romanKeys = GameObject.FindGameObjectsWithTag("RomanKey");
		deleteKey = GameObject.FindGameObjectWithTag("DeleteKey");

		// Debug.Log ("Code Wheel is intialized and ready.");
	}
}