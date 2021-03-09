//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GenerationLogBook
{
    using System;
    using System.Collections.Generic;
    
    public partial class ModuleAuthMaster
    {
        public ModuleAuthMaster()
        {
            this.UserAuthorizations = new HashSet<UserAuthorization>();
        }
    
        public string ModuleId { get; set; }
        public string AuthId { get; set; }
        public string SiteId { get; set; }
        public string AuthText { get; set; }
        public string AuthTextMenu { get; set; }
        public string ControllerAction { get; set; }
        public string StatusId { get; set; }
    
        public virtual Module Module { get; set; }
        public virtual SitesMaster SitesMaster { get; set; }
        public virtual ICollection<UserAuthorization> UserAuthorizations { get; set; }
    }
}