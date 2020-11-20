using System;
using System.Collections.Generic;

namespace Liquid.Entities {

public interface IEventChannel {}

// An event channel handles events for a single event type.
public class EventChannel<Event> : IEventChannel {
    List<Func<Event, bool>> handlers = new List<Func<Event, bool>>();

    // Adds a function as an event handler
    public void Bind(Func<Event, bool> fn) {
        handlers.Add(fn);
    }

    // Emits an event to all event handlers
    public void Emit(in Event e) {
        foreach (var fn in handlers) {
            if (fn(e)) break;
        }
    }
}

} // namespace Liquid.Entities
