using FourLeggedHead.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace DayFourteen
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Advent of Code 2020 - Day Fourteen");

            var path = @"Resources/Input.txt";

            try
            {
                var input = FileReader.ReadAllLines(path);

                var mem = new Dictionary<long, long>();

                var mask = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
                foreach (var line in input)
                {
                    if (line.StartsWith("mask"))
                    {
                        var maskMatch = Regex.Match(line, @"mask = (?<Mask>\w+)");
                        if (maskMatch.Success) mask = maskMatch.Groups["Mask"].Value;
                    }

                    if (line.StartsWith("mem"))
                    {
                        var mactchMem = Regex.Match(line, @"mem[[](?<Address>\d+)[]] = (?<Value>\d+)");
                        if (mactchMem.Success)
                        {
                            var address = long.Parse(mactchMem.Groups["Address"].Value);
                            var value = long.Parse(mactchMem.Groups["Value"].Value);

                            var binaryValue = Convert.ToString(value, 2).PadLeft(36, '0').ToCharArray();

                            for (int i = 0; i < 36; i++)
                            {
                                binaryValue[i] = mask[i] == 'X' ? binaryValue[i] : mask[i];
                            }

                            value = Convert.ToInt64(new String(binaryValue), 2);

                            if (mem.ContainsKey(address)) mem[address] = value;
                            else mem.Add(address, value);
                        }
                    }
                }

                Console.WriteLine(mem.Sum(m => m.Value));

                mem = new Dictionary<long, long>();

                mask = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
                foreach (var line in input)
                {
                    if (line.StartsWith("mask"))
                    {
                        if (line.StartsWith("mask"))
                        {
                            var maskMatch = Regex.Match(line, @"mask = (?<Mask>\w+)");
                            if (maskMatch.Success) mask = maskMatch.Groups["Mask"].Value;
                        }
                    }

                    if (line.StartsWith("mem"))
                    {
                        var mactchMem = Regex.Match(line, @"mem[[](?<Address>\d+)[]] = (?<Value>\d+)");
                        if (mactchMem.Success)
                        {
                            var address = long.Parse(mactchMem.Groups["Address"].Value);
                            var value = long.Parse(mactchMem.Groups["Value"].Value);

                            var binaryValue = Convert.ToString(address, 2).PadLeft(36, '0').ToCharArray();

                            var addressList = new List<string>();

                            switch (mask[0])
                            {
                                case '0':
                                    addressList.Add(binaryValue[0].ToString());
                                    break;
                                case '1':
                                    addressList.Add("1");
                                    break;
                                case 'X':
                                    addressList.Add("0");
                                    addressList.Add("1");
                                    break;
                                default:
                                    break;
                            }

                            for (int i = 1; i < 36; i++)
                            {
                                switch (mask[i])
                                {
                                    case '0':
                                        for (int j = 0; j < addressList.Count; j++)
                                        {
                                            addressList[j] += binaryValue[i].ToString();
                                        }
                                        break;
                                    case '1':
                                        for (int j = 0; j < addressList.Count; j++)
                                        {
                                            addressList[j] += "1";
                                        }
                                        break;
                                    case 'X':
                                        var additionalAddresses = new List<string>();
                                        for (int j = 0; j < addressList.Count; j++)
                                        {
                                            additionalAddresses.Add(new string(addressList[j]) + "0");
                                            addressList[j] += "1";
                                        }
                                        addressList.AddRange(additionalAddresses);
                                        break;
                                    default:
                                        break;
                                }
                            }

                            foreach (var addString in addressList)
                            {
                                var add = Convert.ToInt64(addString, 2);

                                if (mem.ContainsKey(add)) mem[add] = value;
                                else mem.Add(add, value);
                            }
                        }
                    }
                }

                Console.WriteLine(mem.Sum(m => m.Value));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message, ex.StackTrace);
            }
        }
    }
}
