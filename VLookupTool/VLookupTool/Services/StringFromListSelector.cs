﻿using FileManager.Enums;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VLookupTool.Services
{
    public class StringFromListSelector
    {
        public static string GetString(List<string> list, string consolePrompt)
        {
            var input = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title($"[grey]{consolePrompt}[/]")
                    .PageSize(10)
                    .MoreChoicesText("[grey](Move up and down to reveal more objects)[/]")
                    .AddChoices(list)
                    );

            return input;
        }
    }
}
