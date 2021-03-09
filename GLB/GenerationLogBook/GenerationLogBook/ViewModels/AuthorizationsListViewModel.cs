using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GenerationLogBook.ViewModels
{
    public class AuthorizationsListViewModel
    {
        public IEnumerable<UserAuthorization> authorizations;
        public int? UserSequenceNo { get; set; }
    }
}