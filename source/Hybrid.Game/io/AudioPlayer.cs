using NAudio.Wave;

namespace Hybrid.Game.IO;

static class AudioPlayer {

    public static Action<string> OnPlayDone;

    public static void Play(string sound) {
        string fullPath = Path.Join("assets", "audio", "sfx", sound);
        if (!sound.EndsWith(".wav")) {
            fullPath += ".wav";
        }

        new Thread(() => {
            using(var audioFile = new AudioFileReader(fullPath)) {
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