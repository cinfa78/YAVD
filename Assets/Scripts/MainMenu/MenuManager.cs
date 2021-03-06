﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class MenuManager : MonoBehaviour
{

    public Text txt_Title;
    public string textToShowOnTitle;
    string titleNoPrompt;
    public Button startNewGame;
    public Button continueGame;
    public SAudio keyboardClick;
    public SAudio blip;
    Coroutine cursorBlinkingCoroutine;
    //public ASAudioEvent stepSound;
    AudioSource audioSource;
    bool playingCutscene = true;
    float cutsceneSpeed = 1f;
    public Texture2D cursorTexture;
    public int gameScene = 1;
    bool saveGameExists;
    public SPlayerStats playerStats;
    public SPlayerStats defaultPlayerStats;

    // Use this for initialization
    void Start()
    {
        Cursor.visible = false;
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
        titleNoPrompt = txt_Title.text;
        //StartCoroutine(CursorBlinker());
        startNewGame.gameObject.SetActive(false);
        continueGame.gameObject.SetActive(false);
        audioSource = GetComponent<AudioSource>();
        playingCutscene = true;
        saveGameExists = SaveGameManager.instance.SaveGameExists();
        StartCoroutine(AnimateTitle());
    }

    IEnumerator CursorBlinker()
    {

        while (true)
        {
            txt_Title.text = titleNoPrompt + " ";
            yield return new WaitForSeconds(0.8f * cutsceneSpeed);
            txt_Title.text = titleNoPrompt + "_";
            //keyboardClick.Play(audioSource);
            yield return new WaitForSeconds(0.8f);
        }
    }
    IEnumerator AnimateTitle()
    {
        cursorBlinkingCoroutine = StartCoroutine(CursorBlinker());
        yield return new WaitForSeconds(2f * cutsceneSpeed);
        StopCoroutine(cursorBlinkingCoroutine);
        string tempString = "";
        for (int i = 0; i <= textToShowOnTitle.Length; i++)
        {
            tempString = textToShowOnTitle.Substring(0, i);
            txt_Title.text = tempString;
            keyboardClick.Play(audioSource);
            yield return new WaitForSeconds(Random.Range(0.02f, 0.1f) * cutsceneSpeed);
        }
        keyboardClick.Play(audioSource);
        yield return new WaitForSeconds(0.6f * cutsceneSpeed);
        blip.Play(audioSource);
        tempString += "\nLoading...\n";
        txt_Title.text = tempString;
        titleNoPrompt = txt_Title.text;
        cursorBlinkingCoroutine = StartCoroutine(CursorBlinker());
        yield return new WaitForSeconds(2f * cutsceneSpeed);
        blip.Play(audioSource);
        tempString += "\nReady.";
        txt_Title.text = tempString;
        titleNoPrompt = txt_Title.text;
        yield return new WaitForSeconds(0.5f * cutsceneSpeed);
        blip.Play(audioSource);
        startNewGame.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f * cutsceneSpeed);

        //SaveGameManager.instance.Load();
        if (saveGameExists)
        {
            blip.Play(audioSource);
            continueGame.gameObject.SetActive(true);
        }

        playingCutscene = false;
        cutsceneSpeed = 1f;
        //Cursor.visible = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey && playingCutscene)
        {
            cutsceneSpeed = 0f;
        }
        if (!playingCutscene)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                StartNewGame();
            }
            if (saveGameExists && Input.GetKeyDown(KeyCode.C))
            {
                ContinueGame();
            }
        }
    }
    public void StartNewGame()
    {
        defaultPlayerStats.CopyTo(playerStats);
        blip.Play(audioSource);
        StartCoroutine(LoadNewScene(gameScene));
        Debug.Log("Start new game");
    }
    public void ContinueGame()
    {
        SaveGameManager.instance.LoadGame();
        blip.Play(audioSource);
        Debug.Log("Continue game");
        StartCoroutine(LoadNewScene(gameScene));
    }

    IEnumerator LoadNewScene(int scene)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(scene);
        while (!async.isDone)
        {
            yield return null;
        }
    }
}
