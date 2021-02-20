# Engineering Notebook 
## Week 2/12 – 2/19

### Weekly Activities and Progress

We met with our sponsor, Prof. James Intriligator on Tuesday 2/16/2021.
* We updated Prof. Intriligator on our progress on the project, including the work we did
to complete our demo last semester
* We discussed potentially including a purely file audio export option, so that this
program can truly act as a VR mixer.
 

We met as a team on Friday 2/19/2021
* We discussed implementation options, as we need to change the structure of the code pretty
significantly in order to expand functionality.
* We looked at our options for implementing the app with Audio Mixer object, using snapshots to
change between which audio effect is active. This is likely the direction we will go in, as it looks
like it will be pretty easy to change between states of the mixer at runtime.


### Upcoming Challenges (potential blockers)
* Our largest challenge right now is figuring out how to perform our desired actions at runtimes using
scripting in C#. We need to continue looking into this, our next blocker will likely be figuring out
how to create several audio sources from a C# script. 
* A potential blocker we came across is that it seems like it will be difficult to manage a scenario
where some audio effects can be currently playing while editing another effect using C# scripts, 
hopefully we can figure out a smooth way to do this as our only potential solution so far is creating
a mixer snapshot for every possible combination of mixer effects.

