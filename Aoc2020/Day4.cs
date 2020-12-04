using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

namespace Aoc2020
{
    /// <summary>
    /// --- Day 4: Passport Processing ---
    /// </summary>
    public static class Day4
    {
        private static readonly string[] _requiredFields =
        {
            "byr",
            "iyr",
            "eyr",
            "hgt",
            "hcl",
            "ecl",
            "pid"
        };

        public static int Part1(IEnumerable<string> rawInputs)
        {
            var retVal = 0;
            var checkList = _requiredFields.ToList();

            foreach (var row in rawInputs)
            {
                if (row.Length == 0)
                {
                    if (!checkList.Any())
                    {
                        retVal++;
                    }

                    checkList = _requiredFields.ToList();
                    continue;
                }

                var passportFields = row.Split(' ');
                foreach (var item in passportFields)
                {
                    checkList.Remove(item.Substring(0, 3));
                }
            }

            if (!checkList.Any())
            {
                retVal++;
            }

            return retVal;
        }

        public static int Part2(IEnumerable<string> rawInputs)
        {
            var passportList = new List<Passport>();
            var passPort = new Passport();
            foreach (var row in rawInputs)
            {
                if (row.Length == 0)
                {
                    passportList.Add(passPort);
                    passPort = new Passport();
                    continue;
                }

                passPort.setFields(row.Split(' '));
            }
            passportList.Add(passPort);

            return passportList.Count(i => i.IsValid());
        }
    }

    public class Passport
    {
        public int Byr { get; set; }
        public int Iyr { get; set; }
        public int Eyr { get; set; }
        public string Hgt { get; set; }
        public string Hcl { get; set; }
        public string Ecl { get; set; }
        public string Pid { get; set; }

        public Passport()
        {
            Byr = 0;
            Iyr = 0;
            Eyr = 0;
            Hgt = string.Empty;
            Hcl = string.Empty;
            Ecl = string.Empty;
            Pid = string.Empty;
        }

        // To help w/ Debugging
        public string ToString()
        {
            if (IsValid())
            {
                return $"byr={Byr} - iyr={Iyr} - eyr={Eyr} - hgt={Hgt} - hcl={Hcl} - ecl={Ecl} - pid = {Pid}";
            }

            return string.Empty;
        }

        public void setFields(string[] rawFields)
        {
            foreach (var field in rawFields)
            {
                var entry = field.Split(':');
                switch (entry[0])
                {
                    case "byr":
                        Byr = int.Parse(entry[1]);
                        break;
                    case "iyr":
                        Iyr = int.Parse(entry[1]);
                        break;
                    case "eyr":
                        Eyr = int.Parse(entry[1]);
                        break;
                    case "hgt":
                        Hgt = entry[1];
                        break;
                    case "hcl":
                        Hcl = entry[1];
                        break;
                    case "ecl":
                        Ecl = entry[1];
                        break;
                    case "pid":
                        Pid = entry[1];
                        break;
                }
            }
        }

        public bool IsValid()
        {
            return Byr >= 1920 && Byr <= 2002 &&
                   Iyr >= 2010 && Iyr <= 2020 &&
                   Eyr >= 2020 && Eyr <= 2030 &&
                   IsValidHgt() && 
                   IsValidHcl() && 
                   IsValidEcl() &&
                   IsValidPid();
        }

        private bool IsValidHgt()
        {
            var pattern1 = new Regex(@"(1[5-8][0-9]|19[0-3])(cm)");
            var pattern2 = new Regex(@"(59|6[0-9]|7[0-6])(in)");
            return pattern1.IsMatch(Hgt) || pattern2.IsMatch(Hgt);
        }

        private bool IsValidHcl()
        {
            var pattern1 = new Regex(@"#([0-9]|[a-f]){6}");
            return pattern1.IsMatch(Hcl);
        }

        private bool IsValidEcl()
        {
            var pattern1 = new Regex(@"(amb|blu|brn|gry|grn|hzl|oth)");
            return pattern1.IsMatch(Ecl);
        }

        private bool IsValidPid()
        {
            var pattern1 = new Regex(@"^[0-9]{9}$");
            return pattern1.IsMatch(Pid); ;
        }
    }
}
