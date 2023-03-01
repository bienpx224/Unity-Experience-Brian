# Unity-Experience-Brian
Base experience about Unity : 

# Sound Manager: 
- Package manager : Search Eazy Sound Manager và import về.
- Sử dụng file Sounds.cs để quản lý SOund. 
- Tại Scene view, tạo game object, kéo file Sounds vào và setup các file âm thanh. 
- Để gọi, play sound ở đâu đó : 
    + import đầu file : using Hellmade.Sound;
    + Gọi function để play 1 sound :  EazySoundManager.PlaySound(Sounds.Instance.Sfx_Skill_1);
    + VD để chơi nhạc nền : EazySoundManager.GetMusicAudio(Sounds.Instance.sfx_music_main).Play();