
# Documentation for the Audio Mixer
This document explains the workflow of the project from the UI to the audio output.

## Audio Workflow
### The UI (UI_Scriptfinder scripts)
The user interacts with the dropdown menu from the canvas and selects an audio effect to be mapped to positional data (ex. mapping pitch shift level to X positional data). This can be done for all three of the positional data: X, Y, and Z. More information on the UI can be found in the User Interface Documentation.

### Audio Management in Script (DrawController Script)
Once user's chosen audio effect from the canvas dropdown, the mapping information is fed into the DrawController script. In here, we mute all the audio parameters that are not being used and turnn on the parameters that are currently being used. Then, in each frame (as the program runs), we normalize the positional data retrieved from the rightHand to be a value from 0-1. Depending on the audio parameter selected, the script scales the normalized value to match the range of the audio parameter. This is done using a linear or logarithmic scaling. Lastly we set the audio level to its respective audio parameter with this function call: masterMixer.SetFloat(x_mapping,  get_audio_level(x_mapping, normal_x)). (where x_mapping holds the audio parameter mapped to x positional data and normal_x holds the normalized x positional data).

### Using the Unity Audio Mixer
Here is the unity's documentation/manual on the audio mixer: https://docs.unity3d.com/Manual/AudioMixer.html.
