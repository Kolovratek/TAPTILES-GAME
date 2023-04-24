using System;
using Taptiles.Core;

namespace Taptiles
{
    public class Program
    {
        static void Main()
        {
            var field = new Field(4, 4);
            var ui = new ConsoleUI.ConsoleUI(field);
            ui.Play();
        }
    }
}