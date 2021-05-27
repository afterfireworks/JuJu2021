using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MainSite.Models
{
    [MetadataType(typeof(MetaChairman))] 
    public partial class Chairman
    {
        public class MetaChairman
        {

            [DisplayName("主委帳號")]
            public string ChairmanAccount { get; set; }

            [DisplayName("住戶帳號")]
            public string Account { get; set; }

            [DisplayName("密碼")]
            public string Password { get; set; }

            [DisplayName("在任狀態")]
            public bool Working { get; set; }
        }
    }

    [MetadataType(typeof(MetaJanitor))]
    public partial class Janitor
    {
        public class MetaJanitor
        {
            [DisplayName("管理員帳號")]
            public string JanitorAccount { get; set; }

            [DisplayName("管理員密碼")]
            public string Password { get; set; }

            [DisplayName("設定人")]
            public string ChairmanAccount { get; set; }

            public virtual Chairman Chairman { get; set; }
        }
    }

    [MetadataType(typeof(MetaResident))]
    public partial class Resident
    {
        public class MetaResident
        {

            [DisplayName("住戶帳號")]
            public string Account { get; set; }

            [DisplayName("身分證號碼")]
            public string ID { get; set; }

            [DisplayName("姓名")]
            public string Name { get; set; }

            [DisplayName("住戶密碼")]
            public string Password { get; set; }

            [DisplayName("連絡電話")]
            public string Tel { get; set; }

            [DisplayName("連絡地址")]
            public string Address { get; set; }

            [DisplayName("住戶相片")]
            public byte[] Photo { get; set; }

            [DisplayName("管委會身分")]
            public bool Committee { get; set; }
        }
    }

    [MetadataType(typeof(MetaCollector))]
    public partial class Collector
    {
        public class MetaCollector
        {

            [DisplayName("身分證號碼")]
            public string ID { get; set; }

            [DisplayName("住戶帳號")]
            public string Account { get; set; }

            public virtual Resident Resident { get; set; }
        }
    }

    [MetadataType(typeof(MetaComplaint))]
    public partial class Complaint
    {
        public class MetaComplaint {

        [DisplayName("案件編號")]
        public long SN { get; set; }

        [DisplayName("住戶帳號")]
        public string Account { get; set; }

        [DisplayName("日期")]
        public System.DateTime Date { get; set; }

        [DisplayName("標題")]
        public string Title { get; set; }

        [DisplayName("內容")]
        public string Description { get; set; }
        
        public virtual Resident Resident { get; set; }

        }
    }

    [MetadataType(typeof(MetaPackage))]
    public partial class Package
    {
        public class MetaPackage
        {

        [DisplayName("包裹編號")]
        public long SN { get; set; }

        [DisplayName("收件人帳號")]
        public string Account { get; set; }

        [DisplayName("到貨日")]
        //[DataType(DataType.Date)]
        public System.DateTime ArrivalDate { get; set; }

        [DisplayName("領收狀態")]
        public bool Sign { get; set; }

        [DisplayName("領取人")]
        public string Signer { get; set; }

        [DisplayName("取貨日")]
        //[DataType(DataType.Date)]
        public Nullable<System.DateTime> SignDate { get; set; }

        [DisplayName("貨到付款")]
        public Nullable<bool> COD { get; set; }

        public virtual Resident Resident { get; set; }
        }
    }


    [MetadataType(typeof(MetaReturnOfGoods))]
    public partial class ReturnOfGoods
    {
        public class MetaReturnOfGoods
        {

            [DisplayName("退貨編號")]
            public long SN { get; set; }

            [DisplayName("退貨人帳號")]
            public string Account { get; set; }
            //[DataType(DataType.Date)] 
            [DisplayName("物流收貨日")]
            public System.DateTime ReceiptDate { get; set; }

            [DisplayName("物流公司")]
            public string LogisticsCompany { get; set; }

            [DisplayName("領收狀態[物流]")]
            public bool Sign { get; set; }

            [DisplayName("物流簽名")]
            public byte[] CourierSign { get; set; }

            public virtual Resident Resident { get; set; }
        }
    }

    [MetadataType(typeof(MetaAnnouncement))]
    public partial class Announcement
    {
        public class MetaAnnouncement
        {
            [DisplayName("公告編號")]
            public long SN { get; set; }

            [DisplayName("公告日期")]
            public System.DateTime Date { get; set; }

            [DisplayName("主委帳號")]
            public string ChairmanAccount { get; set; }

            [DisplayName("公告分類")]
            public string Category { get; set; }

            [DisplayName("公告標題")]
            public string Title { get; set; }

            [DisplayName("內容描述")]
            public string Description { get; set; }

            [DisplayName("附加檔案")]

            public byte[] Picture { get; set; }

            public virtual Chairman Chairman { get; set; }
        }
    }

    [MetadataType(typeof(MetaMeeting))]
    public partial class Meeting
    {
        public class MetaMeeting
        {
            [Key]
            [DisplayName("會議編號")]
            public long SN { get; set; }

            [DisplayName("會議日期")]
            //[DataType(DataType.Date)]
            public System.DateTime Date { get; set; }

            [DisplayName("會議主題")]
            public string Title { get; set; }

            [DisplayName("詳細描述")]

            public string Description { get; set; }

            [DisplayName("影片連結")]

            public string URL { get; set; }
        }
    }

    [MetadataType(typeof(MetaMeetingDetails))]
    public partial class MeetingDetails
    {
        public class MetaMeetingDetails
        {
            [Key]
            [DisplayName("會議編號")]
            public long SN { get; set; }

            [DisplayName("參與人帳號")]
            public string Account { get; set; }

            [DisplayName("影片連結")]
            public string URL { get; set; }

            public virtual Meeting Meeting { get; set; }
            public virtual Resident Resident { get; set; }
        }
    }

    [MetadataType(typeof(MetaMeetingData))]
    public partial class MetaMeetingData
    {
        public class MeetingData
        {
               public Meeting meeting { get; set; }
            
               public ICollection<MeetingDetails> meetingDetails { get; set; }
        }
     }

}
