using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AviMa.DataObjectLayer
{
  public  class AppConfigDO
    {
        public int SiNo { get; set; }
        public string ConfigKey { get; set; }
        public string ConfigValue { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public string ModiDate { get; set; }
        public string ModiBy { get; set; }
    }

    public class ListAppConfigDO
    {
        List<AppConfigDO> ListAppConfig { get; set; }
    }
}
