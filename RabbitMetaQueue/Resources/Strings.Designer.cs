﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RabbitMetaQueue.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Strings {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Strings() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("RabbitMetaQueue.Resources.Strings", typeof(Strings).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Binding from queue {Queue} to exchange {Exchange} with routing key {RoutingKey} has changed but recreating is not enabled, skipping.
        /// </summary>
        internal static string LogBindingNoRecreate {
            get {
                return ResourceManager.GetString("LogBindingNoRecreate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Recreating binding from queue {Queue} to exchange {Exchange} with routing key {RoutingKey}.
        /// </summary>
        internal static string LogBindingRecreate {
            get {
                return ResourceManager.GetString("LogBindingRecreate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Binding from queue {Queue} to exchange {Exchange} with routing key {RoutingKey} has not been changed, skipping.
        /// </summary>
        internal static string LogBindingUnchanged {
            get {
                return ResourceManager.GetString("LogBindingUnchanged", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Exchange {Exchange} was recreated, creating binding for queue {Queue} with routing key {RoutingKey}.
        /// </summary>
        internal static string LogBindRecreatedExchange {
            get {
                return ResourceManager.GetString("LogBindRecreatedExchange", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Checking queue {Queue} for added or updated bindings.
        /// </summary>
        internal static string LogCheckAddUpdateBindings {
            get {
                return ResourceManager.GetString("LogCheckAddUpdateBindings", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Checking for added or updated exchanges.
        /// </summary>
        internal static string LogCheckAddUpdateExchanges {
            get {
                return ResourceManager.GetString("LogCheckAddUpdateExchanges", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Checking for added or updated queues.
        /// </summary>
        internal static string LogCheckAddUpdateQueues {
            get {
                return ResourceManager.GetString("LogCheckAddUpdateQueues", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Checking for removed bindings.
        /// </summary>
        internal static string LogCheckRemovedBindings {
            get {
                return ResourceManager.GetString("LogCheckRemovedBindings", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Deleting is not enabled, skipping check for removed bindings.
        /// </summary>
        internal static string LogCheckRemovedBindingsSkipped {
            get {
                return ResourceManager.GetString("LogCheckRemovedBindingsSkipped", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Checking for removed exchanges.
        /// </summary>
        internal static string LogCheckRemovedExchanges {
            get {
                return ResourceManager.GetString("LogCheckRemovedExchanges", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Deleting is not enabled, skipping check for removed exchanges.
        /// </summary>
        internal static string LogCheckRemovedExchangesSkipped {
            get {
                return ResourceManager.GetString("LogCheckRemovedExchangesSkipped", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Checking for removed queues.
        /// </summary>
        internal static string LogCheckRemovedQueues {
            get {
                return ResourceManager.GetString("LogCheckRemovedQueues", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Deleting is not enabled, skipping check for removed queues.
        /// </summary>
        internal static string LogCheckRemovedQueuesSkipped {
            get {
                return ResourceManager.GetString("LogCheckRemovedQueuesSkipped", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Creating binding for queue {Queue} to exchange {Exchange} with routing key {RoutingKey}.
        /// </summary>
        internal static string LogCreateBinding {
            get {
                return ResourceManager.GetString("LogCreateBinding", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Creating exchange {Exchange} of type {Type}.
        /// </summary>
        internal static string LogCreateExchange {
            get {
                return ResourceManager.GetString("LogCreateExchange", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Creating queue {queue}.
        /// </summary>
        internal static string LogCreateQueue {
            get {
                return ResourceManager.GetString("LogCreateQueue", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Deleting binding from queue {Queue} to exchange {Exchange} with routing key {RoutingKey}.
        /// </summary>
        internal static string LogDeleteBinding {
            get {
                return ResourceManager.GetString("LogDeleteBinding", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Deleting exchange {Exchange}.
        /// </summary>
        internal static string LogDeleteExchange {
            get {
                return ResourceManager.GetString("LogDeleteExchange", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Deleting queue {Queue}.
        /// </summary>
        internal static string LogDeleteQueue {
            get {
                return ResourceManager.GetString("LogDeleteQueue", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Exchange {Exchange} has changed but recreating is not enabled, skipping.
        /// </summary>
        internal static string LogExchangeNoRecreate {
            get {
                return ResourceManager.GetString("LogExchangeNoRecreate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Recreating exchange {Exchange} (type: {Type}).
        /// </summary>
        internal static string LogExchangeRecreate {
            get {
                return ResourceManager.GetString("LogExchangeRecreate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Exchange {Exchange} has not been changed, skipping.
        /// </summary>
        internal static string LogExchangeUnchanged {
            get {
                return ResourceManager.GetString("LogExchangeUnchanged", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Name must begin with namespace prefix {Prefix} for queues {@Queues}.
        /// </summary>
        internal static string LogInvalidNamespacesQueue {
            get {
                return ResourceManager.GetString("LogInvalidNamespacesQueue", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Queue {Queue} has changed but recreating is not enabled, checking for updated bindings.
        /// </summary>
        internal static string LogQueueNoRecreate {
            get {
                return ResourceManager.GetString("LogQueueNoRecreate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Recreating queue {Queue}.
        /// </summary>
        internal static string LogQueueRecreate {
            get {
                return ResourceManager.GetString("LogQueueRecreate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Queue {Queue} has not been changed, checking for updated bindings.
        /// </summary>
        internal static string LogQueueUnchanged {
            get {
                return ResourceManager.GetString("LogQueueUnchanged", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to When specified, changes are not applied to the RabbitMQ server..
        /// </summary>
        internal static string OptionDescriptionDryRun {
            get {
                return ResourceManager.GetString("OptionDescriptionDryRun", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The host {name} of the RabbitMQ server. Defaults to localhost..
        /// </summary>
        internal static string OptionDescriptionHost {
            get {
                return ResourceManager.GetString("OptionDescriptionHost", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The {file name} of the topology definition. Required..
        /// </summary>
        internal static string OptionDescriptionInput {
            get {
                return ResourceManager.GetString("OptionDescriptionInput", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to When specified, anything not present in the topology definition will be deleted from the server (excluding system entries).
        /// </summary>
        internal static string OptionDescriptionMirror {
            get {
                return ResourceManager.GetString("OptionDescriptionMirror", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The {password} used in the connection. Defaults to guest..
        /// </summary>
        internal static string OptionDescriptionPassword {
            get {
                return ResourceManager.GetString("OptionDescriptionPassword", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The {username} used in the connection. Defaults to guest..
        /// </summary>
        internal static string OptionDescriptionUsername {
            get {
                return ResourceManager.GetString("OptionDescriptionUsername", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Include intermediate steps and skipped items in the output..
        /// </summary>
        internal static string OptionDescriptionVerbose {
            get {
                return ResourceManager.GetString("OptionDescriptionVerbose", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The virtual host {name} as configured in RabbitMQ. Defaults to /..
        /// </summary>
        internal static string OptionDescriptionVirtualHost {
            get {
                return ResourceManager.GetString("OptionDescriptionVirtualHost", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Dry run:.
        /// </summary>
        internal static string OptionDryRun {
            get {
                return ResourceManager.GetString("OptionDryRun", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to no, changes WILL be applied.
        /// </summary>
        internal static string OptionDryRunNo {
            get {
                return ResourceManager.GetString("OptionDryRunNo", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to yes, changes will NOT be applied.
        /// </summary>
        internal static string OptionDryRunYes {
            get {
                return ResourceManager.GetString("OptionDryRunYes", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Topology file not found.
        /// </summary>
        internal static string OptionInputNotFound {
            get {
                return ResourceManager.GetString("OptionInputNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Topology file name is required.
        /// </summary>
        internal static string OptionInputRequired {
            get {
                return ResourceManager.GetString("OptionInputRequired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid arguments: .
        /// </summary>
        internal static string OptionInvalidArguments {
            get {
                return ResourceManager.GetString("OptionInvalidArguments", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to d|dryrun.
        /// </summary>
        internal static string OptionKeyDryRun {
            get {
                return ResourceManager.GetString("OptionKeyDryRun", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to h|host=.
        /// </summary>
        internal static string OptionKeyHost {
            get {
                return ResourceManager.GetString("OptionKeyHost", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to i|input=.
        /// </summary>
        internal static string OptionKeyInput {
            get {
                return ResourceManager.GetString("OptionKeyInput", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to m|mirror.
        /// </summary>
        internal static string OptionKeyMirror {
            get {
                return ResourceManager.GetString("OptionKeyMirror", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to p|password=.
        /// </summary>
        internal static string OptionKeyPassword {
            get {
                return ResourceManager.GetString("OptionKeyPassword", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to u|username=.
        /// </summary>
        internal static string OptionKeyUsername {
            get {
                return ResourceManager.GetString("OptionKeyUsername", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to vb|verbose.
        /// </summary>
        internal static string OptionKeyVerbose {
            get {
                return ResourceManager.GetString("OptionKeyVerbose", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to v|virtualhost=.
        /// </summary>
        internal static string OptionKeyVirtualHost {
            get {
                return ResourceManager.GetString("OptionKeyVirtualHost", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Mirror mode:.
        /// </summary>
        internal static string OptionMirror {
            get {
                return ResourceManager.GetString("OptionMirror", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to no.
        /// </summary>
        internal static string OptionNo {
            get {
                return ResourceManager.GetString("OptionNo", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Options:.
        /// </summary>
        internal static string Options {
            get {
                return ResourceManager.GetString("Options", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Usage:.
        /// </summary>
        internal static string OptionUsage {
            get {
                return ResourceManager.GetString("OptionUsage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Verbose:.
        /// </summary>
        internal static string OptionVerbose {
            get {
                return ResourceManager.GetString("OptionVerbose", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to yes.
        /// </summary>
        internal static string OptionYes {
            get {
                return ResourceManager.GetString("OptionYes", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Comparing topology.
        /// </summary>
        internal static string StatusComparing {
            get {
                return ResourceManager.GetString("StatusComparing", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Connecting to RabbitMQ server [{0}{1}].
        /// </summary>
        internal static string StatusConnectingRabbitMQ {
            get {
                return ResourceManager.GetString("StatusConnectingRabbitMQ", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Done!.
        /// </summary>
        internal static string StatusDone {
            get {
                return ResourceManager.GetString("StatusDone", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Error: {0}.
        /// </summary>
        internal static string StatusError {
            get {
                return ResourceManager.GetString("StatusError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Parsing topology definition.
        /// </summary>
        internal static string StatusParsingDefinition {
            get {
                return ResourceManager.GetString("StatusParsingDefinition", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Reading existing topology.
        /// </summary>
        internal static string StatusReadingTopology {
            get {
                return ResourceManager.GetString("StatusReadingTopology", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Press any Enter key to continue....
        /// </summary>
        internal static string StatusWaitForKey {
            get {
                return ResourceManager.GetString("StatusWaitForKey", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No &lt;Templates&gt; element found.
        /// </summary>
        internal static string XmlNoTemplatesElement {
            get {
                return ResourceManager.GetString("XmlNoTemplatesElement", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Circular reference detected for template {Template}.
        /// </summary>
        internal static string XmlTemplateCircularReference {
            get {
                return ResourceManager.GetString("XmlTemplateCircularReference", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Template {Template} not found.
        /// </summary>
        internal static string XmlTemplateNotFound {
            get {
                return ResourceManager.GetString("XmlTemplateNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Template {Template} is not of the expected type {Type}.
        /// </summary>
        internal static string XmlTemplateUnexpectedType {
            get {
                return ResourceManager.GetString("XmlTemplateUnexpectedType", resourceCulture);
            }
        }
    }
}
