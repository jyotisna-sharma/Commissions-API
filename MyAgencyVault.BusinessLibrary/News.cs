using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyAgencyVault.BusinessLibrary.Base;
using System.Runtime.Serialization;
using DLinq = DataAccessLayer.LinqtoEntity;

namespace MyAgencyVault.BusinessLibrary
{
    [DataContract]
    public class News : IEditable<News>
    {
        #region IEditable<News> Members

        public void AddUpdate()
        {
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {


                DLinq.News NewsDetail = (from e in DataModel.News
                                          where e.NewsId == this.NewsID
                                             select e).FirstOrDefault();

                if (NewsDetail == null)
                {
                    NewsDetail = new DLinq.News
                    {
                        NewsId = this.NewsID,
                        NewsTtitle = this.NewsTitle,
                        NewsContent = this.NewsContent,
                        CreatedOn = this.CreatedOn,
                        LastModifiedOn = this.LastModifiedOn 
                    };
                    DataModel.AddToNews(NewsDetail);
                }
                else
                {

                    NewsDetail.NewsTtitle = this.NewsTitle;
                    NewsDetail.NewsContent = this.NewsContent;
                    NewsDetail.CreatedOn = this.CreatedOn;
                    NewsDetail.LastModifiedOn = DateTime.Now.Date;
                   
                }

                DataModel.SaveChanges();
            }
        }

        public News GetOfID()
        {
            throw new NotImplementedException();
        }

        public void Delete()
        {
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                DLinq.News _news = (from n in DataModel.News
                                           where (n.NewsId == this.NewsID)
                                           select n).FirstOrDefault();
                _news.IsDeleted = true;
                DataModel.SaveChanges();
            }
        }

     
    
        public bool IsValid()
        {
            throw new NotImplementedException();
        }

        #endregion
        #region "Data members aka - public properties."
        [DataMember]
        public Guid NewsID { get; set; }
        [DataMember]
        public string NewsTitle { get; set; }
        [DataMember]
        public string NewsContent { get; set; }
        //[DataMember]
        //public DateTime CreatedOn { get; set; }
        DateTime _createdOn;
        [DataMember]
        public DateTime CreatedOn
        {
            get
            {
                return _createdOn;
            }
            set
            {
                _createdOn = value;
                if (value != null && string.IsNullOrEmpty(CreatedOnstring))
                {
                    CreatedOnstring = value.ToString();
                }
            }
        }
        string _createdOnString;
        [DataMember]
        public string CreatedOnstring
        {
            get
            {
                return _createdOnString;
            }
            set
            {
                _createdOnString = value;
                if (( CreatedOn == null || CreatedOn == DateTime.MinValue) && !string.IsNullOrEmpty(_createdOnString))
                {
                    DateTime dt;
                    DateTime.TryParse(_createdOnString, out dt);
                    CreatedOn = dt;
                }
            }
        }

        //[DataMember]
        //public DateTime LastModifiedOn { get; set; }
        DateTime _LastModifiedOn;
        [DataMember]
        public DateTime LastModifiedOn
        {
            get
            {
                return _LastModifiedOn;
            }
            set
            {
                _LastModifiedOn = value;
                if (value != null && string.IsNullOrEmpty(LastModifiedOnstring))
                {
                    LastModifiedOnstring = value.ToString();
                }
            }
        }
        string _LastModifiedOnString;
        [DataMember]
        public string LastModifiedOnstring
        {
            get
            {
                return _LastModifiedOnString;
            }
            set
            {
                _LastModifiedOnString = value;
                if ((LastModifiedOn == null || LastModifiedOn == DateTime.MinValue )&& !string.IsNullOrEmpty(_LastModifiedOnString))
                {
                    DateTime dt;
                    DateTime.TryParse(_LastModifiedOnString, out dt);
                    LastModifiedOn = dt;
                }
            }
        }
        [DataMember]
        public bool IsDeleted { get; set; }
        #endregion 

        public static List<News> GetNewsList()
        {
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                return (from hd in DataModel.News
                        where (hd.IsDeleted != true)
                        select new News
                        {
                            NewsID = hd.NewsId,
                            NewsTitle = hd.NewsTtitle,
                            NewsContent = hd.NewsContent,
                            LastModifiedOn = (DateTime)hd.LastModifiedOn,
                            CreatedOn = (DateTime)hd.CreatedOn,
                        }).ToList();
            }
        }
        
    }
}
