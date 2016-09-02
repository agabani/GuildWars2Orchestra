using System;
using NAudio.Midi;
using NAudio.Wave;

namespace GuildWars2Orchestra.Midi
{
    public class MyClass : IDisposable
    {
        private WaveOutEvent _waveOutEvent;

        public MyClass()
        {
            _waveOutEvent = new WaveOutEvent();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Run()
        {
            var midiFile = new MidiFile(@"C:/Users/speechless/Desktop/Musician_14th_song_d.gray_man.mid");

            IWaveProvider waveProvider = new MediaFoundationReader(@"file://C:/Users/speechless/Desktop/Musician_14th_song_d.gray_man.mid");
            _waveOutEvent.Init(waveProvider);
            _waveOutEvent.Play();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_waveOutEvent != null)
                {
                    _waveOutEvent.Dispose();
                    _waveOutEvent = null;
                }
            }
        }
    }
}