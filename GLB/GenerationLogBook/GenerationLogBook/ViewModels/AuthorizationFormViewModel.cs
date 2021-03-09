using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GenerationLogBook.Utility;

namespace GenerationLogBook.ViewModels
{
    public class AuthorizationFormViewModel
    {
        public UserAuthorization authorization { get; set; }
        public IEnumerable<AuthStatus> LOVAuthorizationStatus { get; set; }
        public IEnumerable<ModuleAuthMaster> LOVModuleAuthMaster { get; set; }
        public IEnumerable<SitesMaster> LOVSitesMaster { get; set; }
        public List<KeyValue> LOVAuthorizations { get; set; }
        public string SiteId { get; set; }
        public int? UserSequenceNo { get; set; }
    }
}