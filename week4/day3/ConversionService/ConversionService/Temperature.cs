using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ConversionService
{
    [DataContract] // datacontract attribute marks a class as a data type that can be sent or recieved with SOAP.
    public class Temperature
    {
        [DataMember] // datamember attribute marks datacontract members as visible over the service
        public double Value { get; set; }

        // without the proper attribute, methods, classes, properties, etc are invisible to
        // the one accessing this service.
    }
}