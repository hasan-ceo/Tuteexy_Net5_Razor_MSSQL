using Tuteexy.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tuteexy.DataAccess.Repository.IRepository
{
    public interface IQuestionRepository : IRepositoryAsync<Question>
    {
        void Update(Question question);
    }
}
