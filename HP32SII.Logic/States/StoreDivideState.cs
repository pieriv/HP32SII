﻿namespace HP32SII.Logic
{
    class StoreDivideState : State
    {
        public StoreDivideState() : base()
        {
            Display = $"STO /  _";
        }

        public override State HandleButton(Button button, EscapeMode escapeMode)
        {
            if (button.Letter != null)
            {
                BottomStatus = "";
                Display = $"STO /  {button.Letter}";
                var storedValue = calculator.Recall(button.Letter);
                if (output.ToDouble() == 0.0)
                {
                    return new DivideByZero();
                }
                else
                {
                    calculator.Store(button.Letter, storedValue / output.ToDouble());
                    return new WaitForDefaultState();
                }
            }
            else if (button == Buttons.Clear)
            {
                Display = output.ToString();
                return new DefaultState();
            }
            else if (button == Buttons.Back)
            {
                return new StoreState();
            }
            else if (button == Buttons.Solve)
            {
                Display = $"STO / (i)";
                return new WaitForInvalidI();
            }
            else
            {
                return this;
            }
        }

        public override State TimerElapsed()
        {
            return new OffState();
        }
    }
}
