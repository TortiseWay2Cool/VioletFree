using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Networking;
using UnityEngine;

namespace VioletFree.Menu
{
    class AudioManager
    {
        public static string GetFileExtension(string fileName)
        {
            return fileName.ToLower().Split(".")[fileName.Split(".").Length - 1];
        }

        public static async void PlaySoundThroughMicrophone(AudioClip sound, bool looping)
        {
            if (GorillaTagger.Instance.myRecorder != null)
            {
                AudioManager.Play2DAudio(sound, 0.6f);

                GorillaTagger.Instance.myRecorder.SourceType = Photon.Voice.Unity.Recorder.InputSourceType.AudioClip;
                GorillaTagger.Instance.myRecorder.AudioClip = sound;
                GorillaTagger.Instance.myRecorder.LoopAudioClip = looping;
                GorillaTagger.Instance.myRecorder.RestartRecording(true);

                await Task.Delay((int)(sound.length * 1000));
                StopSoundThroughMicrophone();
            }
        }

        public static void StopSoundThroughMicrophone()
        {
            if (GorillaTagger.Instance.myRecorder != null)
            {
                GorillaTagger.Instance.myRecorder.SourceType = Photon.Voice.Unity.Recorder.InputSourceType.Microphone;
                GorillaTagger.Instance.myRecorder.AudioClip = null;
                GorillaTagger.Instance.myRecorder.RestartRecording(true);
            }
        }
        public static AudioType GetAudioType(string extension)
        {
            switch (extension.ToLower())
            {
                case "mp3":
                    return AudioType.MPEG;
                case "wav":
                    return AudioType.WAV;
                case "ogg":
                    return AudioType.OGGVORBIS;
                case "aiff":
                    return AudioType.AIFF;
                case "m4a":
                    return AudioType.MPEG;
            }
            return AudioType.WAV;
        }

        public static Dictionary<string, AudioClip> audioFilePool = new Dictionary<string, AudioClip> { };
        public static AudioClip LoadSoundFromFile(string fileName)
        {
            AudioClip sound = null;

            if (!audioFilePool.ContainsKey(fileName))
            {
                if (!Directory.Exists("Violet"))
                {
                    Directory.CreateDirectory("Violet");
                }
                string filePath = System.IO.Path.Combine(System.Reflection.Assembly.GetExecutingAssembly().Location, "Violet/" + fileName);
                filePath = filePath.Split("BepInEx\\")[0] + "Violet/" + fileName;
                filePath = filePath.Replace("\\", "/");

                UnityWebRequest actualrequest = UnityWebRequestMultimedia.GetAudioClip("file://" + filePath, GetAudioType(GetFileExtension(fileName)));
                UnityWebRequestAsyncOperation newvar = actualrequest.SendWebRequest();
                while (!newvar.isDone) { }

                AudioClip actualclip = DownloadHandlerAudioClip.GetContent(actualrequest);
                sound = Task.FromResult(actualclip).Result;

                audioFilePool.Add(fileName, sound);
            }
            else
            {
                sound = audioFilePool[fileName];
            }

            return sound;
        }

        public static AudioClip LoadSoundFromURL(string resourcePath, string fileName)
        {
            if (!Directory.Exists("Violet"))
                Directory.CreateDirectory("Violet");

            if (!File.Exists("Violet/" + fileName))
            {
                UnityEngine.Debug.Log("Downloading " + fileName);
                WebClient stream = new WebClient();
                stream.DownloadFile(resourcePath, "Violet/" + fileName);
            }

            return LoadSoundFromFile(fileName);
        }

        public static GameObject audiomgr;

        public static void Play2DAudio(AudioClip sound, float volume)
        {
            if (audiomgr == null)
            {
                audiomgr = new GameObject("2DAudioMgr");
                AudioSource temp = audiomgr.AddComponent<AudioSource>();
                temp.spatialBlend = 0f;
            }
            AudioSource ausrc = audiomgr.GetComponent<AudioSource>();
            ausrc.volume = volume;
            ausrc.PlayOneShot(sound);
        }
    }
}
