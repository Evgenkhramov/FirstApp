﻿using FirstApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FirstApp.Core.Interfaces
{
    public interface IListHandler
    {
        Task CollectionItemClick(TaskModel model);
        void RemoveCollectionItem(int itemId);
    }
}
