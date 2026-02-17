using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.Internals;

public static class VisualStateExtensions
{
    //public static VisualStateGroupList Clone(this VisualStateGroupList visualStateGroupList)
    //{
    //    var newList = new VisualStateGroupList();

    //    foreach(var group in visualStateGroupList)
    //    {
    //        var newGroup = new VisualStateGroup { Name = group.Name };
    //        newList.Add(newGroup);

    //        foreach (var state in group.States)
    //        {
    //            var newState = new VisualState { Name = state.Name };
    //            newGroup.States.Add(newState);

    //            foreach (var setter in state.Setters)
    //            {
    //                newState.Setters.Add(new Setter { Property = setter.Property, Value = setter.Value });
    //            }
    //        }
    //    }

    //    return newList;
    //}            
}
