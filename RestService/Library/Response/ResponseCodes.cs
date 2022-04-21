using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyAgencyVault.WcfService.Library.Response
{
    public enum ResponseCodes
    {
        Success=200,
        Failure = 500,
        RecordNotFound=404,
        OutgoingPaymentExist=405,
        EmailNotFound=402,
        PwdKeyExpired=403,
        EmailAlreadyExist= 408,
        UserAlreadyExist= 409,
        UserNameAndEmailExist = 410,
        IssueExistWithEntity = 510,
        NoIssueWithEntity = 201,
        RecordAlreadyExist=411

    }
}