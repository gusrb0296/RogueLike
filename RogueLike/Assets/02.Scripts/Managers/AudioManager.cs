using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // 게임 내 오디오를 관리

    // BGM / SFX 등을 재생하거나 중지

    // 오디오 리소스 로드 / 해제 담당

    // 필요한 효과음이 있다면 주석으로 적어놔 주세요. 보고 추가해두겠습니다.

    private AudioSource _backgroundMusic;
    private AudioSource _soundEffects;

    private Dictionary<string, AudioClip> sfx = new Dictionary<string, AudioClip>();
    private Dictionary<string, AudioClip> bgm = new Dictionary<string, AudioClip>();


    public void InitalizeAudios()
    {
        sfx.Add("bossLongATK", Resources.Load<AudioClip>("Sound/SoundEffect/BossLongDistanceATK"));
        sfx.Add("bossShortATK", Resources.Load<AudioClip>("Sound/SoundEffect/BossShortDistanceATK"));
        sfx.Add("monsterAxeATK", Resources.Load<AudioClip>("Sound/SoundEffect/MonsterAxeATK"));
        sfx.Add("monsterFireATK", Resources.Load<AudioClip>("Sound/SoundEffect/MonsterFireATK"));
        sfx.Add("monsterSwordATK", Resources.Load<AudioClip>("Sound/SoundEffect/MonsterSwordATK"));
        sfx.Add("monsterUpperATK", Resources.Load<AudioClip>("Sound/SoundEffect/MonsterUpperATK"));
        sfx.Add("monsterHit", Resources.Load<AudioClip>("Sound/SoundEffect/MonsterHit"));
        sfx.Add("playerHit", Resources.Load<AudioClip>("Sound/SoundEffect/PlayerHit"));
        sfx.Add("playerNormalATK", Resources.Load<AudioClip>("Sound/SoundEffect/PlayerNormalATK"));
        sfx.Add("buttonClick", Resources.Load<AudioClip>("Sound/SoundEffect/ButtonClick"));
        sfx.Add("gameClear", Resources.Load<AudioClip>("Sound/SoundEffect/GameClear"));
        sfx.Add("jump", Resources.Load<AudioClip>("Sound/SoundEffect/Jump"));
        sfx.Add("potal", Resources.Load<AudioClip>("Sound/SoundEffect/Potal"));
        bgm.Add("normalRoomBGM1", Resources.Load<AudioClip>("Sound/Music/Fractal"));
        bgm.Add("normalRoomBGM2", Resources.Load<AudioClip>("Sound/Music/Frozy"));
        bgm.Add("bossRoomBGM", Resources.Load<AudioClip>("Sound/Music/Jumptowin"));
        bgm.Add("shopRoomBGM", Resources.Load<AudioClip>("Sound/Music/Stars"));
        bgm.Add("startSceneBGM", Resources.Load<AudioClip>("Sound/Music/Nightwind"));

        if (_backgroundMusic == null && _soundEffects == null)
        {
            _backgroundMusic = GetComponent<AudioSource>();
            _soundEffects = GetComponent<AudioSource>();
        }

        _backgroundMusic.loop = true;
        _backgroundMusic.volume = 0.2f;
        _soundEffects.volume = 0.3f;
        _soundEffects.playOnAwake = false;
    }

    public void SFX(string name)
    {
        _soundEffects.PlayOneShot(sfx[name]);
    }

    public void SFX(AudioClip filp)
    {
        _soundEffects.PlayOneShot(filp);
    }

    public void BGM(string name)
    {
        _backgroundMusic.Stop();
        _backgroundMusic.clip = bgm[name];
        _backgroundMusic.Play();
    }

    public float GetVolume()
    {
        return _backgroundMusic.volume;
    }

    public void SetVolume(float volume)
    {
        _backgroundMusic.volume = volume;
    }

}

/*
    [Header("AudioClip")]
    [SerializeField]
    private AudioClip[] audioClips;
    [SerializeField]
    private string[] soundName;


    // SFX
    // Monster
    private AudioClip bossLongATK;
    private AudioClip bossShortATK;
    private AudioClip monsterAxeATK;
    private AudioClip monsterFireATK;
    private AudioClip monsterSwordATK;
    private AudioClip monsterUpperATK;
    private AudioClip monsterHit;

    // Player
    private AudioClip playerHit;
    private AudioClip playerNormalATK;

    // Besides
    private AudioClip buttonClick;
    private AudioClip gameClear;
    private AudioClip jump;
    private AudioClip potal;        // sdsdsdsds

    // BGM
    private AudioClip normalRoomBGM1;
    private AudioClip normalRoomBGM2;
    private AudioClip bossRoomBGM;
    private AudioClip shopRoomBGM;
    private AudioClip startSceneBGM;




    // SFX
    public void BossLongATK() => _soundEffects.PlayOneShot(bossLongATK);
    public void BossShortATK() => _soundEffects.PlayOneShot(bossShortATK);
    public void MonsterAxeATK() => _soundEffects.PlayOneShot(monsterAxeATK);
    public void MonsterFireATK() => _soundEffects.PlayOneShot(monsterFireATK);
    public void MonsterSwordATK() => _soundEffects.PlayOneShot(monsterSwordATK);
    public void MonsterUpperATK() => _soundEffects.PlayOneShot(monsterUpperATK);
    public void MonsterHit() => _soundEffects.PlayOneShot(monsterHit);
    public void PlayerHit() => _soundEffects.PlayOneShot(playerHit);
    public void PlayerNormalATK() => _soundEffects.PlayOneShot(playerNormalATK);
    public void ButtonClick() => _soundEffects.PlayOneShot(buttonClick);
    public void GameClear() => _soundEffects.PlayOneShot(gameClear);
    public void Jump() => _soundEffects.PlayOneShot(jump);

    // BGM
    public void NormalRoomBGM1() => _backgroundMusic.clip = normalRoomBGM1;
    public void NormalRoomBGM2() => _backgroundMusic.clip = normalRoomBGM2;
    public void BossRoomBGM() => _backgroundMusic.clip = bossRoomBGM;
    public void ShopRoomBGM() => _backgroundMusic.clip = shopRoomBGM;
    public void StartSceneBGM() => _backgroundMusic.clip = startSceneBGM;

    public void BGMStart() => _backgroundMusic.Play();
    public void BGMStop() => _backgroundMusic.Stop();
 */