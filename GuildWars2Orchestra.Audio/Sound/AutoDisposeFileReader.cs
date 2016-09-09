using NAudio.Vorbis;
using NAudio.Wave;

namespace GuildWars2Orchestra.Audio.Sound
{
    public class AutoDisposeFileReader : ISampleProvider
    {
        private VorbisWaveReader _reader;

        public AutoDisposeFileReader(VorbisWaveReader reader)
        {
            _reader = reader;
            WaveFormat = reader.WaveFormat;
        }

        public int Read(float[] buffer, int offset, int count)
        {
            if (_reader == null)
            {
                return 0;
            }

            var read = _reader.Read(buffer, offset, count);

            if (read == 0)
            {
                _reader.Dispose();
                _reader = null;
            }

            return read;
        }

        public WaveFormat WaveFormat { get; }
    }
}