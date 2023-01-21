using System;
using System.Collections.Generic;
using System.Text;

namespace FormsPlay.Core
{
    public class ViewModelNumber : Attribute
    {
        public int Number { get; set; }
        public ViewModelNumber(int number)
        {
            Number = number;
        }
    }
}
