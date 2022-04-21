using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyAgencyVault.WcfService.Library.Response
{
    public enum ResponseCodes
    {
        Success=200,
        Failure = 201,
        RecordNotFound=404
    }
}