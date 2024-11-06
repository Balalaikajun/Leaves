using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class Transaction
{
    public int PkTransactionid { get; set; }

    public int FkAccountidfrom { get; set; }

    public int FkAccountidto { get; set; }

    public decimal Amount { get; set; }

    public DateTime Created { get; set; }

    public virtual Account FkAccountidfromNavigation { get; set; } = null!;

    public virtual Account FkAccountidtoNavigation { get; set; } = null!;
}
