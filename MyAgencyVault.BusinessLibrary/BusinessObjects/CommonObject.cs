using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

/// <summary>
/// CreatedBy:Ankit
/// CreatedOn:04,Sept,2018
/// Purpose:This structure is used for pagination ,sorting and searching
/// </summary>
namespace MyAgencyVault.BusinessLibrary
{
    [DataContract]
    public struct ListParams
    {
        int _pageIndex;
        int _pageSize;
        string _filterby;
        string _sortType;
        string _sortBy;
        int _statusId;

        [DataMember]
        public int pageSize { get { return _pageSize; } set { _pageSize = value; } }
        [DataMember]
        public int pageIndex { get { return _pageIndex; } set { _pageIndex = value; } }
        [DataMember]
        public string filterBy { get { return _filterby; } set { _filterby = value; } }
        [DataMember]
        public string sortType { get { return _sortType; } set { _sortType = value; } }
        [DataMember]
        public string sortBy { get { return _sortBy; } set { _sortBy = value; } }
        [DataMember]
        public int statusId { get { return _statusId; } set { _statusId = value; } }

    }

    [DataContract]
    public struct ListParams_Issues
    {
        int _pageIndex;
        int _pageSize;
        string _filterby;
        string _sortType;
        string _sortBy;
        string _statusId;

        [DataMember]
        public int pageSize { get { return _pageSize; } set { _pageSize = value; } }
        [DataMember]
        public int pageIndex { get { return _pageIndex; } set { _pageIndex = value; } }
        [DataMember]
        public string filterBy { get { return _filterby; } set { _filterby = value; } }
        [DataMember]
        public string sortType { get { return _sortType; } set { _sortType = value; } }
        [DataMember]
        public string sortBy { get { return _sortBy; } set { _sortBy = value; } }
        [DataMember]
        public string statusId { get { return _statusId; } set { _statusId = value; } }

    }


    public struct PoliciesCount
    {
        [DataMember]
        public  string TotalPoliciesCount {get;set;}
        [DataMember]
        public string ActivePoliciesCount { get; set; }
        [DataMember]
        public string PendingPoliciesCount { get; set; }
        [DataMember]
        public string TerminatePoliciesCount { get; set; }
        [DataMember]
        public string SelectedCount { get; set; }
    }
}
