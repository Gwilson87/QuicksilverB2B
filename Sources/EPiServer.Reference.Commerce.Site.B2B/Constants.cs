﻿using System.Collections.Generic;

namespace EPiServer.Reference.Commerce.Site.B2B
{
    public static class Constants
    {
        public static class Classes
        {
            public static string Budget = "Budget";
            public static string BudgetFriendly = "Budget";
            public static string Organization = "Organization";
            public static string Contact = "Contact";
        }
        public static class Fields
        {
            public static string StartDate = "StartDate";
            public static string StartDateFriendly = "Start Date";
            public static string DueDate = "DueDate";
            public static string DueDateFriendly = "Due Date";
            public static string Amount = "Amount";
            public static string UserRole = "UserRole";
            public static string UserRoleFriendly = "User Role";
            public static string UserLocation = "UserLocation";
            public static string UserLocationFriendly = "User Location";
        }

        public static class Forms
        {
            public const string EditForm = "[MC_BaseForm]";
            public const string ShortInfoForm = "[MC_ShortViewForm]";
            public const string ViewForm = "[MC_GeneralViewForm]";
        }

        public static class Attributes
        {
            public static string DisplayBlock = "Ref_DisplayBlock";
            public static string DisplayText = "Ref_DisplayText";
            public static string DisplayOrder = "Ref_DisplayOrder";
        }

        public static class Quote
        {
            public static string QuoteExpireDate = "QuoteExpireDate";
            public static string ParentOrderGroupId = "ParentOrderGroupId";
            public static string QuoteStatus = "QuoteStatus";
            public static string RequestQuotation = "RequestQuotation";
            public static string RequestQuotationFinished = "RequestQuotationFinished";
            public static string PreQuoteTotal = "PreQuoteTotal";
            public static string PreQuotePrice = "PreQuotePrice";
            public static string QuoteExpired = "QuoteExpired";            
        }

        public static class B2BNavigationFor
        {
            public static List<string> Admin = new List<string> { "Overview", "Users", "Orders", "Order Pad", "Budgeting" };
            public static List<string> Approver = new List<string> { "Overview", "Orders", "Order Pad", "Budgeting" };
            //public static List<string> Purchaser = new List<string> { "" };
        }

        public static string SectionName = "InfoBlock";
        public static string DefaultDisplayOrder = "10000";
    }
}
