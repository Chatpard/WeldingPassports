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
                nameof(ExamCenter) => (T[])(object)GetExamCenters(),
                nameof(Examination) => (T[])(object)GetExaminations(),
                nameof(Registration) => (T[])(object)GetRegistrations(),
                nameof(Revoke) => (T[])(object)GetRevokes(),
               _ => throw new Exception("Domain Model not found."),
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
                    ExaminationID = 3, // 15-aug-21
                    PEPassportID = 3, // T00471
                    RegistrationTypeID = 1, // Training
                    ProcessID = 1, // Electro
                    CompanyID = 13, // Fabricom
                    ExpiryDate = new DateTime(2022, 10, 20),
                    HasPassed = true
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
                    HasPassed = true,
                },
                new Registration()
                {
                    ID = 6,
                    PreviousRegistrationID = 5,
                    ExaminationID = 3, // 15-aug-21
                    PEPassportID = 11, // T00479
                    RegistrationTypeID = 1, // Training
                    ProcessID = 2, // Butt
                    CompanyID = 21, // Fluvius
                    ExpiryDate = new DateTime(2022, 10, 20),
                    HasPassed = true, // TBC
                },
                new Registration()
                {
                    ID = 7,
                    PreviousRegistrationID = null,
                    ExaminationID = 3, // 15-aug-21
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
                    TrainingCenterID = 3, // T
                    ExamCenterID = 1,
                    ExamDate = new DateTime(2020, 8, 20)
                },
                new Examination()
                {
                    ID = 3,
                    TrainingCenterID = 3, // T
                    ExamCenterID = 1,
                    ExamDate = new DateTime(2021, 8, 15)
                },
                new Examination()
                {
                    ID = 4,
                    TrainingCenterID = 2, // Z
                    ExamCenterID = 1,
                    ExamDate = new DateTime(2021, 11, 15)
                },
                new Examination()
                {
                    ID = 5,
                    TrainingCenterID = 6, // N
                    ExamCenterID = 1,
                    ExamDate = new DateTime(2022, 3, 4)
                },
                new Examination()
                {
                    ID = 6,
                    TrainingCenterID = 4, // S
                    ExamCenterID = 1,
                    ExamDate = new DateTime(2022, 8, 22)
                },
                new Examination()
                {
                    ID = 7,
                    TrainingCenterID = 8, // L
                    ExamCenterID = 1,
                    ExamDate = new DateTime(2022, 11, 15)
                },
                new Examination()
                {
                    ID = 8,
                    TrainingCenterID = 5, // K
                    ExamCenterID = 1,
                    ExamDate = new DateTime(2023, 1, 26)
                }
            };
        }

        private static ExamCenter[] GetExamCenters()
        {
            return new ExamCenter[]
            {
                new ExamCenter()
                {
                    ID = 1,
                    CompanyID = 9,
                    CompanyContactID = 5,
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
                    JobTitle = "Manager",
                    BusinessPhone = "+32 2 274 39 09",
                    MobilePhone = "+32 486 82 46 82",
                    Email = "academy-pe@sibelga.be"
                },
                new CompanyContact()
                {
                    ID = 5,
                    ContactID = 5,
                    CompanyID = 9,
                    JobTitle = "Examinator",
                    BusinessPhone = "+32 2 274 39 09",
                    MobilePhone = "+32 486 82 46 82",
                    Email = "guy.doms@vincotte.be"
                },
                new CompanyContact()
                {
                    ID = 6,
                    ContactID = 6,
                    CompanyID = 24,
                    JobTitle = "Officer",
                    BusinessPhone = "+32 3 237 11 09",
                    MobilePhone = "+32 475 92 05 78",
                    Email = "christian.moenaert@synergrid.be"
                },
                new CompanyContact()
                {
                    ID = 7,
                    ContactID = 7,
                    CompanyID = 21,
                    JobTitle = "Officer",
                    BusinessPhone = "+32 9 263 56 00",
                    MobilePhone = "+32 472 92 20 17",
                    Email = "davy.gijsels@fluvius.be"
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
                    CompanyContactID = 1,
                    Letter = 'V',
                    IsActive = true
                },
                new TrainingCenter()
                {
                    ID = 2,
                    CompanyID = 2,
                    CompanyContactID = 2,
                    Letter = 'Z',
                    IsActive = true
                },
                new TrainingCenter()
                {
                    ID = 3,
                    CompanyID = 3,
                    CompanyContactID = null,
                    Letter = 'T',
                    IsActive = true
                },
                new TrainingCenter()
                {
                    ID = 4,
                    CompanyID = 5,
                    CompanyContactID = 2,
                    Letter = 'S',
                    IsActive = true
                },
                new TrainingCenter()
                {
                    ID = 5,
                    CompanyID = 7,
                    CompanyContactID = null,
                    Letter = 'K',
                    IsActive = true
                },
                new TrainingCenter()
                {
                    ID = 6,
                    CompanyID = 10,
                    CompanyContactID = null,
                    Letter = 'N',
                    IsActive = true
                },
                new TrainingCenter()
                {
                    ID = 7,
                    CompanyID = 6,
                    CompanyContactID = null,
                    Letter = 'P',
                    IsActive = true
                },
                new TrainingCenter()
                {
                    ID = 8,
                    CompanyID = 11,
                    CompanyContactID = null,
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
                    WebPage = "https://v-c-l.be"
                },
                new Company()
                {
                    ID = 2,
                    AddressID = 2,
                    CompanyName = "TCZ",
                    CompanyMainPhone = "+32 2 564 52 87",
                    CompanyEmail = "info@tcz.be",
                    WebPage = "https://tcz.be"
                },
                new Company()
                {
                    ID = 3,
                    AddressID = 3,
                    CompanyName = "Technifutur",
                    CompanyMainPhone = "+32 4 382 45 72",
                    CompanyEmail = "info@technifutur.be",
                    WebPage = "https://technifutur.be"
                },
                new Company()
                {
                    ID = 4,
                    AddressID = 4,
                    CompanyName = "Sibelga Academy",
                    CompanyMainPhone = "+32 2 856 45 82",
                    CompanyEmail = "info@sibelga.be",
                    WebPage = "https://academy.sibelga.be"
                },
                new Company()
                {
                    ID = 5,
                    AddressID = 5,
                    CompanyName = "Technocampus",
                    CompanyMainPhone = "+32 2 754 83 19",
                    CompanyEmail = "info@technocampus.be",
                    WebPage = "https://technocampus.be"
                },
                new Company()
                {
                    ID = 6,
                    AddressID = 6,
                    CompanyName = "Polygone de l'eau",
                    CompanyMainPhone = "+32 8 778 93 30",
                    CompanyEmail = "info@formation-polygone-eau.be",
                    WebPage = "https://formation-polygone-eau.be"
                },
                new Company()
                {
                    ID = 7,
                    AddressID = 7,
                    CompanyName = "Kwalificatiecentrum Brugge bvba",
                    CompanyMainPhone = "+32 5 069 63 74",
                    CompanyEmail = "info@kcbrugge.be",
                    WebPage = "https://kcbrugge.be"
                },
                new Company()
                {
                    ID = 8,
                    AddressID = 8,
                    CompanyName = "Ores",
                    CompanyMainPhone = "+32 2 683 57 32",
                    CompanyEmail = "info@ores.be",
                    WebPage = "https://ores.be"
                },
                new Company()
                {
                    ID = 9,
                    AddressID = 9,
                    CompanyName = "Vinçotte",
                    CompanyMainPhone = "+32 2 364 87 54",
                    CompanyEmail = "info@vinçotte.be",
                    WebPage = "https://vinçotte.be"
                },
                new Company()
                {
                    ID = 10,
                    AddressID = 10,
                    CompanyName = "De Nestor",
                    CompanyMainPhone = "+32 9 375 31 69",
                    CompanyEmail = "info@denestor.be",
                    WebPage = "https://denestor.be"
                },
                new Company()
                {
                    ID = 11,
                    AddressID = 11,
                    CompanyName = "Lascentrum",
                    CompanyMainPhone = "+32 2 954 74 85",
                    CompanyEmail = "info@lascentrum.be",
                    WebPage = "https://lascentrum.be"
                },
                new Company()
                {
                    ID = 12,
                    AddressID = 12,
                    CompanyName = "Hydrogaz",
                    CompanyMainPhone = "+32 2 456 81 37",
                    CompanyEmail = "info@hydrogaz.be",
                    WebPage = "https://hydrogaz.be"
                },
                new Company()
                {
                    ID = 13,
                    AddressID = 12,
                    CompanyName = "Fabricom",
                    CompanyMainPhone = "+32 2 793 98 07",
                    CompanyEmail = "info@fabricom.be",
                    WebPage = "https://fabricom.be"
                },
                new Company()
                {
                    ID = 14,
                    AddressID = 12,
                    CompanyName = "LTC",
                    CompanyMainPhone = "+32 2 951 72 19",
                    CompanyEmail = "info@ltc.be",
                    WebPage = "https://ltc.be"
                },
                new Company()
                {
                    ID = 15,
                    AddressID = 12,
                    CompanyName = "Verhoeven",
                    CompanyMainPhone = "+32 2 837 49 27",
                    CompanyEmail = "info@verhoeven.be",
                    WebPage = "https://verhoeven.be"
                },
                new Company()
                {
                    ID = 16,
                    AddressID = 12,
                    CompanyName = "Alkabel",
                    CompanyMainPhone = "+32 2 864 92 47",
                    CompanyEmail = "info@alkabel.be",
                    WebPage = "https://alkabel.be"
                },
                new Company()
                {
                    ID = 17,
                    AddressID = 12,
                    CompanyName = "Vindevogel",
                    CompanyMainPhone = "+32 2 482 24 35",
                    CompanyEmail = "info@vindevogel.be",
                    WebPage = "https://vindevogel.be"
                },
                new Company()
                {
                    ID = 18,
                    AddressID = 12,
                    CompanyName = "Canalco",
                    CompanyMainPhone = "+32 2 507 74 19",
                    CompanyEmail = "info@canalco.be",
                    WebPage = "https://canalco.be"
                },
                new Company()
                {
                    ID = 19,
                    AddressID = 12,
                    CompanyName = "Verschueren",
                    CompanyMainPhone = "+32 2 864 75 05",
                    CompanyEmail = "info@verschueren.be",
                    WebPage = "https://verschueren.be"
                },
                new Company()
                {
                    ID = 20,
                    AddressID = 12,
                    CompanyName = "APC",
                    CompanyMainPhone = "+32 2 791 43 01",
                    CompanyEmail = "info@apc.be",
                    WebPage = "https://apc.be"
                },
                new Company()
                {
                    ID = 21,
                    AddressID = 12,
                    CompanyName = "Fluvius",
                    CompanyMainPhone = "+32 2 907 43 67",
                    CompanyEmail = "info@fluvius.be",
                    WebPage = "https://fluvius.be"
                },
                new Company()
                {
                    ID = 22,
                    AddressID = 12,
                    CompanyName = "Infra",
                    CompanyMainPhone = "+32 2 703 09 84",
                    CompanyEmail = "info@infra.be",
                    WebPage = "https://infra.be"
                },
                new Company()
                {
                    ID = 23,
                    AddressID = 12,
                    CompanyName = "Dalcom",
                    CompanyMainPhone = "+32 2 378 50 04",
                    CompanyEmail = "info@dalcom.be",
                    WebPage = "https://dalcom.be"
                },
                new Company()
                {
                    ID = 24,
                    AddressID = null,
                    CompanyName = "Synergrid",
                    CompanyMainPhone = "+32 2 237 11 09",
                    CompanyEmail = "info@synergrid.be",
                    WebPage = "https://synergrid.be"
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
                },
                new Contact()
                {
                    ID = 5,
                    FirstName = "Guy",
                    LastName = "Doms",
                    Birthday = new DateTime(1962, 8, 5)
                },
                new Contact()
                {
                    ID = 6,
                    FirstName = "Christian",
                    LastName = "Moenaert",
                    Birthday = new DateTime(1966, 3, 23)
                },
                new Contact()
                {
                    ID = 7,
                    FirstName = "Davy",
                    LastName = "Gijsels",
                    Birthday = new DateTime(1986, 6, 15)
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
                    PreviousRegistrationTypeID = null, // No Registration
                    IsNew = true,
                    CurrentRegistrationTypeID = null,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 2,
                    ExtendableStatus = ExtendableStatus.NotYetExtendable,
                    HasPassed = null,
                    PreviousRegistrationTypeID = null, // No Registration
                    IsNew = false,
                    CurrentRegistrationTypeID = 1,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 3,
                    ExtendableStatus = ExtendableStatus.NotYetExtendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 1, // Training
                    IsNew = true,
                    CurrentRegistrationTypeID = null,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 4,
                    ExtendableStatus = ExtendableStatus.NotYetExtendable,
                    HasPassed = false,
                    PreviousRegistrationTypeID = 1, // Training
                    IsNew = false,
                    CurrentRegistrationTypeID = 1,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 5,
                    ExtendableStatus = ExtendableStatus.NotYetExtendable,
                    HasPassed = false,
                    PreviousRegistrationTypeID = 1, // Training
                    IsNew = false,
                    CurrentRegistrationTypeID = 2,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 6,
                    ExtendableStatus = ExtendableStatus.NotYetExtendable,
                    HasPassed = false,
                    PreviousRegistrationTypeID = 1, // Training
                    IsNew = false,
                    CurrentRegistrationTypeID = 3,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 7,
                    ExtendableStatus = ExtendableStatus.NotYetExtendable,
                    HasPassed = false,
                    PreviousRegistrationTypeID = 1, // Training
                    IsNew = false,
                    CurrentRegistrationTypeID = 4,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 8,
                    ExtendableStatus = ExtendableStatus.NotYetExtendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 1, // Training
                    IsNew = false,
                    CurrentRegistrationTypeID = 1,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 9,
                    ExtendableStatus = ExtendableStatus.NotYetExtendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 1, // Training
                    IsNew = false,
                    CurrentRegistrationTypeID = 2,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 10,
                    ExtendableStatus = ExtendableStatus.NotYetExtendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 1, // Training
                    IsNew = false,
                    CurrentRegistrationTypeID = 3,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 11,
                    ExtendableStatus = ExtendableStatus.NotYetExtendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 1, // Training
                    IsNew = false,
                    CurrentRegistrationTypeID = 4,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 12,
                    ExtendableStatus = ExtendableStatus.NotYetExtendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 2, // Extension
                    IsNew = true,
                    CurrentRegistrationTypeID = null,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 13,
                    ExtendableStatus = ExtendableStatus.NotYetExtendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 2, // Extension
                    IsNew = false,
                    CurrentRegistrationTypeID = 1,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 14,
                    ExtendableStatus = ExtendableStatus.NotYetExtendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 2, // Extension
                    IsNew = false,
                    CurrentRegistrationTypeID = 2,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 15,
                    ExtendableStatus = ExtendableStatus.NotYetExtendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 2, // Extension
                    IsNew = false,
                    CurrentRegistrationTypeID = 3,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 16,
                    ExtendableStatus = ExtendableStatus.NotYetExtendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 2, // Extension
                    IsNew = false,
                    CurrentRegistrationTypeID = 4,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 17,
                    ExtendableStatus = ExtendableStatus.NotYetExtendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 2, // Extension
                    IsNew = false,
                    CurrentRegistrationTypeID = 1,
                    AvailableRegistrationTypeID = 2 // Extension
                },
                new AllowedRegistrationType()
                {
                    ID = 18,
                    ExtendableStatus = ExtendableStatus.NotYetExtendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 2, // Extension
                    IsNew = false,
                    CurrentRegistrationTypeID = 2,
                    AvailableRegistrationTypeID = 2 // Extension
                },
                new AllowedRegistrationType()
                {
                    ID = 19,
                    ExtendableStatus = ExtendableStatus.NotYetExtendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 3, // Extension
                    IsNew = false,
                    CurrentRegistrationTypeID = 1,
                    AvailableRegistrationTypeID = 2 // Extension
                },
                new AllowedRegistrationType()
                {
                    ID = 20,
                    ExtendableStatus = ExtendableStatus.NotYetExtendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 2, // Extension
                    IsNew = false,
                    CurrentRegistrationTypeID = 4,
                    AvailableRegistrationTypeID = 2 // Extension
                },
                new AllowedRegistrationType()
                {
                    ID = 21,
                    ExtendableStatus = ExtendableStatus.NotYetExtendable,
                    HasPassed = false,
                    PreviousRegistrationTypeID = 2, // Extension
                    IsNew = false,
                    CurrentRegistrationTypeID = 1,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 22,
                    ExtendableStatus = ExtendableStatus.NotYetExtendable,
                    HasPassed = false,
                    PreviousRegistrationTypeID = 2, // Extension
                    IsNew = false,
                    CurrentRegistrationTypeID = 2,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 23,
                    ExtendableStatus = ExtendableStatus.NotYetExtendable,
                    HasPassed = false,
                    PreviousRegistrationTypeID = 2, // Extension
                    IsNew = false,
                    CurrentRegistrationTypeID = 3,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 24,
                    ExtendableStatus = ExtendableStatus.NotYetExtendable,
                    HasPassed = false,
                    PreviousRegistrationTypeID = 2, // Extension
                    IsNew = false,
                    CurrentRegistrationTypeID = 4,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 25,
                    ExtendableStatus = ExtendableStatus.NotYetExtendable,
                    HasPassed = false,
                    PreviousRegistrationTypeID = 2, // Extension
                    IsNew = false,
                    CurrentRegistrationTypeID = 1,
                    AvailableRegistrationTypeID = 2 // Extension
                },
                new AllowedRegistrationType()
                {
                    ID = 26,
                    ExtendableStatus = ExtendableStatus.NotYetExtendable,
                    HasPassed = false,
                    PreviousRegistrationTypeID = 2, // Extension
                    IsNew = false,
                    CurrentRegistrationTypeID = 2,
                    AvailableRegistrationTypeID = 2 // Extension
                },
                new AllowedRegistrationType()
                {
                    ID = 27,
                    ExtendableStatus = ExtendableStatus.NotYetExtendable,
                    HasPassed = false,
                    PreviousRegistrationTypeID = 2, // Extension
                    IsNew = false,
                    CurrentRegistrationTypeID = 3,
                    AvailableRegistrationTypeID = 2 // Extension
                },
                new AllowedRegistrationType()
                {
                    ID = 28,
                    ExtendableStatus = ExtendableStatus.NotYetExtendable,
                    HasPassed = false,
                    PreviousRegistrationTypeID = 2, // Extension
                    IsNew = false,
                    CurrentRegistrationTypeID = 4,
                    AvailableRegistrationTypeID = 2 // Extension
                },
                new AllowedRegistrationType()
                {
                    ID = 29,
                    ExtendableStatus = ExtendableStatus.NotYetExtendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 3, // Re-Examination1
                    IsNew = true,
                    CurrentRegistrationTypeID = null,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 30,
                    ExtendableStatus = ExtendableStatus.NotYetExtendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 3, // Re-Examination1
                    IsNew = false,
                    CurrentRegistrationTypeID = 1,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 31,
                    ExtendableStatus = ExtendableStatus.NotYetExtendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 3, // Re-Examination1
                    IsNew = false,
                    CurrentRegistrationTypeID = 2,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 32,
                    ExtendableStatus = ExtendableStatus.NotYetExtendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 3, // Re-Examination1
                    IsNew = false,
                    CurrentRegistrationTypeID = 3,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 33,
                    ExtendableStatus = ExtendableStatus.NotYetExtendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 3, // Re-Examination1
                    IsNew = false,
                    CurrentRegistrationTypeID = 4,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 34,
                    ExtendableStatus = ExtendableStatus.NotYetExtendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 3, // Re-Examination1
                    IsNew = false,
                    CurrentRegistrationTypeID = 1,
                    AvailableRegistrationTypeID = 3 // Re-Examination1
                },
                new AllowedRegistrationType()
                {
                    ID = 35,
                    ExtendableStatus = ExtendableStatus.NotYetExtendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 3, // Re-Examination1
                    IsNew = false,
                    CurrentRegistrationTypeID = 2,
                    AvailableRegistrationTypeID = 3 // Re-Examination1
                },
                new AllowedRegistrationType()
                {
                    ID = 36,
                    ExtendableStatus = ExtendableStatus.NotYetExtendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 3, // Re-Examination1
                    IsNew = false,
                    CurrentRegistrationTypeID = 3,
                    AvailableRegistrationTypeID = 3 // Re-Examination1
                },
                new AllowedRegistrationType()
                {
                    ID = 37,
                    ExtendableStatus = ExtendableStatus.NotYetExtendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 3, // Re-Examination1
                    IsNew = false,
                    CurrentRegistrationTypeID = 4,
                    AvailableRegistrationTypeID = 3 // Re-Examination1
                },
                new AllowedRegistrationType()
                {
                    ID = 38,
                    ExtendableStatus = ExtendableStatus.NotYetExtendable,
                    HasPassed = false,
                    PreviousRegistrationTypeID = 3, // Re-Examination1
                    IsNew = true,
                    CurrentRegistrationTypeID = null,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 39,
                    ExtendableStatus = ExtendableStatus.NotYetExtendable,
                    HasPassed = false,
                    PreviousRegistrationTypeID = 3, // Re-Examination1
                    IsNew = false,
                    CurrentRegistrationTypeID = 1,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 40,
                    ExtendableStatus = ExtendableStatus.NotYetExtendable,
                    HasPassed = false,
                    PreviousRegistrationTypeID = 3, // Re-Examination1
                    IsNew = false,
                    CurrentRegistrationTypeID = 2,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 41,
                    ExtendableStatus = ExtendableStatus.NotYetExtendable,
                    HasPassed = false,
                    PreviousRegistrationTypeID = 3, // Re-Examination1
                    IsNew = false,
                    CurrentRegistrationTypeID = 3,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 42,
                    ExtendableStatus = ExtendableStatus.NotYetExtendable,
                    HasPassed = false,
                    PreviousRegistrationTypeID = 3, // Re-Examination1
                    IsNew = false,
                    CurrentRegistrationTypeID = 4,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 43,
                    ExtendableStatus = ExtendableStatus.NotYetExtendable,
                    HasPassed = false,
                    PreviousRegistrationTypeID = 3, // Re-Examination1
                    IsNew = false,
                    CurrentRegistrationTypeID = 1,
                    AvailableRegistrationTypeID = 3 // Re-Examination1
                },
                new AllowedRegistrationType()
                {
                    ID = 44,
                    ExtendableStatus = ExtendableStatus.NotYetExtendable,
                    HasPassed = false,
                    PreviousRegistrationTypeID = 3, // Re-Examination1
                    IsNew = false,
                    CurrentRegistrationTypeID = 2,
                    AvailableRegistrationTypeID = 3 // Re-Examination1
                },
                new AllowedRegistrationType()
                {
                    ID = 45,
                    ExtendableStatus = ExtendableStatus.NotYetExtendable,
                    HasPassed = false,
                    PreviousRegistrationTypeID = 3, // Re-Examination1
                    IsNew = false,
                    CurrentRegistrationTypeID = 3,
                    AvailableRegistrationTypeID = 3 // Re-Examination1
                },
                new AllowedRegistrationType()
                {
                    ID = 46,
                    ExtendableStatus = ExtendableStatus.NotYetExtendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 4, // Re-Examination2
                    IsNew = true,
                    CurrentRegistrationTypeID = 4,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 47,
                    ExtendableStatus = ExtendableStatus.NotYetExtendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 4, // Re-Examination2
                    IsNew = false,
                    CurrentRegistrationTypeID = 1,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 48,
                    ExtendableStatus = ExtendableStatus.NotYetExtendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 4, // Re-Examination2
                    IsNew = false,
                    CurrentRegistrationTypeID = 2,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 49,
                    ExtendableStatus = ExtendableStatus.NotYetExtendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 4, // Re-Examination2
                    IsNew = false,
                    CurrentRegistrationTypeID = 3,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 50,
                    ExtendableStatus = ExtendableStatus.NotYetExtendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 4, // Re-Examination2
                    IsNew = false,
                    CurrentRegistrationTypeID = 4,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 51,
                    ExtendableStatus = ExtendableStatus.NotYetExtendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 4, // Re-Examination2
                    IsNew = false,
                    CurrentRegistrationTypeID = 1,
                    AvailableRegistrationTypeID = 4 // Re-Examination2
                },
                new AllowedRegistrationType()
                {
                    ID = 52,
                    ExtendableStatus = ExtendableStatus.NotYetExtendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 4, // Re-Examination2
                    IsNew = false,
                    CurrentRegistrationTypeID = 2,
                    AvailableRegistrationTypeID = 4 // Re-Examination2
                },
                new AllowedRegistrationType()
                {
                    ID = 53,
                    ExtendableStatus = ExtendableStatus.NotYetExtendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 4, // Re-Examination2
                    IsNew = false,
                    CurrentRegistrationTypeID = 3,
                    AvailableRegistrationTypeID = 4 // Re-Examination2
                },
                new AllowedRegistrationType()
                {
                    ID = 54,
                    ExtendableStatus = ExtendableStatus.NotYetExtendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 4, // Re-Examination2
                    IsNew = false,
                    CurrentRegistrationTypeID = 4,
                    AvailableRegistrationTypeID = 4 // Re-Examination2
                },
                new AllowedRegistrationType()
                {
                    ID = 55,
                    ExtendableStatus = ExtendableStatus.NotYetExtendable,
                    HasPassed = false,
                    PreviousRegistrationTypeID = 4, // Re-Examination2
                    IsNew = true,
                    CurrentRegistrationTypeID = null,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 56,
                    ExtendableStatus = ExtendableStatus.NotYetExtendable,
                    HasPassed = false,
                    PreviousRegistrationTypeID = 4, // Re-Examination2
                    IsNew = false,
                    CurrentRegistrationTypeID = 1,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 57,
                    ExtendableStatus = ExtendableStatus.NotYetExtendable,
                    HasPassed = false,
                    PreviousRegistrationTypeID = 4, // Re-Examination2
                    IsNew = false,
                    CurrentRegistrationTypeID = 2,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 58,
                    ExtendableStatus = ExtendableStatus.NotYetExtendable,
                    HasPassed = false,
                    PreviousRegistrationTypeID = 4, // Re-Examination2
                    IsNew = false,
                    CurrentRegistrationTypeID = 3,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 59,
                    ExtendableStatus = ExtendableStatus.NotYetExtendable,
                    HasPassed = false,
                    PreviousRegistrationTypeID = 4, // Re-Examination2
                    IsNew = false,
                    CurrentRegistrationTypeID = 4,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 60,
                    ExtendableStatus = ExtendableStatus.NotYetExtendable,
                    HasPassed = false,
                    PreviousRegistrationTypeID = 4, // Re-Examination2
                    IsNew = false,
                    CurrentRegistrationTypeID = 1,
                    AvailableRegistrationTypeID = 4 // Re-Examination2
                },
                new AllowedRegistrationType()
                {
                    ID = 61,
                    ExtendableStatus = ExtendableStatus.NotYetExtendable,
                    HasPassed = false,
                    PreviousRegistrationTypeID = 4, // Re-Examination2
                    IsNew = false,
                    CurrentRegistrationTypeID = 2,
                    AvailableRegistrationTypeID = 4 // Re-Examination2
                },
                new AllowedRegistrationType()
                {
                    ID = 62,
                    ExtendableStatus = ExtendableStatus.NotYetExtendable,
                    HasPassed = false,
                    PreviousRegistrationTypeID = 4, // Re-Examination2
                    IsNew = false,
                    CurrentRegistrationTypeID = 3,
                    AvailableRegistrationTypeID = 4 // Re-Examination2
                },
                new AllowedRegistrationType()
                {
                    ID = 63,
                    ExtendableStatus = ExtendableStatus.NotYetExtendable,
                    HasPassed = false,
                    PreviousRegistrationTypeID = 4, // Re-Examination2
                    IsNew = false,
                    CurrentRegistrationTypeID = 4,
                    AvailableRegistrationTypeID = 4 // Re-Examination2
                },
            #endregion

                #region Extendable
                new AllowedRegistrationType()
                {
                    ID = 64,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = null,
                    PreviousRegistrationTypeID = null, // No Registration
                    IsNew = true,
                    CurrentRegistrationTypeID = null,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 65,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = null,
                    PreviousRegistrationTypeID = null, // No Registration
                    IsNew = false,
                    CurrentRegistrationTypeID = 1,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 66,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = false,
                    PreviousRegistrationTypeID = 1, // Training
                    IsNew = true,
                    CurrentRegistrationTypeID = null,
                    AvailableRegistrationTypeID = 1  // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 67,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = false,
                    PreviousRegistrationTypeID = 1, // Training
                    IsNew = false,
                    CurrentRegistrationTypeID = 1,
                    AvailableRegistrationTypeID = 1  // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 68,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = false,
                    PreviousRegistrationTypeID = 1, // Training
                    IsNew = false,
                    CurrentRegistrationTypeID = 2,
                    AvailableRegistrationTypeID = 1  // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 69,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = false,
                    PreviousRegistrationTypeID = 1, // Training
                    IsNew = false,
                    CurrentRegistrationTypeID = 3,
                    AvailableRegistrationTypeID = 1  // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 70,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = false,
                    PreviousRegistrationTypeID = 1, // Training
                    IsNew = false,
                    CurrentRegistrationTypeID = 4,
                    AvailableRegistrationTypeID = 1  // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 71,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = false,
                    PreviousRegistrationTypeID = 2, // Extension
                    IsNew = true,
                    CurrentRegistrationTypeID = null,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 72,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = false,
                    PreviousRegistrationTypeID = 2, // Extension
                    IsNew = false,
                    CurrentRegistrationTypeID = 1,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 73,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = false,
                    PreviousRegistrationTypeID = 2, // Extension
                    IsNew = false,
                    CurrentRegistrationTypeID = 2,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 74,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = false,
                    PreviousRegistrationTypeID = 2, // Extension
                    IsNew = false,
                    CurrentRegistrationTypeID = 3,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 75,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = false,
                    PreviousRegistrationTypeID = 2, // Extension
                    IsNew = false,
                    CurrentRegistrationTypeID = 4,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 76,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = false,
                    PreviousRegistrationTypeID = 2, // Extension
                    IsNew = true,
                    CurrentRegistrationTypeID = null,
                    AvailableRegistrationTypeID = 3 // Re-Examination1
                },
                new AllowedRegistrationType()
                {
                    ID = 77,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = false,
                    PreviousRegistrationTypeID = 2, // Extension
                    IsNew = false,
                    CurrentRegistrationTypeID = 1,
                    AvailableRegistrationTypeID = 3 // Re-Examination1
                },
                new AllowedRegistrationType()
                {
                    ID = 78,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = false,
                    PreviousRegistrationTypeID = 2, // Extension
                    IsNew = false,
                    CurrentRegistrationTypeID = 2,
                    AvailableRegistrationTypeID = 3 // Re-Examination1
                },
                new AllowedRegistrationType()
                {
                    ID = 79,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = false,
                    PreviousRegistrationTypeID = 2, // Extension
                    IsNew = false,
                    CurrentRegistrationTypeID = 3,
                    AvailableRegistrationTypeID = 3 // Re-Examination1
                },
                new AllowedRegistrationType()
                {
                    ID = 80,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = false,
                    PreviousRegistrationTypeID = 2, // Extension
                    IsNew = false,
                    CurrentRegistrationTypeID = 4,
                    AvailableRegistrationTypeID = 3 // Re-Examination1
                },
                new AllowedRegistrationType()
                {
                    ID = 81,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = false,
                    PreviousRegistrationTypeID = 3, // Re-Examination1
                    IsNew = true,
                    CurrentRegistrationTypeID = null,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 82,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = false,
                    PreviousRegistrationTypeID = 3, // Re-Examination1
                    IsNew = false,
                    CurrentRegistrationTypeID = 1,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 83,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = false,
                    PreviousRegistrationTypeID = 3, // Re-Examination1
                    IsNew = false,
                    CurrentRegistrationTypeID = 2,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 84,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = false,
                    PreviousRegistrationTypeID = 3, // Re-Examination1
                    IsNew = false,
                    CurrentRegistrationTypeID = 3,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 85,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = false,
                    PreviousRegistrationTypeID = 3, // Re-Examination1
                    IsNew = false,
                    CurrentRegistrationTypeID = 4,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 86,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = false,
                    PreviousRegistrationTypeID = 3, // Re-Examination1
                    IsNew = false,
                    CurrentRegistrationTypeID = 1,
                    AvailableRegistrationTypeID = 3 // Re-Examination1
                },
                new AllowedRegistrationType()
                {
                    ID = 87,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = false,
                    PreviousRegistrationTypeID = 3, // Re-Examination1
                    IsNew = false,
                    CurrentRegistrationTypeID = 2,
                    AvailableRegistrationTypeID = 3 // Re-Examination1
                },
                new AllowedRegistrationType()
                {
                    ID = 88,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = false,
                    PreviousRegistrationTypeID = 3, // Re-Examination1
                    IsNew = false,
                    CurrentRegistrationTypeID = 3,
                    AvailableRegistrationTypeID = 3 // Re-Examination1
                },
                new AllowedRegistrationType()
                {
                    ID = 89,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = false,
                    PreviousRegistrationTypeID = 3, // Re-Examination1
                    IsNew = false,
                    CurrentRegistrationTypeID = 4,
                    AvailableRegistrationTypeID = 3 // Re-Examination1
                },
                new AllowedRegistrationType()
                {
                    ID = 90,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = false,
                    PreviousRegistrationTypeID = 3, // Re-Examination1
                    IsNew = true,
                    CurrentRegistrationTypeID = null,
                    AvailableRegistrationTypeID = 4 // Re-Examination2
                },
                new AllowedRegistrationType()
                {
                    ID = 91,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = false,
                    PreviousRegistrationTypeID = 3, // Re-Examination1
                    IsNew = false,
                    CurrentRegistrationTypeID = 1,
                    AvailableRegistrationTypeID = 4 // Re-Examination2
                },
                new AllowedRegistrationType()
                {
                    ID = 92,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = false,
                    PreviousRegistrationTypeID = 3, // Re-Examination1
                    IsNew = false,
                    CurrentRegistrationTypeID = 2,
                    AvailableRegistrationTypeID = 4 // Re-Examination2
                },
                new AllowedRegistrationType()
                {
                    ID = 93,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = false,
                    PreviousRegistrationTypeID = 3, // Re-Examination1
                    IsNew = false,
                    CurrentRegistrationTypeID = 3,
                    AvailableRegistrationTypeID = 4 // Re-Examination2
                },
                new AllowedRegistrationType()
                {
                    ID = 94,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = false,
                    PreviousRegistrationTypeID = 3, // Re-Examination1
                    IsNew = false,
                    CurrentRegistrationTypeID = 4,
                    AvailableRegistrationTypeID = 4 // Re-Examination2
                },
                new AllowedRegistrationType()
                {
                    ID = 95,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = false,
                    PreviousRegistrationTypeID = 4, // Re-Examination2
                    IsNew = true,
                    CurrentRegistrationTypeID = null,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 96,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = false,
                    PreviousRegistrationTypeID = 4, // Re-Examination2
                    IsNew = false,
                    CurrentRegistrationTypeID = 1,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 97,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = false,
                    PreviousRegistrationTypeID = 4, // Re-Examination2
                    IsNew = false,
                    CurrentRegistrationTypeID = 2,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 98,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = false,
                    PreviousRegistrationTypeID = 4, // Re-Examination2
                    IsNew = false,
                    CurrentRegistrationTypeID = 3,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 99,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = false,
                    PreviousRegistrationTypeID = 4, // Re-Examination2
                    IsNew = false,
                    CurrentRegistrationTypeID = 4,
                    AvailableRegistrationTypeID = 4 // Re-Examination2
                },
                new AllowedRegistrationType()
                {
                    ID = 100,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 1, // Training
                    IsNew = true,
                    CurrentRegistrationTypeID = null,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 101,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 1, // Training
                    IsNew = false,
                    CurrentRegistrationTypeID = 1,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 102,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 1, // Training
                    IsNew = false,
                    CurrentRegistrationTypeID = 2,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 103,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 1, // Training
                    IsNew = false,
                    CurrentRegistrationTypeID = 3,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 104,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 1, // Training
                    IsNew = false,
                    CurrentRegistrationTypeID = 4,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 105,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 1, // Training
                    IsNew = true,
                    CurrentRegistrationTypeID = null,
                    AvailableRegistrationTypeID = 2 // Extension
                },
                new AllowedRegistrationType()
                {
                    ID = 106,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 1, // Training
                    IsNew = false,
                    CurrentRegistrationTypeID = 1,
                    AvailableRegistrationTypeID = 2 // Extension
                },
                new AllowedRegistrationType()
                {
                    ID = 107,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 1, // Training
                    IsNew = false,
                    CurrentRegistrationTypeID = 2,
                    AvailableRegistrationTypeID = 2 // Extension
                },
                new AllowedRegistrationType()
                {
                    ID = 108,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 1, // Training
                    IsNew = false,
                    CurrentRegistrationTypeID = 3,
                    AvailableRegistrationTypeID = 2 // Extension
                },
                new AllowedRegistrationType()
                {
                    ID = 109,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 1, // Training
                    IsNew = false,
                    CurrentRegistrationTypeID = 4,
                    AvailableRegistrationTypeID = 2 // Extension
                },
                new AllowedRegistrationType()
                {
                    ID = 110,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 1, // Training
                    IsNew = true,
                    CurrentRegistrationTypeID = null,
                    AvailableRegistrationTypeID = 3 // Re-Examination1
                },
                new AllowedRegistrationType()
                {
                    ID = 111,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 1, // Training
                    IsNew = false,
                    CurrentRegistrationTypeID = 1,
                    AvailableRegistrationTypeID = 3 // Re-Examination1
                },
                new AllowedRegistrationType()
                {
                    ID = 112,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 1, // Training
                    IsNew = false,
                    CurrentRegistrationTypeID = 2,
                    AvailableRegistrationTypeID = 3 // Re-Examination1
                },
                new AllowedRegistrationType()
                {
                    ID = 113,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 1, // Training
                    IsNew = false,
                    CurrentRegistrationTypeID = 3,
                    AvailableRegistrationTypeID = 3 // Re-Examination1
                },
                new AllowedRegistrationType()
                {
                    ID = 114,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 1, // Training
                    IsNew = false,
                    CurrentRegistrationTypeID = 4,
                    AvailableRegistrationTypeID = 3 // Re-Examination1
                },
                new AllowedRegistrationType()
                {
                    ID = 115,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 2, // Extension
                    IsNew = true,
                    CurrentRegistrationTypeID = null,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 116,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 2, // Extension
                    IsNew = false,
                    CurrentRegistrationTypeID = 1,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 117,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 2, // Extension
                    IsNew = false,
                    CurrentRegistrationTypeID = 2,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 118,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 2, // Extension
                    IsNew = false,
                    CurrentRegistrationTypeID = 3,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 119,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 2, // Extension
                    IsNew = false,
                    CurrentRegistrationTypeID = 4,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 120,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 2, // Extension
                    IsNew = false,
                    CurrentRegistrationTypeID = 1,
                    AvailableRegistrationTypeID = 2 // Extension
                },
                new AllowedRegistrationType()
                {
                    ID = 121,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 2, // Extension
                    IsNew = false,
                    CurrentRegistrationTypeID = 2,
                    AvailableRegistrationTypeID = 2 // Extension
                },
                new AllowedRegistrationType()
                {
                    ID = 122,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 2, // Extension
                    IsNew = false,
                    CurrentRegistrationTypeID = 3,
                    AvailableRegistrationTypeID = 2 // Extension
                },
                new AllowedRegistrationType()
                {
                    ID = 123,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 2, // Extension
                    IsNew = false,
                    CurrentRegistrationTypeID = 4,
                    AvailableRegistrationTypeID = 2 // Extension
                },
                new AllowedRegistrationType()
                {
                    ID = 124,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 2, // Extension
                    IsNew = true,
                    CurrentRegistrationTypeID = null,
                    AvailableRegistrationTypeID = 3 // Re-Examination1
                },
                new AllowedRegistrationType()
                {
                    ID = 125,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 2, // Extension
                    IsNew = false,
                    CurrentRegistrationTypeID = 1,
                    AvailableRegistrationTypeID = 3 // Re-Examination1
                },
                new AllowedRegistrationType()
                {
                    ID = 126,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 2, // Extension
                    IsNew = false,
                    CurrentRegistrationTypeID = 2,
                    AvailableRegistrationTypeID = 3 // Re-Examination1
                },
                new AllowedRegistrationType()
                {
                    ID = 127,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 2, // Extension
                    IsNew = false,
                    CurrentRegistrationTypeID = 3,
                    AvailableRegistrationTypeID = 3 // Re-Examination1
                },
                new AllowedRegistrationType()
                {
                    ID = 128,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 2, // Extension
                    IsNew = false,
                    CurrentRegistrationTypeID = 4,
                    AvailableRegistrationTypeID = 3 // Re-Examination1
                },
                new AllowedRegistrationType()
                {
                    ID = 129,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 3, // Re-Examination1
                    IsNew = true,
                    CurrentRegistrationTypeID = null,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 130,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 3, // Re-Examination1
                    IsNew = false,
                    CurrentRegistrationTypeID = 1,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 131,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 3, // Re-Examination1
                    IsNew = false,
                    CurrentRegistrationTypeID = 2,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 132,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 3, // Re-Examination1
                    IsNew = false,
                    CurrentRegistrationTypeID = 3,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 133,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 3, // Re-Examination1
                    IsNew = false,
                    CurrentRegistrationTypeID = 4,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 134,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 3, // Re-Examination1
                    IsNew = true,
                    CurrentRegistrationTypeID = null,
                    AvailableRegistrationTypeID = 2 // Extension
                },
                new AllowedRegistrationType()
                {
                    ID = 135,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 3, // Re-Examination1
                    IsNew = false,
                    CurrentRegistrationTypeID = 1,
                    AvailableRegistrationTypeID = 2 // Extension
                },
                new AllowedRegistrationType()
                {
                    ID = 136,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 3, // Re-Examination1
                    IsNew = false,
                    CurrentRegistrationTypeID = 2,
                    AvailableRegistrationTypeID = 2 // Extension
                },
                new AllowedRegistrationType()
                {
                    ID = 137,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 3, // Re-Examination1
                    IsNew = false,
                    CurrentRegistrationTypeID = 3,
                    AvailableRegistrationTypeID = 2 // Extension
                },
                new AllowedRegistrationType()
                {
                    ID = 138,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 3, // Re-Examination1
                    IsNew = false,
                    CurrentRegistrationTypeID = 4,
                    AvailableRegistrationTypeID = 2 // Extension
                },
                new AllowedRegistrationType()
                {
                    ID = 139,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 3, // Re-Examination1
                    IsNew = false,
                    CurrentRegistrationTypeID = 1,
                    AvailableRegistrationTypeID = 3 // Re-Examination1
                },
                new AllowedRegistrationType()
                {
                    ID = 140,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 3, // Re-Examination1
                    IsNew = false,
                    CurrentRegistrationTypeID = 2,
                    AvailableRegistrationTypeID = 3 // Re-Examination1
                },
                new AllowedRegistrationType()
                {
                    ID = 141,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 3, // Re-Examination1
                    IsNew = false,
                    CurrentRegistrationTypeID = 3,
                    AvailableRegistrationTypeID = 3 // Re-Examination1
                },
                new AllowedRegistrationType()
                {
                    ID = 142,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 3, // Re-Examination1
                    IsNew = false,
                    CurrentRegistrationTypeID = 4,
                    AvailableRegistrationTypeID = 3 // Re-Examination1
                },
                new AllowedRegistrationType()
                {
                    ID = 143,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 4, // Re-Examination2
                    IsNew = true,
                    CurrentRegistrationTypeID = null,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 144,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 4, // Re-Examination2
                    IsNew = false,
                    CurrentRegistrationTypeID = 1,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 145,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 4, // Re-Examination2
                    IsNew = false,
                    CurrentRegistrationTypeID = 2,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 146,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 4, // Re-Examination2
                    IsNew = false,
                    CurrentRegistrationTypeID = 3,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 147,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 4, // Re-Examination2
                    IsNew = false,
                    CurrentRegistrationTypeID = 4,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 148,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 4, // Re-Examination2
                    IsNew = true,
                    CurrentRegistrationTypeID = null,
                    AvailableRegistrationTypeID = 2 // Extension
                },
                new AllowedRegistrationType()
                {
                    ID = 149,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 4, // Re-Examination2
                    IsNew = false,
                    CurrentRegistrationTypeID = 1,
                    AvailableRegistrationTypeID = 2 // Extension
                },
                new AllowedRegistrationType()
                {
                    ID = 150,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 4, // Re-Examination2
                    IsNew = false,
                    CurrentRegistrationTypeID = 2,
                    AvailableRegistrationTypeID = 2 // Extension
                },
                new AllowedRegistrationType()
                {
                    ID = 151,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 4, // Re-Examination2
                    IsNew = false,
                    CurrentRegistrationTypeID = 3,
                    AvailableRegistrationTypeID = 2 // Extension
                },
                new AllowedRegistrationType()
                {
                    ID = 152,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 4, // Re-Examination2
                    IsNew = false,
                    CurrentRegistrationTypeID = 4,
                    AvailableRegistrationTypeID = 2 // Extension
                },
                new AllowedRegistrationType()
                {
                    ID = 153,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 4, // Re-Examination2
                    IsNew = false,
                    CurrentRegistrationTypeID = 1,
                    AvailableRegistrationTypeID = 4 // Re-Examination2
                },
                new AllowedRegistrationType()
                {
                    ID = 154,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 4, // Re-Examination2
                    IsNew = false,
                    CurrentRegistrationTypeID = 2,
                    AvailableRegistrationTypeID = 4 // Re-Examination2
                },
                new AllowedRegistrationType()
                {
                    ID = 155,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 4, // Re-Examination2
                    IsNew = false,
                    CurrentRegistrationTypeID = 3,
                    AvailableRegistrationTypeID = 4 // Re-Examination2
                },
                new AllowedRegistrationType()
                {
                    ID = 156,
                    ExtendableStatus = ExtendableStatus.Extendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 4, // Re-Examination2
                    IsNew = false,
                    CurrentRegistrationTypeID = 4,
                    AvailableRegistrationTypeID = 4 // Re-Examination2
                },
            #endregion

                #region NoMoreExtendable
                new AllowedRegistrationType()
                {
                    ID = 157,
                    ExtendableStatus = ExtendableStatus.NoMoreExtendable,
                    HasPassed = null,
                    PreviousRegistrationTypeID = null, // No Registration
                    IsNew = false,
                    CurrentRegistrationTypeID = 1,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 158,
                    ExtendableStatus = ExtendableStatus.NoMoreExtendable,
                    HasPassed = null,
                    PreviousRegistrationTypeID = null, // No Registration
                    IsNew = true,
                    CurrentRegistrationTypeID = null,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 159,
                    ExtendableStatus = ExtendableStatus.NoMoreExtendable,
                    HasPassed = false,
                    PreviousRegistrationTypeID = 1, // Training
                    IsNew = false,
                    CurrentRegistrationTypeID = 1,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 160,
                    ExtendableStatus = ExtendableStatus.NoMoreExtendable,
                    HasPassed = false,
                    PreviousRegistrationTypeID = 1, // Training
                    IsNew = false,
                    CurrentRegistrationTypeID = 2,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 161,
                    ExtendableStatus = ExtendableStatus.NoMoreExtendable,
                    HasPassed = false,
                    PreviousRegistrationTypeID = 1, // Training
                    IsNew = false,
                    CurrentRegistrationTypeID = 3,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 162,
                    ExtendableStatus = ExtendableStatus.NoMoreExtendable,
                    HasPassed = false,
                    PreviousRegistrationTypeID = 1, // Training
                    IsNew = false,
                    CurrentRegistrationTypeID = 4,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 163,
                    ExtendableStatus = ExtendableStatus.NoMoreExtendable,
                    HasPassed = false,
                    PreviousRegistrationTypeID = 1, // Training
                    IsNew = true,
                    CurrentRegistrationTypeID = null,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 164,
                    ExtendableStatus = ExtendableStatus.NoMoreExtendable,
                    HasPassed = false,
                    PreviousRegistrationTypeID = 2, // Extension
                    IsNew = false,
                    CurrentRegistrationTypeID = 1,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 165,
                    ExtendableStatus = ExtendableStatus.NoMoreExtendable,
                    HasPassed = false,
                    PreviousRegistrationTypeID = 2, // Extension
                    IsNew = false,
                    CurrentRegistrationTypeID = 2,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 166,
                    ExtendableStatus = ExtendableStatus.NoMoreExtendable,
                    HasPassed = false,
                    PreviousRegistrationTypeID = 2, // Extension
                    IsNew = false,
                    CurrentRegistrationTypeID = 3,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 167,
                    ExtendableStatus = ExtendableStatus.NoMoreExtendable,
                    HasPassed = false,
                    PreviousRegistrationTypeID = 2, // Extension
                    IsNew = false,
                    CurrentRegistrationTypeID = 4,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 168,
                    ExtendableStatus = ExtendableStatus.NoMoreExtendable,
                    HasPassed = false,
                    PreviousRegistrationTypeID = 2, // Extension
                    IsNew = false,
                    CurrentRegistrationTypeID = 1,
                    AvailableRegistrationTypeID = 2 // Extension
                },
                new AllowedRegistrationType()
                {
                    ID = 169,
                    ExtendableStatus = ExtendableStatus.NoMoreExtendable,
                    HasPassed = false,
                    PreviousRegistrationTypeID = 2, // Extension
                    IsNew = false,
                    CurrentRegistrationTypeID = 2,
                    AvailableRegistrationTypeID = 2 // Extension
                },
                new AllowedRegistrationType()
                {
                    ID = 170,
                    ExtendableStatus = ExtendableStatus.NoMoreExtendable,
                    HasPassed = false,
                    PreviousRegistrationTypeID = 2, // Extension
                    IsNew = false,
                    CurrentRegistrationTypeID = 3,
                    AvailableRegistrationTypeID = 2 // Extension
                },
                new AllowedRegistrationType()
                {
                    ID = 171,
                    ExtendableStatus = ExtendableStatus.NoMoreExtendable,
                    HasPassed = false,
                    PreviousRegistrationTypeID = 2, // Extension
                    IsNew = false,
                    CurrentRegistrationTypeID = 4,
                    AvailableRegistrationTypeID = 2 // Extension
                },
                new AllowedRegistrationType()
                {
                    ID = 172,
                    ExtendableStatus = ExtendableStatus.NoMoreExtendable,
                    HasPassed = false,
                    PreviousRegistrationTypeID = 2, // Extension
                    IsNew = true,
                    CurrentRegistrationTypeID = null,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 173,
                    ExtendableStatus = ExtendableStatus.NoMoreExtendable,
                    HasPassed = false,
                    PreviousRegistrationTypeID = 3, // Re-Examination1
                    IsNew= false,
                    CurrentRegistrationTypeID = 1,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 174,
                    ExtendableStatus = ExtendableStatus.NoMoreExtendable,
                    HasPassed = false,
                    PreviousRegistrationTypeID = 3, // Re-Examination1
                    IsNew= false,
                    CurrentRegistrationTypeID = 2,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 175,
                    ExtendableStatus = ExtendableStatus.NoMoreExtendable,
                    HasPassed = false,
                    PreviousRegistrationTypeID = 3, // Re-Examination1
                    IsNew= false,
                    CurrentRegistrationTypeID = 3,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 176,
                    ExtendableStatus = ExtendableStatus.NoMoreExtendable,
                    HasPassed = false,
                    PreviousRegistrationTypeID = 3, // Re-Examination1
                    IsNew= false,
                    CurrentRegistrationTypeID = 4,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 177,
                    ExtendableStatus = ExtendableStatus.NoMoreExtendable,
                    HasPassed = false,
                    PreviousRegistrationTypeID = 3, // Re-Examination1
                    IsNew= false,
                    CurrentRegistrationTypeID = 1,
                    AvailableRegistrationTypeID = 3 // Re-Examination1
                },
                new AllowedRegistrationType()
                {
                    ID = 178,
                    ExtendableStatus = ExtendableStatus.NoMoreExtendable,
                    HasPassed = false,
                    PreviousRegistrationTypeID = 3, // Re-Examination1
                    IsNew= false,
                    CurrentRegistrationTypeID = 2,
                    AvailableRegistrationTypeID = 3 // Re-Examination1
                },
                new AllowedRegistrationType()
                {
                    ID = 179,
                    ExtendableStatus = ExtendableStatus.NoMoreExtendable,
                    HasPassed = false,
                    PreviousRegistrationTypeID = 3, // Re-Examination1
                    IsNew= false,
                    CurrentRegistrationTypeID = 3,
                    AvailableRegistrationTypeID = 3 // Re-Examination1
                },
                new AllowedRegistrationType()
                {
                    ID = 180,
                    ExtendableStatus = ExtendableStatus.NoMoreExtendable,
                    HasPassed = false,
                    PreviousRegistrationTypeID = 3, // Re-Examination1
                    IsNew= false,
                    CurrentRegistrationTypeID = 4,
                    AvailableRegistrationTypeID = 3 // Re-Examination1
                },
                new AllowedRegistrationType()
                {
                    ID = 181,
                    ExtendableStatus = ExtendableStatus.NoMoreExtendable,
                    HasPassed = false,
                    PreviousRegistrationTypeID = 3, // Re-Examination1
                    IsNew= true,
                    CurrentRegistrationTypeID = null,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 182,
                    ExtendableStatus = ExtendableStatus.NoMoreExtendable,
                    HasPassed = false,
                    PreviousRegistrationTypeID = 4, // Re-Examination2
                    IsNew = false,
                    CurrentRegistrationTypeID = 1,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 183,
                    ExtendableStatus = ExtendableStatus.NoMoreExtendable,
                    HasPassed = false,
                    PreviousRegistrationTypeID = 4, // Re-Examination2
                    IsNew = false,
                    CurrentRegistrationTypeID = 2,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 184,
                    ExtendableStatus = ExtendableStatus.NoMoreExtendable,
                    HasPassed = false,
                    PreviousRegistrationTypeID = 4, // Re-Examination2
                    IsNew = false,
                    CurrentRegistrationTypeID = 3,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 185,
                    ExtendableStatus = ExtendableStatus.NoMoreExtendable,
                    HasPassed = false,
                    PreviousRegistrationTypeID = 4, // Re-Examination2
                    IsNew = false,
                    CurrentRegistrationTypeID = 4,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 186,
                    ExtendableStatus = ExtendableStatus.NoMoreExtendable,
                    HasPassed = false,
                    PreviousRegistrationTypeID = 4, // Re-Examination2
                    IsNew = false,
                    CurrentRegistrationTypeID = 1,
                    AvailableRegistrationTypeID = 4 // Re-Examination2
                },
                new AllowedRegistrationType()
                {
                    ID = 187,
                    ExtendableStatus = ExtendableStatus.NoMoreExtendable,
                    HasPassed = false,
                    PreviousRegistrationTypeID = 4, // Re-Examination2
                    IsNew = false,
                    CurrentRegistrationTypeID = 2,
                    AvailableRegistrationTypeID = 4 // Re-Examination2
                },
                new AllowedRegistrationType()
                {
                    ID = 188,
                    ExtendableStatus = ExtendableStatus.NoMoreExtendable,
                    HasPassed = false,
                    PreviousRegistrationTypeID = 4, // Re-Examination2
                    IsNew = false,
                    CurrentRegistrationTypeID = 3,
                    AvailableRegistrationTypeID = 4 // Re-Examination2
                },
                new AllowedRegistrationType()
                {
                    ID = 189,
                    ExtendableStatus = ExtendableStatus.NoMoreExtendable,
                    HasPassed = false,
                    PreviousRegistrationTypeID = 4, // Re-Examination2
                    IsNew = false,
                    CurrentRegistrationTypeID = 4,
                    AvailableRegistrationTypeID = 4 // Re-Examination2
                },
                new AllowedRegistrationType()
                {
                    ID = 190,
                    ExtendableStatus = ExtendableStatus.NoMoreExtendable,
                    HasPassed = false,
                    PreviousRegistrationTypeID = 4, // Re-Examination2
                    IsNew = true,
                    CurrentRegistrationTypeID = null,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 191,
                    ExtendableStatus = ExtendableStatus.NoMoreExtendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 1, // Training
                    IsNew = false,
                    CurrentRegistrationTypeID = 1,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 192,
                    ExtendableStatus = ExtendableStatus.NoMoreExtendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 1, // Training
                    IsNew = false,
                    CurrentRegistrationTypeID = 2,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 193,
                    ExtendableStatus = ExtendableStatus.NoMoreExtendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 1, // Training
                    IsNew = false,
                    CurrentRegistrationTypeID = 3,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 194,
                    ExtendableStatus = ExtendableStatus.NoMoreExtendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 1, // Training
                    IsNew = false,
                    CurrentRegistrationTypeID = 4,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 195,
                    ExtendableStatus = ExtendableStatus.NoMoreExtendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 1, // Training
                    IsNew = true,
                    CurrentRegistrationTypeID = null,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 196,
                    ExtendableStatus = ExtendableStatus.NoMoreExtendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 2, // Extension
                    IsNew = false,
                    CurrentRegistrationTypeID = 1,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 197,
                    ExtendableStatus = ExtendableStatus.NoMoreExtendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 2, // Extension
                    IsNew = false,
                    CurrentRegistrationTypeID = 2,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 198,
                    ExtendableStatus = ExtendableStatus.NoMoreExtendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 2, // Extension
                    IsNew = false,
                    CurrentRegistrationTypeID = 3,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 199,
                    ExtendableStatus = ExtendableStatus.NoMoreExtendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 2, // Extension
                    IsNew = false,
                    CurrentRegistrationTypeID = 4,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 200,
                    ExtendableStatus = ExtendableStatus.NoMoreExtendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 2, // Extension
                    IsNew = false,
                    CurrentRegistrationTypeID = 1,
                    AvailableRegistrationTypeID = 2 // Extension
                },
                new AllowedRegistrationType()
                {
                    ID = 201,
                    ExtendableStatus = ExtendableStatus.NoMoreExtendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 2, // Extension
                    IsNew = false,
                    CurrentRegistrationTypeID = 2,
                    AvailableRegistrationTypeID = 2 // Extension
                },
                new AllowedRegistrationType()
                {
                    ID = 202,
                    ExtendableStatus = ExtendableStatus.NoMoreExtendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 2, // Extension
                    IsNew = false,
                    CurrentRegistrationTypeID = 3,
                    AvailableRegistrationTypeID = 2 // Extension
                },
                new AllowedRegistrationType()
                {
                    ID = 203,
                    ExtendableStatus = ExtendableStatus.NoMoreExtendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 2, // Extension
                    IsNew = false,
                    CurrentRegistrationTypeID = 4,
                    AvailableRegistrationTypeID = 2 // Extension
                },
                new AllowedRegistrationType()
                {
                    ID = 204,
                    ExtendableStatus = ExtendableStatus.NoMoreExtendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 2, // Extension
                    IsNew = true,
                    CurrentRegistrationTypeID = null,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 205,
                    ExtendableStatus = ExtendableStatus.NoMoreExtendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 3, // Re-Examination1
                    IsNew = false,
                    CurrentRegistrationTypeID = 1,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 206,
                    ExtendableStatus = ExtendableStatus.NoMoreExtendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 3, // Re-Examination1
                    IsNew = false,
                    CurrentRegistrationTypeID = 2,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 207,
                    ExtendableStatus = ExtendableStatus.NoMoreExtendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 3, // Re-Examination1
                    IsNew = false,
                    CurrentRegistrationTypeID = 3,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 208,
                    ExtendableStatus = ExtendableStatus.NoMoreExtendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 3, // Re-Examination1
                    IsNew = false,
                    CurrentRegistrationTypeID = 4,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 209,
                    ExtendableStatus = ExtendableStatus.NoMoreExtendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 3, // Re-Examination1
                    IsNew = false,
                    CurrentRegistrationTypeID = 1,
                    AvailableRegistrationTypeID = 3 // Re-Examination1
                },
                new AllowedRegistrationType()
                {
                    ID = 210,
                    ExtendableStatus = ExtendableStatus.NoMoreExtendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 3, // Re-Examination1
                    IsNew = false,
                    CurrentRegistrationTypeID = 2,
                    AvailableRegistrationTypeID = 3 // Re-Examination1
                },
                new AllowedRegistrationType()
                {
                    ID = 211,
                    ExtendableStatus = ExtendableStatus.NoMoreExtendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 3, // Re-Examination1
                    IsNew = false,
                    CurrentRegistrationTypeID = 3,
                    AvailableRegistrationTypeID = 3 // Re-Examination1
                },
                new AllowedRegistrationType()
                {
                    ID = 212,
                    ExtendableStatus = ExtendableStatus.NoMoreExtendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 3, // Re-Examination1
                    IsNew = false,
                    CurrentRegistrationTypeID = 4,
                    AvailableRegistrationTypeID = 3 // Re-Examination1
                },
                new AllowedRegistrationType()
                {
                    ID = 213,
                    ExtendableStatus = ExtendableStatus.NoMoreExtendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 3, // Re-Examination1
                    IsNew = true,
                    CurrentRegistrationTypeID = null,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 214,
                    ExtendableStatus = ExtendableStatus.NoMoreExtendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 4, // Re-Examination2
                    IsNew = false,
                    CurrentRegistrationTypeID = 1,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 215,
                    ExtendableStatus = ExtendableStatus.NoMoreExtendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 4, // Re-Examination2
                    IsNew = false,
                    CurrentRegistrationTypeID = 2,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 216,
                    ExtendableStatus = ExtendableStatus.NoMoreExtendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 4, // Re-Examination2
                    IsNew = false,
                    CurrentRegistrationTypeID = 3,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 217,
                    ExtendableStatus = ExtendableStatus.NoMoreExtendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 4, // Re-Examination2
                    IsNew = false,
                    CurrentRegistrationTypeID = 4,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 218,
                    ExtendableStatus = ExtendableStatus.NoMoreExtendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 4, // Re-Examination2
                    IsNew = false,
                    CurrentRegistrationTypeID = 1,
                    AvailableRegistrationTypeID = 4 // Re-Examination2
                },
                new AllowedRegistrationType()
                {
                    ID = 219,
                    ExtendableStatus = ExtendableStatus.NoMoreExtendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 4, // Re-Examination2
                    IsNew = false,
                    CurrentRegistrationTypeID = 2,
                    AvailableRegistrationTypeID = 4 // Re-Examination2
                },
                new AllowedRegistrationType()
                {
                    ID = 220,
                    ExtendableStatus = ExtendableStatus.NoMoreExtendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 4, // Re-Examination2
                    IsNew = false,
                    CurrentRegistrationTypeID = 3,
                    AvailableRegistrationTypeID = 4 // Re-Examination2
                },
                new AllowedRegistrationType()
                {
                    ID = 221,
                    ExtendableStatus = ExtendableStatus.NoMoreExtendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 4, // Re-Examination2
                    IsNew = false,
                    CurrentRegistrationTypeID = 4,
                    AvailableRegistrationTypeID = 4 // Re-Examination2
                },
                new AllowedRegistrationType()
                {
                    ID = 222,
                    ExtendableStatus = ExtendableStatus.NoMoreExtendable,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 4, // Re-Examination2
                    IsNew = true,
                    CurrentRegistrationTypeID = null,
                    AvailableRegistrationTypeID = 1 // Training
                },
            #endregion

                #region Revoked
                new AllowedRegistrationType()
                {
                    ID = 223,
                    ExtendableStatus = ExtendableStatus.Revoked,
                    HasPassed = null,
                    PreviousRegistrationTypeID = null, // No Registration
                    IsNew = false,
                    CurrentRegistrationTypeID = 1,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 224,
                    ExtendableStatus = ExtendableStatus.Revoked,
                    HasPassed = null,
                    PreviousRegistrationTypeID = null, // No Registration
                    IsNew = true,
                    CurrentRegistrationTypeID = null,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 225,
                    ExtendableStatus = ExtendableStatus.Revoked,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 1, // Training
                    IsNew = false,
                    CurrentRegistrationTypeID = 1,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 226,
                    ExtendableStatus = ExtendableStatus.Revoked,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 1, // Training
                    IsNew = false,
                    CurrentRegistrationTypeID = 2,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 227,
                    ExtendableStatus = ExtendableStatus.Revoked,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 1, // Training
                    IsNew = false,
                    CurrentRegistrationTypeID = 3,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 228,
                    ExtendableStatus = ExtendableStatus.Revoked,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 1, // Training
                    IsNew = false,
                    CurrentRegistrationTypeID = 4,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 229,
                    ExtendableStatus = ExtendableStatus.Revoked,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 1, // Training
                    IsNew = true,
                    CurrentRegistrationTypeID = null,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 230,
                    ExtendableStatus = ExtendableStatus.Revoked,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 2, // Extension
                    IsNew = false,
                    CurrentRegistrationTypeID = 1,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 231,
                    ExtendableStatus = ExtendableStatus.Revoked,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 2, // Extension
                    IsNew = false,
                    CurrentRegistrationTypeID = 2,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 232,
                    ExtendableStatus = ExtendableStatus.Revoked,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 2, // Extension
                    IsNew = false,
                    CurrentRegistrationTypeID = 3,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 233,
                    ExtendableStatus = ExtendableStatus.Revoked,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 2, // Extension
                    IsNew = false,
                    CurrentRegistrationTypeID = 4,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 234,
                    ExtendableStatus = ExtendableStatus.Revoked,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 2, // Extension
                    IsNew = false,
                    CurrentRegistrationTypeID = 1,
                    AvailableRegistrationTypeID = 2 // Extension
                },
                new AllowedRegistrationType()
                {
                    ID = 235,
                    ExtendableStatus = ExtendableStatus.Revoked,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 2, // Extension
                    IsNew = false,
                    CurrentRegistrationTypeID = 2,
                    AvailableRegistrationTypeID = 2 // Extension
                },
                new AllowedRegistrationType()
                {
                    ID = 236,
                    ExtendableStatus = ExtendableStatus.Revoked,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 2, // Extension
                    IsNew = false,
                    CurrentRegistrationTypeID = 3,
                    AvailableRegistrationTypeID = 2 // Extension
                },
                new AllowedRegistrationType()
                {
                    ID = 237,
                    ExtendableStatus = ExtendableStatus.Revoked,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 2, // Extension
                    IsNew = false,
                    CurrentRegistrationTypeID = 4,
                    AvailableRegistrationTypeID = 2 // Extension
                },
                new AllowedRegistrationType()
                {
                    ID = 238,
                    ExtendableStatus = ExtendableStatus.Revoked,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 2, // Extension
                    IsNew = true,
                    CurrentRegistrationTypeID = null,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 239,
                    ExtendableStatus = ExtendableStatus.Revoked,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 3, // Re-Examination1
                    IsNew = false,
                    CurrentRegistrationTypeID = 1,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 240,
                    ExtendableStatus = ExtendableStatus.Revoked,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 3, // Re-Examination1
                    IsNew = false,
                    CurrentRegistrationTypeID = 2,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 241,
                    ExtendableStatus = ExtendableStatus.Revoked,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 3, // Re-Examination1
                    IsNew = false,
                    CurrentRegistrationTypeID = 3,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 242,
                    ExtendableStatus = ExtendableStatus.Revoked,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 3, // Re-Examination1
                    IsNew = false,
                    CurrentRegistrationTypeID = 4,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 243,
                    ExtendableStatus = ExtendableStatus.Revoked,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 3, // Re-Examination1
                    IsNew = true,
                    CurrentRegistrationTypeID = null,
                    AvailableRegistrationTypeID = 3 // Re-Examination1
                },
                new AllowedRegistrationType()
                {
                    ID = 244,
                    ExtendableStatus = ExtendableStatus.Revoked,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 3, // Re-Examination1
                    IsNew = true,
                    CurrentRegistrationTypeID = null,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 245,
                    ExtendableStatus = ExtendableStatus.Revoked,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 4, // Re-Examination2
                    IsNew = false,
                    CurrentRegistrationTypeID = 1,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 246,
                    ExtendableStatus = ExtendableStatus.Revoked,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 4, // Re-Examination2
                    IsNew = false,
                    CurrentRegistrationTypeID = 2,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 247,
                    ExtendableStatus = ExtendableStatus.Revoked,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 4, // Re-Examination2
                    IsNew = false,
                    CurrentRegistrationTypeID = 3,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 248,
                    ExtendableStatus = ExtendableStatus.Revoked,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 4, // Re-Examination2
                    IsNew = false,
                    CurrentRegistrationTypeID = 4,
                    AvailableRegistrationTypeID = 1 // Training
                },
                new AllowedRegistrationType()
                {
                    ID = 249,
                    ExtendableStatus = ExtendableStatus.Revoked,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 4, // Re-Examination2
                    IsNew = false,
                    CurrentRegistrationTypeID = 1,
                    AvailableRegistrationTypeID = 4 // Re-Examination2
                },
                new AllowedRegistrationType()
                {
                    ID = 250,
                    ExtendableStatus = ExtendableStatus.Revoked,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 4, // Re-Examination2
                    IsNew = false,
                    CurrentRegistrationTypeID = 2,
                    AvailableRegistrationTypeID = 4 // Re-Examination2
                },
                new AllowedRegistrationType()
                {
                    ID = 251,
                    ExtendableStatus = ExtendableStatus.Revoked,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 4, // Re-Examination2
                    IsNew = false,
                    CurrentRegistrationTypeID = 3,
                    AvailableRegistrationTypeID = 4 // Re-Examination2
                },
                new AllowedRegistrationType()
                {
                    ID = 252,
                    ExtendableStatus = ExtendableStatus.Revoked,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 4, // Re-Examination2
                    IsNew = false,
                    CurrentRegistrationTypeID = 4,
                    AvailableRegistrationTypeID = 4 // Re-Examination2
                },
                new AllowedRegistrationType()
                {
                    ID = 253,
                    ExtendableStatus = ExtendableStatus.Revoked,
                    HasPassed = true,
                    PreviousRegistrationTypeID = 4, // Re-Examination2
                    IsNew = true,
                    CurrentRegistrationTypeID = null,
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
