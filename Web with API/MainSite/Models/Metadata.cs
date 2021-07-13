using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace MainSite.Models
{
    [MetadataType(typeof(MetaChairman))] 
    public partial class Chairman
    {
        public class MetaChairman
        {

            [DisplayName("主委帳號")]
            [Required(ErrorMessage = "未輸入帳號")]
            public string ChairmanAccount { get; set; }

            [DisplayName("關聯住戶帳號")]
            [Required(ErrorMessage = "未輸入密碼")]
            public string Account { get; set; }

            [DisplayName("登入密碼")]
            [Required]
            public string Password { get; set; }

            [DisplayName("在任狀態")]
            [Required]
            public bool Working { get; set; }
        }
    }

    [MetadataType(typeof(MetaJanitor))]
    public partial class Janitor
    {
        public class MetaJanitor
        {
            [DisplayName("管理員帳號")]
            [Required(ErrorMessage = "未輸入帳號")]
            public string JanitorAccount { get; set; }

            [DisplayName("登入密碼")]
            [Required(ErrorMessage = "未輸入密碼")]
            public string Password { get; set; }

            [DisplayName("設定人")]
            public string ChairmanAccount { get; set; }

        }
    }

    [MetadataType(typeof(MetaResident))]
    public partial class Resident
    {
        public class MetaResident
        {

            [DisplayName("住戶帳號")]
            [Required]
            [RegularExpression(@"[A-Za-z0-9]{4,10}$", ErrorMessage = "請輸入4-10碼(含英文或數字)")]
            public string Account { get; set; }

            [DisplayName("住戶密碼")]
            [Required]
            [RegularExpression(@"[A-Za-z0-9]{6,10}$", ErrorMessage = "請輸入6-10碼(含英文或數字)")]
            public string Password { get; set; }

            [DisplayName("身分證號碼")]
            [Required]
            [CheakIDNumber]
            [RegularExpression("[A-Z][12][0-9]{8}", ErrorMessage = "格式有誤")]
            public string ID { get; set; }

            [DisplayName("姓名")]
            [Required]
            //目前一堆外國名字 ...
            [RegularExpression("^[\u4e00-\u9fa5]{2,5}$|^[A-Za-z]{2,10}$", ErrorMessage = "請輸入2-5個中文字或2-10個英文字")]
            public string Name { get; set; }

            [DisplayName("連絡手機")]
            [Required]
            [RegularExpression(@"[0][9][0-9]{8}$")]
            public string Tel { get; set; }

            [DisplayName("連絡地址")]
            [Required]
            public string Address { get; set; }

            [DisplayName("住戶相片")]
            //這只能在純圖檔起作用
            //[FileExtensions(Extensions = "jpg|jpeg|png", ErrorMessage = "Please select an Excel file.")]
            public byte[] Photo { get; set; }

            [DisplayName("管委會身分")]
            public bool Committee { get; set; }

            public class CheakIDNumber : ValidationAttribute
            {
                public CheakIDNumber()
                {
                    ErrorMessage = "此身份證字號不正確";
                }
                public override bool IsValid(object value)
                {
                    string id = value.ToString();

                    const string eng = "ABCDEFGHJKLMNPQRSTUVXYWZIO";
                    string w = id.Substring(0, 1);

                    int inteng = eng.IndexOf(w) + 10;

                    int n1 = inteng / 10;
                    int n2 = inteng % 10;

                    int[] idarray = new int[9];

                    for (int i = 0; i < idarray.Length; i++)
                    {
                        idarray[i] = Convert.ToInt32(id.Substring(i + 1, 1));
                    }

                    int sum = n1 + n2 * 9;

                    for (int i = 0; i < 8; i++)
                    {
                        sum += idarray[i] * (8 - i);
                    }

                    sum += idarray[8];

                    bool result = sum % 10 == 0 ? true : false;

                    return result;
                }
            }
        }
    }

    [MetadataType(typeof(MetaCollector))]
    public partial class Collector
    {
        public class MetaCollector
        {
            public long SN { get; set; }

            [DisplayName("本人")]
            [Required]
            public string Account { get; set; }

            [DisplayName("代收人")]
            [Required]
            public string ID { get; set; }

        }
    }

    [MetadataType(typeof(MetaComplaint))]
    public partial class Complaint
    {
        public class MetaComplaint {

        [DisplayName("案件編號")]
        public long SN { get; set; }

        [DisplayName("住戶帳號")]
        [Required]
        public string Account { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        [DisplayName("日期")]
        public System.DateTime Date { get; set; }

        [DisplayName("標題")]
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        [DisplayName("內容")]
        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        [DisplayName("處理狀態")]
        public bool Solved { get; set; }

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
        [Required]
        public string Account { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        [DisplayName("包裹到達日")]
        public System.DateTime ArrivalDate { get; set; }

        [DisplayName("領收狀態")]
        public bool Sign { get; set; }

        [DisplayName("領收人")]
        public string Signer { get; set; }

        [DisplayName("領收日")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> SignDate { get; set; }

        [DisplayName("貨到付款")]
        public Nullable<bool> COD { get; set; }

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

            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
            [DisplayName("物流收貨日")]
            public Nullable<System.DateTime> ReceiptDate { get; set; }

            [DisplayName("物流公司")]
            [Required]
            public string LogisticsCompany { get; set; }

            [DisplayName("物流領收")]
            public bool Sign { get; set; }

            [DisplayName("物流簽名")]
            public byte[] CourierSign { get; set; }

        }
    }

    [MetadataType(typeof(MetaAnnouncement))]
    public partial class Announcement
    {
        public class MetaAnnouncement
        {
            [DisplayName("公告編號")]
            public long SN { get; set; }

            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
            [DisplayName("公告日期")]
            public System.DateTime Date { get; set; }

            [DisplayName("主委帳號")]
            public string ChairmanAccount { get; set; }

            [DisplayName("公告分類")]
            [Required]
            public string Category { get; set; }

            [DisplayName("公告標題")]
            [Required]
            [StringLength(20)]
            public string Title { get; set; }

            [DisplayName("內容描述")]
            [Required]
            [StringLength(300)]
            public string Description { get; set; }

            [DisplayName("附加檔案")]

            public byte[] Picture { get; set; }

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

            [DisplayName("會議主持人")]
            [Required]
            public string ChairmanAccount { get; set; }

            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
            [DisplayName("會議日期")]
            public System.DateTime Date { get; set; }

            [DisplayName("會議主題")]
            public string Title { get; set; }


            [DisplayName("影片連結")]

            public string URL { get; set; }
        }
    }

    //檢查身分證字號
    //public string Check(string id)
    //{
    //    // 使用「正規表達式」檢驗格式 [A~Z] {1}個數字 [0~9] {9}個數字
    //    var regex = new Regex("^[A-Z]{1}[0-9]{9}$");
    //    if (!regex.IsMatch(id))
    //    {
    //        //Regular Expression 驗證失敗，回傳 ID 錯誤
    //        return "身分證基本格式錯誤";
    //    }

    //    //除了檢查碼外每個數字的存放空間 
    //    int[] seed = new int[10];

    //    //建立字母陣列(A~Z)
    //    //A=10 B=11 C=12 D=13 E=14 F=15 G=16 H=17 J=18 K=19 L=20 M=21 N=22
    //    //P=23 Q=24 R=25 S=26 T=27 U=28 V=29 X=30 Y=31 W=32  Z=33 I=34 O=35            
    //    string[] charMapping = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "J", "K", "L", "M", "N", "P", "Q", "R", "S", "T", "U", "V", "X", "Y", "W", "Z", "I", "O" };
    //    string target = id.Substring(0, 1); //取第一個英文數字
    //    for (int index = 0; index < charMapping.Length; index++)
    //    {
    //        if (charMapping[index] == target)
    //        {
    //            index += 10;
    //            //10進制的高位元放入存放空間   (權重*1)
    //            seed[0] = index / 10;

    //            //10進制的低位元*9後放入存放空間 (權重*9)
    //            seed[1] = (index % 10) * 9;

    //            break;
    //        }
    //    }
    //    for (int index = 2; index < 10; index++) //(權重*8~1)
    //    {   //將剩餘數字乘上權數後放入存放空間                
    //        seed[index] = Convert.ToInt32(id.Substring(index - 1, 1)) * (10 - index);
    //    }
    //    //檢查是否符合檢查規則，10減存放空間所有數字和除以10的餘數的個位數字是否等於檢查碼            
    //    //(10 - ((seed[0] + .... + seed[9]) % 10)) % 10 == 身分證字號的最後一碼   
    //    if ((10 - (seed.Sum() % 10)) % 10 != Convert.ToInt32(id.Substring(9, 1)))
    //    {
    //        return "請輸入正確身分證";
    //    }

    //    return "身分證號碼正確";
    //}
}
