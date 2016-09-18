﻿using System.Linq;
using System.Text;
using GuildWars2Orchestra.Domain.Values;

namespace GuildWars2Orchestra.MusicBoxNotation.Serializer
{
    public class ChordSerializer
    {
        private readonly NoteSerializer _noteSerializer;

        public ChordSerializer(NoteSerializer noteSerializer)
        {
            _noteSerializer = noteSerializer;
        }

        public string Serialize(Chord chord)
        {
            var stringBuilder = new StringBuilder();

            if (chord.Notes.Count() > 1)
            {
                stringBuilder.Append("[");
            }

            foreach (var note in chord.Notes)
            {
                stringBuilder.Append(_noteSerializer.Serialize(note));
            }

            if (chord.Notes.Count() > 1)
            {
                stringBuilder.Append("]");
            }

            if (chord.Length.Nominator != 1)
            {
                stringBuilder.Append(chord.Length.Nominator);
            }

            if (chord.Length.Denominator != 1)
            {
                stringBuilder.Append("/");
                stringBuilder.Append(chord.Length.Denominator);
            }

            return stringBuilder.ToString();
        }
    }
}