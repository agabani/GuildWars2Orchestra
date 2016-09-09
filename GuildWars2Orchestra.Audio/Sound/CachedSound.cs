using System.Collections.Generic;
using System.Linq;
using NAudio.Vorbis;
using NAudio.Wave;

namespace GuildWars2Orchestra.Audio.Sound
{
    public class CachedSound
    {
        public CachedSound(VorbisWaveReader vorbisWaveReader)
        {
            WaveFormat = vorbisWaveReader.WaveFormat;

            var wholeFile = new List<float>((int) (vorbisWaveReader.Length/4));
            var readBuffer = new float[vorbisWaveReader.WaveFormat.SampleRate*vorbisWaveReader.WaveFormat.Channels];

            int samplesRead;
            while ((samplesRead = vorbisWaveReader.Read(readBuffer, 0, readBuffer.Length)) > 0)
            {
                wholeFile.AddRange(readBuffer.Take(samplesRead));
            }

            AudioData = wholeFile.ToArray();
        }

        public float[] AudioData { get; }

        public WaveFormat WaveFormat { get; }
    }
}