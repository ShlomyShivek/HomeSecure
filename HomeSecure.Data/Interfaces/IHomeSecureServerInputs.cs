using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using HomeSecure.Data.Entities;

namespace HomeSecure.Data.Interfaces
{
    [ServiceContract]
    public interface IHomeSecureServerInputs
    {
        [OperationContract(Name = "AddMotionSecurityEvent")]
        void AddMotionSecurityEvent(MotionDetectionEvent securityEvent);

        [OperationContract(Name = "GetMotionSecurityEvents")]
        List<MotionDetectionEvent> GetMotionSecurityEvents(int pageSize, int pageNumber, out int total);

    }
}
