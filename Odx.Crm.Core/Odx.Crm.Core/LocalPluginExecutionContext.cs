using System;
using Microsoft.Xrm.Sdk;

namespace Odx.Xrm.Core
{
    internal class LocalPluginExecutionContext : ILocalPluginExecutionContext
    {
        private bool isPostImageLoaded;
        private bool isPreImageLoaded;
        private bool isTargetEntityLoaded;
        private bool isTargetLoaded;
        private Entity postImage;
        private Entity preImage;
        private Entity targetEntity;
        private object target;

        public LocalPluginExecutionContext(IPluginExecutionContext context)
        {
            this.Context = context;
        }

        public IPluginExecutionContext Context { get; private set; }
        public virtual Entity PostImage
        {
            get
            {
                return GetEntityImage(nameof(this.PostImage), Context.PostEntityImages, ref this.postImage, ref this.isPostImageLoaded);
            }
        }

        public virtual Entity PreImage
        {
            get
            {
                return GetEntityImage(nameof(this.PreImage), Context.PreEntityImages, ref this.preImage, ref this.isPreImageLoaded);
            }
        }

        public virtual object Target
        {
            get
            {
                if (!this.isTargetLoaded)
                {
                    this.isTargetLoaded = true;
                    this.target = Context.InputParameters[nameof(Target)];
                }

                return this.target;
            }
        }

        public virtual Entity TargetEntity
        {
            get
            {
                if (!this.isTargetEntityLoaded)
                {
                    this.isTargetEntityLoaded = true;
                    this.targetEntity = this.Target as Entity;
                }

                return this.targetEntity;
            }
        }

        public bool CheckSharedContextForFlag(string flagname, IPluginExecutionContext context = null)
        {
            if (context == null)
            {
                context = this.Context;
            }

            if (context.SharedVariables.ContainsKey(flagname))
            {
                return true;
            }
            else
            {
                if (context.ParentContext == null)
                {
                    return false;
                }

                return CheckSharedContextForFlag(flagname, context.ParentContext);
            }
        }

        public V GetInputParameter<V>(string key)
        {
            if (this.Context.InputParameters.ContainsKey(key))
            {
                return (V)this.Context.InputParameters[key];
            }

            return default(V);
        }

        public TMessage GetRequestMessage<TMessage>()
                                                    where TMessage : OrganizationRequest, new()
        {
            var request = new TMessage()
            {
                Parameters = this.Context.InputParameters
            };

            return request;
        }

        public TMessage GetResponseMessage<TMessage>()
            where TMessage : OrganizationRequest, new()
        {
            var request = new TMessage()
            {
                Parameters = this.Context.OutputParameters
            };

            return request;
        }
        public void SetInputParameter<V>(string key, V value)
        {
            this.Context.InputParameters[key] = value;
        }

        public void SetOutputParameter<V>(string key, V value)
        {
            if (this.Context.OutputParameters.ContainsKey(key))
            {
                this.Context.OutputParameters[key] = value;
            }
            else
            {
                this.Context.OutputParameters.Add(key, value);
            }
        }

        public ILocalPluginExecutionContext<T> ToEntity<T>() where T : Entity, new()
        {
            return new LocalPluginExecutionContext<T>(this.Context);
        }

        protected Entity GetEntityImage(string imageName, EntityImageCollection imagesCollection, ref Entity imageField, ref bool isImageFieldLoaded)
        {
            if (!isImageFieldLoaded)
            {

                isImageFieldLoaded = true;
                if (imagesCollection.Contains(imageName))
                {
                    imageField = imagesCollection[imageName];
                }
            }

            return imageField;
        }
    }

    internal class LocalPluginExecutionContext<T> : LocalPluginExecutionContext, ILocalPluginExecutionContext<T> 
        where T : Entity
    {
        public LocalPluginExecutionContext(IPluginExecutionContext context) : base(context)
        {

        }

        public new T PostImage
        {
            get
            {
                return base.PostImage.ToEntity<T>();
            }
        }

        public new T PreImage
        {
            get
            {
                return base.PreImage.ToEntity<T>();
            }
        }

        public new T TargetEntity
        {
            get
            {
                return base.TargetEntity.ToEntity<T>();
            }
        }
    }
}
