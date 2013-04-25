
/// <summary>
/// Generic methods for getting data in and out of HealthVault
/// </summary>
using Microsoft.Health;
using System;
using Microsoft.Health.Web;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Microsoft.Health.ItemTypes;

namespace HealthVaultHelper
{

    public class HVDataAccessor
    {

        public static Guid[] allKnownTypeIds = 
        {
            Basic.TypeId,
            Personal.TypeId,
            Allergy.TypeId,
            Height.TypeId,
            BloodGlucose.TypeId,
            BloodPressure.TypeId,
            Condition.TypeId,
            Procedure.TypeId,
            Medication.TypeId,
            Weight.TypeId,
        };

        public Participant Participant { get; set; }

        // Searcher for HV items. Will create connection and searcher on first access.
        private HealthRecordSearcher _searcher;
        public HealthRecordSearcher Searcher
        {
            get
            {
                if (_searcher == null && Participant != null)
                {
                    OfflineWebApplicationConnection conn = HVConnectionManager.CreateConnection(Participant.HVPersonID);
                    HealthRecordAccessor accessor = new HealthRecordAccessor(conn, Participant.HVRecordID);
                    _searcher = accessor.CreateSearcher();
                }
                return _searcher;
            }
        }

        // All records in search results. Returns cached results if they exist. Else does the search
        private ReadOnlyCollection<HealthRecordItemCollection> _AllRecordItems;
        public ReadOnlyCollection<HealthRecordItemCollection> AllRecordItems
        {
            get
            {
                if (_AllRecordItems == null)
                {
                    _AllRecordItems = Searcher.GetMatchingItems();
                }
                return _AllRecordItems;
            }
        }

        // maps a HV TypeID guid to the index its index in the search results collection
        private Dictionary<Guid, int> filterMap;

        public HVDataAccessor(Participant participant)
        {
            this.Participant = participant;
            this.filterMap = new Dictionary<Guid, int>();
        }

        public void AddFilter(Guid TypeId)
        {
            if (filterMap.ContainsKey(TypeId))
                return;
            Searcher.Filters.Add(new HealthRecordFilter(TypeId));
            int filterIndex = Searcher.Filters.Count - 1;
            filterMap.Add(TypeId, filterIndex);
        }

        public void AddAllFilters()
        {
            foreach (Guid typeID in allKnownTypeIds)
            {
                AddFilter(typeID);
            }
        }

        public HealthRecordItemCollection GetItemCollection(Guid TypeId)
        {
            return AllRecordItems[filterMap[TypeId]];
        }

        // returns the first record item
        public HealthRecordItem GetItem(Guid TypeId)
        {
            return GetItem(TypeId, 0);
        }

        private HealthRecordItem GetItem(Guid TypeId, int index)
        {
            HealthRecordItemCollection collection = GetItemCollection(TypeId);
            if (collection.Count > index)
            {
                return collection[index];
            }
            return null;
        }

        public String GetItemString(Guid TypeId)
        {
            return GetItemString(TypeId, "");
        }

        // returns the items ToString(), or else the `Default`
        public String GetItemString(Guid TypeId, String Default)
        {
            HealthRecordItem item = GetItem(TypeId);
            return item != null ? item.ToString() : Default;
        }

        // Returns CCD (as a string) of all items in the filter list.
        public string getCcd()
        {
            return Searcher.GetTransformedItems("toccd");
        }


        // Assumes that the Basic and Personal info has been added to this accessor.
        // returns something like "Age 24 (M)"
        public string BuildBasicInfoString()
        {
            if (!filterMap.ContainsKey(Basic.TypeId))
                AddFilter(Basic.TypeId);

            if (!filterMap.ContainsKey(Personal.TypeId))

                AddFilter(Personal.TypeId);

            string basicInfo = "";
            string genderStr = GetGenderString();
            int age = GetAge();
            if (age > 0)
                basicInfo = "Age " + age;
            if (!String.IsNullOrEmpty(genderStr))
                basicInfo += " (" + genderStr + ")";
            return basicInfo;
        }

        private string GetGenderString()
        {
            string genderStr = "";
            Basic basicInfo = (Basic)GetItem(Basic.TypeId);
            switch (basicInfo.Gender)
            {
                case Gender.Male:
                    genderStr = "M";
                    break;
                case Gender.Female:
                    genderStr = "F";
                    break;
            }
            return genderStr;
        }

        private int GetAge()
        {
            int age = 0;
            Personal personalInfo = (Personal)GetItem(Personal.TypeId);
            HealthServiceDateTime dob = personalInfo.BirthDate;
            if (dob != null)
                age = YearsAfter(dob.ToDateTime());
            return age;
        }

        private int YearsAfter(DateTime before)
        {
            // definitely not accurate...
            return (int)DateTime.Now.Subtract(before).TotalDays / 365;
        }
    }
}