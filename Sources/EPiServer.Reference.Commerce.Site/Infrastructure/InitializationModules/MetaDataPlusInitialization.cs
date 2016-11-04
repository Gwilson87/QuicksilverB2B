﻿using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using Mediachase.Commerce.Catalog;
using Mediachase.MetaDataPlus;
using Mediachase.MetaDataPlus.Configurator;

namespace EPiServer.Reference.Commerce.Site.Infrastructure.InitializationModules
{
    [InitializableModule]
    [ModuleDependency(typeof(EPiServer.Commerce.Initialization.InitializationModule))]
    public class MetaDataPlusInitialization : IInitializableModule
    {
        public void Initialize(InitializationEngine context)
        {
            MetaDataContext mdContext = CatalogContext.MetaDataContext;

            //AddMetaFieldToClass(mdContext, metaDataNamespace, metaClassName, metaFieldName, MetaDataType.LongString, 255,
            //    true, false);
        }

        private void AddMetaFieldToClass(MetaDataContext mdContext, string metaDataNamespace, string metaClassName,
            string metaFieldName, MetaDataType type, int length, bool allowNulls, bool cultureSpecific)
        {
            var metaField = CreateMetaField(mdContext, metaDataNamespace, metaFieldName, type, length, allowNulls,
                cultureSpecific);
            JoinField(mdContext, metaField, metaClassName);
        }

        private MetaField CreateMetaField(MetaDataContext mdContext, string metaDataNamespace, string metaFieldName, MetaDataType type, int length, bool allowNulls, bool cultureSpecific)
        {
            var metaField = MetaField.Load(mdContext, metaFieldName) ??
                    MetaField.Create(mdContext, metaDataNamespace, metaFieldName, metaFieldName, string.Empty, type, length, allowNulls, cultureSpecific, false, false);

            if (type != MetaDataType.Decimal) return metaField;

            metaField.Attributes[MetaFieldAttributeConstants.MdpPrecisionAttributeName] = "18";
            metaField.Attributes[MetaFieldAttributeConstants.MdpScaleAttributeName] = "2";
            return metaField;
        }

        private void JoinField(MetaDataContext mdContext, MetaField field, string metaClassName)
        {
            var cls = MetaClass.Load(mdContext, metaClassName);
            if (!MetaFieldIsNotConnected(field, cls)) return;

            cls.AddField(field);
        }

        private static bool MetaFieldIsNotConnected(MetaField field, MetaClass cls)
        {
            return cls != null && !cls.MetaFields.Contains(field);
        }

        public void Uninitialize(InitializationEngine context)
        {
            //Add uninitialization logic
        }
    }
}