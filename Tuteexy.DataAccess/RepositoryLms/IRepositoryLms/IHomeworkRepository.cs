﻿using Tuteexy.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Tuteexy.DataAccess.Repository.IRepository
{
    public interface IHomeworkRepository : IRepositoryAsync<Homework>
    {
        void Update(Homework homework);
    }
}