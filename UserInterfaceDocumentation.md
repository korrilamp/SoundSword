# Sound Sword User Interface
Documentation on how we implemented the interactive UI.

## Main components & Unity building blocks
### Canvas 
The first hierarchal component to the UI is a Canvas. Canvases are Unity Gameobjects in the UI class. We create the choice dropdowns by adding Dropdown gameobjects
as children to the Canvas. A single dropdown object is needed for each axis to map user positional data to. In the inspector the apperance and text of these GUI elements
can be easily updated with new values to further customize the words and visuals.

### UI_Scriptfinder Scripts
Each Dropdown menu object has a script component attached called the `X_UI_Scriptfinder`, `Y_UI_Scriptfinder`, and `Z_UI_Scriptfinder` accordingly. The main purpose of these scripts 
is for the user's chosen audio effect (among the options in the dropdown) to enable the effect of the same name in the Audio Mixer. 

###Interaction between the Canvas and the AudioMixer
Within the last function of each of the UI_Scriptfinder scripts is the code that accesses the `drawController` script, to call that script's functions. Namely, it calls the 
functions `muteEffect`,`enableEffect`, and sets the value of the positional mapping, such as, `x_mapping`
