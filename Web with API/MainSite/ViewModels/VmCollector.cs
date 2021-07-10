using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using MainSite.Models;

namespace MainSite.ViewModels
{
    public class VmCollector
    {
        [DisplayName("本人帳號")]
        public string Account { get; set; }

        [DisplayName("代收人身分證")]
        public string ID { get; set; }

        [DisplayName("本人")]
        public string AccountName { get; set; }

        [DisplayName("代收人")]
        public string IDName { get; set; }
    }
}