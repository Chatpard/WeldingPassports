using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.Repositories.SQL
{
    public interface IExamCentersSQLRepository
    {
        SelectList ExamCenterSelectList();
    }
}
