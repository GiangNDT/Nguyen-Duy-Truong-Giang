using System;
using System.Collections.Generic;

namespace Thuc_hanh_webApi.Entites;

public partial class Production
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime? ExpDate { get; set; }

    public int Status { get; set; }

    public decimal Price { get; set; }

    public int Amount { get; set; }

    public string? Description { get; set; }

    public DateTime CreatedDate { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? UpdatedDate { get; set; }

    public string? UpdatedBy { get; set; }
}
