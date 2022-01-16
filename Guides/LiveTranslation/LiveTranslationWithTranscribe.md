# Live translation with transcribe
## Intro
If you like to watch live streams, sometimes you may come across a streamer that does not speak your language but you liked the show and would like to understand the streamer too. Unfortunately, the streamer does not provide live transcribe either, so your translation options are limited. However…
There is a great, free tool that allows you to live translate someone speaking to you face to face, or during conversation, including the transcription - [Microsoft Translator](URL) (available for [Android](), [Web](), and maybe other platforms).
But the streamer is not a part of your conversation, so you can't live translate and transcribe his stream. Or can you?
## Live translation
With [Microsoft Translator](URL) for web and some tricks you can actually live transcribe the streamer. There are limitations to this, but it works well enough, I think.
First you need to ensure that `Stereo Mix` is available on your system.
[stereomix.png]
It's probably better if you setup it as default device for voice input:
[stereomix_default_device.png]
When you're done you can go to [Microsoft Translator](URL). There you need to start a conversation. Unfortunately, you have to login to do so with either MS or Google account, but you can always create fake account for this purpose.
[ms_login.png]
When you have logged in, then input your participant name (any will do) and your **translation target language** (so, if you want to translate to English, then `Your language` should be set to `English`). When you're done, click `Start conversation`.
[ms_start_conversation.png]
[Microsoft Translator](URL) will ask you about presenter mode. Ensure that `Mic stays on` and `Mute others` options are disabled and click `OK`. Copy your conversation code.
[ms_conversation_code.png]
Now open another window in different browser or incognito mode. Ensure that both windows are visible (i.e., next to each other). [Microsoft Translator](URL) requires to be visible. There is probably some workaround to that, but I couldn't bother to discover that.
Head to [Microsoft Translator](URL) again. Now select `Join conversation` tab. There you have to input your conversation code, participant name (any will do) and source language, for example - Japanese. Then click `Join conversation`.
[ms_join_conversation.png]
Let's get back to the listener window. Now you have to configure additional properties. Click on the cogwheel icon and toggle those options to your heart content:
`Mask profanity`;
`Show original message`;
`Show partial message`;
[listener_mask_profanity.png]
[listener_settings.png]
Those two options are optional but it helps you, if you want to have source transcription too. Otherwise, you're good to go here.
Get back to another window (speaker). Click the cogwheel icon and toggle `Mic stays on` option. 
[speaker_mic_stays_on.png]
## Testing
With setup done, head to any source of speech, like live stream or something to test whether it works. As soon something starts to talk, you should see the results in the listener window.
Below is the screenshot of live translation of Japanese streamer I watched.
[demo.png]
## Limitations
This solution is not always accurate.  The accuracy depends on the speaker, selected language and so on, but it is possible to work with that. Since it uses `Stereo Mix`, the more sounds your PC emits, the more noise it generates, so best results will be received if you don't play more sounds that is minimum required.
There are stability problems. [Microsoft Translator](URL) sometimes lose connection to translation service and you have to toggle `Mic stays on` option again.
Another limitation is that the speaker window has to be visible. Focus is not required but if you want to do something else, then ensure that the window is somehow visible (on another screen, or something). Otherwise, it will not work properly.
Sometimes conversation will crash and you have to setup it again.
## Final thoughts
For some reason, Microsoft provides such powerful tool for free while, for example live transcribe in office suite requires payment, which makes no sense, since Google Docs provide live transcription on for free.
You can also write your own application that can overcome majority if not all limitations but again, it requires payment for API quota.
