// Send MMS messages

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class MMSHandler : MonoBehaviour
{
	public string mmsURL = "http://locomoku.com/projects/samnoble/strangecreatures/uploads/";
	public string mmsclientID = "7030";
	public string mmsCampaignID = "103240";
	public string mmsToken = "f677d301a63cf734609e97e912104bb7";
	public string mmsSubject = "Sam Noble Oklahoma Museum of Natural History";
	public string mmsMessage = "is visiting Galileo’s World at the Sam Noble Oklahoma Museum of Natural History. Come build your own Strange Creature at Through the Eyes of the Lynx: Galileo, Natural History and the Americas from August 1, 2015 - January 18, 2016.";
	public string mmsWebsite = "Learn more at http://samnoblemuseum.ou.edu/slider/galileos-world-an-exhibition-without-walls/";
	public Text mmsSendName;
	public Text mmsPhoneNum;
	string theAnimal;
		
	[HideInInspector] public string thePhone;
	[HideInInspector] public string theName;

	[HideInInspector] public int phoneLength;
	[HideInInspector] public int nameLength;

	public void getNameLength ()
	{
		nameLength = mmsSendName.text.Length;
		Debug.Log ("The length of your name is " + nameLength);
	}

	public void getPhoneLength ()
	{
		phoneLength = mmsPhoneNum.text.Length;
		Debug.Log ("The length of your phone number is " + phoneLength);
	}

	public void sendMMS() 
	{
		StartCoroutine(createMMS(1.0f));
	}
	
	IEnumerator createMMS(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);

		// Keep this code below to work out how to do things purely programmatically

		// string nameText = GameObject.FindGameObjectWithTag("NameText").GetComponent<InputField>().text.ToString();
		// string phoneText = GameObject.FindGameObjectWithTag("PhoneText").GetComponent<InputField>().text.ToString();
		// mmsSendName = nameText;
		// mmsPhoneNum = phoneText;

		theName = mmsSendName.text;
		thePhone = mmsPhoneNum.text;

		ImageHandler img = GetComponent<ImageHandler>() ;
		string jpgFileName = img.fileName;
			
		Debug.Log("Your MMS URL path is: " + mmsURL+jpgFileName);

		string escURL = WWW.EscapeURL(mmsURL+jpgFileName);
		string escMessage = WWW.EscapeURL(theName+" "+mmsMessage+" "+mmsWebsite);
		string escSubject = WWW.EscapeURL(mmsSubject);
		
		string APICall = 
			"https://api.mogreet.com/moms/transaction.send?client_id="+mmsclientID+"&token="+mmsToken+"&campaign_id="+mmsCampaignID+"&to="+thePhone+"&message="+escMessage+"&content_url="+escURL+"&subject="+escSubject+"&format=json";
		
		Debug.Log (APICall);
		
		WWW w = new WWW(APICall);
		yield return w;
		
		if (!string.IsNullOrEmpty(w.error)) {
			print(w.error + " FILE NOT SENT VIA MMS");
		}
		else {
			print(jpgFileName + " was sent via MMS!");
		}
	}
}