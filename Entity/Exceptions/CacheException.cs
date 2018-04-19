using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity.Exceptions
{
    /// <summary>
    /// CacheException
    /// </summary>
    [Serializable]
    public class CacheException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Games.NBall.Entity.Exceptions.CacheException"/> class.
        /// </summary>
        public CacheException()
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="Games.NBall.Entity.Exceptions.CacheException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public CacheException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Games.NBall.Entity.Exceptions.CacheException"/> class with a specified error message
        /// and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">Ther error message that explains the reason for the exception.</param>
        /// <param name="inner">The exception that is the cause of the current exception,
        /// or a null (Noting in Visual Basic) if no inner exception is specified.</param>
        public CacheException(string message, Exception inner)
            : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Games.NBall.Entity.Exceptions.CacheException"/> class with serialized data.
        /// </summary>
        /// <param name="info">The System.Runtime.Serialization.SerializationInfo that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The System.Runtime.Serialization.StreamingContext that contains contextual information about the source or destination.</param>
        protected CacheException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }
    }
}
