﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Windows.Speech;


namespace AR.Core.Speech
{
    public class SpeechProcessing : MonoBehaviour
    {
        [SerializeField]
        private string[] m_Keywords = { "Dense", "Sparce", "Test", "Badger" };
        private Graph.Graph m_graph;
        private KeywordRecognizer m_Recognizer;


        public SpeechProcessing(string[] Keywords, Graph.Graph g)
        {
            m_Keywords = Keywords;
            m_graph = g;
        }

        public void Start() 
        {
            m_Recognizer = new KeywordRecognizer(m_Keywords);
            m_Recognizer.OnPhraseRecognized += OnPhraseRecognized;
            m_Recognizer.Start();
        }

        private void OnPhraseRecognized(PhraseRecognizedEventArgs args)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("{0} ({1}){2}", args.text, args.confidence, Environment.NewLine);
            builder.AppendFormat("\tTimestamp: {0}{1}", args.phraseStartTime, Environment.NewLine);
            builder.AppendFormat("\tDuration: {0} seconds{1}", args.phraseDuration.TotalSeconds, Environment.NewLine);
            Debug.Log(builder.ToString());
        }


    }
}
