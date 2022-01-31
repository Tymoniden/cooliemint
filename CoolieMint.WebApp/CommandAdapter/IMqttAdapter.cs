using MQTTnet;
using System;
using System.Collections.Generic;

namespace WebControlCenter.CommandAdapter
{
    /// <summary>
    /// Interface for adapters that use mqtt as communication protocol
    /// </summary>
    public interface IMqttAdapter
    {
        /// <summary>
        /// The identifier for the controller instance. E.g. switch/1 would be 1
        /// </summary>
        string Identifier { get; }

        /// <summary>
        /// The type of adapter. In all configuration parts will this string be used to identify the adapter.
        /// An example is "Mqtt:MultiSwitchAdapter"
        /// </summary>
        string Type { get; }

        /// <summary>
        /// This method is called for every adapter after they will be initialized.
        /// It is used to ask the specific controller for the state so we have the real world state right after
        /// the app started.
        /// </summary>
        void AdapterInitialize();

        /// <summary>
        /// The list of monitored mqtt topics. 
        /// </summary>
        /// <returns></returns>
        List<string> MonitoredTopics();

        /// <summary>
        /// The method that is called when an mqtt message was inbound. Usually it's just a lookup in MonitoredTopics.
        /// </summary>
        /// <param name="topic">The topic of the inbound mqtt message</param>
        /// <returns>True if the adapter knows what to do with the message.</returns>
        bool CanHandleMqttMessage(string topic);

        /// <summary>
        /// The method that is called when CanHandleMqttMessage returned true. To handle the actual message.
        /// </summary>
        /// <param name="eventArguments">The mqtt message.</param>
        void HandleMqttMessage(MqttApplicationMessageReceivedEventArgs eventArguments);

        /// <summary>
        /// The method returns the current status of the known adapter parameter.
        /// </summary>
        /// <returns>Returns the status message.</returns>
        IAdapterStatusMessage GetStatus();
        
        /// <summary>
        /// The method returns the current status of the adapter parameter.
        /// This method should return null if there was no change after the timestamp.
        /// </summary>
        /// <param name="timestamp">The reference timestamp after the new status had to be happen.</param>
        /// <returns></returns>
        IAdapterStatusMessage GetStatus(DateTime timestamp);

        /// <summary>
        /// This method is called when the adapter gets a call from a web client.
        /// </summary>
        /// <param name="payload">The actual message.</param>
        void HandleWebMessage(string payload);

        List<IControllerState> GetPossibleStates();

        object GetInitializationArguments();

        /// <summary>
        /// This method is should be implemented as concatenating the adapter type and identifier.
        /// It is used to filter the configured controllers.
        /// </summary>
        /// <returns></returns>
        string ToString();
    }
}
