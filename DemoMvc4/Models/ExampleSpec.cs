using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoMvc4.Models
{
    //   object[] testSpecValues = new object[] {
    //    "01-PP-Mzda-DanPoj-SlevyZaklad", // name 01-PP
    //    201301u ,// period                  201301
    //    40     ,// schedule                40
    //    0      ,// absence                 0
    //    15000m  ,// salary                  CZK 15000
    //    (TAX_DECLARED),// tax payer               DECLARE
    //    1u      ,// health payer            YES
    //    1u      ,// health minim            YES
    //    1u      ,// social payer            YES
    //    0u      ,// pension payer           NO
    //    1u      ,// tax payer benefit       YES
    //    0u      ,// tax child benefit       0
    //    0u      ,// tax disability benefit1 NO:NO:NO
    //    0u      ,// tax disability benefit2 NO:NO:NO
    //    0u      ,// tax disability benefit3 NO:NO:NO
    //    0u      ,// tax studying benefit    NO
    //    1u      ,// health employer         YES
    //    1u       // social employer         YES
    //};

    public class ExampleSpec
    {
        static readonly bool yes = true;
        static readonly bool no = false;

        static IList<ExampleSpec> examples = null;

        public ExampleSpec()
        {
        }

        public uint Id { get; set; }
        public string Name { get; set; }
        public string[] Description { get; set; }
        public string Number { get; set; }
        public string Department { get; set; }
        public string Placeholder { get; set; }

        public int Schedule { get; set; }
        public int Absence { get; set; }
        public decimal Salary { get; set; }
        public bool TaxPayer { get; set; }
        public bool TaxDeclaration { get; set; }
        public bool InsSocialPayer { get; set; }
        public bool InsHealthPayer { get; set; }
        public bool InsHealthMinim { get; set; }
        public bool TaxBenefitPayer { get; set; }
        public bool TaxBenefitDisab1 { get; set; }
        public bool TaxBenefitDisab2 { get; set; }
        public bool TaxBenefitDisab3 { get; set; }
        public bool TaxBebefitStudy { get; set; }
        public bool[] TaxBenefitChild { get; set; }
        public bool InsSocialEmployer { get; set; }
        public bool InsHealthEmployer { get; set; }

        public static ExampleSpec ExampleForStarterEmployee(uint id)
        {
            return new ExampleSpec {
                        Id = id,
                        Name = "Starter Employee.",
                        Number = "10000",
                        Department = "Reception",
                        Description = new string[] {
                            "Working schedule: 40 hours/Weekly",
                            "Salary: 15 000 CZK",
                            "Tax, Social and Health Insurance Payer",
                            "Claim payer tax benefit",
                            "No child"
                        },
                        Placeholder = "Example-1.jpg",
                        Schedule = 40,
                        Absence = 0,
                        Salary = 15000m,
                        TaxPayer = yes,
                        TaxDeclaration  = yes,
                        InsSocialPayer  = yes,
                        InsHealthPayer  = yes,
                        InsHealthMinim  = yes,
                        TaxBenefitPayer  = yes,
                        TaxBenefitDisab1 = no,
                        TaxBenefitDisab2 = no,
                        TaxBenefitDisab3 = no,
                        TaxBebefitStudy  = no,
                        TaxBenefitChild = new bool[0],
                        InsSocialEmployer = yes,
                        InsHealthEmployer = yes
                        };
        }

        public static ExampleSpec ExampleForJuniorProgrammer(uint id)
        {
            return new ExampleSpec
            {
                Id = 2,
                Name = "Junior Programmer.",
                Number = "10001",
                Department = "Java Division",
                Description = new string[] {
                        "Working schedule: 40 hours/Weekly",
                        "Salary: 25 000 CZK",
                        "Tax, Social and Health Insurance Payer",
                        "Claim payer tax benefit",
                        "Claim tax benfit for 1 child"
                    },
                Placeholder = "Example-2.jpg",
                Schedule = 40,
                Absence = 0,
                Salary = 25000m,
                TaxPayer = yes,
                TaxDeclaration = yes,
                InsSocialPayer = yes,
                InsHealthPayer = yes,
                InsHealthMinim = yes,
                TaxBenefitPayer = yes,
                TaxBenefitDisab1 = no,
                TaxBenefitDisab2 = no,
                TaxBenefitDisab3 = no,
                TaxBebefitStudy = no,
                TaxBenefitChild = new bool[] { yes },
                InsSocialEmployer = yes,
                InsHealthEmployer = yes
            };
        }
        public static ExampleSpec ExampleForSeniorProgrammer(uint id)
        {
            return new ExampleSpec
            {
                Id = id,
                Name = "Senior Programmer.",
                Number = "00010",
                Department = ".NET Division",
                Description = new string[] {
                        "Working schedule: 40 hours/Weekly",
                        "Salary: 75 000 CZK",
                        "Tax, Social and Health Insurance Payer",
                        "Claim payer tax benefit",
                        "Claim tax benfit for 2 children"
                    },
                Placeholder = "Example-3.jpg",
                Schedule = 40,
                Absence = 0,
                Salary = 75000m,
                TaxPayer = yes,
                TaxDeclaration = yes,
                InsSocialPayer = yes,
                InsHealthPayer = yes,
                InsHealthMinim = yes,
                TaxBenefitPayer = yes,
                TaxBenefitDisab1 = no,
                TaxBenefitDisab2 = no,
                TaxBenefitDisab3 = no,
                TaxBebefitStudy = no,
                TaxBenefitChild = new bool[] { yes, yes },
                InsSocialEmployer = yes,
                InsHealthEmployer = yes
            };
        }
        public static ExampleSpec ExampleForMarketing(uint id)
        {
            return new ExampleSpec
            {
                Id = id,
                Name = "Marketing.",
                Number = "00200",
                Department = "Marketing",
                Description = new string[] {
                        "Working schedule: 20 hours/Weekly",
                        "Salary: 85 000 CZK",
                        "Tax, Social and Health Insurance Payer",
                        "Claim payer tax benefit",
                        "Claim tax benfit for 3 children"
                    },
                Placeholder = "Example-4.jpg",
                Schedule = 40,
                Absence = 0,
                Salary = 85000m,
                TaxPayer = yes,
                TaxDeclaration = yes,
                InsSocialPayer = yes,
                InsHealthPayer = yes,
                InsHealthMinim = yes,
                TaxBenefitPayer = yes,
                TaxBenefitDisab1 = no,
                TaxBenefitDisab2 = no,
                TaxBenefitDisab3 = no,
                TaxBebefitStudy = no,
                TaxBenefitChild = new bool[] { yes, yes, yes },
                InsSocialEmployer = yes,
                InsHealthEmployer = yes
            };
        }
        public static ExampleSpec ExampleForManagement(uint id)
        {
            return new ExampleSpec
            {
                Id = id,
                Name = "Management.",
                Number = "00001",
                Department = "Administration",
                Description = new string[] {
                        "Working schedule: 40 hours/Weekly",
                        "Salary: 125 000 CZK",
                        "Tax, Social and Health Insurance Payer",
                        "Claim payer tax benefit",
                        "Claim tax benfit for 4 children"
                    },
                Placeholder = "Example-5.jpg",
                Schedule = 40,
                Absence = 0,
                Salary = 125000m,
                TaxPayer = yes,
                TaxDeclaration = yes,
                InsSocialPayer = yes,
                InsHealthPayer = yes,
                InsHealthMinim = yes,
                TaxBenefitPayer = yes,
                TaxBenefitDisab1 = no,
                TaxBenefitDisab2 = no,
                TaxBenefitDisab3 = no,
                TaxBebefitStudy = no,
                TaxBenefitChild = new bool[] { yes, yes, yes, yes },
                InsSocialEmployer = yes,
                InsHealthEmployer = yes
            };
        }
        public static ExampleSpec ExampleForMaintenance(uint id)
        {
            return new ExampleSpec
            {
                Id = id,
                Name = "Maintenance.",
                Number = "00300",
                Department = "Maintenance",
                Description = new string[] {
                        "Working schedule: 10 hours/Weekly",
                        "Salary: 5 000 CZK",
                        "Tax Payer",
                        "No Social and Health Insurance",
                        "No tax declaration and benefits"
                    },
                Placeholder = "Example-6.jpg",
                Schedule = 10,
                Absence = 0,
                Salary = 5000m,
                TaxPayer = yes,
                TaxDeclaration = no,
                InsSocialPayer = no,
                InsHealthPayer = no,
                InsHealthMinim = no,
                TaxBenefitPayer = no,
                TaxBenefitDisab1 = no,
                TaxBenefitDisab2 = no,
                TaxBenefitDisab3 = no,
                TaxBebefitStudy = no,
                TaxBenefitChild = new bool[0],
                InsSocialEmployer = no,
                InsHealthEmployer = no
            };
        }
        public static ExampleSpec ExampleForDesigner(uint id)
        {
            return new ExampleSpec
            {
                Id = id,
                Name = "Designer.",
                Number = "05000",
                Department = "Marketing",
                Description = new string[] {
                        "Working schedule: 40 hours/Weekly",
                        "Salary: 105 000 CZK",
                        "Tax, Social and Health Insurance Payer",
                        "Claim payer tax benefit",
                        "No child"
                    },
                Placeholder = "Example-7.jpg",
                Schedule = 40,
                Absence = 0,
                Salary = 105000m,
                TaxPayer = yes,
                TaxDeclaration = yes,
                InsSocialPayer = yes,
                InsHealthPayer = yes,
                InsHealthMinim = yes,
                TaxBenefitPayer = yes,
                TaxBenefitDisab1 = no,
                TaxBenefitDisab2 = no,
                TaxBenefitDisab3 = no,
                TaxBebefitStudy = no,
                TaxBenefitChild = new bool[0],
                InsSocialEmployer = yes,
                InsHealthEmployer = yes
            };
        }
        public static ExampleSpec ExampleForSalesman(uint id)
        {
            return new ExampleSpec
            {
                Id = id,
                Name = "Salesman.",
                Number = "06000",
                Department = "Sales",
                Description = new string[] {
                        "Working schedule: 40 hours/Weekly",
                        "Salary: 65 000 CZK",
                        "Tax, Social and Health Insurance Payer",
                        "Claim payer tax benefit",
                        "Claim tax benfit for 2 children"
                    },
                Placeholder = "Example-8.jpg",
                Schedule = 40,
                Absence = 0,
                Salary = 105000m,
                TaxPayer = yes,
                TaxDeclaration = yes,
                InsSocialPayer = yes,
                InsHealthPayer = yes,
                InsHealthMinim = yes,
                TaxBenefitPayer = yes,
                TaxBenefitDisab1 = no,
                TaxBenefitDisab2 = no,
                TaxBenefitDisab3 = no,
                TaxBebefitStudy = no,
                TaxBenefitChild = new bool[] {yes, yes},
                InsSocialEmployer = yes,
                InsHealthEmployer = yes
            };
        }
        public static ExampleSpec ExampleForVisionary(uint id)
        {
            return new ExampleSpec
            {
                Id = id,
                Name = "Visionary.",
                Number = "00011",
                Department = "Enchantment",
                Description = new string[] {
                        "Working schedule: 40 hours/Weekly",
                        "Salary: 336 667 CZK",
                        "Tax, Social and Health Insurance Payer",
                        "Claim payer tax benefit",
                        "Claim tax benfit for 2 children"
                    },
                Placeholder = "Example-9.jpg",
                Schedule = 40,
                Absence = 0,
                Salary = 336667m,
                TaxPayer = yes,
                TaxDeclaration = yes,
                InsSocialPayer = yes,
                InsHealthPayer = yes,
                InsHealthMinim = yes,
                TaxBenefitPayer = yes,
                TaxBenefitDisab1 = no,
                TaxBenefitDisab2 = no,
                TaxBenefitDisab3 = no,
                TaxBebefitStudy = no,
                TaxBenefitChild = new bool[] {yes, yes},
                InsSocialEmployer = yes,
                InsHealthEmployer = yes
            };
        }
        public static ExampleSpec ExampleForContracter(uint id)
        {
            return new ExampleSpec
            {
                Id = id,
                Name = "Contracter.",
                Number = "07011",
                Department = "Sales",
                Description = new string[] {
                        "Working schedule: 24 hours/Weekly",
                        "Salary: 5 000 CZK",
                        "Tax Payer",
                        "Social and Health Insurance",
                        "No tax declaration and benefits"
                    },
                Placeholder = "Example-C.jpg",
                Schedule = 24,
                Absence = 0,
                Salary = 5000m,
                TaxPayer = yes,
                TaxDeclaration = no,
                InsSocialPayer = yes,
                InsHealthPayer = yes,
                InsHealthMinim = yes,
                TaxBenefitPayer = no,
                TaxBenefitDisab1 = no,
                TaxBenefitDisab2 = no,
                TaxBenefitDisab3 = no,
                TaxBebefitStudy = no,
                TaxBenefitChild = new bool[0],
                InsSocialEmployer = yes,
                InsHealthEmployer = yes
            };
        }
        public static ExampleSpec ExampleForMaxSocial(uint id)
        {
            return new ExampleSpec
            {
                Id = id,
                Name = "Max Social insurance base.",
                Number = "08011",
                Department = "Management",
                Description = new string[] {
                        "Working schedule: 40 hours/Weekly",
                        "Salary: 1 242 532 CZK",
                        "Tax, Social and Health Insurance Payer",
                        "Claim payer tax benefit",
                        "Claim disability 3 benefit",
                        "Claim studying benefit"
                    },
                Placeholder = "Example-S.jpg",
                Schedule = 24,
                Absence = 0,
                Salary = 1242532m,
                TaxPayer = yes,
                TaxDeclaration = yes,
                InsSocialPayer = yes,
                InsHealthPayer = yes,
                InsHealthMinim = yes,
                TaxBenefitPayer = yes,
                TaxBenefitDisab1 = no,
                TaxBenefitDisab2 = no,
                TaxBenefitDisab3 = yes,
                TaxBebefitStudy = yes,
                TaxBenefitChild = new bool[0],
                InsSocialEmployer = yes,
                InsHealthEmployer = yes
            };
        }
        public static ExampleSpec ExampleForMaxHealth(uint id)
        {
            return new ExampleSpec
            {
                Id = id,
                Name = "Max Health insurance base.",
                Number = "08012",
                Department = "Management",
                Description = new string[] {
                        "Working schedule: 40 hours/Weekly",
                        "Salary: 1 809 964 CZK",
                        "Tax, Social and Health Insurance Payer",
                        "Claim payer tax benefit",
                        "Claim disability 3 benefit",
                        "Claim studying benefit"
                    },
                Placeholder = "Example-H.jpg",
                Schedule = 40,
                Absence = 0,
                Salary = 1809964m,
                TaxPayer = yes,
                TaxDeclaration = yes,
                InsSocialPayer = yes,
                InsHealthPayer = yes,
                InsHealthMinim = yes,
                TaxBenefitPayer = yes,
                TaxBenefitDisab1 = no,
                TaxBenefitDisab2 = no,
                TaxBenefitDisab3 = yes,
                TaxBebefitStudy = yes,
                TaxBenefitChild = new bool[0],
                InsSocialEmployer = yes,
                InsHealthEmployer = yes
            };
        }
        public static ExampleSpec ExampleForMaxBonus(uint id)
        {
            return new ExampleSpec
            {
                Id = id,
                Name = "Max Child's Tax Bonus.",
                Number = "08013",
                Department = "Management",
                Description = new string[] {
                        "Working schedule: 40 hours/Weekly",
                        "Salary: 10 000 CZK",
                        "Tax, Social and Health Insurance Payer",
                        "Claim payer tax benefit",
                        "Claim tax benfit for 5 children"
                    },
                Placeholder = "Example-CH5.jpg",
                Schedule = 40,
                Absence = 0,
                Salary = 10000m,
                TaxPayer = yes,
                TaxDeclaration = yes,
                InsSocialPayer = yes,
                InsHealthPayer = yes,
                InsHealthMinim = yes,
                TaxBenefitPayer = yes,
                TaxBenefitDisab1 = no,
                TaxBenefitDisab2 = no,
                TaxBenefitDisab3 = no,
                TaxBebefitStudy = no,
                TaxBenefitChild = new bool[] {yes, yes, yes, yes, yes},
                InsSocialEmployer = yes,
                InsHealthEmployer = yes
            };
        }
        public static ExampleSpec ExampleForMinBonus(uint id)
        {
            return new ExampleSpec
            {
                Id = id,
                Name = "2 Child's Tax Bonus.",
                Number = "08014",
                Department = "Management",
                Description = new string[] {
                        "Working schedule: 40 hours/Weekly",
                        "Salary: 15 000 CZK",
                        "Tax, Social and Health Insurance Payer",
                        "Claim payer tax benefit",
                        "Claim tax benfit for 2 children"
                    },
                Placeholder = "Example-CH2.jpg",
                Schedule = 24,
                Absence = 0,
                Salary = 15000m,
                TaxPayer = yes,
                TaxDeclaration = yes,
                InsSocialPayer = yes,
                InsHealthPayer = yes,
                InsHealthMinim = yes,
                TaxBenefitPayer = yes,
                TaxBenefitDisab1 = no,
                TaxBenefitDisab2 = no,
                TaxBenefitDisab3 = no,
                TaxBebefitStudy = no,
                TaxBenefitChild = new bool[] {yes, yes},
                InsSocialEmployer = yes,
                InsHealthEmployer = yes
            };
        }

        public static IEnumerable<ExampleSpec> ExamplesStatic()
        {
            if (examples == null)
            {
                examples = new List<ExampleSpec>
                {
                    ExampleSpec.ExampleForStarterEmployee(1u),
                    //ExampleSpec.ExampleForJuniorProgrammer(2u),
                    //ExampleSpec.ExampleForSeniorProgrammer(3u),
                    ExampleSpec.ExampleForMarketing(4u),
                    ExampleSpec.ExampleForMaintenance(5u),
                    ExampleSpec.ExampleForManagement(6u),
                    ExampleSpec.ExampleForDesigner(7u),
                    ExampleSpec.ExampleForSalesman(8u),
                    ExampleSpec.ExampleForVisionary(10u),
                    ExampleSpec.ExampleForContracter(11u),
                    ExampleSpec.ExampleForMaxHealth(12u),
                    ExampleSpec.ExampleForMaxSocial(13u),
                    ExampleSpec.ExampleForMaxBonus(14u),
                    ExampleSpec.ExampleForMinBonus(15u)
                };
            }
            return examples;
        }
    }

}