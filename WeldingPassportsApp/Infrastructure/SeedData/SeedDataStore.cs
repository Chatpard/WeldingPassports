using Domain;
using Domain.Models;
using System;

namespace Infrastructure.SeedData
{
    public static class SeedDataStore
    {
        public static T[] GetSeedData<T>() where T : IDomainModel
        {
            return typeof(T).Name switch
            {
                nameof(AppSettings) => (T[])(object)GetAppSetings(),
                nameof(Process) => (T[])(object)GetProcesses(),
                nameof(RegistrationType) => (T[])(object)GetRegistrationTypes(),
                nameof(AllowedRegistrationType) => (T[])(object)GetAllowedRegistrationTypes(),
                nameof(UIColor) => (T[])(object)GetUIColors(),
                nameof(UserToApprove) => (T[])(object)GetUsersToApprove(),
                nameof(PEWelder) => (T[])(object)GetPEWelders(),
                nameof(Contact) => (T[])(object)GetContacts(),
                nameof(Address) => (T[])(object)GetAddresses(),
                nameof(Company) => (T[])(object)GetCompanies(),
                nameof(TrainingCenter) => (T[])(object)GetTrainingCenters(),
                nameof(PEPassport) => (T[])(object)GetPEPassports(),
                nameof(CompanyContact) => (T[])(object)GetCompanyContacts(),
                nameof(ExamCenter) => (T[])(object)GetExamCenter(),
                nameof(Examination) => (T[])(object)GetExaminations(),
                nameof(Registration) => (T[])(object)GetRegistrations(),
                nameof(Revoke) => (T[])(object)GetRevokes(),
                nameof(ListTrainingCenter) => (T[])(object)GetListTrainingCenters(),
                nameof(ListExamCenter) => (T[])(object)GetListExamCenters(),
                _ => throw new Exception("Domain Model not found."),
            };
        }

        private static ListExamCenter[] GetListExamCenters()
        {
            return new ListExamCenter[]
            {
                new ListExamCenter()
                {
                    ID = 1,
                    ExamCenterID = 1,
                    CompanyContactID = 4
                }
            };
        }

        private static ListTrainingCenter[] GetListTrainingCenters()
        {
            return new ListTrainingCenter[]
            {
                new ListTrainingCenter()
                {
                    ID = 1,
                    TrainingCenterID = 1,
                    CompanyContactID = 1
                }
            };
        }

        private static Revoke[] GetRevokes()
        {
            return new Revoke[]
            {
                new Revoke()
                {
                    ID = 1,
                    RegistrationID = 5,
                    CompanyContactID = 4,
                    RevokeDate = new DateTime(2021, 6, 12),
                    Comment = "Inappropriate welding technique."
                }
            };
        }

        private static Registration[] GetRegistrations()
        {
            return new Registration[]
            {
                new Registration()
                {
                    ID = 1,
                    PreviousRegistrationID = null,
                    ExaminationID = 1, // 05-Jun-21
                    PEPassportID = 1, // V00469
                    RegistrationTypeID = 1, // Training
                    ProcessID = 1, // Electro
                    CompanyID = 12, // Hydrogaz
                    ExpiryDate = new DateTime(2022, 6, 5),
                    HasPassed = true
                },
                new Registration()
                {
                    ID = 2,
                    PreviousRegistrationID = null,
                    ExaminationID = 1, // 05-Jun-21
                    PEPassportID = 9, // V00477
                    RegistrationTypeID = 1, // Training
                    ProcessID = 2, // Butt
                    CompanyID = 19, // Verschueren
                    ExpiryDate = new DateTime(2022, 6, 5),
                    HasPassed = false
                },
                new Registration()
                {
                    ID = 3,
                    PreviousRegistrationID = null,
                    ExaminationID = 2, // 20-aug-20
                    PEPassportID = 3, // T00471
                    RegistrationTypeID = 1, // Training 
                    ProcessID = 1, // Electro
                    CompanyID = 13, // Fabricom
                    ExpiryDate = new DateTime(2021, 8, 20),
                    HasPassed = true
                },
                new Registration()
                {
                    ID = 4,
                    PreviousRegistrationID = 3,
                    ExaminationID = 3, // 20-okt-21
                    PEPassportID = 3, // T00471
                    RegistrationTypeID = 3, // Re-Examination1
                    ProcessID = 1, // Electro
                    CompanyID = 13, // Fabricom
                    ExpiryDate = new DateTime(2022, 10, 20),
                    HasPassed = null // TBC
                },
                new Registration()
                {
                    ID = 5,
                    PreviousRegistrationID = null,
                    ExaminationID = 2, // 20-aug-20
                    PEPassportID = 11, // T00479
                    RegistrationTypeID = 1, // Training
                    ProcessID = 2, // Butt
                    CompanyID = 21, // Fluvius
                    ExpiryDate = new DateTime(2021, 8, 20),
                    HasPassed = null, // TBC
                },
                new Registration()
                {
                    ID = 6,
                    PreviousRegistrationID = 5,
                    ExaminationID = 3, // 20-okt-21
                    PEPassportID = 11, // T00479
                    RegistrationTypeID = 3, // Re-Examination1
                    ProcessID = 2, // Butt
                    CompanyID = 21, // Fluvius
                    ExpiryDate = new DateTime(2022, 10, 20),
                    HasPassed = null, // TBC
                },
                new Registration()
                {
                    ID = 7,
                    PreviousRegistrationID = null,
                    ExaminationID = 3, // 20-okt-21
                    PEPassportID = 13, // T00481
                    RegistrationTypeID = 1, // Training
                    ProcessID = 1, // Electro
                    CompanyID = 23, // Dalcom
                    ExpiryDate = new DateTime(2022, 10, 20),
                    HasPassed = null, // TBC
                }
            };
        }

        private static Examination[] GetExaminations()
        {
            return new Examination[]
            {
                new Examination()
                {
                    ID = 1,
                    TrainingCenterID = 1, // V
                    ExamCenterID = 1,
                    ExamDate = new DateTime(2021, 6, 5)
                },
                new Examination()
                {
                    ID = 2,
                    TrainingCenterID = 7, // P
                    ExamCenterID = 1,
                    ExamDate = new DateTime(2020, 8, 20)
                },
                new Examination()
                {
                    ID = 3,
                    TrainingCenterID = 3, // T
                    ExamCenterID = 1,
                    ExamDate = new DateTime(2021, 10, 20)
                },
                new Examination()
                {
                    ID = 4,
                    TrainingCenterID = 2,
                    ExamCenterID = 1,
                    ExamDate = new DateTime(2021, 11, 15)
                },
                new Examination()
                {
                    ID = 5,
                    TrainingCenterID = 6,
                    ExamCenterID = 1,
                    ExamDate = new DateTime(2022, 3, 4)
                },
                new Examination()
                {
                    ID = 6,
                    TrainingCenterID = 4,
                    ExamCenterID = 1,
                    ExamDate = new DateTime(2022, 8, 22)
                },
                new Examination()
                {
                    ID = 7,
                    TrainingCenterID = 8,
                    ExamCenterID = 1,
                    ExamDate = new DateTime(2022, 11, 15)
                },
                new Examination()
                {
                    ID = 8,
                    TrainingCenterID = 5,
                    ExamCenterID = 1,
                    ExamDate = new DateTime(2023, 1, 26)
                }
            };
        }

        private static ExamCenter[] GetExamCenter()
        {
            return new ExamCenter[]
            {
                new ExamCenter()
                {
                    ID = 1,
                    CompanyID = 9,
                    IsActive = true
                }
            };
        }

        private static CompanyContact[] GetCompanyContacts()
        {
            return new CompanyContact[]
            {
                new CompanyContact()
                {
                    ID = 1,
                    ContactID = 1,
                    CompanyID = 1,
                    AddressID = 1,
                    JobTitle = "Manager",
                    BusinessPhone = "+32 2 520 56 58",
                    MobilePhone = "+32 486 64 45 52",
                    Email = "leen.dezillie@v-c-l.be "
                },
                new CompanyContact()
                {
                    ID = 2,
                    ContactID = 2,
                    CompanyID = 5,
                    AddressID = 5,
                    JobTitle = "Manager",
                    BusinessPhone = "+32 2 845 89 47",
                    MobilePhone = "+32 486 64 45 52",
                    Email = "fabrice.vermeiren@technocampus.be"
                },
                new CompanyContact()
                {
                    ID = 3,
                    ContactID = 3,
                    CompanyID = 2,
                    AddressID = 2,
                    JobTitle = "Manager",
                    BusinessPhone = "+32 5 050 08 29",
                    MobilePhone = "+32 496 56 87 24",
                    Email = "koen@atriumopleidingen.be"
                },
                new CompanyContact()
                {
                    ID = 4,
                    ContactID = 4,
                    CompanyID = 4,
                    JobTitle = "CEO",
                    BusinessPhone = "+32 2 274 39 09",
                    MobilePhone = "+32 486 82 46 82",
                    Email = "academy-pe@sibelga.be"
                }
            };
        }

        private static PEPassport[] GetPEPassports()
        {
            return new PEPassport[]
            {
                new PEPassport()
                {
                    ID = 1,
                    TrainingCenterID = 1, // V
                    PEWelderID = 1, // Nancy Pelosi
                    AVNumber = 469
                },
                new PEPassport()
                {
                    ID = 2,
                    TrainingCenterID = 4, // S
                    PEWelderID = 2, // Hillary Clinton
                    AVNumber = 470
                },
                new PEPassport()
                {
                    ID = 3,
                    TrainingCenterID = 3, // T
                    PEWelderID = 3, // Donald Trump
                    AVNumber = 471
                },
                new PEPassport()
                {
                    ID = 4,
                    TrainingCenterID = 4, // S
                    PEWelderID = 4, // Mike ¨Pence
                    AVNumber = 472
                },
                new PEPassport()
                {
                    ID = 5,
                    TrainingCenterID = 3, // T
                    PEWelderID = 5, // Sarah Palin
                    AVNumber = 473
                },
                new PEPassport()
                {
                    ID = 6,
                    TrainingCenterID = 1, // V
                    PEWelderID = 6, // Ted Cruz
                    AVNumber = 474
                }, new PEPassport()
                {
                    ID = 7,
                    TrainingCenterID = 5, // K
                    PEWelderID = 7, // Elizabeth Warren
                    AVNumber = 475
                },
                new PEPassport()
                {
                    ID = 8,
                    TrainingCenterID = 3, // T
                    PEWelderID = 8, // Bernie Sanders
                    AVNumber = 476
                }, new PEPassport()
                {
                    ID = 9,
                    TrainingCenterID = 1,
                    PEWelderID = 9, // Kamala Harris
                    AVNumber = 477
                },
                new PEPassport()
                {
                    ID = 10,
                    TrainingCenterID = 4,
                    PEWelderID = 10, // Mitt Romney
                    AVNumber = 478
                }, new PEPassport()
                {
                    ID = 11,
                    TrainingCenterID = 3, // T
                    PEWelderID = 11, // Barack Obama
                    AVNumber = 479
                },
                new PEPassport()
                {
                    ID = 12,
                    TrainingCenterID = 5, // K
                    PEWelderID = 12, // Andrew Yang
                    AVNumber = 480
                }, new PEPassport()
                {
                    ID = 13,
                    TrainingCenterID = 3, // T
                    PEWelderID = 13, // Joe Biden
                    AVNumber = 481
                },
                new PEPassport()
                {
                    ID = 14,
                    TrainingCenterID = 3, // T
                    PEWelderID = 14, // Tulsi Gabbard
                    AVNumber = 482
                }
            };
        }

        private static TrainingCenter[] GetTrainingCenters()
        {
            return new TrainingCenter[]
            {
                new TrainingCenter()
                {
                    ID = 1,
                    CompanyID = 1,
                    Letter = 'V',
                    IsActive = true
                },
                new TrainingCenter()
                {
                    ID = 2,
                    CompanyID = 2,
                    Letter = 'Z',
                    IsActive = true
                },
                new TrainingCenter()
                {
                    ID = 3,
                    CompanyID = 3,
                    Letter = 'T',
                    IsActive = true
                },
                new TrainingCenter()
                {
                    ID = 4,
                    CompanyID = 5,
                    Letter = 'S',
                    IsActive = true
                },
                new TrainingCenter()
                {
                    ID = 5,
                    CompanyID = 7,
                    Letter = 'K',
                    IsActive = true
                },
                new TrainingCenter()
                {
                    ID = 6,
                    CompanyID = 10,
                    Letter = 'N',
                    IsActive = true
                },
                new TrainingCenter()
                {
                    ID = 7,
                    CompanyID = 6,
                    Letter = 'P',
                    IsActive = true
                },
                new TrainingCenter()
                {
                    ID = 8,
                    CompanyID = 11,
                    Letter = 'L',
                    IsActive = true
                }
            };
        }

        private static Company[] GetCompanies()
        {
            return new Company[]
            {
                new Company()
                {
                    ID = 1,
                    AddressID = 1,
                    CompanyName = "VCL",
                    CompanyMainPhone = "+32 2 520 56 58",
                    CompanyEmail = "info@v-c-l.be",
                    WebPage = "v-c-l.be"
                },
                new Company()
                {
                    ID = 2,
                    AddressID = 2,
                    CompanyName = "TCZ",
                    CompanyMainPhone = "+32 2 564 52 87",
                    CompanyEmail = "info@tcz.be",
                    WebPage = "tcz.be"
                },
                new Company()
                {
                    ID = 3,
                    AddressID = 3,
                    CompanyName = "Technifutur",
                    CompanyMainPhone = "+32 4 382 45 72",
                    CompanyEmail = "info@technifutur.be",
                    WebPage = "technifutur.be"
                },
                new Company()
                {
                    ID = 4,
                    AddressID = 4,
                    CompanyName = "Sibelga Academy",
                    CompanyMainPhone = "+32 2 856 45 82",
                    CompanyEmail = "info@sibelga.be",
                    WebPage = "academy.sibelga.be"
                },
                new Company()
                {
                    ID = 5,
                    AddressID = 5,
                    CompanyName = "Technocampus",
                    CompanyMainPhone = "+32 2 754 83 19",
                    CompanyEmail = "info@technocampus.be",
                    WebPage = "technocampus.be"
                },
                new Company()
                {
                    ID = 6,
                    AddressID = 6,
                    CompanyName = "Polygone de l'eau",
                    CompanyMainPhone = "+32 8 778 93 30",
                    CompanyEmail = "info@formation-polygone-eau.be",
                    WebPage = "formation-polygone-eau.be"
                },
                new Company()
                {
                    ID = 7,
                    AddressID = 7,
                    CompanyName = "Kwalificatiecentrum Brugge bvba",
                    CompanyMainPhone = "+32 5 069 63 74",
                    CompanyEmail = "info@kcbrugge.be",
                    WebPage = "kcbrugge.be"
                },
                new Company()
                {
                    ID = 8,
                    AddressID = 8,
                    CompanyName = "Ores",
                    CompanyMainPhone = "+32 2 683 57 32",
                    CompanyEmail = "info@ores.be",
                    WebPage = "ores.be"
                },
                new Company()
                {
                    ID = 9,
                    AddressID = 9,
                    CompanyName = "Vinçotte",
                    CompanyMainPhone = "+32 2 364 87 54",
                    CompanyEmail = "info@vinçotte.be",
                    WebPage = "vinçotte.be"
                },
                new Company()
                {
                    ID = 10,
                    AddressID = 10,
                    CompanyName = "De Nestor",
                    CompanyMainPhone = "+32 9 375 31 69",
                    CompanyEmail = "info@denestor.be",
                    WebPage = "denestor.be"
                },
                new Company()
                {
                    ID = 11,
                    AddressID = 11,
                    CompanyName = "Lascentrum",
                    CompanyMainPhone = "+32 2 954 74 85",
                    CompanyEmail = "info@lascentrum.be",
                    WebPage = "lascentrum.be"
                },
                new Company()
                {
                    ID = 12,
                    AddressID = 12,
                    CompanyName = "Hydrogaz",
                    CompanyMainPhone = "+32 2 456 81 37",
                    CompanyEmail = "info@hydrogaz.be",
                    WebPage = "hydrogaz.be"
                },
                new Company()
                {
                    ID = 13,
                    AddressID = 12,
                    CompanyName = "Fabricom",
                    CompanyMainPhone = "+32 2 793 98 07",
                    CompanyEmail = "info@fabricom.be",
                    WebPage = "fabricom.be"
                },
                new Company()
                {
                    ID = 14,
                    AddressID = 12,
                    CompanyName = "LTC",
                    CompanyMainPhone = "+32 2 951 72 19",
                    CompanyEmail = "info@ltc.be",
                    WebPage = "ltc.be"
                },
                new Company()
                {
                    ID = 15,
                    AddressID = 12,
                    CompanyName = "Verhoeven",
                    CompanyMainPhone = "+32 2 837 49 27",
                    CompanyEmail = "info@verhoeven.be",
                    WebPage = "verhoeven.be"
                },
                new Company()
                {
                    ID = 16,
                    AddressID = 12,
                    CompanyName = "Alkabel",
                    CompanyMainPhone = "+32 2 864 92 47",
                    CompanyEmail = "info@alkabel.be",
                    WebPage = "alkabel.be"
                },
                new Company()
                {
                    ID = 17,
                    AddressID = 12,
                    CompanyName = "Vindevogel",
                    CompanyMainPhone = "+32 2 482 24 35",
                    CompanyEmail = "info@vindevogel.be",
                    WebPage = "vindevogel.be"
                },
                new Company()
                {
                    ID = 18,
                    AddressID = 12,
                    CompanyName = "Canalco",
                    CompanyMainPhone = "+32 2 507 74 19",
                    CompanyEmail = "info@canalco.be",
                    WebPage = "canalco.be"
                },
                new Company()
                {
                    ID = 19,
                    AddressID = 12,
                    CompanyName = "Verschueren",
                    CompanyMainPhone = "+32 2 864 75 05",
                    CompanyEmail = "info@verschueren.be",
                    WebPage = "verschueren.be"
                },
                new Company()
                {
                    ID = 20,
                    AddressID = 12,
                    CompanyName = "APC",
                    CompanyMainPhone = "+32 2 791 43 01",
                    CompanyEmail = "info@apc.be",
                    WebPage = "apc.be"
                },
                new Company()
                {
                    ID = 21,
                    AddressID = 12,
                    CompanyName = "Fluvius",
                    CompanyMainPhone = "+32 2 907 43 67",
                    CompanyEmail = "info@fluvius.be",
                    WebPage = "fluvius.be"
                },
                new Company()
                {
                    ID = 22,
                    AddressID = 12,
                    CompanyName = "Infra",
                    CompanyMainPhone = "+32 2 703 09 84",
                    CompanyEmail = "info@infra.be",
                    WebPage = "infra.be"
                },
                new Company()
                {
                    ID = 23,
                    AddressID = 12,
                    CompanyName = "Dalcom",
                    CompanyMainPhone = "+32 2 378 50 04",
                    CompanyEmail = "info@dalcom.be",
                    WebPage = "dalcom.be"
                }
            };
        }

        private static Address[] GetAddresses()
        {
            return new Address[]
            {
                new Address()
                {
                    ID = 1,
                    BusinessAddress = "Antoon Van Osslaan 1, Bus 4",
                    BusinessAddressPostalCode = "1120",
                    BusinessAddressCity = "Brussel",
                    BusinessAddressCountry = "Belgium"
                },
                new Address()
                {
                    ID = 2,
                    BusinessAddress = "KielStraat 1",
                    BusinessAddressPostalCode = "8380",
                    BusinessAddressCity = "Zeebrugge",
                    BusinessAddressCountry = "Belgium"
                },
                new Address()
                {
                    ID = 3,
                    BusinessAddress = "Liège Science Park, Rue du Bois St-Jean, 15-17",
                    BusinessAddressPostalCode = "4102",
                    BusinessAddressCity = "Seraing",
                    BusinessAddressCountry = "Belgium"
                },
                new Address()
                {
                    ID = 4,
                    BusinessAddress = "Quai des usines 16",
                    BusinessAddressPostalCode = "1000",
                    BusinessAddressCity = "Brussel",
                    BusinessAddressCountry = "Belgium"
                },
                new Address()
                {
                    ID = 5,
                    BusinessAddress = "Quai du Pont Canal 5",
                    BusinessAddressPostalCode = "7110",
                    BusinessAddressCity = "Strépy-Bracquegnies",
                    BusinessAddressCountry = "Belgium"
                },
                new Address()
                {
                    ID = 6,
                    BusinessAddress = "Rue de Limbourg, 41B",
                    BusinessAddressPostalCode = "4800",
                    BusinessAddressCity = "Verviers",
                    BusinessAddressCountry = "Belgium"
                },
                new Address()
                {
                    ID = 7,
                    BusinessAddress = "Rademakerstraat 8/4",
                    BusinessAddressPostalCode = "8380",
                    BusinessAddressCity = "Lissewege",
                    BusinessAddressCountry = "Belgium"
                },
                new Address()
                {
                    ID = 8,
                    BusinessAddress = "Rue d'Ores",
                    BusinessAddressPostalCode = "1000",
                    BusinessAddressCity = "Brussel",
                    BusinessAddressCountry = "Belgium"
                },
                new Address()
                {
                    ID = 9,
                    BusinessAddress = "Rue de Vinçotte",
                    BusinessAddressPostalCode = "1000",
                    BusinessAddressCity = "Brussel",
                    BusinessAddressCountry = "Belgium"
                },
                new Address()
                {
                    ID = 10,
                    BusinessAddress = "Rue de Nestor",
                    BusinessAddressPostalCode = "1000",
                    BusinessAddressCity = "Brussel",
                    BusinessAddressCountry = "Belgium"
                },
                new Address()
                {
                    ID = 11,
                    BusinessAddress = "Rue du Lascentrum",
                    BusinessAddressPostalCode = "1000",
                    BusinessAddressCity = "Brussel",
                    BusinessAddressCountry = "Belgium"
                },
                new Address()
                {
                    ID = 12,
                    BusinessAddress = "Rue de Company",
                    BusinessAddressPostalCode = "1000",
                    BusinessAddressCity = "Brussel",
                    BusinessAddressCountry = "Belgium"
                }
            };
        }

        private static Contact[] GetContacts()
        {
            return new Contact[]
            {
                new Contact()
                {
                    ID = 1,
                    FirstName = "Leen",
                    LastName = "Dezillie",
                    Birthday = new DateTime(1985, 3, 4)
                },
                new Contact()
                {
                    ID = 2,
                    FirstName = "Fabrice",
                    LastName = "Vermeiren",
                    Birthday = new DateTime(1977, 5, 5)
                },
                new Contact()
                {
                    ID = 3,
                    FirstName = "Koen",
                    LastName = "Nies",
                    Birthday = new DateTime(1984, 11, 25)
                },
                new Contact()
                {
                    ID = 4,
                    FirstName = "Bastien",
                    LastName = "De Spiegelaere",
                    Birthday = new DateTime(1982, 8, 5)
                }
            };
        }

        private static PEWelder[] GetPEWelders()
        {
            return new PEWelder[]
            {
                new PEWelder()
                {
                    ID = 1,
                    FirstName = "Nancy",
                    LastName = "Pelosi",
                    NationalNumber = "05.23.45-768-99",
                    IdNumber = "452-3456789-84"
                },
                new PEWelder()
                {
                    ID = 2,
                    FirstName = "Hillary",
                    LastName = "Clinton",
                    NationalNumber = "12.23.71-678-05",
                    IdNumber = "983-3456569-80"
                },
                new PEWelder()
                {
                    ID = 3,
                    FirstName = "Donald",
                    LastName = "Trump",
                    NationalNumber = "01.23.45-678-99",
                    IdNumber = "012-3456789-99"
                },
                new PEWelder()
                {
                    ID = 4,
                    FirstName = "Mike",
                    LastName = "Pence",
                    NationalNumber = "08.23.45-127-89",
                    IdNumber = "634-6916789-85"
                },
                new PEWelder()
                {
                    ID = 5,
                    FirstName = "Sarah",
                    LastName = "Palin",
                    NationalNumber = "86.58.45-669-45",
                    IdNumber = "834-3467289-85"
                },
                new PEWelder()
                {
                    ID = 6,
                    FirstName = "Ted",
                    LastName = "Cruz",
                    NationalNumber = "86.86.56-463-99",
                    IdNumber = "862-6386789-65"
                },
                new PEWelder()
                {
                    ID = 7,
                    FirstName = "Elizabeth",
                    LastName = "Warren",
                    NationalNumber = "37.23.86-578-98",
                    IdNumber = "853-5456783-85"
                },
                new PEWelder()
                {
                    ID = 8,
                    FirstName = "Bernie",
                    LastName = "Sanders",
                    NationalNumber = "92.23.58-638-83",
                    IdNumber = "682-6826789-54"
                },
                new PEWelder()
                {
                    ID = 9,
                    FirstName = "Kamala",
                    LastName = "Harris",
                    NationalNumber = "55.63.45-856-63",
                    IdNumber = "872-5256587-69"
                },
                new PEWelder()
                {
                    ID = 10,
                    FirstName = "Mitt",
                    LastName = "Romney",
                    NationalNumber = "63.98.45-682-63",
                    IdNumber = "015-5256893-85"
                },
                new PEWelder()
                {
                    ID = 11,
                    FirstName = "Barack",
                    LastName = "Obama",
                    NationalNumber = "01.85.45-454-65",
                    IdNumber = "512-8454289-95"
                },
                new PEWelder()
                {
                    ID = 12,
                    FirstName = "Andrew",
                    LastName = "Yang",
                    NationalNumber = "99.23.64-937-52",
                    IdNumber = "082-3861789-08"
                },
                new PEWelder()
                {
                    ID = 13,
                    FirstName = "Joe",
                    LastName = "Biden",
                    NationalNumber = "88.98.45-893-63",
                    IdNumber = "572-1447629-95"
                },
                new PEWelder()
                {
                    ID = 14,
                    FirstName = "Tulsi",
                    LastName = "Gabard",
                    NationalNumber = "28.23.48-689-63",
                    IdNumber = "052-5873789-57"
                }
            };
        }

        private static UserToApprove[] GetUsersToApprove()
        {
            return new UserToApprove[]
            {
                new UserToApprove()
                {
                    ID = 1,
                    Email = "john.doe@hotmail.com",
                    FirstName = "John",
                    LastName = "Doe",
                    Birthday = new DateTime(1990, 4, 12),
                    JobTitle = "CEO",
                    BusinessPhone = "+32 2 165 45 85",
                    BusinessAddress = "Rue de John 10",
                    BusinessAddressPostalCode = "1000",
                    BusinessAddressCity = "Brussel",
                    BusinessAddressCountry = "Belgium",
                    MobilePhone = "+32 495 25 12 37",
                    CompanyName = "VCL",
                    CompanyMainPhone = "+32 2 520 56 58",
                    CompanyEmail = "info@v-c-l.be",
                    WebPage = "v-c-l.be",
                    EmailConfirmed = true,
                    EmailLanguage = "en"
                },
                new UserToApprove()
                {
                    ID = 2,
                    Email = "alice.smith@hotmail.com",
                    FirstName = "Alice",
                    LastName = "Smith",
                    Birthday = new DateTime(1985, 7, 22),
                    JobTitle = "CEO",
                    BusinessPhone = "+32 2 634 72 81",
                    BusinessAddress = "Rue d'Alice 16",
                    BusinessAddressPostalCode = "1000",
                    BusinessAddressCity = "Brussel",
                    BusinessAddressCountry = "Belgium",
                    MobilePhone = "+32 482 93 71 09",
                    CompanyName = "Sibelga Academy",
                    CompanyMainPhone = "+32 2 856 45 82",
                    CompanyEmail = "info@sibelga.be",
                    WebPage = "academy.sibelga.be",
                    EmailConfirmed = true,
                    EmailLanguage = "en"
                },
                new UserToApprove()
                {
                    ID = 3,
                    Email = "james.bond@outlook.com",
                    FirstName = "James",
                    LastName = "Bond",
                    Birthday = new DateTime(1975, 2, 5),
                    JobTitle = "CEO",
                    BusinessPhone = "+32 2 456 92 71",
                    BusinessAddress = "Rue de James 60",
                    BusinessAddressPostalCode = "1000",
                    BusinessAddressCity = "Brussel",
                    BusinessAddressCountry = "Belgium",
                    MobilePhone = "+32 457 76 24 82",
                    CompanyName = "Technifutur",
                    CompanyMainPhone = "+32 4 382 45 72",
                    CompanyEmail = "info@technifutur.be",
                    WebPage = "technifutur.be",
                    EmailConfirmed = true,
                    EmailLanguage = "en"
                },
                new UserToApprove()
                {
                    ID = 4,
                    Email = "antoine.m1996@hotmail.com",
                    FirstName = "Antoine",
                    LastName = "Moenaert",
                    Birthday = new DateTime(1996, 5, 22),
                    JobTitle = "CEO",
                    BusinessPhone = "+32 2 485 73 04",
                    BusinessAddress = "Leopold III-lei 16",
                    BusinessAddressPostalCode = "2650",
                    BusinessAddressCity = "Edegem",
                    BusinessAddressCountry = "Belgium",
                    MobilePhone = "+32 487 10 73 64",
                    CompanyName = "Technifutur",
                    CompanyMainPhone = "+32 3 230 05 28",
                    CompanyEmail = "info@info.be",
                    WebPage = "info.be",
                    EmailConfirmed = true,
                    EmailLanguage = "nl"
                }
            };
        }

        private static UIColor[] GetUIColors()
        {
            return new UIColor[]
            {
                new UIColor()
                {
                    ID = 1,
                    ExtendableStatus = ExtendableStatus.NotYetExtendable,
                    HasPassed = false,
                    Color = null
                },
                new UIColor()
                {
                    ID = 2,
                    ExtendableStatus = ExtendableStatus.NotYetExtendable,
                    HasPassed = true,
                    Color = "table-success"
                },
                new UIColor()
                {
                    ID = 3,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = false,
                    Color = null
                },
                new UIColor()
                {
                    ID = 4,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = true,
                    Color = "table-warning"
                },
                new UIColor()
                {
                    ID = 5,
                    ExtendableStatus = ExtendableStatus.NoMoreExtendable,
                    HasPassed = false,
                    Color = null
                },
                new UIColor()
                {
                    ID = 6,
                    ExtendableStatus = ExtendableStatus.NoMoreExtendable,
                    HasPassed = true,
                    Color = null
                },
                new UIColor()
                {
                    ID = 7,
                    ExtendableStatus = ExtendableStatus.Revoked,
                    HasPassed = false,
                    Color = "table-danger"
                },
                new UIColor()
                {
                    ID = 8,
                    ExtendableStatus = ExtendableStatus.Revoked,
                    HasPassed = true,
                    Color = "table-danger"
                }
            };
        }

        private static AllowedRegistrationType[] GetAllowedRegistrationTypes()
        {
            return new AllowedRegistrationType[]
            {
                #region NotYetExtendable
                new AllowedRegistrationType()
                {
                    ID = 1,
                    ExtendableStatus = ExtendableStatus.NotYetExtendable,
                    HasPassed = null,
                    RegistrationTypeID = null, // No Registration
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 2,
                    ExtendableStatus = ExtendableStatus.NotYetExtendable,
                    HasPassed = false,
                    RegistrationTypeID = 1, // Training
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 3,
                    ExtendableStatus = ExtendableStatus.NotYetExtendable,
                    HasPassed = false,
                    RegistrationTypeID = 2, // Extension
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 4,
                    ExtendableStatus = ExtendableStatus.NotYetExtendable,
                    HasPassed = false,
                    RegistrationTypeID = 3, // Re-Examination1
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 5,
                    ExtendableStatus = ExtendableStatus.NotYetExtendable,
                    HasPassed = false,
                    RegistrationTypeID = 4, // Re-Examination2
                    AvailableRegistrationTypeID = 1 // Training
                },
            #endregion

                #region Extendable
                new AllowedRegistrationType()
                {
                    ID = 6,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = null,
                    RegistrationTypeID = null, // No Registration
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 7,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = false,
                    RegistrationTypeID = 1, // Training
                    AvailableRegistrationTypeID = 1  // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 8,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = false,
                    RegistrationTypeID = 2, // Extension
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 9,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = false,
                    RegistrationTypeID = 2, // Extension
                    AvailableRegistrationTypeID = 3, // Re-Examination1
                },
                new AllowedRegistrationType()
                {
                    ID = 10,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = false,
                    RegistrationTypeID = 3, // Re-Examination1
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 11,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = false,
                    RegistrationTypeID = 3, // Re-Examination1
                    AvailableRegistrationTypeID = 4 // Re-Examination2
                },
                new AllowedRegistrationType()
                {
                    ID = 12,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = false,
                    RegistrationTypeID = 4, // Re-Examination2
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 13,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = true,
                    RegistrationTypeID = 1, // Training
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 14,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = true,
                    RegistrationTypeID = 1, // Training
                    AvailableRegistrationTypeID = 2 // Extension
                },
                new AllowedRegistrationType()
                {
                    ID = 15,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = true,
                    RegistrationTypeID = 1, // Training
                    AvailableRegistrationTypeID = 3 // Re-Examination1
                },
                new AllowedRegistrationType()
                {
                    ID = 16,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = true,
                    RegistrationTypeID = 2, // Extension
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 17,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = true,
                    RegistrationTypeID = 2, // Extension
                    AvailableRegistrationTypeID = 3 // Re-Examination1
                },
                new AllowedRegistrationType()
                {
                    ID = 18,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = true,
                    RegistrationTypeID = 3, // Re-Examination1
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 19,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = true,
                    RegistrationTypeID = 3, // Re-Examination1
                    AvailableRegistrationTypeID = 2 // Extension
                },
                new AllowedRegistrationType()
                {
                    ID = 20,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = true,
                    RegistrationTypeID = 3, // Re-Examination1
                    AvailableRegistrationTypeID = 3  // Re-Examination1
                },
                new AllowedRegistrationType()
                {
                    ID = 21,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = true,
                    RegistrationTypeID = 4, // Re-Examination2
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 22,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = true,
                    RegistrationTypeID = 4, // Re-Examination2
                    AvailableRegistrationTypeID = 2 // Extension
                },
                new AllowedRegistrationType()
                {
                    ID = 23,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = true,
                    RegistrationTypeID = 4, // Re-Examination2
                    AvailableRegistrationTypeID = 3 // Re-Examination1
                },
            #endregion

                #region NoMoreExtendable
                new AllowedRegistrationType()
                {
                    ID = 24,
                    ExtendableStatus = ExtendableStatus.NoMoreExtendable,
                    HasPassed = null,
                    RegistrationTypeID = null, // No Registration
                    AvailableRegistrationTypeID = 1, // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 25,
                    ExtendableStatus = ExtendableStatus.NoMoreExtendable,
                    HasPassed = false,
                    RegistrationTypeID = 1, // Training
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 26,
                    ExtendableStatus = ExtendableStatus.NoMoreExtendable,
                    HasPassed = false,
                    RegistrationTypeID = 2, // Extension
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 27,
                    ExtendableStatus = ExtendableStatus.NoMoreExtendable,
                    HasPassed = false,
                    RegistrationTypeID = 3, // Re-Examination1
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 28,
                    ExtendableStatus = ExtendableStatus.NoMoreExtendable,
                    HasPassed = false,
                    RegistrationTypeID = 4, // Re-Examination2
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 29,
                    ExtendableStatus = ExtendableStatus.NoMoreExtendable,
                    HasPassed = true,
                    RegistrationTypeID = 1, // Training
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 30,
                    ExtendableStatus = ExtendableStatus.NoMoreExtendable,
                    HasPassed = true,
                    RegistrationTypeID = 2, // Extension
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 31,
                    ExtendableStatus = ExtendableStatus.NoMoreExtendable,
                    HasPassed = true,
                    RegistrationTypeID = 3, // Re-Examination1
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 32,
                    ExtendableStatus = ExtendableStatus.NoMoreExtendable,
                    HasPassed = true,
                    RegistrationTypeID = 4, // Re-Examination2
                    AvailableRegistrationTypeID = 1 // Training
                },
            #endregion

                #region Revoked
                new AllowedRegistrationType()
                {
                    ID = 33,
                    ExtendableStatus = ExtendableStatus.Revoked,
                    HasPassed = null,
                    RegistrationTypeID = null, // No Registration
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 34,
                    ExtendableStatus = ExtendableStatus.Revoked,
                    HasPassed = true,
                    RegistrationTypeID = 1, // Training
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 35,
                    ExtendableStatus = ExtendableStatus.Revoked,
                    HasPassed = true,
                    RegistrationTypeID = 2, // Extension
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 36,
                    ExtendableStatus = ExtendableStatus.Revoked,
                    HasPassed = true,
                    RegistrationTypeID = 3, // Re-Examination1
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 37,
                    ExtendableStatus = ExtendableStatus.Revoked,
                    HasPassed = true,
                    RegistrationTypeID = 4, // Re-Examination2
                    AvailableRegistrationTypeID = 1 // Training
                }
                #endregion
            };
        }

        private static RegistrationType[] GetRegistrationTypes()
        {
            return new RegistrationType[]
            {
                new RegistrationType()
                    {
                        ID = 1,
                        RegistrationTypeName = "Training",
                        IsActive = true
                    },
                    new RegistrationType()
                    {
                        ID = 2,
                        RegistrationTypeName = "Extension",
                        IsActive = true
                    },
                    new RegistrationType()
                    {
                        ID = 3,
                        RegistrationTypeName = "Re-Examination1",
                        IsActive = true
                    },
                    new RegistrationType()
                    {
                        ID = 4,
                        RegistrationTypeName = "Re-Examination2",
                        IsActive = true
                    }
            };
        }

        private static Process[] GetProcesses()
        {
            return new Process[]
            {
                new Process()
                {
                    ID = 1,
                    ProcessName = "Electro",
                    IsActive = true
                },
                new Process()
                {
                    ID = 2,
                    ProcessName = "Butt",
                    IsActive = true
                }
            };
        }

        private static AppSettings[] GetAppSetings()
        {
            return new AppSettings[]
            {
                new AppSettings()
                {
                    ID = 1,
                    MaxExpiryDays = 365,
                    MaxExtensionDays = 270,
                    MaxInAdvanceDays = 30
                }
            };
        }
    }
}
