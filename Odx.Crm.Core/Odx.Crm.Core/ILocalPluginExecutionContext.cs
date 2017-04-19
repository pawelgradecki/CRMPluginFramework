﻿using Microsoft.Xrm.Sdk;

namespace Odx.Crm.Core
{
    public interface ILocalPluginExecutionContext
    {
        IPluginExecutionContext Context { get; }
        Entity PostImage { get; }
        Entity PreImage { get; }
        Entity Target { get; }

        bool CheckSharedContextForFlag(string flagname, IPluginExecutionContext context = null);
        V GetInputParameter<V>(string key);
        TMessage GetRequestMessage<TMessage>() where TMessage : OrganizationRequest, new();
        TMessage GetResponseMessage<TMessage>() where TMessage : OrganizationRequest, new();
        void SetInputParameter<V>(string key, V value);
        void SetOutputParameter<V>(string key, V value);
        ILocalPluginExecutionContext<T> ToEntity<T>() where T: Entity, new();
    }

    public interface ILocalPluginExecutionContext<T> : ILocalPluginExecutionContext
        where T : Entity
    {
        new T PostImage { get; }
        new T PreImage { get; }
        new T Target { get; }
    }
}