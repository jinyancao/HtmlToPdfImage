using System;

namespace Czar.HtmlToPdfImage.Options
{
    public class OptionFlag : Attribute
    {
        public string Name { get; private set; }

        public OptionFlag(string name)
        {
            Name = name;
        }
    }
}
