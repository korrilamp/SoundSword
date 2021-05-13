# SoundSword User Interface
Our method of implementing the interactive UI to choose which effects to map positional data values to.

## Main components & Unity building blocks
### Canvas 
The first hierarchal component to the UI is a Canvas. Canvases are Unity Gameobjects in the UI class. We create the choice dropdowns by adding Dropdown gameobjects
as children to the Canvas. A single dropdown object is needed for each axis to map user positional data to. In the inspector the apperance and text of these GUI elements
can be easily updated with new values to further customize the words and visuals. More information on using Unity Canvas objects in VR can be found [here](https://arvrjourney.com/how-to-build-vr-uis-with-unity-and-oculus-rift-part-2-69e31b32dd82)

### UI_Scriptfinder Scripts
Each Dropdown menu object has a script component attached called the `X_UI_Scriptfinder`, `Y_UI_Scriptfinder`, and `Z_UI_Scriptfinder` accordingly. The main purpose of these scripts 
is for the user's chosen audio effect (among the options in the dropdown) to enable the effect of the same name in the Audio Mixer. 

### Interaction between the Canvas and the AudioMixer
Within the last function of each of the UI_Scriptfinder scripts is the code that accesses the `drawController` script (that lives within the `vader` prefab), 
to call that script's functions. Namely, it calls the functions `muteEffect`,`enableEffect`, and sets the value of the positional mapping, such as, `x_mapping`

## User Interaction through Raycasting
### Enabling Raycasting from Left-Controller 
Note: Maybe just write up on the structure of components we had to add, preview packages we had to install, any values we had to set in the inspector etc
### Additional Details
Note: mention how to add the reticle as a extra sphere in the scene and other things like how it works 
