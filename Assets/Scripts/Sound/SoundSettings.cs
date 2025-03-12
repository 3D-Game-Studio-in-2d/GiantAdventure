using System;

public class SoundSettings
{
        private float _masterVolume = 1f;
        private float _sfxVolume = 1f;
        private float _musicVolume = 1f;

        public float MasterVolume
        {
                get
                {
                        return _masterVolume;
                }
                
                set
                {
                        _musicVolume = Math.Clamp(value, 0f, 1f);
                }
        }

        public float SfxVolume {
                get
                {
                        return _sfxVolume * MasterVolume;
                }
                
                set
                {
                        _sfxVolume = Math.Clamp(value, 0f, 1f);
                }
        }
        
        public float MusicVolume {
                get
                {
                        return _musicVolume * MasterVolume;
                }

                set
                {
                        _musicVolume = Math.Clamp(value, 0f, 1f);
                }
        }
}