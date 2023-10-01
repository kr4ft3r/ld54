using LD54.UI.Component;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD54.UI
{
    internal class TypeWriter
    {
        public readonly TextWindow textWindow;
        private readonly SoundEffect sfx;
        public TypeWriter(TextWindow textWindow) 
        { 
            this.textWindow = textWindow;

            sfx = SoundEffect.FromFile("sfx" + Path.DirectorySeparatorChar + "prp.wav");
        }

        public readonly Queue<string> paragraphs = new Queue<string>();

        private string _currentParagraph = "";

        private double _elapsed;

        public void Update(double elapsed) 
        {
            if (_currentParagraph == "" && paragraphs.Count == 0) return;

            _elapsed += elapsed;
            double step = 0.05;//.02;
            if (_elapsed >= step)
            {
                _elapsed -= step;
            }
            else return;

            string msg = _currentParagraph == "" ? paragraphs.Dequeue() : _currentParagraph;
            char type = msg[0];
            textWindow.TypeIn(type);
            msg = msg.Remove(0, 1);
            if (msg.Length == 0)
            {
                textWindow.NewLine();
                textWindow.NewLine();
            }
            _currentParagraph = msg;
            sfx.Play(1.0f, -.25f + new Random().NextSingle()*.5f, 0f);
        }
    }
}
