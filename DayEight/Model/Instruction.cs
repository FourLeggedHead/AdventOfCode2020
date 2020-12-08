using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DayEight.Model
{
    public enum Operation
    {
        NoOperation,
        Jump,
        Accumulate
    }

    public class Instruction
    {
        public Operation Operation { get; set; }
        public int Argument { get; set; }
        public int RunCount { get; set; }

        public Instruction(string instruction)
        {
            var instructionMatch = Regex.Match(instruction, @"(?<Operation>nop|acc|jmp) (?<Argument>[+-]\d+)");

            if (instructionMatch.Success)
            {
                Operation = instructionMatch.Groups["Operation"].Value switch
                {
                    "jmp" => Operation.Jump,
                    "acc" => Operation.Accumulate,
                    _ => Operation.NoOperation,
                };

                Argument = int.Parse(instructionMatch.Groups["Argument"].Value);
            }
        }
    }
}
