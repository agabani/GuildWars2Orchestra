using System;
using GuildWars2Orchestra.Audio.Repositories;
using GuildWars2Orchestra.Audio.Sound;
using GuildWars2Orchestra.GuildWars2.Controls;
using GuildWars2Orchestra.GuildWars2.Instrument;

namespace GuildWars2Orchestra.Audio.Emulators
{
    public class HarpEmulator : IKeyboard
    {
        private HarpNote.Octaves _octave = HarpNote.Octaves.Middle;

        private readonly HarpSoundRepository _soundRepository = new HarpSoundRepository();

        public void Press(GuildWarsKeyboard.Controls key)
        {
            switch (key)
            {
                case GuildWarsKeyboard.Controls.WeaponSkill1:
                case GuildWarsKeyboard.Controls.WeaponSkill2:
                case GuildWarsKeyboard.Controls.WeaponSkill3:
                case GuildWarsKeyboard.Controls.WeaponSkill4:
                case GuildWarsKeyboard.Controls.WeaponSkill5:
                case GuildWarsKeyboard.Controls.HealingSkill:
                case GuildWarsKeyboard.Controls.UtilitySkill1:
                case GuildWarsKeyboard.Controls.UtilitySkill2:
                    AudioPlaybackEngine.Instance.PlaySound(_soundRepository.Get(key, _octave));
                    break;
                case GuildWarsKeyboard.Controls.UtilitySkill3:
                    DecreaseOctave();
                    break;
                case GuildWarsKeyboard.Controls.EliteSkill:
                    IncreaseOctave();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void Release(GuildWarsKeyboard.Controls key)
        {
        }

        public void PressAndRelease(GuildWarsKeyboard.Controls key)
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