using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GuildWars2Orchestra.Domain.Values;
using GuildWars2Orchestra.GuildWars2.Controls;

namespace GuildWars2Orchestra.GuildWars2.Instrument
{
    public class Harp
    {
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
            var harpNote = HarpNote.From(note);

            if (RequiresAction(harpNote))
            {
                harpNote = OptimizeNote(harpNote);
                await PressNote(NoteMap[harpNote.Key]);
            }
        }

        public async Task GoToOctave(Note note)
        {
            var harpNote = HarpNote.From(note);

            if (RequiresAction(harpNote))
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

        private static bool RequiresAction(HarpNote harpNote)
        {
            return harpNote.Key != HarpNote.Keys.None;
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