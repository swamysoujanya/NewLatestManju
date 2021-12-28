using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AviMa.DataObjectLayer
{
    class UserACLDO
    {
        /// <summary>
        /// Enabled features for the user
        /// </summary>
        public List<int> EnabledFeat { get; set; }
        public int UserID { get; set; }
    }
}
