using System;
using System.Collections.Generic;
using System.Threading;
using GuildWars2Orchestra.Controls;
using GuildWars2Orchestra.Values;

namespace GuildWars2Orchestra.Instrument
{
    public class Harp
    {
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
        public readonly int NoteTimeout = 5;
        private const int OctaveTimeout = 1;

        private Note.Octaves _currentOctave = Note.Octaves.Middle;

        public Harp(IKeyboard keyboard)
        {
            _keyboard = keyboard;
        }

        public void PlayNote(Note note)
        {
            note = OptimizeNote(note);

            GoToOctave(note.Octave);
            PressNote(NoteMap[note.Key]);
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

        private void GoToOctave(Note.Octaves targetOctave)
        {
            while (_currentOctave != targetOctave)
            {
                if (_currentOctave < targetOctave)
                {
                    IncreaseOctave();
                }
                else
                {
                    DecreaseOctave();
                }
            }
        }

        private void IncreaseOctave()
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
            Thread.Sleep(OctaveTimeout);
            _keyboard.Release("0");
        }

        private void DecreaseOctave()
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
            Thread.Sleep(OctaveTimeout);
            _keyboard.Release("9");
        }

        private void PressNote(string key)
        {
            _keyboard.Press(key);
            Thread.Sleep(NoteTimeout);
            _keyboard.Release(key);
        }
    }
}