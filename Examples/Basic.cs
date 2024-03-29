﻿using Eventive;
using Eventive.Models;

namespace Examples;

public class Basic
{
    // This is an empty event and it should hold no data.
    // All events should inherit from the Event model.
    //
    // See the example named EventData.cs for an example on how to make an event that does hold data.
    protected class TestEvent : Event
    {
        //
    }

    // This is an event listener, all listeners should inherit from the Listener model.
    //
    // To register handlers for events, use the IHandles interface,
    // this interface accepts one type argument, the type of the event that it should handle with this implementation.
    protected class TestListener : Listener,
        IHandles<TestEvent>
    {
        public void Handle(TestEvent @event)
        {
            Console.WriteLine("Handling TestEvent from TestListener!");
        }
    }

    // This is the Event Service in which you'll register the Event -> Listener mapping for this specific Service.
    protected class EventService : EventServiceProvider
    {
        // This is the syntax that should be used to register listeners for a specific event.
        // See the example MultipleListeners.cs for how to register multiple listeners for one event.
        protected override Dictionary<Type, Type[]> Listen { get; } = new()
        {
            [Event<TestEvent>()] = new []
            {
                Listener<TestListener>()
            }
        };
    }

    public static void Main()
    {
        var eventService = new EventService();
        eventService.InvokeEvent(new TestEvent());
    }
}