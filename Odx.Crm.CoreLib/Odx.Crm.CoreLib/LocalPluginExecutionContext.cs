using Microsoft.Xrm.Sdk;

namespace Odx.Crm.Core
{
    public class LocalPluginExecutionContext
    {
        public IPluginExecutionContext Context { get; private set; }

        public LocalPluginExecutionContext(IPluginExecutionContext context)
        {
            this.Context = context;
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

        public V GetInputParameter<V>(string key)
        {
            if (this.Context.InputParameters.ContainsKey(key))
            {
                return (V)this.Context.InputParameters[key];
            }

            return default(V);
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
    }

    public class LocalPluginExecutionContext<T> : LocalPluginExecutionContext
        where T : Entity
    {
        public LocalPluginExecutionContext(IPluginExecutionContext context) : base(context)
        {

        }

        private T preImage;
        private bool isPreImageLoaded;
        private T postImage;
        private bool isPostImageLoaded;
        private T target;
        private bool isTargetLoaded;

        public T PreImage
        {
            get
            {
                return GetEntityImage(nameof(this.PreImage), Context.PreEntityImages, ref this.preImage, ref this.isPreImageLoaded);
            }
        }

        public T PostImage
        {
            get
            {
                return GetEntityImage(nameof(this.PostImage), Context.PostEntityImages, ref this.postImage, ref this.isPostImageLoaded);
            }
        }

        public T Target
        {
            get
            {
                if (!this.isTargetLoaded)
                {
                    this.isTargetLoaded = true;
                    this.target = ((Entity)Context.InputParameters[nameof(Target)]).ToEntity<T>();
                }

                return this.target;
            }
        }

        private T GetEntityImage(string imageName, EntityImageCollection imagesCollection, ref T imageField, ref bool isImageFieldLoaded)
        {
            if (!isImageFieldLoaded)
            {

                isImageFieldLoaded = true;
                if (imagesCollection.Contains(imageName))
                {
                    imageField = imagesCollection[imageName].ToEntity<T>();
                }
            }

            return imageField;
        }
    }
}
