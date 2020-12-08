using System;
using System.Collections.Generic;
using System.Linq;
using FourLeggedHead.IO;
using DayEight.Model;

namespace DayEight
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Advent of Code 2020 - Day Eight");

            var path = @"Resources/input.txt";

            try
            {
                var instructions = FileReader.ReadAllLines(path);

                var bootCode = new List<Instruction>();

                foreach (var instruction in instructions)
                {
                    bootCode.Add(new Instruction(instruction));
                }

                var run = true;
                var position = 0;
                var accumulator = 0;

                while (run)
                {
                    var instruction = bootCode[position];

                    if (instruction.RunCount > 0) break; //run = false;

                    switch (instruction.Operation)
                    {
                        case Operation.NoOperation:
                            position++;
                            break;
                        case Operation.Jump:
                            position += instruction.Argument;
                            break;
                        case Operation.Accumulate:
                            position++;
                            accumulator += instruction.Argument;
                            break;
                        default:
                            break;
                    }

                    instruction.RunCount++;
                }
                
                Console.WriteLine(accumulator);

                
                var complete = false;

                while (!complete)
                {
                    foreach (var instruction in bootCode)
                    {
                        instruction.RunCount = 0;
                    }

                    var id = bootCode.FindIndex(i => (i.Operation == Operation.Jump || i.Operation == Operation.NoOperation) && !i.Changed);
                    if (id >= 0)
                    {
                        bootCode[id].Changed = true;
                        bootCode[id].SwapNopJump();
                    }
                    else break;

                    run = true;
                    position = 0;
                    accumulator = 0;

                    while (run)
                    {
                        var instruction = bootCode[position];

                        if (instruction.RunCount > 0) break; //run = false;

                        switch (instruction.Operation)
                        {
                            case Operation.NoOperation:
                                position++;
                                break;
                            case Operation.Jump:
                                position += instruction.Argument;
                                break;
                            case Operation.Accumulate:
                                position++;
                                accumulator += instruction.Argument;
                                break;
                            default:
                                break;
                        }

                        instruction.RunCount++;

                        if (position >= bootCode.Count)
                        {
                            complete = true;
                            break;
                        }
                    }

                    bootCode[id].SwapNopJump();

                    Console.WriteLine(id + " " + complete + " " + accumulator);
                }

                Console.WriteLine(accumulator);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message, ex.StackTrace);
            }
        }
    }
}
