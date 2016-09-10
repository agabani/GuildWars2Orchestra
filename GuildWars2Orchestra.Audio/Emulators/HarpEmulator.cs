using System;
using GuildWars2Orchestra.Audio.Sound;
using GuildWars2Orchestra.GuildWars2.Controls;
using GuildWars2Orchestra.GuildWars2.Instrument;

namespace GuildWars2Orchestra.Audio.Emulators
{
    public class HarpEmulator : IKeyboard
    {
        private HarpNote.Octaves _octave = HarpNote.Octaves.Middle;

        private readonly HarpSoundRepository _soundRepository = new HarpSoundRepository();

        public void Press(GuildWarsKeyboard.GuildWarsSkill key)
        {
            switch (key)
            {
                case GuildWarsKeyboard.GuildWarsSkill.Skill1:
                case GuildWarsKeyboard.GuildWarsSkill.Skill2:
                case GuildWarsKeyboard.GuildWarsSkill.Skill3:
                case GuildWarsKeyboard.GuildWarsSkill.Skill4:
                case GuildWarsKeyboard.GuildWarsSkill.Skill5:
                case GuildWarsKeyboard.GuildWarsSkill.Skill6:
                case GuildWarsKeyboard.GuildWarsSkill.Skill7:
                case GuildWarsKeyboard.GuildWarsSkill.Skill8:
                    AudioPlaybackEngine.Instance.PlaySound(_soundRepository.Get(key, _octave));
                    break;
                case GuildWarsKeyboard.GuildWarsSkill.Skill9:
                    DecreaseOctave();
                    break;
                case GuildWarsKeyboard.GuildWarsSkill.Skill0:
                    IncreaseOctave();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void Release(GuildWarsKeyboard.GuildWarsSkill key)
        {
        }

        public void PressAndRelease(GuildWarsKeyboard.GuildWarsSkill key)
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