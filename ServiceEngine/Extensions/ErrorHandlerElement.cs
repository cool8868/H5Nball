using System;
using System.ServiceModel.Configuration;

namespace Games.NBall.ServiceEngine.Extensions
{
    public class ErrorHandlerElement : BehaviorExtensionElement
    {
        public override Type BehaviorType
        {
            get { return typeof(ErrorHandlerBehavior); }
        }

        protected override object CreateBehavior()
        {
            return new ErrorHandlerBehavior(typeof(ErrorHandler));
        }
    }
}

