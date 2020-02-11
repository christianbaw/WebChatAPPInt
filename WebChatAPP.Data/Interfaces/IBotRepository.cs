using System;
using System.Collections.Generic;
using System.Text;
using WebChatAPP.Data.Models;

namespace WebChatAPP.Data.Interfaces
{
    public interface IBotRepository
    {
        List<CsvModel> getStockData();
    }
}
