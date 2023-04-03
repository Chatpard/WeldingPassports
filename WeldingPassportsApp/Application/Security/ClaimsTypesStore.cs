using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;

namespace Application.Security
{
    public static class ClaimsTypesStore
    {
        public const string PEPassports = "PEPassports";
        public const string Welders = "Welders";
        public const string TrainingCenters = "TrainingCenters";
        public const string Examinations = "Examinations";
        public const string Certificates = "Certificates";
        public const string ExamCenters = "ExamCenters";
        public const string CompanyContacts = "CompanyContacts";
        public const string Companies = "Companies";
        public const string Addresses = "Addresses";
        public const string Contacts = "Contacts";
        public const string Admin = "Admin";

        public static List<string> ClaimsTypes()
        {
            return new List<string> { PEPassports, Welders, TrainingCenters, Examinations, Certificates, ExamCenters, CompanyContacts, Companies, Addresses, Contacts, Admin };
        }

        public static Dictionary<string, string> Claims(string role)
        {
            switch (role)
            {
                case RolesStore.Admin:
                    return new Dictionary<string, string>
                        {
                            {PEPassports, ClaimsValuesStore.CanReadEditCreateDelete },
                            {Welders, ClaimsValuesStore.CanReadEditCreateDelete },
                            {TrainingCenters, ClaimsValuesStore.CanReadEditCreateDelete },
                            {Examinations, ClaimsValuesStore.CanReadEditCreateDelete },
                            {Certificates, ClaimsValuesStore.CanUpdateRevokeReadEditCreateDelete },
                            {ExamCenters, ClaimsValuesStore.CanReadEditCreateDelete },
                            {CompanyContacts, ClaimsValuesStore.CanReadEditCreateDelete },
                            {Companies, ClaimsValuesStore.CanReadEditCreateDelete },
                            {Addresses, ClaimsValuesStore.CanReadEditCreateDelete },
                            {Contacts, ClaimsValuesStore.CanReadEditCreateDelete },
                            {Admin, ClaimsValuesStore.CanReadEditCreateDelete }
                        };
                case RolesStore.TC:
                    return new Dictionary<string, string>
                        {
                            {PEPassports, ClaimsValuesStore.CanReadEditCreateDelete },
                            {Welders, ClaimsValuesStore.CanReadEditCreateDelete },
                            {TrainingCenters, ClaimsValuesStore.CanReadEdit },
                            {Examinations, ClaimsValuesStore.CanReadEditCreateDelete },
                            {Certificates, ClaimsValuesStore.CanReadEditCreateDelete },
                            {ExamCenters, ClaimsValuesStore.CanRead },
                            {CompanyContacts, ClaimsValuesStore.CanRead },
                            {Companies, ClaimsValuesStore.CanReadEditCreateDelete },
                            {Addresses, ClaimsValuesStore.CanReadEditCreateDelete },
                            {Contacts, ClaimsValuesStore.CanReadEditCreateDelete }
                        };
                case RolesStore.DSO:
                    return new Dictionary<string, string>
                        {
                            {PEPassports, ClaimsValuesStore.CanRead },
                            {Welders, ClaimsValuesStore.CanRead },
                            {TrainingCenters, ClaimsValuesStore.CanRead },
                            {Examinations, ClaimsValuesStore.CanRead },
                            {Certificates, ClaimsValuesStore.CanRevoke },
                            {ExamCenters, ClaimsValuesStore.CanRead },
                            {CompanyContacts, ClaimsValuesStore.CanRead },
                            {Companies, ClaimsValuesStore.CanRead },
                            {Addresses, ClaimsValuesStore.CanRead },
                            {Contacts, ClaimsValuesStore.CanRead }
                        };
                case RolesStore.EC:
                    return new Dictionary<string, string>
                        {
                            {PEPassports, ClaimsValuesStore.CanRead },
                            {Welders, ClaimsValuesStore.CanRead },
                            {TrainingCenters, ClaimsValuesStore.CanRead },
                            {Examinations, ClaimsValuesStore.CanRead },
                            {Certificates, ClaimsValuesStore.CanUpdate },
                            {ExamCenters, ClaimsValuesStore.CanReadEdit },
                            {CompanyContacts, ClaimsValuesStore.CanRead },
                            {Companies, ClaimsValuesStore.CanReadEdit},
                            {Addresses, ClaimsValuesStore.CanReadEditCreateDelete },
                            {Contacts, ClaimsValuesStore.CanReadEditCreateDelete }
                        };
            }
            return new Dictionary<string, string>();
        }
    }
}
