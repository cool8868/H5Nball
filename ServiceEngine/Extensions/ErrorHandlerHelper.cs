﻿using System;
using System.ServiceModel;
using System.Diagnostics;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.ServiceModel.Channels;
using Games.NBall.Common;

namespace Games.NBall.ServiceEngine.Extensions
{
    public static class ErrorHandlerHelper
    {
        private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(ErrorHandlerHelper));

        public static void PromoteException(Type serviceType, Exception error, MessageVersion version, ref Message fault)
        {
            //Is error in the form of FaultException<T> ? 
            if (error.GetType().IsGenericType && error is FaultException)
            {
                Debug.Assert(error.GetType().GetGenericTypeDefinition() == typeof(FaultException<>));
                return;
            }

            bool inContract = ExceptionInContract(serviceType, error);
            if (inContract == false)
            {
                return;
            }
            try
            {
                Type faultUnboundedType = typeof(FaultException<>);
                Type faultBoundedType = faultUnboundedType.MakeGenericType(error.GetType());

                ConstructorInfo info;

                Type[] parameter1 = { typeof(string) };
                info = error.GetType().GetConstructor(parameter1);
                Debug.Assert(info != null, "Exception type " + error.GetType() + " does not have suitable constructor");

                Exception newException = (Exception)Activator.CreateInstance(error.GetType(), error.Message);


                Type[] parameter2 = { newException.GetType() };
                info = faultBoundedType.GetConstructor(parameter2);
                Debug.Assert(info != null, "Exception type " + faultBoundedType + " does not have suitable constructor");

                FaultException faultException = (FaultException)Activator.CreateInstance(faultBoundedType, newException);

                MessageFault messageFault = faultException.CreateMessageFault();
                fault = Message.CreateMessage(version, messageFault, faultException.Action);
            }
            catch (Exception ex)
            {
                LogHelper.LogError(logger, "PromoteException", "PromoteException异常", ex);
            }
        }
        //Can only be called inside a service
        public static void PromoteException(Exception error, MessageVersion version, ref Message fault)
        {
            StackFrame frame = new StackFrame(1);

            Type serviceType = frame.GetMethod().ReflectedType;
            PromoteException(serviceType, error, version, ref fault);
        }

        //public static void LogError(Exception error)
        //{
        //    LogError(error, null);
        //}

        public static bool ExceptionInContract(Type serviceType, Exception error)
        {
            List<FaultContractAttribute> faultAttributes = new List<FaultContractAttribute>();
            Type[] interfaces = serviceType.GetInterfaces();

            string serviceMethod = GetServiceMethod(error);
            FaultContractAttribute[] attributes;

            foreach (Type interfaceType in interfaces)
            {
                MethodInfo[] methods = interfaceType.GetMethods();
                foreach (MethodInfo methodInfo in methods)
                {
                    if (methodInfo.Name == serviceMethod)//Does not deal with overlaoded methods 
                    //or same method name on different contracts implemented explicitly 
                    {
                        attributes = GetFaults(methodInfo);
                        faultAttributes.AddRange(attributes);
                        return FindError(faultAttributes, error);
                    }
                }
            }
            return false;
        }

        public static string GetServiceMethod(Exception error)
        {
            const string WCFPrefix = "SyncInvoke";
            if (error.StackTrace != null)
            {
                int start = error.StackTrace.IndexOf(WCFPrefix);
                //Debug.Assert(start != -1);//Did they change the prefix???

                string trimedTillMethod = error.StackTrace.Substring(start + WCFPrefix.Length);
                string[] parts = trimedTillMethod.Split('(');
                return parts[0];
            }
            return null;
        }

        public static FaultContractAttribute[] GetFaults(MethodInfo methodInfo)
        {
            object[] attributes = methodInfo.GetCustomAttributes(typeof(FaultContractAttribute), false);
            return attributes as FaultContractAttribute[];
        }

        public static bool FindError(List<FaultContractAttribute> faultAttributes, Exception error)
        {
            Predicate<FaultContractAttribute> sameFault = (fault) =>
            {
                Type detailType = fault.DetailType;
                return detailType == error.GetType();
            };
            return faultAttributes.Exists(sameFault);
        }

        public static string GetFileName(Exception error)
        {
            if (error.StackTrace == null)
            {
                return "Unavailable";
            }
            int originalLineIndex = error.StackTrace.IndexOf(":line");
            if (originalLineIndex == -1)
            {
                return "Unavailable";
            }
            string originalLine = error.StackTrace.Substring(0, originalLineIndex);
            string[] sections = originalLine.Split('\\');
            return sections[sections.Length - 1];
        }

        public static int GetLineNumber(Exception error)
        {
            if (error.StackTrace == null)
            {
                return 0;
            }
            string[] sections = error.StackTrace.Split(' ');
            int index = 0;
            foreach (string section in sections)
            {
                if (section.EndsWith(":line"))
                {
                    break;
                }
                index++;
            }
            Debug.Assert(index != 0);
            if (index == sections.Length)
            {
                return 0;
            }
            string lineNumber = sections[index + 1];
            int number = -1;
            try//Strip the /r/n if present
            {
                number = Convert.ToInt32(lineNumber.Substring(0, lineNumber.Length - 2));
            }
            catch (FormatException)
            {
                number = Convert.ToInt32(lineNumber);
            }

            return number;
        }
    }
}