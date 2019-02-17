After Import, you're ready to go. It's real simple and can be called from anywhere in your code.

Let's say you want to fade in a UI Element with a Canvas Group - this is your code:

FadeItAll.FadeCanvasGroup(yourCanvasGroup,3f,true);

The parameters are: Your UI Element (or any Gameobject with a Canvas Group), the time to fade, true for fade IN or false for fade OUT.


The same works for Audio!

FadeItAll.FadeAudio(yourAudioSource,1.5f,true);

It's basically the same like with the Canvas Group, but here your first parameter is the Audio Source you want to fade.


Now for the Scene Change Fade. Your code would be:

FadeItAll.FadeSceneChange("nameOfYourScene",Color.black,4f);

The parameters are - you guessed it - the Scene you want to load, the color you want to use for the transition fade and the time of the fading duration.


I'm planning on extending this more, so feel free to contact me with bugs or ideas!



credits to "flattutorials" who gave me the idea for this tool.






