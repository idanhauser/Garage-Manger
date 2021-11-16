using System;
using System.Reflection;

namespace Ex03.GarageLogic
{
    public class RequiredInformation
    {
        private readonly string r_Information;
        private readonly Type r_Type;

        public RequiredInformation(string i_Information, Type i_Type)
        {
            r_Information = i_Information;
            r_Type = i_Type;
        }

        public Type Type
        {
            get { return r_Type; }
        }

        public string Information
        {
            get { return r_Information; }
        }
    }
}