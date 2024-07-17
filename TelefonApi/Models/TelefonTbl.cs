using System;
using System.Collections.Generic;

namespace TelefonApi.Models;

public partial class TelefonTbl
{
    public int Id { get; set; }

    public string? Marka { get; set; }

    public string? Model { get; set; }

    public decimal? Ucret { get; set; }

    public int? SatisAdet { get; set; }


}
