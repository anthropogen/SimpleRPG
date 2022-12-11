using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleRPG.Infrastructure;

namespace SimpleRPG.UI
{
    public interface IDragSource<T>
    {
        T GetItem();
        int GetCount();
        void RemoveItem(int count);
    }
}