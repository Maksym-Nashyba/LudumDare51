using System;

namespace Misc
{
    public class EntryCutscene: Cutscene
    {
        private bool _isButtonPressed;
        
        public override void Show(Action callback)
        {
            
        }

        public void OnPlayButtonPressed()
        {
            _isButtonPressed = true;
        }
    }
}