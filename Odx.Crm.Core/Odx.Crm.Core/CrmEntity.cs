using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;

namespace Odx.Xrm.Core
{
    public class CrmEntity : Entity
    {
        private Dictionary<string, string> logicalNamesDictionary;

        public CrmEntity()
        {
            this.LogicalName = this.GetType().GetCustomAttribute<EntityLogicalNameAttribute>().LogicalName;
            this.logicalNamesDictionary = this.GetType().GetProperties().ToDictionary(x => x.Name, x => x.GetCustomAttribute<AttributeLogicalNameAttribute>()?.LogicalName);
        }

        public string GetAttributeName(string propertyName)
        {
            try
            {
                return this.logicalNamesDictionary[propertyName] ?? propertyName.ToLowerInvariant();
            }
            catch
            {
                throw new InvalidPluginExecutionException(propertyName);
            }

        }

        public Guid GetId()
        {
            return this.GetAttributeValue<Guid>(this.LogicalName + "id");
        }

        public T GetAttribute<T>([CallerMemberName]string propertyName = null)
        {
            return this.GetAttributeValue<T>(GetAttributeName(propertyName));
        }

        public string GetFormattedAttribute([CallerMemberName]string propertyName = null)
        {
            return this.GetFormattedAttributeValue(GetAttributeName(propertyName));
        }

        public void SetAttribute(object value, [CallerMemberName]string propertyName = null)
        {
            this.SetAttributeValue(GetAttributeName(propertyName), value);
        }

        public void SetId(Guid? value, [CallerMemberName]string propertyName = null)
        {
            this.SetAttribute(value, propertyName);
            if (value.HasValue)
            {
                base.Id = value.Value;
            }
            else
            {
                base.Id = Guid.Empty;
            }
        }

        public int? GetOptionSetValue([CallerMemberName]string propertyName = null)
        {
            var optionSetValue = this.GetAttribute<OptionSetValue>(propertyName);
            if (optionSetValue == null)
            {
                return null;
            }

            return optionSetValue.Value;
        }

        public LabeledOptionSetValue GetLabeledOptionSetValue([CallerMemberName]string propertyName = null)
        {
            var optionSetValue = this.GetAttribute<OptionSetValue>(propertyName);
            if (optionSetValue == null)
            {
                return null;
            }

            return new LabeledOptionSetValue(optionSetValue.Value)
            {
                Value = optionSetValue.Value,
                Label = this.GetFormattedAttribute(propertyName)
            };
        }

        public void SetLabeledOptionSetValue(LabeledOptionSetValue value, [CallerMemberName]string propertyName = null)
        {

            this.SetOptionSetValue(value?.Value, propertyName);
        }

        public void SetLabeledOptionSetValue<T>(LabeledOptionSetValue<T> value, [CallerMemberName]string propertyName = null)
        {
            if (value == null)
            {
                this.SetAttribute(null, propertyName);
            }
            else
            {
                this.SetOptionSetValue(value.Value, propertyName);
            }
        }

        public void SetOptionSetValue(object value, [CallerMemberName]string propertyName = null)
        {
            if (value == null)
            {
                this.SetAttribute(null, propertyName);
            }
            else
            {
                this.SetAttribute(new OptionSetValue((int)value), propertyName);
            }
        }

        [AttributeLogicalName("createdon")]
        public DateTime? CreatedOn
        {
            get => GetAttribute<DateTime?>();
            set => SetAttribute(value);
        }

        [AttributeLogicalName("overriddencreatedon")]
        public DateTime? OverriddenCreatedOn
        {
            get => GetAttribute<DateTime?>();
            set => SetAttribute(value);
        }

        [AttributeLogicalName("modifiedon")]
        public DateTime? ModifiedOn
        {
            get => GetAttribute<DateTime?>();
            set => SetAttribute(value);
        }
    }
}
