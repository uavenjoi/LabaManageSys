using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LabaManageSys.WebUI.Models
{
    public class FilterLists
    {
        public IEnumerable<string> Authors { get; set; }

        public IEnumerable<string> Topics { get; set; }

        public IEnumerable<string> Levels { get; set; }
    }
}