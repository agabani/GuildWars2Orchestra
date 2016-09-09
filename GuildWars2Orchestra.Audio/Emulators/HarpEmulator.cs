using System;
using System.Collections.Generic;
using GuildWars2Orchestra.Audio.Sound;
using GuildWars2Orchestra.GuildWars2.Controls;
using GuildWars2Orchestra.GuildWars2.Instrument;
using NAudio.Vorbis;

namespace GuildWars2Orchestra.Audio.Emulators
{
    public class HarpEmulator : IKeyboard
    {
        private static readonly Dictionary<string, CachedSound> Sound = new Dictionary<string, CachedSound>
        {
            {"1" + HarpNote.Octaves.Low, new CachedSound(new VorbisWaveReader(@"Resources\Harp\C3.ogg"))},
            {"2" + HarpNote.Octaves.Low, new CachedSound(new VorbisWaveReader(@"Resources\Harp\D3.ogg"))},
            {"3" + HarpNote.Octaves.Low, new CachedSound(new VorbisWaveReader(@"Resources\Harp\E3.ogg"))},
            {"4" + HarpNote.Octaves.Low, new CachedSound(new VorbisWaveReader(@"Resources\Harp\F3.ogg"))},
            {"5" + HarpNote.Octaves.Low, new CachedSound(new VorbisWaveReader(@"Resources\Harp\G3.ogg"))},
            {"6" + HarpNote.Octaves.Low, new CachedSound(new VorbisWaveReader(@"Resources\Harp\A3.ogg"))},
            {"7" + HarpNote.Octaves.Low, new CachedSound(new VorbisWaveReader(@"Resources\Harp\B3.ogg"))},
            {"8" + HarpNote.Octaves.Low, new CachedSound(new VorbisWaveReader(@"Resources\Harp\C4.ogg"))},
            {"1" + HarpNote.Octaves.Middle, new CachedSound(new VorbisWaveReader(@"Resources\Harp\C4.ogg"))},
            {"2" + HarpNote.Octaves.Middle, new CachedSound(new VorbisWaveReader(@"Resources\Harp\D4.ogg"))},
            {"3" + HarpNote.Octaves.Middle, new CachedSound(new VorbisWaveReader(@"Resources\Harp\E4.ogg"))},
            {"4" + HarpNote.Octaves.Middle, new CachedSound(new VorbisWaveReader(@"Resources\Harp\F4.ogg"))},
            {"5" + HarpNote.Octaves.Middle, new CachedSound(new VorbisWaveReader(@"Resources\Harp\G4.ogg"))},
            {"6" + HarpNote.Octaves.Middle, new CachedSound(new VorbisWaveReader(@"Resources\Harp\A4.ogg"))},
            {"7" + HarpNote.Octaves.Middle, new CachedSound(new VorbisWaveReader(@"Resources\Harp\B4.ogg"))},
            {"8" + HarpNote.Octaves.Middle, new CachedSound(new VorbisWaveReader(@"Resources\Harp\C5.ogg"))},
            {"1" + HarpNote.Octaves.High, new CachedSound(new VorbisWaveReader(@"Resources\Harp\C5.ogg"))},
            {"2" + HarpNote.Octaves.High, new CachedSound(new VorbisWaveReader(@"Resources\Harp\D5.ogg"))},
            {"3" + HarpNote.Octaves.High, new CachedSound(new VorbisWaveReader(@"Resources\Harp\E5.ogg"))},
            {"4" + HarpNote.Octaves.High, new CachedSound(new VorbisWaveReader(@"Resources\Harp\F5.ogg"))},
            {"5" + HarpNote.Octaves.High, new CachedSound(new VorbisWaveReader(@"Resources\Harp\G5.ogg"))},
            {"6" + HarpNote.Octaves.High, new CachedSound(new VorbisWaveReader(@"Resources\Harp\A5.ogg"))},
            {"7" + HarpNote.Octaves.High, new CachedSound(new VorbisWaveReader(@"Resources\Harp\B5.ogg"))},
            {"8" + HarpNote.Octaves.High, new CachedSound(new VorbisWaveReader(@"Resources\Harp\C6.ogg"))}
        };

        private HarpNote.Octaves _octave = HarpNote.Octaves.Middle;

        public void Press(string key)
        {
            switch (key)
            {
                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                case "7":
                case "8":
                    AudioPlaybackEngine.Instance.PlaySound(Sound[key + _octave]);
                    break;
                case "9":
                    DecreaseOctave();
                    break;
                case "0":
                    IncreaseOctave();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void Release(string key)
        {
        }

        public void PressAndRelease(string key)
        {
        }

        private void PlaySound(string key)
        {
        }

        private void IncreaseOctave()
        {
            switch (_octave)
            {
                case HarpNote.Octaves.None:
                    break;
                case HarpNote.Octaves.Low:
                    _octave = HarpNote.Octaves.Middle;
                    break;
                case HarpNote.Octaves.Middle:
                    _octave = HarpNote.Octaves.High;
                    break;
                case HarpNote.Octaves.High:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void DecreaseOctave()
        {
            switch (_octave)
            {
                case HarpNote.Octaves.None:
                    break;
                case HarpNote.Octaves.Low:
                    break;
                case HarpNote.Octaves.Middle:
                    _octave = HarpNote.Octaves.Low;
                    break;
                case HarpNote.Octaves.High:
                    _octave = HarpNote.Octaves.High;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}