using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using Istina;
using Istina.Parser;
using System.Runtime.InteropServices;
using System.Reflection;
using Microsoft.Xna.Framework.Audio;
using System.IO;
using System.Diagnostics;

namespace LD54.UI.Component
{
    internal class InputBox
    {
        private SpriteFont _font;
        private UserInterface _ui;
        private StringBuilder _characters = new StringBuilder();
        private string _prompt = "> ";
        public readonly Vector2 position;
        private Action<string> _processInput;
        private readonly SoundEffect sfx;
        public readonly State state;
        private string NL = Environment.NewLine;
        public InputBox(SpriteFont font, Vector2 position, UserInterface ui, Action<string> processInput)
        {
            _font = font;
            _ui = ui;
            this.position = position;
            this._processInput = processInput;

            sfx = SoundEffect.FromFile("sfx" + Path.DirectorySeparatorChar + "uip.wav");

            _characters.Append(_prompt);

            //Enable();
            state = State.BuildFromString(
                "inputBox",
                "@input:string: " + NL +
                "processing,enabled,enable" + NL +
                "enabled,processing,process"
                ,
                new NaiveCsvParser());

            state.StateChanged += StateUpdate;
        }

        public void Draw()
        {
            _ui.spriteBatch.DrawString(_font, _characters, position, Color.WhiteSmoke);
        }

        public void StateUpdate(object sender, string newState)
        {
            typeof(InputBox).GetMethod("On_" + newState, BindingFlags.NonPublic | BindingFlags.Instance)?.Invoke(this, null);
        }

        private void On_enabled()
        {
            Enable();
        }

        private void On_processing()
        {
            Clear();
            Disable();
        }

        private string GetInput()
        {
            return _characters.ToString().Remove(0, _prompt.Length);
        }

        private void Clear()
        {
            _characters.Clear();
            _characters.Append(_prompt);
        }

        private void Enable()
        {
            _ui.window.TextInput += OnInput;
        }

        private void Disable()
        {
            _ui.window.TextInput -= OnInput;
        }

        public void OnInput(object sender, TextInputEventArgs e)
        {
            var k = e.Key;
            var c = e.Character;
            if (char.IsLetter(c) || c == (char)Keys.Space)
            {
                c = char.ToUpper(c);
                _characters.Append(c);
                state.fieldTable.SetString("input", GetInput());
                sfx.Play();
            }
            if (c == (char)Keys.Back && _characters.Length > _prompt.Length)
            {
                _characters.Length--;
                state.fieldTable.SetString("input", GetInput());
            }
            if (c == (char)Keys.Enter)
            {
                string input = GetInput();
                Debug.WriteLine("input:" + input);
                state.ReceiveEvent("process");

                _processInput(input);
            }

            //if (char.IsControl(c))
        }
    }
}
