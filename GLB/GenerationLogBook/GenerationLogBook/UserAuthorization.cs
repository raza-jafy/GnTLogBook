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
    
    public partial class UserAuthorization
    {
        public string UserId { get; set; }
        public string AuthId { get; set; }
        public string AuthStatusId { get; set; }
        public string Comments { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime ChangedOn { get; set; }
        public string ChangedBy { get; set; }
    
        public virtual AuthStatus AuthStatus { get; set; }
        public virtual ModuleAuthMaster ModuleAuthMaster { get; set; }
        public virtual User User { get; set; }
    }
}
