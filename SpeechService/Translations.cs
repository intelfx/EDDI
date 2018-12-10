using EddiDataDefinitions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using Utilities;
using System.Linq;

namespace EddiSpeechService
{
    /// <summary>Translations for Elite items for text-to-speech</summary>
    public class Translations
    {
        /// <summary>Fix up power names</summary>
        public static string Power(string power)
        {
            if (power == null)
            {
                return null;
            }

            switch (power)
            {
                case "Archon Delaine":
                    return "<phoneme alphabet=\"ipa\" ph=\"" + Properties.Phonetics.archon + "\">Archon</phoneme> <phoneme alphabet=\"ipa\" ph=\"" + Properties.Phonetics.delaine + "\">Delaine</phoneme>";
                case "Aisling Duval":
                    return "<phoneme alphabet=\"ipa\" ph=\"" + Properties.Phonetics.aisling + "\">Aisling</phoneme> <phoneme alphabet=\"ipa\" ph=\"" + Properties.Phonetics.duval + "\">Duval</phoneme>";
                case "Arissa Lavigny-Duval":
                    return "<phoneme alphabet=\"ipa\" ph=\"" + Properties.Phonetics.arissa + "\">Arissa</phoneme> <phoneme alphabet=\"ipa\" ph=\"" + Properties.Phonetics.lavigny + "\">Lavigny</phoneme> <phoneme alphabet=\"ipa\" ph=\"" + Properties.Phonetics.duval + "\">Duval</phoneme>";
                case "Denton Patreus":
                    return "<phoneme alphabet=\"ipa\" ph=\"" + Properties.Phonetics.denton + "\">Denton</phoneme> <phoneme alphabet=\"ipa\" ph=\"" + Properties.Phonetics.patreus + "\">Patreus</phoneme>";
                case "Edmund Mahon":
                    return "<phoneme alphabet=\"ipa\" ph=\"" + Properties.Phonetics.edmund + "\">Edmund</phoneme> <phoneme alphabet=\"ipa\" ph=\"" + Properties.Phonetics.mahon + "\">Mahon</phoneme>";
                case "Felicia Winters":
                    return "<phoneme alphabet=\"ipa\" ph=\"" + Properties.Phonetics.felicia + "\">Felicia</phoneme> <phoneme alphabet=\"ipa\" ph=\"" + Properties.Phonetics.winters + "\">Winters</phoneme>";
                case "Pranav Antal":
                    return "<phoneme alphabet=\"ipa\" ph=\"" + Properties.Phonetics.pranav + "\">Pranav</phoneme> <phoneme alphabet=\"ipa\" ph=\"" + Properties.Phonetics.antal + "\">Antal</phoneme>";
                case "Zachary Hudson":
                    return "<phoneme alphabet=\"ipa\" ph=\"" + Properties.Phonetics.zachary + "\">Zachary</phoneme> <phoneme alphabet=\"ipa\" ph=\"" + Properties.Phonetics.hudson + "\">Hudson</phoneme>";
                case "Zemina Torval":
                    return "<phoneme alphabet=\"ipa\" ph=\"" + Properties.Phonetics.zemina + "\">Zemina</phoneme> <phoneme alphabet=\"ipa\" ph=\"" + Properties.Phonetics.torval + "\">Torval</phoneme>";
                case "Li Yong-Rui":
                    return "<phoneme alphabet=\"ipa\" ph=\"" + Properties.Phonetics.li + "\">Li</phoneme> <phoneme alphabet=\"ipa\" ph=\"" + Properties.Phonetics.yong + "\">Yong</phoneme> <phoneme alphabet=\"ipa\" ph=\"" + Properties.Phonetics.rui + "\">Rui</phoneme>";
                case "Yuri Grom":
                    return "<phoneme alphabet=\"ipa\" ph=\"" + Properties.Phonetics.yuri + "\">Yuri</phoneme> <phoneme alphabet=\"ipa\" ph=\"" + Properties.Phonetics.grom + "\">Grom</phoneme>";
                default:
                    return power;
            }
        }

        // Fixes to avoid issues with some of the more strangely-named systems
        private static readonly Dictionary<string, string> STAR_SYSTEM_FIXES = new Dictionary<string, string>()
        {
            { "VESPER-M4", "Vesper M 4" }, // Stop Vesper being treated as a sector
            { "Sagittarius A*", "Sagittarius " + spellOut("A", false) + " Star" }, // Allow the * to be parsed out
        };

        // Fixes to avoid issues with pronunciation of station model names
        private static readonly Dictionary<string, string> STATION_MODEL_FIXES = new Dictionary<string, string>()
        {
            { "Orbis Starport", "Or-bis Starport" }, // Stop "Or-bis" from sometimes being pronounced as "Or-bise"
            { "Megaship", "Mega-ship" } // Stop "Mega-Ship" from sometimes being pronounced as "Meg-AH-ship"
        };

        // Fixes to avoid issues with some of the more strangely-named factions
        private static readonly Dictionary<string, string> FACTION_FIXES = new Dictionary<string, string>()
        {
            { "SCORPIONS ORDER", "Scorpions Order" }, // Stop it being treated as a sector
            { "Federation Unite!", "Federation Unite"} // Stop pausing at the end of Unite!
        };

        private static readonly Dictionary<string, string[]> STAR_SYSTEM_PRONUNCIATIONS = new Dictionary<string, string[]>()
        {
            { "Achenar", new string[] { Properties.Phonetics.achenar } },
            { "Acihault", new string[] { Properties.Phonetics.acihault } },
            { "Adan", new string[] { Properties.Phonetics.adan } },
            { "Alcyone", new string[] { Properties.Phonetics.alcyone } },
            { "Aldebaran", new string[] { Properties.Phonetics.aldebaran } },
            { "Anemoi", new string[] { Properties.Phonetics.anemoi } },
            { "Apoyota", new string[] { Properties.Phonetics.apoyota } },
            { "Arque", new string[] { Properties.Phonetics.arque } },
            { "Asterope", new string[] { Properties.Phonetics.asterope } },
            { "Atlas", new string[] { Properties.Phonetics.atlas } },
            { "Aulin", new string[] { Properties.Phonetics.aulin } },
            { "Bunda", new string[] { Properties.Phonetics.bunda } },
            { "Cayutorme", new string[] { Properties.Phonetics.cayutorme } },
            { "Celaeno", new string[] { Properties.Phonetics.celaeno } },
            { "Ceos", new string[] { Properties.Phonetics.ceos } },
            { "Cygnus", new string[] { Properties.Phonetics.cygnus } },
            { "Deciat", new string[] { Properties.Phonetics.deciat } },
            { "Diso", new string[] { Properties.Phonetics.diso } },
            { "Djiwal", new string[] { Properties.Phonetics.djiwal } },
            { "Dvorsi", new string[] { Properties.Phonetics.dvorsi } },
            { "Electra", new string[] { Properties.Phonetics.electra } },
            { "Eravate" , new string[] { Properties.Phonetics.eravate } },
            { "Eranin" , new string[] { Properties.Phonetics.eranin } },
            { "Frigaha", new string[] { Properties.Phonetics.frigaha } },
            { "Grandmort" , new string[] { Properties.Phonetics.grandmort } },
            { "Hecate" , new string[] { Properties.Phonetics.hecate } },
            { "Hotas" , new string[] { Properties.Phonetics.hotas } },
            { "Isleta" , new string[] { Properties.Phonetics.hotas } },
            { "i Bootis" , new string[] { Properties.Phonetics.ibootis_i, Properties.Phonetics.ibootis_bootis } },
            { "Lave", new string[] { Properties.Phonetics.lave } },
            { "Kaia Bajaja", new string[] { Properties.Phonetics.kaiabajaja_kaia, Properties.Phonetics.kaiabajaja_bajaja } },
            { "Kigana", new string[] { Properties.Phonetics.kigana } },
            { "Kini", new string[] { Properties.Phonetics.kini } },
            { "Kremainn", new string[] { Properties.Phonetics.kremainn } },
            { "Laksak", new string[] { Properties.Phonetics.laksak } },
            { "Leesti", new string[] { Properties.Phonetics.leesti } },
            { "Leucos", new string[] { Properties.Phonetics.leucos } },
            { "Luyten's Star", new string[] { Properties.Phonetics.luytens, Properties.Phonetics.star } },
            { "Maia", new string[] { Properties.Phonetics.maia } },
            { "Mata", new string[] { Properties.Phonetics.mata } },
            { "Merope", new string[] { Properties.Phonetics.merope } },
            { "Mu Koji", new string[] { Properties.Phonetics.mukoji_mu, Properties.Phonetics.mukoji_koji } },
            { "Nuenets", new string[] { Properties.Phonetics.nuenets } },
            { "Okinura", new string[] { Properties.Phonetics.okinura } },
            { "Orrere", new string[] { Properties.Phonetics.orrere } },
            { "Pai Szu", new string[] { Properties.Phonetics.paiszu_pai, Properties.Phonetics.paiszu_szu } },
            { "Pleione", new string[] { Properties.Phonetics.pleione } },
            { "Procyon", new string[] { Properties.Phonetics.procyon } },
            { "Potriti", new string[] { Properties.Phonetics.potriti } },
            { "Reorte", new string[] { Properties.Phonetics.reorte } },
            { "Sagittarius A Star", new string[] {Properties.Phonetics.Sagittarius, Properties.Phonetics.Sagittarius_A, Properties.Phonetics.Sagittarius_A_star} },
            { "Sakti", new string[] { Properties.Phonetics.sakti } },
            { "Shinrarta Dezhra", new string[] { Properties.Phonetics.shinrartadezhra_shinrarta, Properties.Phonetics.shinrartadezhra_dezhra } },
            { "Surya", new string[] { Properties.Phonetics.surya } },
            { "Taygeta", new string[] { Properties.Phonetics.taygeta } },
            { "Tse", new string[] { Properties.Phonetics.tse } },
            { "Xihe", new string[] { Properties.Phonetics.xihe } },
            { "Xinca", new string[] { Properties.Phonetics.xinca } },
            { "Yakabugai", new string[] { Properties.Phonetics.yakabugai } },
            { "Zaonce", new string[] { Properties.Phonetics.zaonce } },
            { "Zhang Fei", new string[] { Properties.Phonetics.zhangfei_zhang, Properties.Phonetics.zhangfei_fei } },
        };

        public static string StellarClass(string val)
        {
            if (val == null)
            {
                return null;
            }

            // Some test to speech voices replace "TTS" with "text-to-speech". Fix that here.
            return val != "TTS" ? val : val.Replace("TTS", "T T S");
        }

        public static string PlanetClass(string val)
        {
            if (val == null)
            {
                return null;
            }

            // Properly handle roman numerals in planet classes
            foreach (PlanetClass planetClass in EddiDataDefinitions.PlanetClass.AllOfThem)
            {
                if (val.Contains(planetClass.localizedName))
                {
                    string numeralToNumber = planetClass.localizedName
                        .Replace(" I ", " 1 ")
                        .Replace(" II ", " 2 ")
                        .Replace(" III ", " 3 ")
                        .Replace(" IV ", " 4 ")
                        .Replace(" V ", " 5 ")
                        .Replace(" VI ", " 6 ");
                    val = val.Replace(planetClass.localizedName, numeralToNumber);
                }
            }
            return val;
        }

        private static readonly Dictionary<string, string[]> CONSTELLATION_PRONUNCIATIONS = new Dictionary<string, string[]>()
        {
            { "Alrai" , new string[] { Properties.Phonetics.Alrai } },
            { "Antliae" , new string[] { Properties.Phonetics.Antliae } },
            { "Aquarii" , new string[] { Properties.Phonetics.Aquarii } },
            { "Arietis" , new string[] { Properties.Phonetics.Arietis } },
            { "Bei Dou" , new string[] { Properties.Phonetics.Bei, Properties.Phonetics.Dou } },
            { "Blanco" , new string[] { Properties.Phonetics.Blanco } },
            { "Bleae Thaa" , new string[] { Properties.Phonetics.BleaeThaa_Bleae, Properties.Phonetics.BleaeThaa_Thaa } },
            { "Capricorni" , new string[] { Properties.Phonetics.Capricorni } },
            { "Cepheus" , new string[] { Properties.Phonetics.Cepheus } },
            { "Cephei" , new string[] { Properties.Phonetics.Cephei } },
            { "Ceti" , new string[] { Properties.Phonetics.Ceti } },
            { "Chi Persei" , new string[] { Properties.Phonetics.ChiPersei_Chi, Properties.Phonetics.ChiPersei_Persei } },
            { "Crucis" , new string[] { Properties.Phonetics.Crucis } },
            { "Cygni" , new string[] { Properties.Phonetics.Cygni } },
            { "Eta Carina" , new string[] { Properties.Phonetics.EtaCarina_Eta, Properties.Phonetics.EtaCarina_Carina } },
            { "Fornacis" , new string[] { Properties.Phonetics.Fornacis } },
            { "Herculis" , new string[] { Properties.Phonetics.Herculis } },
            { "Hyades" , new string[] { Properties.Phonetics.Hyades } },
            { "Hydrae" , new string[] { Properties.Phonetics.Hydrae } },
            { "Lupus" , new string[] { Properties.Phonetics.Lupus } },
            { "Lyncis" , new string[] { Properties.Phonetics.Lyncis } },
            { "Omega" , new string[] { Properties.Phonetics.Omega } },
            { "Ophiuchus" , new string[] { Properties.Phonetics.Ophiuchus } },
            { "Pegasi" , new string[] { Properties.Phonetics.Pegasi } },
            { "Persei" , new string[] { Properties.Phonetics.Persei } },
            { "Piscium" , new string[] { Properties.Phonetics.Piscium } },
            { "Pleiades" , new string[] { Properties.Phonetics.Pleiades } },
            { "Puppis" , new string[] { Properties.Phonetics.Puppis } },
            { "Pru Euq" , new string[] { Properties.Phonetics.PruEuq_Pru, Properties.Phonetics.PruEuq_Euq } },
            { "Rho Ophiuchi" , new string[] { Properties.Phonetics.RhoOphiuchi_Rho, Properties.Phonetics.RhoOphiuchi_Ophiuchi } },
            { "Sagittarius", new string[] { Properties.Phonetics.Sagittarius } },
            { "Scorpii", new string[] { Properties.Phonetics.Scorpii } },
            { "Shui Wei", new string[] { Properties.Phonetics.ShuiWei_Shui, Properties.Phonetics.ShuiWei_Wei } },
            { "Tau Ceti" , new string[] { Properties.Phonetics.TauCeti_Tau, Properties.Phonetics.TauCeti_Ceti } },
            { "Tascheter", new string[] { Properties.Phonetics.Tascheter } },
            { "Trianguli", new string[] { Properties.Phonetics.Trianguli } },
            { "Trifid", new string[] { Properties.Phonetics.Trifid} },
            { "Tucanae", new string[] { Properties.Phonetics.Tucanae } },
            { "Wredguia", new string[] { Properties.Phonetics.Wredguia } },
        };

        // Regexes for the system name parser
        private static readonly Regex ALPHA_THEN_NUMERIC = new Regex(@"[A-Za-z][0-9]");
        private static readonly Regex NUMERIC_THEN_ALPHA = new Regex(@"[0-9][A-Za-z]");
        private static readonly Regex UPPERCASE = new Regex(@"\b[A-Z]{2,}\b");
        private static readonly Regex NUMBERS = new Regex(@"\b[0-9]+\b");
        private static readonly Regex DECIMAL_DIGITS = new Regex(@"(?<= point )[0-9]{2,}\b");
        private static readonly Regex ALPHA_SINGLE = new Regex(@"\b[A-Za-z]\b");
        private static readonly Regex ALNUM_WORD = new Regex(@"\b\w*([0-9][A-Za-z]|[A-Za-z][0-9])\w*\b");
        private static readonly Regex SECTOR = new Regex(@"(.*) ([A-Za-z][A-Za-z]-[A-Za-z]) ([A-Za-z])([0-9-]+)");

        // Regexes for the body name parser
        private static readonly string G_INDEX_ALPHA = @"[A-Z]";
        private static readonly string G_INDEX_NUMBER = @"[0-9]{1,2}";
        private static readonly string G_STAR = @"[A-E]";
        private static readonly string G_MULTISTAR = @"(?=[A-E])A?B?C?D?E?"; // was: @"^A[BCDE]?[CDE]?[DE]?[E]?|B[CDE]?[DE]?[E]?|C[DE]?[E]?|D[E]?$"
        private static readonly string G_PLANET = @"[0-9]{1,2}";
        private static readonly string G_MOON = @"[a-z]";
        private static readonly string G_BELT = $@"((?<belt>{G_INDEX_ALPHA}) Belt)";
        private static readonly string G_RING = $@"((?<ring>{G_INDEX_ALPHA}) Ring)";
        private static readonly string G_CLUSTER = $@"(Cluster (?<cluster>{G_INDEX_NUMBER}))";
        private static readonly string G_ASTEROIDS = $@"({G_RING}|{G_BELT}|{G_BELT} {G_CLUSTER})";
        private static readonly Regex BODY = new Regex($@"\b((?<star>{G_STAR})( {G_ASTEROIDS})?|((?<star>{G_MULTISTAR}) )?(?<planet>{G_PLANET}( {G_MOON})*)( {G_ASTEROIDS})?)$", RegexOptions.ExplicitCapture);

        /// <summary>Fix up faction names</summary>
        public static string Faction(string faction)
        {
            if (faction == null)
            {
                return null;
            }

            // Specific fixing of names to avoid later confusion
            if (FACTION_FIXES.ContainsKey(faction))
            {
                faction = FACTION_FIXES[faction];
            }

            // Faction names can contain system names; hunt them down and change them
            foreach (var pronunciation in STAR_SYSTEM_PRONUNCIATIONS)
            {
                if (faction.Contains(pronunciation.Key))
                {
                    var replacement = replaceWithPronunciation(pronunciation.Key, pronunciation.Value);
                    return faction.Replace(pronunciation.Key, replacement);
                }
            }

            return faction;
        }

        /// <summary>Fix up body names</summary>
        public static string Body(string body, bool useICAO = false)
        {
            // e.g. "Pru Aescs NC-M d7-192 A A Belt", "Prai Flyou JQ-F b30-3 B Belt Cluster 9", "Oopailks NV-X c17-1 AB 6 A Ring"

            if (body == null)
            {
                return null;
            }

            List<string> systemBodyPieces = new List<string>();

            // @BODY regex is strict and anchored to the end of the input; use this to separate the body from the system.
            Match match = BODY.Match(body);
            if (!match.Success)
            {
                // No match at all -- don't bother
                return body;
            }

            // Translate the system name if it is present
            if (body.Length != match.Length)
            {
                string system = body.Substring(0, body.Length - match.Length).Trim();
                systemBodyPieces.Add(StarSystem(system, useICAO).Trim(new char[] { '"' }));
                systemBodyPieces.Add("<break strength=\"weak\" time=\"100ms\" />");
            }

            // Translate the body name itself
            List<string> bodyPieces = new List<string>();
            if (match.Groups["star"].Success)
            {
                bodyPieces.Add(spellOut(match.Groups["star"].Value, useICAO));
            }
            if (match.Groups["planet"].Success)
            {
                bodyPieces.Add(spellOut(match.Groups["planet"].Value, useICAO));
            }
            if (match.Groups["ring"].Success)
            {
                bodyPieces.Add("<break strength=\"x-weak\" time=\"25ms\" />");
                bodyPieces.Add(spellOut(match.Groups["ring"].Value, useICAO));
                bodyPieces.Add("Ring");
            }
            if (match.Groups["belt"].Success)
            {
                bodyPieces.Add("<break strength=\"x-weak\" time=\"25ms\" />");
                bodyPieces.Add(spellOut(match.Groups["belt"].Value, useICAO));
                bodyPieces.Add("Belt");
            }
            if (match.Groups["cluster"].Success)
            {
                bodyPieces.Add("Cluster");
                bodyPieces.Add(spellOut(match.Groups["cluster"].Value, useICAO));
            }
            systemBodyPieces.Add(string.Join(" ", bodyPieces));

            // Double-quote the whole result (helps with pronunciation)
            return "\"" + Regex.Replace(string.Join(" ", systemBodyPieces), @"\s+", " ") + "\"";
        }

        /// <summary>Fix up star system names</summary>
        public static string StarSystem(string starSystem, bool useICAO = false)
        {
            if (starSystem == null)
            {
                return null;
            }

            // This parser is non-strict and opportunistic, so we will need to check if we did
            // any significant replacements and skip applying final fix-ups if that's not the case,
            // so that P() can detect a non-match and attempt to apply other translations.
            string originalStarSystem = starSystem;

            // Specific fixing of names to avoid later confusion
            if (STAR_SYSTEM_FIXES.ContainsKey(starSystem))
            {
                starSystem = STAR_SYSTEM_FIXES[starSystem];
            }

            // Specific translations
            if (STAR_SYSTEM_PRONUNCIATIONS.ContainsKey(starSystem))
            {
                return replaceWithPronunciation(starSystem, STAR_SYSTEM_PRONUNCIATIONS[starSystem]);
            }

            List<string> pieces = new List<string>();

            // Common star catalogues
            if (starSystem.StartsWith("HIP "))
            {
                starSystem = starSystem.Replace("HIP ", "Hip ");
            }
            else if (starSystem.StartsWith("L ")
                || starSystem.StartsWith("LFT ")
                || starSystem.StartsWith("LHS ")
                || starSystem.StartsWith("LP ")
                || starSystem.StartsWith("LTT ")
                || starSystem.StartsWith("NLTT ")
                || starSystem.StartsWith("LPM ")
                || starSystem.StartsWith("PPM ")
                || starSystem.StartsWith("ADS ")
                || starSystem.StartsWith("HR ")
                || starSystem.StartsWith("HD ")
                || starSystem.StartsWith("Luyten ")
            )
            {
                starSystem = starSystem.Replace("-", " " + Properties.Phrases.dash + " ");
            }
            else if (starSystem.StartsWith("Gliese "))
            {
                starSystem = starSystem.Replace(".", " " + Properties.Phrases.point + " ");
            }
            else if (SECTOR.IsMatch(starSystem))
            {
                // Generated star systems
                // Need to handle the pieces before and after the sector marker separately
                var match = SECTOR.Match(starSystem);

                // Fix common names
                string sectorName = match.Groups[1].Value
                    .Replace("Col ", "Coll ")
                    .Replace("R CrA ", "R CRA ")
                    .Replace("Tr ", "TR ")
                    .Replace("Skull and Crossbones Neb. ", "Skull and Crossbones ")
                    .Replace("(", "").Replace(")", "");

                // Various items between the sector name and 'Sector' need to be removed to allow us to find the base pronunciation
                string sectorNameTail = "";
                if (sectorName.EndsWith(" Dark Region B Sector"))
                {
                    sectorName = sectorName.Replace(" Dark Region B Sector", "");
                    sectorNameTail = " Dark Region B Sector";
                }
                else if (sectorName.EndsWith(" Sector"))
                {
                    sectorName = sectorName.Replace(" Sector", "");
                    sectorNameTail = " Sector";
                }

                // Translate sector name
                sectorName = lookupPronunciation(sectorName, CONSTELLATION_PRONUNCIATIONS);

                // Spell out sector indices
                void spellOutSectorIndices(string s)
                {
                    foreach(var piece in s.Split('-'))
                    {
                        pieces.Add(spellOut(piece, useICAO, SpellOutFlags.SmartNumbers));
                        pieces.Add("<break strength=\"x-weak\" time=\"25ms\" />");
                    }
                    pieces.RemoveAt(pieces.Count - 1); // remove the last separator, FIXME ugly
                }

                pieces.Add(sectorName + sectorNameTail);
                pieces.Add("<break strength=\"weak\" time=\"100ms\" />");
                spellOutSectorIndices(match.Groups[2].Value);
                pieces.Add("<break strength=\"weak\" time=\"100ms\" />");
                pieces.Add(spellOut(match.Groups[3].Value, useICAO));
                spellOutSectorIndices(match.Groups[4].Value);
            }
            else if (starSystem.StartsWith("2MASS ")
                || starSystem.StartsWith("AC ")
                || starSystem.StartsWith("AG") // Note no space
                || starSystem.StartsWith("BD")
                || starSystem.StartsWith("CFBDSIR ")
                || starSystem.StartsWith("CXOONC ")
                || starSystem.StartsWith("CXOU ")
                || starSystem.StartsWith("CPD") // Note no space
                || starSystem.StartsWith("CSI") // Note no space
                || starSystem.StartsWith("Csi") // Note no space
                || starSystem.StartsWith("IDS ")
                || starSystem.StartsWith("LF ")
                || starSystem.StartsWith("MJD95 ")
                || starSystem.StartsWith("SDSS ")
                || starSystem.StartsWith("UGCS ")
                || starSystem.StartsWith("WISE ")
                || starSystem.StartsWith("XTE ")
                )
            {
                // Star systems with +/- (and sometimes .)
                starSystem = starSystem.Replace("Csi", "CSI")
                                       .Replace("WISE ", "Wise ")
                                       .Replace("2MASS ", "2 mass ")
                                       .Replace("+", " " + Properties.Phrases.plus + " ")
                                       .Replace("-", " " + Properties.Phrases.minus + " ")
                                       .Replace(".", " " + Properties.Phrases.point + " ");
            }
            else
            {
                // It's possible that the name contains a constellation, in which case translate it
                string[] words = starSystem.Split(' ');
                words = words.Select(x => lookupPronunciation(x, CONSTELLATION_PRONUNCIATIONS)).ToArray();
                starSystem = string.Join(" ", words);
            }

            // If pieces haven't been populated, the input string was updated in-place. Insert it as the single piece.
            if (pieces.Count == 0)
            {
                pieces.Add(starSystem);
            }

            // Apply last-minute fixups to spell out any hard to pronounce or unnatural words.
            // In order to avoid "fixing up" words that in fact are previously added SSML tags, we skip pieces that look like tags.
            //
            // Ideally, every fixup in this function should generate its own pieces for the very same reason, but code gets too complicated,
            // hence for now we're inserting SSML tags in place. This means we need to be very careful with order of fixups in this function.
            string fixupPiece(string s)
            {
                // Is it an SSML tag?
                if (s.StartsWith("<"))
                {
                    return s;
                }
                // Pieces must not contain SSML tags in the middle of them (that's the whole point of pieces).
                Debug.Assert(!s.Contains('<'));

                // Spell out standalone letters (goes first because otherwise we'll catch all the spelled out letters)
                s = ALPHA_SINGLE.Replace(s, match => spellOut(match.Value, useICAO));

                // Spell out any word containing both letters and digits
                s = ALNUM_WORD.Replace(s, match => spellOut(match.Value, useICAO, SpellOutFlags.IgnoreNumbers));

                // Spell out any words of 2 or more uppercase letters (abbreviations)
                s = UPPERCASE.Replace(s, match => spellOut(match.Value, useICAO));

                // Spell out numbers
                s = NUMBERS.Replace(s, match => spellOut(match.Value, useICAO, SpellOutFlags.SmartNumbers));

                /*
                // Spell out any digits after a decimal point
                s = DECIMAL_DIGITS.Replace(s, match => spellOut(match.Value, useICAO, SpellOutFlags.IgnoreNumbers));
                */

                return s;
            }
            pieces = pieces.Select(fixupPiece).ToList();

            // Bail out if we have not made any significant replacements to signal a non-match to P().
            if (pieces.Count == 1 && pieces[0] == originalStarSystem)
            {
                return originalStarSystem;
            }

            starSystem = string.Join(" ", pieces);

            // Double-quote the whole result -- this helps with prosody
            return "\"" + Regex.Replace(starSystem, @"\s+", " ") + "\"";
        }

        /// <summary>Fix up station related pronunciations </summary>
        public static string Station(string station)
        {
            // Specific fixing of station model pronunciations
            if (STATION_MODEL_FIXES.ContainsKey(station))
            {
                station = STATION_MODEL_FIXES[station];
            }
            // Strip plus signs and spaces from station name suffixes
            char[] charsToTrim = { '+', ' ' };
            station = station.TrimEnd(charsToTrim);
            return station;
        }

        private static string replaceWithPronunciation(string sourcePhrase, string[] pronunciation)
        {
            StringBuilder sb = new StringBuilder();
            int i = 0;
            foreach (string source in sourcePhrase.Split(' '))
            {
                if (i > 0)
                {
                    sb.Append(" ");
                }
                sb.Append("<phoneme alphabet=\"ipa\" ph=\"");
                sb.Append(pronunciation[i++]);
                sb.Append("\">");
                sb.Append(source);
                sb.Append("</phoneme>");
            }
            return sb.ToString();
        }

        private static string lookupPronunciation(string sourcePhrase, Dictionary<string, string[]> dictionary)
        {
            string[] pronunciation;
            if (dictionary.TryGetValue(sourcePhrase, out pronunciation))
            {
                return replaceWithPronunciation(sourcePhrase, pronunciation);
            }
            return sourcePhrase;
        }

        public static string ICAO(string callsign, bool passDash = false)
        {
            if (callsign == null)
            {
                return null;
            }

            List<string> elements = new List<string>();
            foreach (char c in callsign.ToUpperInvariant())
            {
                switch (c)
                {
                    case 'A':
                        elements.Add("<phoneme alphabet=\"ipa\" ph=\"ˈælfə\">alpha</phoneme>");
                        break;
                    case 'B':
                        elements.Add("<phoneme alphabet=\"ipa\" ph=\"ˈbrɑːˈvo\">bravo</phoneme>");
                        break;
                    case 'C':
                        elements.Add("<phoneme alphabet=\"ipa\" ph=\"ˈtʃɑːli\">charlie</phoneme>");
                        break;
                    case 'D':
                        elements.Add("<phoneme alphabet=\"ipa\" ph=\"ˈdɛltə\">delta</phoneme>");
                        break;
                    case 'E':
                        elements.Add("<phoneme alphabet=\"ipa\" ph=\"ˈeko\">echo</phoneme>");
                        break;
                    case 'F':
                        elements.Add("<phoneme alphabet=\"ipa\" ph=\"ˈfɒkstrɒt\">foxtrot</phoneme>");
                        break;
                    case 'G':
                        elements.Add("<phoneme alphabet=\"ipa\" ph=\"ɡɒlf\">golf</phoneme>");
                        break;
                    case 'H':
                        elements.Add("<phoneme alphabet=\"ipa\" ph=\"hoːˈtel\">hotel</phoneme>");
                        break;
                    case 'I':
                        elements.Add("<phoneme alphabet=\"ipa\" ph=\"ˈindiˑɑ\">india</phoneme>");
                        break;
                    case 'J':
                        elements.Add("<phoneme alphabet=\"ipa\" ph=\"ˈdʒuːliˑˈet\">juliet</phoneme>");
                        break;
                    case 'K':
                        elements.Add("<phoneme alphabet=\"ipa\" ph=\"ˈkiːlo\">kilo</phoneme>");
                        break;
                    case 'L':
                        elements.Add("<phoneme alphabet=\"ipa\" ph=\"ˈliːmɑ\">lima</phoneme>");
                        break;
                    case 'M':
                        elements.Add("<phoneme alphabet=\"ipa\" ph=\"maɪk\">mike</phoneme>");
                        break;
                    case 'N':
                        elements.Add("<phoneme alphabet=\"ipa\" ph=\"noˈvembə\">november</phoneme>");
                        break;
                    case 'O':
                        elements.Add("<phoneme alphabet=\"ipa\" ph=\"ˈɒskə\">oscar</phoneme>");
                        break;
                    case 'P':
                        elements.Add("<phoneme alphabet=\"ipa\" ph=\"pəˈpɑ\">papa</phoneme>");
                        break;
                    case 'Q':
                        elements.Add("<phoneme alphabet=\"ipa\" ph=\"keˈbek\">quebec</phoneme>");
                        break;
                    case 'R':
                        elements.Add("<phoneme alphabet=\"ipa\" ph=\"ˈroːmiˑo\">romeo</phoneme>");
                        break;
                    case 'S':
                        elements.Add("<phoneme alphabet=\"ipa\" ph=\"siˈerə\">sierra</phoneme>");
                        break;
                    case 'T':
                        elements.Add("<phoneme alphabet=\"ipa\" ph=\"ˈtænɡo\">tango</phoneme>");
                        break;
                    case 'U':
                        elements.Add("<phoneme alphabet=\"ipa\" ph=\"ˈjuːnifɔːm\">uniform</phoneme>");
                        break;
                    case 'V':
                        elements.Add("<phoneme alphabet=\"ipa\" ph=\"ˈvɪktə\">victor</phoneme>");
                        break;
                    case 'W':
                        elements.Add("<phoneme alphabet=\"ipa\" ph=\"ˈwiski\">whiskey</phoneme>");
                        break;
                    case 'X':
                        elements.Add("<phoneme alphabet=\"ipa\" ph=\"ˈeksˈrei\">x-ray</phoneme>");
                        break;
                    case 'Y':
                        elements.Add("<phoneme alphabet=\"ipa\" ph=\"ˈjænki\">yankee</phoneme>");
                        break;
                    case 'Z':
                        elements.Add("<phoneme alphabet=\"ipa\" ph=\"ˈzuːluː\">zulu</phoneme>");
                        break;
                    case '0':
                        elements.Add("<phoneme alphabet=\"ipa\" ph=\"ˈzɪərəʊ\">zero</phoneme>");
                        break;
                    case '1':
                        elements.Add("<phoneme alphabet=\"ipa\" ph=\"ˈwʌn\">one</phoneme>");
                        break;
                    case '2':
                        elements.Add("<phoneme alphabet=\"ipa\" ph=\"ˈtuː\">two</phoneme>");
                        break;
                    case '3':
                        elements.Add("<phoneme alphabet=\"ipa\" ph=\"ˈtriː\">tree</phoneme>");
                        break;
                    case '4':
                        elements.Add("<phoneme alphabet=\"ipa\" ph=\"ˈfoʊ.ər\">fawer</phoneme>");
                        break;
                    case '5':
                        elements.Add("<phoneme alphabet=\"ipa\" ph=\"ˈfaɪf\">fife</phoneme>");
                        break;
                    case '6':
                        elements.Add("<phoneme alphabet=\"ipa\" ph=\"ˈsɪks\">six</phoneme>");
                        break;
                    case '7':
                        elements.Add("<phoneme alphabet=\"ipa\" ph=\"ˈsɛvɛn\">seven</phoneme>");
                        break;
                    case '8':
                        elements.Add("<phoneme alphabet=\"ipa\" ph=\"ˈeɪt\">eight</phoneme>");
                        break;
                    case '9':
                        elements.Add("<phoneme alphabet=\"ipa\" ph=\"ˈnaɪnər\">niner</phoneme>");
                        break;
                    case '-':
                        if (passDash)
                        {
                            elements.Add("-");
                        }

                        break;
                }
            }

            return String.Join(" ", elements).Trim();
        }

        // FIXME: port all uses to spellOut() and drop this
        public static string sayAsLettersOrNumbers(string part)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var c in part)
            {
                var s = c.ToString();
                if (new Regex(@"\d").IsMatch(s))
                {
                    sb.Append(@"<say-as interpret-as=""number"">" + s + @"</say-as>");
                }
                else if (new Regex(@"\w").IsMatch(s))
                {
                    sb.Append(@"<say-as interpret-as=""characters"">" + s + @"</say-as>");
                }
                else
                {
                    sb.Append(s);
                }
            }
            return sb.ToString();
        }

        [Flags]
        public enum SpellOutFlags
        {
            None = 0x0b,
            PassDash = 0x1b,
            IgnoreNumbers = 0x10b,
            SmartNumbers = 0x100b, // only pronounce as a number if it's small enough (< 100), otherwise spell out
        }

        public static string spellOut(string part, bool useICAO, SpellOutFlags flags = SpellOutFlags.None)
        {
            if (useICAO)
            {
                return ICAO(part, flags.HasFlag(SpellOutFlags.PassDash));
            }
            else if (!flags.HasFlag(SpellOutFlags.IgnoreNumbers)
                  && int.TryParse(part, out int number)
                  && (!flags.HasFlag(SpellOutFlags.SmartNumbers) || Math.Abs(number) < 100))
            {
                return @"<say-as interpret-as=""number"">" + part + @"</say-as>";
            }
            else
            {
                if (!flags.HasFlag(SpellOutFlags.PassDash))
                {
                    part = part.Replace("-", "");
                }
                part = string.Join<char>(" ", part);
                return @"<say-as interpret-as=""characters"">" + part + @"</say-as>";
            }
        }

        private static (int number, int nextDigit) Normalize(decimal? value, decimal order)
        {
            return (
                number: (int)(value / order),
                nextDigit: (int)((value % order) / (order / 10))
            );
        }

        public static string Humanize(decimal? value, bool recursed = false)
        {
            if (value == null)
            {
                return null;
            }

            string maybeMinus = "";
            if (value < 0)
            {
                maybeMinus = Properties.Phrases.minus + " ";
                value = -value;
            }

            if (value == 0)
            {
                return Properties.Phrases.zero;
            }

            if (value < 10)
            {
                // Work out how many 0s to begin with
                int numzeros = -1;
                while (value < 1)
                {
                    value *= 10;
                    numzeros++;
                }
                // Now round it to 2sf
                return maybeMinus + (Math.Round((double)value * 10) / (Math.Pow(10, numzeros + 2))).ToString();
            }

            int number;
            int nextDigit;
            string order;
            int digits = (int)Math.Truncate(Math.Log10((double)value));
            if (digits < 2)
            {
                // Units
                number = (int)value;
                order = "";
                nextDigit = (int)((value - number) * 10);
            }
            else if (digits < 3)
            {
                // Hundreds
                (number, nextDigit) = Normalize(value, 100);
                order = Properties.Phrases.hundred;
            }
            else if (digits < 6)
            {
                // Thousands
                (number, nextDigit) = Normalize(value, 1E3M);
                order = Properties.Phrases.thousand;
            }
            else if (digits < 9)
            {
                // Millions
                (number, nextDigit) = Normalize(value, 1E6M);
                order = Properties.Phrases.million;
            }
            else if (digits < 12)
            {
                // Billions
                (number, nextDigit) = Normalize(value, 1E9M);
                order = Properties.Phrases.billion;
            }
            else if (digits < 15)
            {
                // Trillions
                (number, nextDigit) = Normalize(value, 1E12M);
                order = Properties.Phrases.trillion;
            }
            else
            {
                // Quadrillions
                (number, nextDigit) = Normalize(value, 1E15M);
                order = Properties.Phrases.quadrillion;
            }

            // emit exact value
            if (order == "")
            {
                // we recurse only if we know that there _is_ a nested order (i. e. stuff like "51 hundred thousand")
                // otherwise we won't be able to round correctly
                Debug.Assert(recursed == false);
                return maybeMinus + number;
            }

            // recurse to handle nested orders (i. e. "50 hundred thousand")
            else if (number >= 100)
            {
                return maybeMinus + Humanize(number, true) + " " + order;
            }

            // shorten (round) the normalized value to 1 or 2 significant digits so that it can be pronounced with a single word
            // (i. e. say 1..19 as is, but round 21 to "twenty")
            else
            {
                if (recursed)
                {
                    Debug.Assert(number > 0 && number < 10);
                }
                else
                {
                    Debug.Assert(number > 0 && number < 100);
                }

                if (number <= 20)
                {
                    // we can say @number precisely with a single word.
                    // round to .5
                    if (nextDigit == 0)
                    {
                        // the figure we are saying is round enough already
                        return maybeMinus + number + " " + order;
                    }
                    else if (nextDigit < 3)
                    {
                        // round @number down
                        return Properties.Phrases.over + " " + maybeMinus + number + " " + order;
                    }
                    else if (nextDigit < 5)
                    {
                        // round @number up to .5
                        return Properties.Phrases.under + " " + maybeMinus + number + " " + Properties.Phrases.andahalf + " " + order;
                    }
                    else if (nextDigit == 5)
                    {
                        // the figure we are saying is round enough already
                        return maybeMinus + number + " " + Properties.Phrases.andahalf + " " + order;
                    }
                    else if (nextDigit < 8)
                    {
                        // round @number down to .5
                        return Properties.Phrases.over + " " + maybeMinus + number + " " + Properties.Phrases.andahalf + " " + order;
                    }
                    else
                    {
                        // round @number up
                        return Properties.Phrases.under + " " + maybeMinus + (number + 1) + " " + order;
                    }
                }
                else
                {
                    // otherwise round @number even more so that it can be pronounced with a single word.
                    // that is, round to 5 -- same plan as above
                    nextDigit = number % 10;
                    number -= nextDigit;

                    if (nextDigit == 0)
                    {
                        // the figure we are saying is round enough already
                        return maybeMinus + number + " " + order;
                    }
                    else if (nextDigit < 3)
                    {
                        // round @number down
                        return Properties.Phrases.over + " " + maybeMinus + number + " " + order;
                    }
                    else if (nextDigit < 5)
                    {
                        // round @number up 5
                        return Properties.Phrases.under + " " + maybeMinus + (number + 5) + " " + order;
                    }
                    else if (nextDigit == 5)
                    {
                        // the figure we are saying is round enough already
                        return maybeMinus + (number + 5) + " " + order;
                    }
                    else if (nextDigit < 8)
                    {
                        // round @number down to 5
                        return Properties.Phrases.over + " " + maybeMinus + (number + 5) + " " + order;
                    }
                    else
                    {
                        // round @number up
                        return Properties.Phrases.under + " " + maybeMinus + (number + 10) + " " + order;
                    }
                }
            }
        }
    }
}
