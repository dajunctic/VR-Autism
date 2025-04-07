using System;
using System.IO;
using HuggingFace.API;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SpeechRecognition : MonoBehaviour {
    [SerializeField] private SpeechResponser speechResponser;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private int clipLengthCycle = 10;
    [SerializeField] private float checkInterval = 5f;
    private AudioClip clip;
    private byte[] bytes;
    private bool recording;
    
    public void StartRecording() {
        text.color = Color.white;
        text.text = "Listening...";
        clip = Microphone.Start(null, true, clipLengthCycle, 44100);
        recording = true;
        StartCoroutine(SendRecordingPeriodically());
    }

    private void StopRecordingAndRestart() {
        if (clip == null || clip.samples == 0) {
            Debug.LogError("AudioClip is null or has no data!");
            return;
        }

        int position = Microphone.GetPosition(null);
        if (position <= 0) {
            // Debug.LogError("Microphone position is invalid!");
            return;
        }

        Microphone.End(null);
    
        int sampleCount = Math.Min(position * clip.channels, clip.samples);
        float[] samples = new float[sampleCount];

        try {
            clip.GetData(samples, 0);
        } catch (Exception e) {
            Debug.LogError("Error getting audio data: " + e.Message);
            return;
        }

        bytes = EncodeAsWAV(samples, clip.frequency, clip.channels);
        recording = false;
        SendRecording();
        StartRecording();
    }

    public void StopRecording()
    {
        recording = false;
        Microphone.End(null);
    }

    private IEnumerator SendRecordingPeriodically() {
        while (recording) {
            yield return new WaitForSeconds(checkInterval);
            StopRecordingAndRestart();
        }
    }

    private void SendRecording() {
        text.color = Color.yellow;
        text.text = "Processing...";
        HuggingFaceAPI.AutomaticSpeechRecognition(bytes, response => {
            text.color = Color.white;
            text.text = response;
            AnalyzeSpeech(response);
        }, error => {
            Debug.LogError(error);
            text.color = Color.red;
            text.text = error;
        });
    }

    private void AnalyzeSpeech(string speech) {
        if (string.IsNullOrEmpty(speech))
        {
            speech = "";
        }

        if (speechResponser != null)
        {
            speechResponser.AnalyzeSpeech(speech);
        }
        
    }

    private byte[] EncodeAsWAV(float[] samples, int frequency, int channels) {
        using (var memoryStream = new MemoryStream(44 + samples.Length * 2)) {
            using (var writer = new BinaryWriter(memoryStream)) {
                writer.Write("RIFF".ToCharArray());
                writer.Write(36 + samples.Length * 2);
                writer.Write("WAVE".ToCharArray());
                writer.Write("fmt ".ToCharArray());
                writer.Write(16);
                writer.Write((ushort)1);
                writer.Write((ushort)channels);
                writer.Write(frequency);
                writer.Write(frequency * channels * 2);
                writer.Write((ushort)(channels * 2));
                writer.Write((ushort)16);
                writer.Write("data".ToCharArray());
                writer.Write(samples.Length * 2);
                foreach (var sample in samples) {
                    writer.Write((short)(sample * short.MaxValue));
                }
            }
            return memoryStream.ToArray();
        }
    }
}
