using System.IO;
using HuggingFace.API;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SpeechRecognition : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI text;
    private AudioClip clip;
    private byte[] bytes;
    private bool recording;
    private float checkInterval = 5f; // Kiểm tra mỗi 5 giây
    
    private void Start() {
        // StartRecording(); // Bắt đầu ghi âm ngay khi scene chạy
    }

    private void Update() {
        if (recording && Microphone.GetPosition(null) >= clip.samples) {
            StopRecordingAndRestart();
        }
    }

    public void StartRecording() {
        text.color = Color.white;
        text.text = "Listening...";
        clip = Microphone.Start(null, true, 10, 44100);
        recording = true;
        StartCoroutine(SendRecordingPeriodically());
    }

    private void StopRecordingAndRestart() {
        var position = Microphone.GetPosition(null);
        Microphone.End(null);
        var samples = new float[position * clip.channels];
        clip.GetData(samples, 0);
        bytes = EncodeAsWAV(samples, clip.frequency, clip.channels);
        recording = false;
        SendRecording();
        StartRecording(); // Bắt đầu ghi âm lại
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
            text.color = Color.red;
            text.text = error;
        });
    }

    private void AnalyzeSpeech(string speech) {
        if (string.IsNullOrEmpty(speech)) return;

        if (speech.Contains("tên")) {
            text.text = "Con vừa nói tên! Tốt lắm!";
        } else if (speech.Contains("tuổi")) {
            text.text = "Giờ hãy nói tuổi của con!";
        } else if (speech.Contains("thích")) {
            text.text = "Hãy nói sở thích của con!";
        } else {
            text.text = "Cố gắng nói rõ hơn nhé!";
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
