﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentalPlantObserver.Repository.Interfaces
{
    public interface IRepository<Tid, Tdomain>
    {

        public Tdomain Get(Tid id);

        public ObservableCollection<Tdomain> GetAll();
    

    }
}
