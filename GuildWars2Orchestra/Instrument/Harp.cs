using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GuildWars2Orchestra.Controls;
using GuildWars2Orchestra.Values;

namespace GuildWars2Orchestra.Instrument
{
    public class Harp
    {
        private static readonly TimeSpan NoteTimeout = TimeSpan.FromMilliseconds(10);
        private static readonly TimeSpan OctaveTimeout = TimeSpan.FromTicks(1000);

        private static readonly Dictionary<Note.Keys, string> NoteMap = new Dictionary<Note.Keys, string>
        {
            {Note.Keys.Note1, "1"},
            {Note.Keys.Note2, "2"},
            {Note.Keys.Note3, "3"},
            {Note.Keys.Note4, "4"},
            {Note.Keys.Note5, "5"},
            {Note.Keys.Note6, "6"},
            {Note.Keys.Note7, "7"},
            {Note.Keys.Note8, "8"}
        };

        private readonly IKeyboard _keyboard;

        private Note.Octaves _currentOctave = Note.Octaves.Middle;

        public Harp(IKeyboard keyboard)
        {
            _keyboard = keyboard;
        }

        public async Task PlayNote(Note note)
        {
            if (note.Key != Note.Keys.None)
            {
                note = OptimizeNote(note);
                await PressNote(NoteMap[note.Key]);
            }
        }

        public async Task GoToOctave(Note note)
        {
            if (note.Octave != Note.Octaves.None)
            {
                note = OptimizeNote(note);

                while (_currentOctave != note.Octave)
                {
                    if (_currentOctave < note.Octave)
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

        private Note OptimizeNote(Note note)
        {
            if (note.Equals(new Note(Note.Keys.Note1, Note.Octaves.Middle)) && _currentOctave == Note.Octaves.Low)
            {
                note = new Note(Note.Keys.Note8, Note.Octaves.Low);
            }
            else if (note.Equals(new Note(Note.Keys.Note1, Note.Octaves.High)) && _currentOctave == Note.Octaves.Middle)
            {
                note = new Note(Note.Keys.Note8, Note.Octaves.Middle);
            }
            return note;
        }

        private async Task IncreaseOctave()
        {
            switch (_currentOctave)
            {
                case Note.Octaves.Low:
                    _currentOctave = Note.Octaves.Middle;
                    break;
                case Note.Octaves.Middle:
                    _currentOctave = Note.Octaves.High;
                    break;
                case Note.Octaves.High:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            _keyboard.Press("0");
            _keyboard.Release("0");

//            _keyboard.PressAndRelease("0");

            //return Task.FromResult<object>(null);
            await Task.Delay(OctaveTimeout);
        }

        private async Task DecreaseOctave()
        {
            switch (_currentOctave)
            {
                case Note.Octaves.Low:
                    break;
                case Note.Octaves.Middle:
                    _currentOctave = Note.Octaves.Low;
                    break;
                case Note.Octaves.High:
                    _currentOctave = Note.Octaves.Middle;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            _keyboard.Press("9");
            _keyboard.Release("9");

//            _keyboard.PressAndRelease("9");

            //return Task.FromResult<object>(null);
            await Task.Delay(OctaveTimeout);
        }

        private async Task PressNote(string key)
        {
            _keyboard.Press(key);
            _keyboard.Release(key);

//            _keyboard.PressAndRelease(key);

            await Task.Delay(NoteTimeout);
        }
    }
}