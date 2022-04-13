﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace TaxesV1
{
    public class Taxes
    {
        private const string TaxSureVilla = "TaxSureVilla";
        private const string TaxSureImmeuble = "TaxSureImmeuble";
        private const string TaxSureEconomie = "TaxSureEconomie";

        public List<Tax> _taxes { get; private set; }
        public float TotalNet => _taxes.Where(tax => tax.Selected).Sum(tax => tax.Total);
        public float TotalAmends => _taxes.Where(tax => tax.Selected).Sum(tax => tax.Amends);
        public float TotalDefaultDec => _taxes.Where(tax => tax.Selected).Sum(tax => tax.DefaultDecl);
        public float Total => TotalNet + TotalAmends + TotalDefaultDec;

        public class Tax
        {
            public bool Selected = true;
            public int Year { get; internal set; }
            public string NDeclaration { get; internal set; }
            public string DateDeclaration { get; internal set; }
            public string Calcul => $"{MtPrincipal}+{DefaultDecl}+{Amends}";
            public float MtPrincipal { get; internal set; }
            public float DefaultDecl { get; internal set; }
            public float Amends { get; internal set; }

            public float Total => MtPrincipal + DefaultDecl + Amends;

            public string NAvis { get; internal set; }
            public string NQuitance { get; internal set; }
        }


        static Taxes GetTaxes(Dossier dossier, int starYear = 0)
        {
            Taxes taxes = new Taxes();
            if (starYear == 0)
                starYear = DateTime.Now.Year - 5;

            for (int year = starYear; year <= DateTime.Now.Year; year++)
            {
                Tax tax = new Tax
                {
                    Year = year
                };
                if (dossier.Terrain.Etat == "Bati" && dossier.Terrain.DateChangementEtat.Value.Year >= year) break;

                if (year < DateTime.Now.Year || DateTime.Now.Month > 3)
                {
                    try
                    {
                        Declaration declaration = dossier.Declarations.First(dec => dec.Anne == year);
                        tax.NDeclaration = declaration.ID.ToString();
                        tax.DateDeclaration = declaration.DateDeclaration.Value.ToShortDateString();
                    }
                    catch (ArgumentNullException)
                    {
                        tax.DefaultDecl = 500;
                    }
                }

                if (dossier.Terrain.Etat == "Construction" &&
                    dossier.Terrain.DateChangementEtat.Value.Year >= year &&
                    dossier.Terrain.DateChangementEtat.Value.Year + 3 <= year)
                {
                    continue;
                }

                tax.MtPrincipal = GetLastKeyChange(GetTaxType(dossier.Terrain.Type), year) *
                                  (float)dossier.Terrain.SuperficeTaxable.Value;
                
                int numberOfLateMonths = MonthDifference(new DateTime(year, 3, 1), DateTime.Now);
                if (numberOfLateMonths > 0)
                    tax.Amends = (0.15f + 0.05f * (numberOfLateMonths - 1)) * tax.MtPrincipal;
                taxes._taxes.Add(tax);
            }

            return taxes;
        }

        public static int MonthDifference(DateTime startDate, DateTime EndDate)
        {
            return (EndDate.Year - startDate.Year) * 12 + EndDate.Month - startDate.Month;
        }

        public static float GetLastKeyChange(string key, int Year)
        {
            return (float)Data.Entities.Constants.Where(constant => constant.Key == key && constant.Date.Year <= Year)
                .OrderByDescending(constant => constant.Date).First().Value;
        }

        private static string GetTaxType(string category)
        {
            switch (category)
            {
                case "Villa":
                    return TaxSureVilla;
                case "Immeuble":
                    return TaxSureImmeuble;
                case "Economie":
                    return TaxSureEconomie;
                default: return "";
            }
        }
    }
}