# Engineering Notebook 
## Week 3/7 – 3/12

### Weekly Activities and Progress
* We met as a group in the EPDC in the SEC with the VR equipment and worked together again. We were able to make progress on the playback 
feature for the visual aspects. We also found an approach to replay audio with the mixer's effects applied to it.
* We solved some of the bugs we found when playing back recorded visual features. The primary bug occured where the spawned objects weren't
following the appropriate tracks. However, we determined that the issue came from the test files that we were using. Replaying spawned objects
now works properly.
* We have planned out the needed changes for the Replay Manager and the Record Manager in order to make playback modular. Currently, objects 
are hardcoded to spawn in. However, we can track the number objects that we need to spawn when we record, and add that to the playback file.

### Upcoming Challenges (potential blockers)
The big parts remaining to develop for our program are:
* Applying effects to more than one layer of audio
* Visual effects
* User Interface and Menus
