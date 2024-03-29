#ArrayMaker Change log

###1.1.3
**New**  
- Support for Sprite assets  

###1.1.2
**Fix**  
- Removed uncessary debug logs in actions  

###1.1.1
**Improvement**  
- Included Unity 5 refactored changes, so now the package opens as is in Unity 5 without any additional steps  

### 1.1.0  
**New**  
- Refactored Github Repositories, using now submodule  
- Moved thirdParty code and samples in respective folders ( for clarity and to be compatible with submodule)  

### 1.0.4  
**New**  
- Support for "byte" value  

### 1.0.3
**New**  
- New ArrayListActivateGameObjects

**Improvement**  
- Get the variable's index in ArrayListAdd ( thanks to [dudebxl](http://hutonggames.com/playmakerforum/index.php?topic=10012.msg32820#new) from PlayMaker forum)

### 1.0.2
**Fix**  
- Fixed ArrayListSendEventToGameObjects Finish() call

### 1.0.1
**Fix**  
- Fixed ArrayListSendEventToGameObjects only sending to owner instead of GamObjects in the list
- Safer actions when fsmvar not defined



### 1.0.0
**Improvement**   
- Better git hosting with PlayMaker Utils as SubModule  
- Comply with Ecosystem ChangeLog and versionning system  


###0.9.9

**New**  
- MIT License Addition

**Fix**   
- Windows Phone compatibility work in progress, with extensions for ArrayList LastIndexOf and manual snapshots for lists copies and removal of commented lines giving a hard time to the compiler  
- easySave addon HashTableEasyLoad action  
- texture usage in EasySave sample to use it's own texture set to read write properly.



###0.9.8
**Issue**  
- removed undo when adding proxies to selection, just not finding the right way to do a consistent behavior... and it created obsolete warnings on 4.x

**New**   
- added reset to iterative actions to make sure we have a way to force iterating back from the the first item if we exited the loop early.  
- added currentIndex in iterative actions to know where we are, not just to get the value itself.  
- added ArrayListIsEmpty and HashTableIsEmpty action  
- added ArrayListGetFarthestGameObjectInSight  
- added HashtableEditKey action  
- remove obsolete warnings on U4  
- added arrayList min max and average values custom actions to start some statistic actions around content.  

###0.9.7
**New**  
- added snapshots: prefilled data is stored and use RevertToSnapshot to get back to it, and TakeSnapShot to record the current state of things.  

###0.9.6
**New**  
- fixed prefill setup when downsizing the number of prefilled items.  

###0.9.5
**New**  
- added ArrayListSwapIndex 

###0.9.4
**New**  
- added ArrayListGetGameObjectMaxFsmFloatIndex, thanks for FlyingRobot: http://hutonggames.com/playmakerforum/index.php?topic=5116  
- added ArrayListGetClosestGameObjectInSight, thanks to FlyingRobot: http://hutonggames.com/playmakerforum/index.php?topic=5056  
- added ArrayListInsert, thanks to FlyingRobot: http://hutonggames.com/playmakerforum/index.php?topic=5146.0  
- added backEvent properties actions.

###0.9.3
**Fix**  
- fixed vector2 support ( display of vector2 data in inspector)

**New**
- added AudioClip support  
- added HashTableAddMany, HashTableSetMany, HashTableGetMany  

###0.9.2
**New**
- Added addons for EasySave serialization   
- Added proper support for Vector2 in arrayList and HashTable inspector  

**Improvement** 
- Using now utomate to automate packaging  
- Packages now separated, just the framework, with samples and addons  

**Fix**  
- fixed null values display in inspector for arrayList nad HashTable.

###0.9.1
**Note:** **_Breaking Build_**. NOT COMPATIBLE WITH PREVIOUS VERSIONS  
Many actions public interface changed, so you will need to re-assign setters and getters on all of them.   
This version is a huge improvment in terms of ease of use, with now much more comprehensive and logical actions interfaces, no more lenghtly actions for nothing, 
 the latest playmaker version features a new FsmVar class that let the user select first the variable type, and then select it or feed some values, I don't have to expose ALL possibilities anymore which was confusing, prone to error and frustrations and totally messy.
 
###0.8.0
**Note:** **_Breaking Build_**. previous version had a different file organisation, so now it sits in its own folder and not within "PlayMaker" which was a mistake, 
 I assumed actions would only be detected if within the Actions folder...
 
**Note**  
- goes to 0.9 because I can :) well I had a 0.8 version sort of, but so much went that I moved to 0.9

**New**  
- New actions  

**Fix**  
- incorporate fixes for Texture and proceduralMaterial assignment  
- fixed prefab instance editing not serializing properly  
- fixed ArrayListCopyTo action  

###0.7.0

**Improvement**  
- now using gamObject for reference instead of the component itself via fsmObject, since this doesn't bring any advantages ( as we still need a string reference...)

###0.6.0  
**Note:**  
- Initial Public Release
