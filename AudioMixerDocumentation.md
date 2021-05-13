
# Documentation for the Audio Mixer
This document explains the workflow of the project from the UI to the audio output.

## Audio Workflow
### The UI (UI_Scriptfinder scripts)
The user interacts with the dropdown menu from the canvas and selects an audio effect to be mapped to positional data (ex. mapping pitch shift level to X positional data). This can be done for all three of the positional data: X, Y, and Z. More information on the UI can be found in the User Interface Documentation.

### Audio Management in Script (DrawController Script)
Once user's chosen audio effect from the canvas dropdown, the mapping information is fed into the DrawController script. In here, we mute all the audio parameters that are not being used and turnn on the parameters that are currently being used. Then, in each frame (as the program runs), we normalize the positional data retrieved from the rightHand to be a value from 0-1. Depending on the audio parameter selected, the script scales the normalized value to match the range of the audio parameter. This is done using a linear or logarithmic scaling. Lastly we set the audio level to its respective audio parameter with this function call: masterMixer.SetFloat(x_mapping,  get_audio_level(x_mapping, normal_x)). (where x_mapping holds the audio parameter mapped to x positional data and normal_x holds the normalized x positional data).

### Using the Unity Audio Mixer
Here is the unity's documentation/manual on the audio mixer: https://docs.unity3d.com/Manual/AudioMixer.html. We use the Unity audio mixer in order to apply effects to the audio tracks. Our current mixer is only set up for one sound source, but could easily be modified to account for multiple sources. For each sound source, we route the output of the source to a distinct group in the audio mixer. This group takes in audio output from the source, and applies various effects to the sound. In order to enable/disable effects, we have determined what the default settings are for each effect so that it effectively does not alter the sound. When a script "disables" an effect, it is really chaning the parameters of the effect so that it has little to no impact on the sound. The output from each effect is routed into the input of the next effect, so that effects can be stacked on top of one another. In order to see how the program is changing the audio effects, simply view the mixer asthe program is running and monitor the effects on the right hand side of the screen.  
