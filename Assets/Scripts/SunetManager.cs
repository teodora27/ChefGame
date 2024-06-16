using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunetManager : MonoBehaviour
{
    private const string PLAYER_PREFS_SOUND_EFFECTS_VOLUME = "SoundEffectsVolume";

    public static SunetManager Instance {  get; private set; }
    [SerializeField] private AudioClipsSO clip_audio_SO;

    private float volum=1f;

    private void Awake()
    {
        Instance = this;
        volum = PlayerPrefs.GetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, 1f);
    }
    private void Start()
    {
        LivrariManager.Instanta.Cand_Reteta_E_Buna += Livrare_Cand_Reteta_E_Buna;
        LivrariManager.Instanta.Cand_Reteta_Nu_E_Buna += Livrare_Cand_Reteta_Nu_E_Buna;
        DulapTaiere.Cand_Taie_Ceva += DulapTaiere_Cand_Taie_Ceva;
        Jucator.Instanta.Cand_Apuca_Ceva += Jucator_Cand_Apuca_Ceva;
        BazaDulap.Cand_e_Asezat_Un_Obiect += BazaDulap_Cand_e_Asezat_Un_Obiect;
        Gunoi.Cand_Obiectul_E_Aruncat += Gunoi_Cand_Obiectul_E_Aruncat;
    }

    private void Gunoi_Cand_Obiectul_E_Aruncat(object sender, System.EventArgs e)
    {
        Gunoi gunoi = sender as Gunoi;
        PlaySunet(clip_audio_SO.gunoi, gunoi.transform.position);
    }

    private void BazaDulap_Cand_e_Asezat_Un_Obiect(object sender, System.EventArgs e)
    {
        BazaDulap baza_dulap = sender as BazaDulap;
        PlaySunet(clip_audio_SO.luat_obiect, baza_dulap.transform.position);
    }

    private void Jucator_Cand_Apuca_Ceva(object sender, System.EventArgs e)
    {
        PlaySunet(clip_audio_SO.luat_obiect, Jucator.Instanta.transform.position);
    }

    private void DulapTaiere_Cand_Taie_Ceva(object sender, System.EventArgs e)
    {
        DulapTaiere dulap_taiere = sender as DulapTaiere;
        PlaySunet(clip_audio_SO.taiat, dulap_taiere.transform.position);
    }

    private void Livrare_Cand_Reteta_Nu_E_Buna(object sender, System.EventArgs e)
    {
        DulapLivrari dulap_livrari = DulapLivrari.Instance;
        PlaySunet(clip_audio_SO.livrare_fail, dulap_livrari.transform.position);
    }

    private void Livrare_Cand_Reteta_E_Buna(object sender, System.EventArgs e)
    {
        DulapLivrari dulap_livrari = DulapLivrari.Instance;
        PlaySunet(clip_audio_SO.livrare_buna, dulap_livrari.transform.position);
    }

    private void PlaySunet(AudioClip[] clip_audio_vector, Vector3 pozitie, float volum=1f)
    {
        PlaySunet(clip_audio_vector[Random.Range(0, clip_audio_vector.Length)], pozitie, volum);
    }

    private void PlaySunet(AudioClip clip_audio, Vector3 pozitie, float volum = 1f)
    {
        AudioSource.PlayClipAtPoint(clip_audio, pozitie, volum);
    }
    public void PlayPasiSunet(Vector3 pozitie, float volum_multiplier)
    {
        PlaySunet(clip_audio_SO.footsteps, pozitie, volum_multiplier*volum);
    }
    public void PlayNumaratoareInversaSunet()
    {
        PlaySunet(clip_audio_SO.atentie, Vector3.zero);
    }
    public void PlaySemnalArdere(Vector3 pozitie)
    {
        PlaySunet(clip_audio_SO.atentie, pozitie);
    }
    public void SchimbaVolumul()
    {
        volum += .1f;
        if(volum>1f)
        {
            volum = 0f;
        }
        PlayerPrefs.SetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, volum);
        PlayerPrefs.Save();
    }
    public float GetVolum()
    {
        return volum;
    }
}
