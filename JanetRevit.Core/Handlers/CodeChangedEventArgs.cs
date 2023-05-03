using System;

namespace RevitSwitchAddin
{
    public class CodeChangedEventArgs: EventArgs
    {
        public string Code { get; set; }

        public CodeChangedEventArgs(string code)
        {
            Code = code;
        }
    }
}