using NAudio.Wave;

namespace SporeMan.Console.IO;

static class AudioPlayer {

    public static Action<string> OnPlayDone;

    public static void Play(string sound) {
        new Thread(() => {
            using(var audioFile = new AudioFileReader(sound)) {
                using(var outputDevice = new WaveOutEvent()) {
                    outputDevice.Init(audioFile);
                    outputDevice.Play();
                    while (outputDevice.PlaybackState == PlaybackState.Playing)
                    {
                        Thread.Sleep(1000);
                    }
                    OnPlayDone?.Invoke(sound);
                }
            }
        }).Start();
    }
}