using System;
using GuildWars2Orchestra.GuildWars2.Controls;
using GuildWars2Orchestra.GuildWars2.Instrument;

namespace GuildWars2Orchestra.Audio.Emulation
{
    public class HarpEmulator : IKeyboard
    {
        private HarpNote.Octaves _octave = HarpNote.Octaves.Middle;

        public void Press(string key)
        {
            switch (key)
            {
                case "1":
                    break;
                case "2":
                    break;
                case "3":
                    break;
                case "4":
                    break;
                case "5":
                    break;
                case "6":
                    break;
                case "7":
                    break;
                case "8":
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