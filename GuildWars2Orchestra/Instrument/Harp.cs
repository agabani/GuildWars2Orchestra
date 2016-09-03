using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GuildWars2Orchestra.Controls;
using GuildWars2Orchestra.Domain.Values;

namespace GuildWars2Orchestra.Instrument
{
    public class Harp
    {
        private static readonly Dictionary<string, HarpNote> Map = new Dictionary<string, HarpNote>
        {
            {$"{Note.Keys.None}{Note.Octaves.None}", new HarpNote(HarpNote.Keys.None, HarpNote.Octaves.None)},
            {$"{Note.Keys.C}{Note.Octaves.Low}", new HarpNote(HarpNote.Keys.Note1, HarpNote.Octaves.Low)},
            {$"{Note.Keys.D}{Note.Octaves.Low}", new HarpNote(HarpNote.Keys.Note2, HarpNote.Octaves.Low)},
            {$"{Note.Keys.E}{Note.Octaves.Low}", new HarpNote(HarpNote.Keys.Note3, HarpNote.Octaves.Low)},
            {$"{Note.Keys.F}{Note.Octaves.Low}", new HarpNote(HarpNote.Keys.Note4, HarpNote.Octaves.Low)},
            {$"{Note.Keys.G}{Note.Octaves.Low}", new HarpNote(HarpNote.Keys.Note5, HarpNote.Octaves.Low)},
            {$"{Note.Keys.A}{Note.Octaves.Low}", new HarpNote(HarpNote.Keys.Note6, HarpNote.Octaves.Low)},
            {$"{Note.Keys.B}{Note.Octaves.Low}", new HarpNote(HarpNote.Keys.Note7, HarpNote.Octaves.Low)},
            {$"{Note.Keys.C}{Note.Octaves.Middle}", new HarpNote(HarpNote.Keys.Note1, HarpNote.Octaves.Middle)},
            {$"{Note.Keys.D}{Note.Octaves.Middle}", new HarpNote(HarpNote.Keys.Note2, HarpNote.Octaves.Middle)},
            {$"{Note.Keys.E}{Note.Octaves.Middle}", new HarpNote(HarpNote.Keys.Note3, HarpNote.Octaves.Middle)},
            {$"{Note.Keys.F}{Note.Octaves.Middle}", new HarpNote(HarpNote.Keys.Note4, HarpNote.Octaves.Middle)},
            {$"{Note.Keys.G}{Note.Octaves.Middle}", new HarpNote(HarpNote.Keys.Note5, HarpNote.Octaves.Middle)},
            {$"{Note.Keys.A}{Note.Octaves.Middle}", new HarpNote(HarpNote.Keys.Note6, HarpNote.Octaves.Middle)},
            {$"{Note.Keys.B}{Note.Octaves.Middle}", new HarpNote(HarpNote.Keys.Note7, HarpNote.Octaves.Middle)},
            {$"{Note.Keys.C}{Note.Octaves.High}", new HarpNote(HarpNote.Keys.Note1, HarpNote.Octaves.High)},
            {$"{Note.Keys.D}{Note.Octaves.High}", new HarpNote(HarpNote.Keys.Note2, HarpNote.Octaves.High)},
            {$"{Note.Keys.E}{Note.Octaves.High}", new HarpNote(HarpNote.Keys.Note3, HarpNote.Octaves.High)},
            {$"{Note.Keys.F}{Note.Octaves.High}", new HarpNote(HarpNote.Keys.Note4, HarpNote.Octaves.High)},
            {$"{Note.Keys.G}{Note.Octaves.High}", new HarpNote(HarpNote.Keys.Note5, HarpNote.Octaves.High)},
            {$"{Note.Keys.A}{Note.Octaves.High}", new HarpNote(HarpNote.Keys.Note6, HarpNote.Octaves.High)},
            {$"{Note.Keys.B}{Note.Octaves.High}", new HarpNote(HarpNote.Keys.Note7, HarpNote.Octaves.High)},
            {$"{Note.Keys.C}{Note.Octaves.Highest}", new HarpNote(HarpNote.Keys.Note8, HarpNote.Octaves.High)}
        };

        private static readonly TimeSpan NoteTimeout = TimeSpan.FromMilliseconds(5);
        private static readonly TimeSpan OctaveTimeout = TimeSpan.FromTicks(500);

        private static readonly Dictionary<HarpNote.Keys, string> NoteMap = new Dictionary<HarpNote.Keys, string>
        {
            {HarpNote.Keys.Note1, "1"},
            {HarpNote.Keys.Note2, "2"},
            {HarpNote.Keys.Note3, "3"},
            {HarpNote.Keys.Note4, "4"},
            {HarpNote.Keys.Note5, "5"},
            {HarpNote.Keys.Note6, "6"},
            {HarpNote.Keys.Note7, "7"},
            {HarpNote.Keys.Note8, "8"}
        };

        private readonly IKeyboard _keyboard;

        private HarpNote.Octaves _currentOctave = HarpNote.Octaves.Middle;

        public Harp(IKeyboard keyboard)
        {
            _keyboard = keyboard;
        }

        public async Task PlayNote(Note note)
        {
            var harpNote = Map[$"{note.Key}{note.Octave}"];

            if (harpNote.Key != HarpNote.Keys.None)
            {
                harpNote = OptimizeNote(harpNote);
                await PressNote(NoteMap[harpNote.Key]);
            }
        }

        public async Task GoToOctave(Note note)
        {
            var harpNote = Map[$"{note.Key}{note.Octave}"];

            if (harpNote.Octave != HarpNote.Octaves.None)
            {
                harpNote = OptimizeNote(harpNote);

                while (_currentOctave != harpNote.Octave)
                {
                    if (_currentOctave < harpNote.Octave)
                    {
                        await IncreaseOctave();
                    }
                    else
                    {
                        await DecreaseOctave();
                    }
                }
            }
        }

        private HarpNote OptimizeNote(HarpNote note)
        {
            if (note.Equals(new HarpNote(HarpNote.Keys.Note1, HarpNote.Octaves.Middle)) && _currentOctave == HarpNote.Octaves.Low)
            {
                note = new HarpNote(HarpNote.Keys.Note8, HarpNote.Octaves.Low);
            }
            else if (note.Equals(new HarpNote(HarpNote.Keys.Note1, HarpNote.Octaves.High)) && _currentOctave == HarpNote.Octaves.Middle)
            {
                note = new HarpNote(HarpNote.Keys.Note8, HarpNote.Octaves.Middle);
            }
            return note;
        }

        private async Task IncreaseOctave()
        {
            switch (_currentOctave)
            {
                case HarpNote.Octaves.Low:
                    _currentOctave = HarpNote.Octaves.Middle;
                    break;
                case HarpNote.Octaves.Middle:
                    _currentOctave = HarpNote.Octaves.High;
                    break;
                case HarpNote.Octaves.High:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            _keyboard.Press("0");
            _keyboard.Release("0");

            await Task.Delay(OctaveTimeout);
        }

        private async Task DecreaseOctave()
        {
            switch (_currentOctave)
            {
                case HarpNote.Octaves.Low:
                    break;
                case HarpNote.Octaves.Middle:
                    _currentOctave = HarpNote.Octaves.Low;
                    break;
                case HarpNote.Octaves.High:
                    _currentOctave = HarpNote.Octaves.Middle;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            _keyboard.Press("9");
            _keyboard.Release("9");

            await Task.Delay(OctaveTimeout);
        }

        private async Task PressNote(string key)
        {
            _keyboard.Press(key);
            _keyboard.Release(key);

            await Task.Delay(NoteTimeout);
        }
    }
}