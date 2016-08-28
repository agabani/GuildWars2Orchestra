using System;
using GuildWars2Orchestra.Values;

namespace GuildWars2Orchestra.Parsers
{
    public class NoteParser
    {
        public Note Parse(string text)
        {
            var key = ParseKey(text);
            var octave = ParseOctave(text);

            return new Note(key, octave);
        }

        private static Note.Keys ParseKey(string text)
        {
            Note.Keys key;
            switch (text)
            {
                case "C,":
                case "C":
                case "c":
                    key = Note.Keys.Note1;
                    break;
                case "D,":
                case "D":
                case "d":
                    key = Note.Keys.Note2;
                    break;
                case "E,":
                case "E":
                case "e":
                    key = Note.Keys.Note3;
                    break;
                case "F,":
                case "F":
                case "f":
                    key = Note.Keys.Note4;
                    break;
                case "G,":
                case "G":
                case "g":
                    key = Note.Keys.Note5;
                    break;
                case "A,":
                case "A":
                case "a":
                    key = Note.Keys.Note6;
                    break;
                case "B,":
                case "B":
                case "b":
                    key = Note.Keys.Note7;
                    break;
                case "c'":
                    key = Note.Keys.Note8;
                    break;
                default:
                    throw new NotSupportedException(text);
            }
            return key;
        }

        private static Note.Octaves ParseOctave(string text)
        {
            Note.Octaves octave;
            switch (text)
            {
                case "C,":
                case "D,":
                case "E,":
                case "F,":
                case "G,":
                case "A,":
                case "B,":
                    octave = Note.Octaves.Low;
                    break;
                case "C":
                case "D":
                case "E":
                case "F":
                case "G":
                case "A":
                case "B":
                    octave = Note.Octaves.Middle;
                    break;
                case "c":
                case "d":
                case "e":
                case "f":
                case "g":
                case "a":
                case "b":
                case "c'":
                    octave = Note.Octaves.High;
                    break;
                default:
                    throw new NotSupportedException(text);
            }
            return octave;
        }
    }
}