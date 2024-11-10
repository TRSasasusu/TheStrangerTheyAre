using UnityEngine;

namespace TheStrangerTheyAre
{
    public static class CustomTitleScreen
    {
        //The different objects needed for the title screen to work right
        public static AssetBundle titleBundle;
        //public static AudioClip titleMusic = null;
        //private static bool vanillaTitle = false;
        public static GameObject titleEffectsObject;

        /**
         * Creates all of the objects when the title screen first loads, and disables them if they're not yet needed
         */
        public static void FirstTimeTitleEdits()
        {
            //Load the title screen music
            //titleMusic = AudioUtilities.LoadAudio(Path.Combine(DeepBramble.instance.ModHelper.Manifest.ModFolderPath, "assets", "Audio", "title_music.ogg"));

            //Find the background object
            GameObject backgroundObject = GameObject.Find("Scene/Background");

            //Load the custom effects bundle, make it a child of the background object
            titleEffectsObject = titleBundle.LoadAsset<GameObject>("Assets/NewTitlePlanet.prefab");
            titleEffectsObject = GameObject.Instantiate(titleEffectsObject, backgroundObject.transform);
            titleEffectsObject.transform.position = new Vector3(116.455f, 368.8177f, -47.0909f);
            titleEffectsObject.transform.rotation = Quaternion.Euler(327.4284f, 1.9997f, 340.8541f);

            //Set the custom audio
            /*OWAudioSource musicSource = GameObject.Find("Scene/AudioSource_Music").GetComponent<OWAudioSource>();
            musicSource._audioLibraryClip = (AudioType.None);
            musicSource.GetAudioSource().clip = titleMusic;
            musicSource.SetMaxVolume(0.2f);*/
        }

        /**
         * Enables the special title screen behavior
         */
        public static void EnableTitleEdits()
        {
            titleEffectsObject.SetActive(true);

            //Set the custom audio
            /*OWAudioSource musicSource = GameObject.Find("Scene/AudioSource_Music").GetComponent<OWAudioSource>();
            musicSource._audioLibraryClip = AudioType.None;
            musicSource.GetAudioSource().clip = titleMusic;
            musicSource.SetMaxVolume(0.2f);
            musicSource.Play();*/
        }
    }
}
